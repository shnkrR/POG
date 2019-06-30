using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public enum eBuildingType
{
    FERTILITY,
    WORK,
    HEALTH,
    MORGUE,
}

public class Buildings
{
    public MyDelegate.DelegateVoidSingleParam<Buildings> OnBuildingDead;
    public MyDelegate.DelegateVoidSingleParam<Character> OnBuildingTaskComplete;


    // Attributes
    public float _TimeRequiredInBuilding { get { return mAttributes._TimeRequiredInBuilding; } private set { mAttributes._TimeRequiredInBuilding = value; } }
    public int _Capacity { get { return mAttributes._Capacity; } private set { mAttributes._Capacity = value; } }

    // Meta
    public eBuildingType _BuildingType { get { return mMeta._BuildingType; } private set { mMeta._BuildingType = value; } }

    private BuildingInfoData.BuildingAttributes mAttributes;
    private BuildingInfoData.BuildingMeta mMeta;

    // Object
    public GameObject _GameObject;

    // World Info
    private Vector3 mPosition;
    private Vector3 mRotation;

    // Game Controller
    private GameController mGameController;

    private Dictionary<Character, float> mCharacterTaskTime;


    #region Constructors
    /// <summary>
    /// Create a building.
    /// </summary>
    /// <param name="gameObject">World gameobject of this building</param>
    public Buildings(GameController gameController, GameObject gameObject, eBuildingType buildingType)
    {
        mAttributes = new BuildingInfoData.BuildingAttributes();
        mMeta = new BuildingInfoData.BuildingMeta();

        _TimeRequiredInBuilding = 5.0f;
        _Capacity = 10;

        _GameObject = gameObject;
        _GameObject.transform.position = GetRandomPosition();
        mPosition = _GameObject.transform.position;
        mRotation = new Vector3(0.0f, Random.Range(30.0f, 60.0f), 0.0f);
        _GameObject.transform.Rotate(mRotation);

        _BuildingType = buildingType;
    }


    /// <summary>
    /// Create a building.
    /// </summary>
    /// <param name="timeRequiredInBuilding">Time required in seconds for the character in this building to complete it's action</param>
    /// <param name="capacity">Max building capacity</param>
    public Buildings(GameController gameController, float timeRequiredInBuilding, int capacity, GameObject gameObject, eBuildingType buildingType)
    {
        mAttributes = new BuildingInfoData.BuildingAttributes();
        mMeta = new BuildingInfoData.BuildingMeta();

        _TimeRequiredInBuilding = timeRequiredInBuilding;
        _Capacity = capacity;

        _GameObject = gameObject;
        _GameObject.transform.position = GetRandomPosition();
        mPosition = _GameObject.transform.position;
        mRotation = new Vector3(0.0f, Random.Range(30.0f, 60.0f), 0.0f);
        _GameObject.transform.Rotate(mRotation);

        _BuildingType = buildingType;
    }
    #endregion

    public void LateUpdate()
    {
        if (mCharacterTaskTime != null && mCharacterTaskTime.Count > 0)
        {
            foreach(KeyValuePair<Character, float> pair in mCharacterTaskTime.ToList())
            {
                if (pair.Value >= _TimeRequiredInBuilding)
                {
                    mCharacterTaskTime.Remove(pair.Key);
                    if (OnBuildingTaskComplete != null)
                        OnBuildingTaskComplete(pair.Key);
                    break;
                }
                else
                {
                    mCharacterTaskTime[pair.Key] += Time.deltaTime;
                }
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(GameController._BottomLeftWorldBounds.x, GameController._TopRightWorldBounds.x),
                            GameController._TopRightWorldBounds.y,
                            Random.Range(GameController._BottomLeftWorldBounds.z, GameController._TopRightWorldBounds.z));
    }

    #region Tasks
    public void StartTask(Character character)
    {
        if (mCharacterTaskTime == null)
            mCharacterTaskTime = new Dictionary<Character, float>();

        mCharacterTaskTime.Add(character, 0);
    }
    #endregion

    #region Destructors
    public void Destroy()
    {
        _GameObject = null;
    }

    ~Buildings()
    {

    }
    #endregion
}
