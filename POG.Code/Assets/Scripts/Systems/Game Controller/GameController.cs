using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private CharacterManager mCharacterManager;

    private StateMachine mStateMachine;


    private void Awake()
    {
        mStateMachine = new StateMachine();        
    }

    private void Start()
    {
        mCharacterManager = new CharacterManager(mStateMachine);
    }

    private void Update()
    {
        mCharacterManager.Update();
    }

    private void LateUpdate()
    {
        mStateMachine.LateUpdate();
    }
}
