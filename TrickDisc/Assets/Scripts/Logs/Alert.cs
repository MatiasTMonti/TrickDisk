using UnityEngine;
using System;

public class Alert : AndroidJavaProxy
{
    public Action positiveAction;
    public Action negativeAction;
    const string alertInterfaceName = "com.monti2023.mylibrary.Alert";

    public Alert() : base(alertInterfaceName) { }

    public void OnPositive()
    {
        positiveAction?.Invoke();
    }

    public void OnNegative()
    {
        negativeAction?.Invoke();
    }
}
