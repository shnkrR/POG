//Description::
/*
 * Delegates to be used should be declared in this place
 */
using UnityEngine;
using System.Collections;

//[System.Serializable]
public class MyDelegate
{
    public delegate void DelegateVoid();
    public delegate T DelegateReturn<T>();
    public delegate void DelegateVoidSingleParam<T>(T a_param1);
    public delegate T DelegateReturnSingleParam<T, U>(U a_param);
    public delegate void DelegateVoidDualParam<T1, T2>(T1 a_param1, T2 a_param2);
    public delegate T DelegateReturnDualParam<T, T1, T2>(T1 a_param1, T2 a_param2);
    public delegate void DelegateVoidParamList<T>(params T[] a_list);

    public delegate void DelegateVoidTrippleParam<T1, T2, T3>(T1 a_param1, T2 a_param2, T3 a_param3);
    public delegate T DelegateReturnTrippleParam<T, T1, T2, T3>(T1 a_param1, T2 a_param2, T3 a_param3);

    public delegate void DelegateVoidQuadParam<T1, T2, T3, T4>(T1 a_param1, T2 a_param2, T3 a_param3, T4 a_param4);
    public delegate void DelegateVoidPentaParam<T1, T2, T3, T4, T5>(T1 a_param1, T2 a_param2, T3 a_param3, T4 a_param4, T5 a_param5);
    public delegate void DelegateVoidHexaParam<T1, T2, T3, T4, T5, T6>(T1 a_param1, T2 a_param2, T3 a_param3, T4 a_param4, T5 a_param5, T6 a_param6);
}