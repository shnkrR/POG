using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager
{
    public const int BUILDING_POOL_START_SIZE = 10;
    public const string BUILDING_OBJECT_PATH = "Prefabs/Buildings/Building_Generic";


    private GameObject mBuildingObjectTemplate;
    private GameObject mBuildingPoolParent;

    private List<GameObject> mBuildingObjectPool;

    private List<Buildings> mBuildings;

    
    public BuildingsManager()
    {
        InitBuildingObjectPool();

        mBuildings = new List<Buildings>();

        SpawnDefaultBuildings();
    }

    #region Building Pool
    private void InitBuildingObjectPool()
    {
        mBuildingObjectPool = new List<GameObject>();
        mBuildingObjectTemplate = Resources.Load<GameObject>(BUILDING_OBJECT_PATH);
        if (mBuildingObjectTemplate != null)
        {
            mBuildingPoolParent = new GameObject("BuildingPool");
            for (int i = 0; i < BUILDING_POOL_START_SIZE; i++)
            {
                GameObject poolObject = Object.Instantiate(mBuildingObjectTemplate);
                poolObject.name += "_" + i.ToString("00");
                poolObject.tag = "UnusedBuilding";
                poolObject.transform.parent = mBuildingPoolParent.transform;
                poolObject.SetActive(false);
                mBuildingObjectPool.Add(poolObject);
            }
        }
    }

    private GameObject GetBuildingObject()
    {
        GameObject go = null;
        go = mBuildingObjectPool.Find(g => g.tag == "UnusedBuilding");

        if (go == null)
        {
            GameObject poolObject = Object.Instantiate(mBuildingObjectTemplate);
            poolObject.name += "_" + mBuildingObjectPool.Count.ToString("00");
            poolObject.tag = "UnusedBuilding";
            poolObject.transform.parent = mBuildingPoolParent.transform;
            mBuildingObjectPool.Add(poolObject);
            go = poolObject;
        }

        go.SetActive(true);
        go.transform.parent = null;
        go.tag = "Building";
        return go;
    }

    private void ResetBuildingObject(ref GameObject buildingObject)
    {
        buildingObject.tag = "UnusedBuilding";
        buildingObject.transform.parent = mBuildingPoolParent.transform;
        buildingObject.SetActive(false);
    }
    #endregion

    public void SpawnDefaultBuildings()
    {
        Buildings building = new Buildings(GetBuildingObject());
        building.OnBuildingDead += OnBuildingDead;
        mBuildings.Add(building);
    }

    public void LateUpdate()
    {
        for (int i = 0; i < mBuildings.Count; i++)
        {
            mBuildings[i].LateUpdate();
        }
    }

    private void OnBuildingDead(Buildings building)
    {
        if (building != null)
        {
            mBuildings.Remove(building);
            ResetBuildingObject(ref building._GameObject);
            building.OnBuildingDead -= OnBuildingDead;
            building.Destroy();
            building = null;
        }
    }

    ~BuildingsManager()
    {
        for (int i = 0; i < mBuildings.Count; i++)
        {
            mBuildings[i] = null;
        }

        mBuildings.Clear();
        mBuildings = null;
    }
}
