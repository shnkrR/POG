﻿using UnityEngine;


public enum eCharacterStates
{
    Idle = 0,
    Walk,
    Work,
    Dead,
}

public class Character
{
    public MyDelegate.DelegateVoidSingleParam<Character> OnCharacterDead;


    // Attributes
    public float _Speed { get; private set; }
    public float _Work { get; private set; }
    public float _Health { get; private set; }
    public float _Fertility { get; private set; }

    // Object
    public GameObject _GameObject;

    // World Info
    private Vector3 mSpawnPosition;
    private Vector3 mDestinationPosition;

    // Special
    private int mSoulScore;

    // State Machine
    private StateMachine mStateMachine;

    private float mTimeBetweenStates = 3.0f;
    private float mCurrentStateTime = 0.0f;

#if ENABLE_DEBUG
    private float mAliveTime = 0.0f;
#endif


    #region Constructors
    /// <summary>
    /// Create a character.
    /// </summary>
    /// <param name="stateMachine">State Machine instance</param>
    /// <param name="gameObject">World gameobject of this character</param>
    public Character(GameObject gameObject)
    {
        _Speed = _Work = _Health = _Fertility = 0.5f;

        mSoulScore = 1;

        mSpawnPosition = GetRandomDestination();

        _GameObject = gameObject;
        _GameObject.transform.position = mSpawnPosition;

        SubscribeListeners();
    }


    /// <summary>
    /// Create a character.
    /// </summary>
    /// <param name="speed">Move speed</param>
    /// <param name="health">Max health</param>
    /// <param name="work">Work rate</param>
    /// <param name="fertility">Fertility rate</param>
    /// <param name="stateMachine">State Machine instance</param>
    /// <param name="gameObject">World gameobject of this character</param>
    public Character(float speed, float health, float work, float fertility, GameObject gameObject)
    {
        _Speed = speed;
        _Health = health;
        _Work = work;
        _Fertility = fertility;

        mSoulScore = 1;

        mSpawnPosition = GetRandomDestination();

        _GameObject = gameObject;
        _GameObject.transform.position = mSpawnPosition;

        SubscribeListeners();
    }

    /// <summary>
    /// Create a character.
    /// </summary>
    /// <param name="speed">Move speed</param>
    /// <param name="health">Max health</param>
    /// <param name="work">Work rate</param>
    /// <param name="fertility">Fertility rate</param>
    /// <param name="spawnPosition">Spawn poisition</param>
    /// <param name="stateMachine">State Machine instance</param>
    /// <param name="gameObject">World gameobject of this character</param>
    public Character(float speed, float health, float work, float fertility, Vector3 spawnPosition, GameObject gameObject)
    {
        _Speed = speed;
        _Health = health;
        _Work = work;
        _Fertility = fertility;

        mSoulScore = 1;

        mSpawnPosition = spawnPosition;

        _GameObject = gameObject;
        _GameObject.transform.position = mSpawnPosition;

        SubscribeListeners();
    }
#endregion

    #region State Machine
    private void SubscribeListeners()
    {
#if ENABLE_DEBUG
        mAliveTime = Time.time;
#endif

        StateMachine.OnEnterState += OnEnterState;
        StateMachine.OnUpdateState += OnUpdateState;
        StateMachine.OnExitState += OnExitState;

        mStateMachine = new StateMachine();
        mStateMachine.ChangeState((int)eCharacterStates.Idle);
    }

    private void OnEnterState(int prevState, int newState)
    {
        mCurrentStateTime = 0.0f;

        eCharacterStates prevCharState = (eCharacterStates)prevState;
        eCharacterStates newCharState = (eCharacterStates)newState;

        Debug.Log("Prev State: " + prevCharState + " New State: " + newCharState);

        // Trigger new state
        switch (newCharState)
        {
            case eCharacterStates.Idle:
                break;

            case eCharacterStates.Walk:
                mDestinationPosition = GetRandomDestination();
                break;

            case eCharacterStates.Work:
                break;

            case eCharacterStates.Dead:
                mSoulScore = 0;
                OnCharacterDead?.Invoke(this);
                break;
        }
    }

    private void OnUpdateState(int currentState, float deltaTime)
    {
        eCharacterStates currentCharState = (eCharacterStates)currentState;

        // Update current state
        switch (currentCharState)
        {
            case eCharacterStates.Idle:
                break;

            case eCharacterStates.Walk:
                _GameObject.transform.position = Vector3.MoveTowards(_GameObject.transform.position, mDestinationPosition, Time.deltaTime * 2.0f);
                break;

            case eCharacterStates.Work:
                break;
        }

        _Health -= (deltaTime / 20.0f);
        if (_Health <= 0.0f)
        {
            mStateMachine.ChangeState((int)eCharacterStates.Dead);
        }
        else
        {
            mCurrentStateTime += deltaTime;
            if (mCurrentStateTime >= mTimeBetweenStates)
            {
                int newState = Random.Range(0, (int)eCharacterStates.Dead);
                mStateMachine.ChangeState(newState);
            }
        }
    }

    private void OnExitState(int currentState, int newState)
    {
        eCharacterStates currentCharState = (eCharacterStates)currentState;
        eCharacterStates newCharState = (eCharacterStates)newState;

        // Debug.Log("Current State: " + currentCharState + " New State: " + newCharState);

        // Close out old state
        switch (currentCharState)
        {
            case eCharacterStates.Idle:
                break;

            case eCharacterStates.Walk:
                mDestinationPosition = mSpawnPosition;
                break;

            case eCharacterStates.Work:
                break;
        }
    }

    private void UnSubscribeListeners()
    {
        StateMachine.OnEnterState -= OnEnterState;
        StateMachine.OnUpdateState -= OnUpdateState;
        StateMachine.OnExitState -= OnExitState;

        mStateMachine = null;
    }
    #endregion

    #region Movement
    private Vector3 GetRandomDestination()
    {
        return new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0.0f);
    }

    private Vector3 GetRandomMoveDirection()
    {
        return new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);
    }
    #endregion

    public void LateUpdate()
    {
        mStateMachine.LateUpdate();
    }

    #region Destructors
    public void Destroy()
    {
#if ENABLE_DEBUG
        Debug.Log(Time.time - mAliveTime);
#endif
        _GameObject = null;
        UnSubscribeListeners();
        mStateMachine = null;
    }

    ~Character()
    {
        
    }
#endregion
}