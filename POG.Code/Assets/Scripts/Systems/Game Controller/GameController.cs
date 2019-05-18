using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private CharacterManager mCharacterManager;


    private void Awake()
    {
        
    }

    private void Start()
    {
        mCharacterManager = new CharacterManager();
    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        mCharacterManager.LateUpdate();
    }
}
