using UnityEngine;


public enum eCharacterStates
{
    Idle = 0,
    Walk,
    Work,
    Dead,
}

public class Character
{
    // Attributes
    public float _Speed { get; private set; }
    public float _Work { get; private set; }
    public float _Health { get; private set; }
    public float _Fertility { get; private set; }

    // State
    public eCharacterStates _CurrState { get; private set; }

    // World Info
    private Vector3 mSpawnPosition;

    // Special
    private int _SoulScore;


    public Character()
    {
        _Speed = _Work = _Health = _Fertility = 0.5f;

        _SoulScore = 1;

        mSpawnPosition = Vector3.zero;
    }

    public Character(float speed, float health, float work, float fertility)
    {
        _Speed = speed;
        _Health = health;
        _Work = work;
        _Fertility = fertility;

        _SoulScore = 1;

        mSpawnPosition = Vector3.zero;
    }

    public Character(float speed, float health, float work, float fertility, Vector3 spawnPosition)
    {
        _Speed = speed;
        _Health = health;
        _Work = work;
        _Fertility = fertility;

        _SoulScore = 1;

        mSpawnPosition = spawnPosition;
    }

    private void ChangeState(eCharacterStates newState)
    {
        // Close out old state
        switch (_CurrState)
        {
            case eCharacterStates.Idle:
                break;

            case eCharacterStates.Walk:
                break;

            case eCharacterStates.Work:
                break;

            case eCharacterStates.Dead:
                break;
        }

        // Trigger new state
        switch (newState)
        {
            case eCharacterStates.Idle:
                break;

            case eCharacterStates.Walk:
                break;

            case eCharacterStates.Work:
                break;

            case eCharacterStates.Dead:
                _SoulScore = 0;
                break;
        }

        // Cache state
        _CurrState = newState;
    }

    ~Character()
    {
        ChangeState(eCharacterStates.Dead);
    }
}
