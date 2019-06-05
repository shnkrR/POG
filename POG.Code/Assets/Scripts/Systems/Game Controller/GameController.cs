using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private CharacterManager mCharacterManager;

    private BuildingsManager mBuildingsManager;


    private void Awake()
    {
        
    }

    private void Start()
    {
        mCharacterManager = new CharacterManager();

        mBuildingsManager = new BuildingsManager();
    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        mCharacterManager.LateUpdate();

        mBuildingsManager.LateUpdate();
    }
}
