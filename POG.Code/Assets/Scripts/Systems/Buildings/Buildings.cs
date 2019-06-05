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


    // Attributes
    public float _TimeRequiredInBuilding { get; private set; }
    public int _Capacity { get; private set; }

    // Object
    public GameObject _GameObject;

    // World Info
    private Vector3 mPosition;


    #region Constructors
    /// <summary>
    /// Create a building.
    /// </summary>
    /// <param name="gameObject">World gameobject of this building</param>
    public Buildings(GameObject gameObject)
    {
        _TimeRequiredInBuilding = 5.0f;

        _GameObject = gameObject;
        _GameObject.transform.position = GetRandomPosition();
        mPosition = _GameObject.transform.position;
    }


    /// <summary>
    /// Create a building.
    /// </summary>
    /// <param name="timeRequiredInBuilding">Time required in seconds for the character in this building to complete it's action</param>
    /// <param name="capacity">Max building capacity</param>
    public Buildings(float timeRequiredInBuilding, int capacity, GameObject gameObject)
    {
        _TimeRequiredInBuilding = timeRequiredInBuilding;
        _Capacity = capacity;

        _GameObject = gameObject;
        _GameObject.transform.position = GetRandomPosition();
        mPosition = _GameObject.transform.position;
    }
    #endregion

    public void LateUpdate()
    {
        
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0.0f);
    }

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
