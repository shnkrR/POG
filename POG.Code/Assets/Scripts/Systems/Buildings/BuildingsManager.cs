using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager
{
    public const int BUILDING_POOL_START_SIZE = 3;

    public const string BUILDING_OBJECT_PATH = "Prefabs/Buildings/";
    public const string BUILDING_DATA_PATH = "GameData/Buildings/BuildingInfoData";


    private GameObject[] mBuildingObjectTemplate;
    private GameObject mBuildingPoolParent;

    private List<GameObject> mBuildingObjectPool;

    private List<Buildings> mBuildings;

    private BuildingInfoData mBuildingInfoData;

    private GameController mGameController;

    
    public BuildingsManager(GameController gameController)
    {
        mGameController = gameController;

        LoadBuildingStats();
        InitBuildingObjectPool();

        mBuildings = new List<Buildings>();

        SpawnDefaultBuildings();
    }

    #region Building Pool
    private void InitBuildingObjectPool()
    {
        if (mBuildingInfoData == null)
            return;

        mBuildingObjectPool = new List<GameObject>();
        mBuildingPoolParent = new GameObject("BuildingPool");

        mBuildingObjectTemplate = new GameObject[mBuildingInfoData._BuildingInfo.Count];
        for (int i = 0; i < mBuildingInfoData._BuildingInfo.Count; i++)
        {
            mBuildingObjectTemplate[i] = Resources.Load<GameObject>(BUILDING_OBJECT_PATH + mBuildingInfoData._BuildingInfo[i]._Meta._PrefabName);
            if (mBuildingObjectTemplate != null)
            {
                for (int j = 0; j < BUILDING_POOL_START_SIZE; j++)
                {
                    GameObject poolObject = Object.Instantiate(mBuildingObjectTemplate[i]);
                    poolObject.name = mBuildingInfoData._BuildingInfo[i]._Meta._PrefabName + "_" + j.ToString("00");
                    poolObject.tag = "UnusedBuilding";
                    poolObject.transform.parent = mBuildingPoolParent.transform;
                    poolObject.SetActive(false);
                    mBuildingObjectPool.Add(poolObject);
                }
            }
        }
    }

    private GameObject GetBuildingObject(string prefabName)
    {
        GameObject go = null;
        go = mBuildingObjectPool.Find(g => g.tag == "UnusedBuilding" && g.name.Contains(prefabName));

        if (go == null)
        {
            int index = mBuildingInfoData._BuildingInfo.FindIndex(bi => bi._Meta._PrefabName == prefabName);
            GameObject poolObject = Object.Instantiate(mBuildingObjectTemplate[index]);
            poolObject.name = mBuildingInfoData._BuildingInfo[index]._Meta._PrefabName + "_" + mBuildingObjectPool.Count.ToString("00");
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

    private void LoadBuildingStats()
    {
        mBuildingInfoData = Resources.Load<BuildingInfoData>(BUILDING_DATA_PATH);
    }

    public void SpawnDefaultBuildings()
    {
        for (int i = 0; i < mBuildingInfoData._BuildingInfo.Count; i++)
        {
            Buildings building = new Buildings(mGameController, GetBuildingObject(mBuildingInfoData._BuildingInfo[i]._Meta._PrefabName), mBuildingInfoData._BuildingInfo[i]._Meta._BuildingType);
            building.OnBuildingDead += OnBuildingDead;
            mBuildings.Add(building);
        }
    }

    public Buildings GetNearestBuildingOfType(Vector3 toPosition, eBuildingType buildingType)
    {
        float distance = float.MaxValue;
        int index = 0;
        List<Buildings> filteredBuildings = mBuildings.FindAll(b => b._BuildingType == buildingType);
        for (int i = 0; i < mBuildings.Count; i++)
        {
            if (distance > Vector3.Distance(mBuildings[i]._GameObject.transform.position, toPosition))
            {
                index = i;
            }
        }

        return mBuildings[index];
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
