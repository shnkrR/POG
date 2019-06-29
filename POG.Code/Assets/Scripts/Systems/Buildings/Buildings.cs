using UnityEngine;


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
    public MyDelegate.DelegateVoid OnBuildingTaskComplete;


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
    }
    #endregion

    public void LateUpdate()
    {
        
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(GameController._BottomLeftWorldBounds.x, GameController._TopRightWorldBounds.x),
                            GameController._TopRightWorldBounds.y,
                            Random.Range(GameController._BottomLeftWorldBounds.z, GameController._TopRightWorldBounds.z));
    }

    #region Tasks
    public void StartTask()
    {

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
