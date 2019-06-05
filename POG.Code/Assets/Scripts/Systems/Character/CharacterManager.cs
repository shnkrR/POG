using System.Collections.Generic;
using UnityEngine;

public class CharacterManager
{
    public const int CHARACTER_POOL_START_SIZE = 15;
    public const string CHARACTER_OBJECT_PATH = "Characters/Character_Generic";


    private GameObject mCharacterObjectTemplate;
    private GameObject mCharacterPoolParent;

    private List<GameObject> mCharacterObjectPool;

    private List<Character> mCharacters;


    public CharacterManager()
    {
        InitCharacterObjectPool();

        mCharacters = new List<Character>();

        SpawnCharacter();
    }

    #region Character Pool
    private void InitCharacterObjectPool()
    {
        mCharacterObjectPool = new List<GameObject>();
        mCharacterObjectTemplate = Resources.Load<GameObject>(CHARACTER_OBJECT_PATH);
        if (mCharacterObjectTemplate != null)
        {
            mCharacterPoolParent = new GameObject("CharacterPool");
            for (int i = 0; i < CHARACTER_POOL_START_SIZE; i++)
            {
                GameObject poolObject = Object.Instantiate(mCharacterObjectTemplate);
                poolObject.name += "_" + i.ToString("00");
                poolObject.tag = "UnusedCharacter";
                poolObject.transform.parent = mCharacterPoolParent.transform;
                poolObject.SetActive(false);
                mCharacterObjectPool.Add(poolObject);
            }
        }
    }

    private GameObject GetCharacterObject()
    {
        GameObject go = null;
        go = mCharacterObjectPool.Find(g => g.tag == "UnusedCharacter");

        if (go == null)
        {
            GameObject poolObject = Object.Instantiate(mCharacterObjectTemplate);
            poolObject.name += "_" + mCharacterObjectPool.Count.ToString("00");
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

    public void SpawnCharacter()
    {
        Character character = new Character(GetCharacterObject());
        character.OnCharacterDead += OnCharacterDead;
        mCharacters.Add(character);
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
