using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager
{
    private List<Character> mCharacter;

    private StateMachine mStateMachine;


    public CharacterManager(StateMachine stateMachine)
    {
        mStateMachine = stateMachine;

        mCharacter = new List<Character>();

        SpawnCharacter();
    }

    public void SpawnCharacter()
    {
        Character character = new Character(mStateMachine);
        character.OnCharacterDead += OnCharacterDead;
        mCharacter.Add(character);
    }

    public void Update()
    {

    }

    private void OnCharacterDead(Character character)
    {
        if (character != null)
        {
            mCharacter.Remove(character);
            character.OnCharacterDead -= OnCharacterDead;
            character.Destroy();
            character = null;
        }
    }

    ~CharacterManager()
    {
        mStateMachine = null;

        for (int i = 0; i < mCharacter.Count; i++)
        {
            mCharacter[i] = null;
        }

        mCharacter.Clear();
        mCharacter = null;
    }
}
