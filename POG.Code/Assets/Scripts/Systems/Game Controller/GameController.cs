using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private const int FLOOR_COLLISION_LAYER = 9;
    private const int FLOOR_COLLISION_RAY_DSITANCE = 500;
    private const int BORDER_SAFE_AREA = 10;
    
    public static Vector3 _BottomLeftWorldBounds { get; private set; }
    public static Vector3 _TopRightWorldBounds { get; private set; }

    private CharacterManager mCharacterManager;

    private BuildingsManager mBuildingsManager;

    private Camera mMainCamera;


    private void Awake()
    {
        mMainCamera = Camera.main;
    }

    private void Start()
    {
        GetWorldBounds();

        mCharacterManager = new CharacterManager();

        mBuildingsManager = new BuildingsManager();
    }

    private void Update()
    {

    }

    private void GetWorldBounds()
    {
        _BottomLeftWorldBounds = Vector3.zero;
        _TopRightWorldBounds = Vector3.zero;

        Ray topLeft = mMainCamera.ScreenPointToRay(Vector3.zero + new Vector3(BORDER_SAFE_AREA, BORDER_SAFE_AREA, 0.0f));
        Ray bottomRight = mMainCamera.ScreenPointToRay(new Vector3(Screen.width, Screen.height, 0.0f) - new Vector3(BORDER_SAFE_AREA, BORDER_SAFE_AREA, 0.0f));

        topLeft.direction = mMainCamera.transform.forward;
        bottomRight.direction = mMainCamera.transform.forward;

        //Debug.DrawRay(topLeft.origin, topLeft.direction * FLOOR_COLLISION_RAY_DSITANCE, Color.yellow);
        //Debug.DrawRay(bottomRight.origin, bottomRight.direction * FLOOR_COLLISION_RAY_DSITANCE, Color.yellow);

        int layerMask = 1 << FLOOR_COLLISION_LAYER;

        RaycastHit hit;
        if (Physics.Raycast(topLeft, out hit, FLOOR_COLLISION_RAY_DSITANCE, layerMask))
        {
            _BottomLeftWorldBounds = hit.point;
        }

        if (Physics.Raycast(bottomRight, out hit, FLOOR_COLLISION_RAY_DSITANCE, layerMask))
        {
            _TopRightWorldBounds = hit.point;
        }

        //Debug.Log(_BottomLeftWorldBounds + "::" + _TopRightWorldBounds);
    }

    private void LateUpdate()
    {
        mCharacterManager.LateUpdate();

        mBuildingsManager.LateUpdate();
    }
}
