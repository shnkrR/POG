using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(BuildingInfoData))]
public class BuildingInfoDataInspector : Editor
{
    public override void OnInspectorGUI()
    {
        BuildingInfoData mTarget = (BuildingInfoData)target;

        EditorGUILayout.LabelField("Use the Building Info Data Editor to change values in this file.");

        if (GUILayout.Button("Open Editor"))
        {
            BuildingInfoDataEditor.ShowWindow();
        }
    }
}