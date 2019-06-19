using UnityEngine;


[CreateAssetMenu(fileName = "BuildingInfoData", menuName = "POG/Building", order = 1)]
public class BuildingInfoData : ScriptableObject
{
    [System.Serializable]
    public class BuildingInfo
    {
        public BuildingMeta _Meta;
        public BuildingAttributes _Attributes;

        public BuildingInfo()
        {
            _Meta = new BuildingMeta();
            _Meta._PrefabName = "Prefab Name";

            _Attributes = new BuildingAttributes();
            _Attributes._TimeRequiredInBuilding = 0.0f;
            _Attributes._Capacity = 0;
        }
    }

    [System.Serializable]
    public class BuildingAttributes
    {
        public float _TimeRequiredInBuilding;
        public int _Capacity;
    }

    [System.Serializable]
    public class BuildingMeta
    {
        public string _PrefabName;
    }


    public System.Collections.Generic.List<BuildingInfo> _BuildingInfo;
}
