using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    /// <summary>
    /// Fires when entering a new state. Sends previous and new states
    /// </summary>
    public static MyDelegate.DelegateVoidDualParam<int, int> OnEnterState;

    /// <summary>
    /// Fires every late update. Sends current state and deltaTime
    /// </summary>
    public static MyDelegate.DelegateVoidDualParam<int, float> OnUpdateState;

    /// <summary>
    /// Fires when exiting a state. Sends current and new states.
    /// </summary>
    public static MyDelegate.DelegateVoidDualParam<int, int> OnExitState;


    private int mCurrentState = -1;

    private bool mStateChanged = false;


    /// <summary>
    /// Sitch to a different state
    /// </summary>
    /// <param name="newState">State index to switch to</param>
    public void ChangeState(int newState)
    {
        OnExitState?.Invoke(mCurrentState, newState);

        OnEnterState?.Invoke(mCurrentState, newState);

        mCurrentState = newState;

        mStateChanged = true;
    }

    public void LateUpdate()
    {
        if (!mStateChanged)
        {
            if (mCurrentState >= 0)
                OnUpdateState?.Invoke(mCurrentState, Time.deltaTime);
        }

        mStateChanged = false;
    }
}
