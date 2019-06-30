using UnityEngine;
using UnityEditor;


public class BuildingInfoDataEditor : EditorWindow
{
    private static BuildingInfoData mBuildingInfoData;

    private static bool[] mBuildingFoldouts;


    private Vector2 mScrollView;


    [MenuItem("Game Tools/Building/Building Info Data")]
    public static void ShowWindow()
    {
        GetWindow(typeof(BuildingInfoDataEditor));
    }

    private void OnSelectionChange()
    {
        Init();
    }

    private void OnFocus()
    {
        Init();
    }

    private void OnLostFocus()
    {
        Init();
    }

    private static void Init()
    {
        //mBuildingInfoData = null;

        if (Selection.activeObject != null)
        {
            Object[] selObject = Selection.GetFiltered(typeof(BuildingInfoData), SelectionMode.Assets | SelectionMode.ExcludePrefab);
            if (selObject.Length > 0 && selObject[0].GetType() == typeof(BuildingInfoData))
            {
                mBuildingInfoData = selObject[0] as BuildingInfoData;
                mBuildingFoldouts = new bool[mBuildingInfoData._BuildingInfo.Count];
            }
        }
    }

    private void OnGUI()
    {
        if (mBuildingInfoData == null)
        {
            EditorGUILayout.LabelField("Select a building info file in order to be able to edit it");
            return;
        }

        EditorGUILayout.LabelField("Building Data List", EditorStyles.boldLabel);

        if (GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(mBuildingInfoData);
            AssetDatabase.SaveAssets();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Add Building Info"))
        {
            mBuildingInfoData._BuildingInfo.Add(new BuildingInfoData.BuildingInfo());

            bool[] temp = mBuildingFoldouts;
            mBuildingFoldouts = new bool[mBuildingInfoData._BuildingInfo.Count];
            for (int i = 0; i < mBuildingInfoData._BuildingInfo.Count; i++)
            {
                if (i < temp.Length)
                {
                    mBuildingFoldouts[i] = temp[i];
                }
                else
                {
                    mBuildingFoldouts[i] = false;
                }
            }
        }

        EditorGUILayout.Space();

        mScrollView = EditorGUILayout.BeginScrollView(mScrollView);

        for (int i = 0; i < mBuildingInfoData._BuildingInfo.Count; i++)
        {
            EditorGUILayout.BeginVertical("Box");

            mBuildingFoldouts[i] = EditorGUILayout.Foldout(mBuildingFoldouts[i], mBuildingInfoData._BuildingInfo[i]._Meta._PrefabName, true);

            if (mBuildingFoldouts[i])
            {
                EditorGUILayout.LabelField("Meta", EditorStyles.boldLabel);
                mBuildingInfoData._BuildingInfo[i]._Meta._PrefabName = EditorGUILayout.TextField("Prefab Name: ", mBuildingInfoData._BuildingInfo[i]._Meta._PrefabName);
                mBuildingInfoData._BuildingInfo[i]._Meta._BuildingType = (eBuildingType)EditorGUILayout.EnumPopup("Building Type: ", mBuildingInfoData._BuildingInfo[i]._Meta._BuildingType);

                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Attributes", EditorStyles.boldLabel);
                mBuildingInfoData._BuildingInfo[i]._Attributes._TimeRequiredInBuilding = EditorGUILayout.FloatField("Time Required In Building: ", mBuildingInfoData._BuildingInfo[i]._Attributes._TimeRequiredInBuilding);
                mBuildingInfoData._BuildingInfo[i]._Attributes._Capacity = EditorGUILayout.IntField("Capacity: ", mBuildingInfoData._BuildingInfo[i]._Attributes._Capacity);
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndScrollView();
    }
}
