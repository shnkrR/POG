using System.Collections.Generic;
using UnityEngine;

public class CharacterManager
{
    public const int CHARACTER_POOL_START_SIZE = 5;

    public const string CHARACTER_OBJECTS_PATH = "Prefabs/Characters/";
    public const string CHARACTER_DATA_PATH = "GameData/Characters/CharacterInfoData";


    private GameObject[] mCharacterObjectTemplates;

    private GameObject mCharacterPoolParent;

    private List<GameObject> mCharacterObjectPool;

    private List<Character> mCharacters;

    private CharacterInfoData mCharacterInfoData;

    private GameController mGameController;


    public CharacterManager(GameController gameController)
    {
        mGameController = gameController;

        LoadCharacterStats();
        InitCharacterObjectPool();

        mCharacters = new List<Character>();

        SpawnCharacter();
    }

    #region Character Pool
    private void InitCharacterObjectPool()
    {
        if (mCharacterInfoData == null)
            return;

        mCharacterObjectPool = new List<GameObject>();
        mCharacterPoolParent = new GameObject("CharacterPool");

        mCharacterObjectTemplates = new GameObject[mCharacterInfoData._CharacterInfo.Count];
        for (int i = 0; i < mCharacterInfoData._CharacterInfo.Count; i++)
        {
            mCharacterObjectTemplates[i] = Resources.Load<GameObject>(CHARACTER_OBJECTS_PATH + mCharacterInfoData._CharacterInfo[i]._Meta._PrefabName);
            if (mCharacterObjectTemplates[i] != null)
            {
                for (int j = 0; j < CHARACTER_POOL_START_SIZE; j++)
                {
                    GameObject poolObject = Object.Instantiate(mCharacterObjectTemplates[i]);
                    poolObject.name = mCharacterInfoData._CharacterInfo[i]._Meta._PrefabName + "_" + j.ToString("00");
                    poolObject.tag = "UnusedCharacter";
                    poolObject.transform.parent = mCharacterPoolParent.transform;
                    poolObject.SetActive(false);
                    mCharacterObjectPool.Add(poolObject);
                }
            }
        }
    }

    private GameObject GetCharacterObject(string prefabName)
    {
        GameObject go = null;
        go = mCharacterObjectPool.Find(g => g.tag == "UnusedCharacter" && g.name.Contains(prefabName));

        if (go == null)
        {
            int index = mCharacterInfoData._CharacterInfo.FindIndex(ci => ci._Meta._PrefabName == prefabName);
            GameObject poolObject = Object.Instantiate(mCharacterObjectTemplates[index]);
            poolObject.name = mCharacterInfoData._CharacterInfo[index]._Meta._PrefabName + "_" + mCharacterObjectPool.Count.ToString("00");
            poolObject.tag = "UnusedCharacter";
            poolObject.transform.parent = mCharacterPoolParent.transform;
            mCharacterObjectPool.Add(poolObject);
            go = poolObject;
        }

        go.SetActive(true);
        go.transform.parent = null;
        go.tag = "Character";
        return go;
    }

    private void ResetCharacterObject(ref GameObject characterObject)
    {
        characterObject.tag = "UnusedCharacter";
        characterObject.transform.parent = mCharacterPoolParent.transform;
        characterObject.SetActive(false);
    }
    #endregion

    private void LoadCharacterStats()
    {
        mCharacterInfoData = Resources.Load<CharacterInfoData>(CHARACTER_DATA_PATH);
    }

    public void SpawnCharacter()
    {
        if (mCharacterInfoData == null)
            return;

        CharacterInfoData.CharacterInfo charTypeToSpawn = mCharacterInfoData._CharacterInfo[Random.Range(0, mCharacterInfoData._CharacterInfo.Count)];
        if (charTypeToSpawn != null)
        {
            Character character = new Character(mGameController,
                                                charTypeToSpawn._Attributes._Speed,
                                                charTypeToSpawn._Attributes._Health,
                                                charTypeToSpawn._Attributes._Work,
                                                charTypeToSpawn._Attributes._Fertility,
                                                charTypeToSpawn._Meta._SoulScore,
                                                GetCharacterObject(charTypeToSpawn._Meta._PrefabName));

        character.OnCharacterDead += OnCharacterDead;
        mCharacters.Add(character);
    }
    }

    public void LateUpdate()
    {
        for (int i = 0; i < mCharacters.Count; i++)
        {
            mCharacters[i].LateUpdate();
        }
    }

    private void OnCharacterDead(Character character)
    {
        if (character != null)
        {
            mCharacters.Remove(character);
            ResetCharacterObject(ref character._GameObject);
            character.OnCharacterDead -= OnCharacterDead;
            character.Destroy();
            character = null;
        }

        SpawnCharacter();
    }

    ~CharacterManager()
    {
        for (int i = 0; i < mCharacters.Count; i++)
        {
            mCharacters[i] = null;
        }

        mCharacters.Clear();
        mCharacters = null;
    }
}
