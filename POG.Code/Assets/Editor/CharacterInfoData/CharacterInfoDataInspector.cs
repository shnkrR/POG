using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(CharacterInfoData))]
public class CharacterInfoDataInspector : Editor
{
    public override void OnInspectorGUI()
    {
        CharacterInfoData mTarget = (CharacterInfoData)target;

        EditorGUILayout.LabelField("Use the Character Info Data Editor to change values in this file.");

        if (GUILayout.Button("Open Editor"))
        {
            CharacterInfoDataEditor.ShowWindow();
        }
    }
}
