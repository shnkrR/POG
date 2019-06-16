using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


public class CharacterInfoDataEditor : EditorWindow
{
    private static CharacterInfoData mCharacterInfoData;

    private static bool[] mCharacterFoldouts;


    private Vector2 mScrollView;


    [MenuItem("Game Tools/Character/Character Info Data")]
    public static void ShowWindow()
    {
        GetWindow(typeof(CharacterInfoDataEditor));
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
        mCharacterInfoData = null;

        if (Selection.activeObject != null)
        {
            Object[] selObject = Selection.GetFiltered(typeof(CharacterInfoData), SelectionMode.Assets | SelectionMode.ExcludePrefab);
            if (selObject.Length > 0 && selObject[0].GetType() == typeof(CharacterInfoData))
            {
                mCharacterInfoData = selObject[0] as CharacterInfoData;
                mCharacterFoldouts = new bool[mCharacterInfoData._CharacterInfo.Count];
            }
        }
    }

    private void OnGUI()
    {
        if (mCharacterInfoData == null)
        {
            EditorGUILayout.LabelField("Select a character info file in order to be able to edit it");
            return;
        }

        EditorGUILayout.LabelField("Character Data List", EditorStyles.boldLabel);

        if (GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(mCharacterInfoData);
            AssetDatabase.SaveAssets();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Add Character Info"))
        {
            mCharacterInfoData._CharacterInfo.Add(new CharacterInfoData.CharacterInfo());

            bool[] temp = mCharacterFoldouts;
            mCharacterFoldouts = new bool[mCharacterInfoData._CharacterInfo.Count];
            for (int i = 0; i < mCharacterInfoData._CharacterInfo.Count; i++)
            {
                if (i < temp.Length)
                {
                    mCharacterFoldouts[i] = temp[i];
                }
                else
                {
                    mCharacterFoldouts[i] = false;
                }
            }
        }

        EditorGUILayout.Space();

        mScrollView = EditorGUILayout.BeginScrollView(mScrollView);

        for (int i = 0; i < mCharacterInfoData._CharacterInfo.Count; i++)
        {
            EditorGUILayout.BeginVertical("Box");

            mCharacterFoldouts[i] = EditorGUILayout.Foldout(mCharacterFoldouts[i], mCharacterInfoData._CharacterInfo[i]._Meta._PrefabName, true);

            if (mCharacterFoldouts[i])
            {
                EditorGUILayout.LabelField("Meta");
                mCharacterInfoData._CharacterInfo[i]._Meta._PrefabName = EditorGUILayout.TextField("Prefab Name: ", mCharacterInfoData._CharacterInfo[i]._Meta._PrefabName);
                mCharacterInfoData._CharacterInfo[i]._Meta._SoulScore = EditorGUILayout.IntField("Soul Score: ", mCharacterInfoData._CharacterInfo[i]._Meta._SoulScore);

                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Attributes");
                mCharacterInfoData._CharacterInfo[i]._Attributes._Speed = EditorGUILayout.FloatField("Speed: ", mCharacterInfoData._CharacterInfo[i]._Attributes._Speed);
                mCharacterInfoData._CharacterInfo[i]._Attributes._Work = EditorGUILayout.FloatField("Work: ", mCharacterInfoData._CharacterInfo[i]._Attributes._Work);
                mCharacterInfoData._CharacterInfo[i]._Attributes._Health = EditorGUILayout.FloatField("Health: ", mCharacterInfoData._CharacterInfo[i]._Attributes._Health);
                mCharacterInfoData._CharacterInfo[i]._Attributes._Fertility = EditorGUILayout.FloatField("Fertility: ", mCharacterInfoData._CharacterInfo[i]._Attributes._Fertility);
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndScrollView();
    }
}
