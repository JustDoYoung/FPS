using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAlarm : MonoBehaviour
{
    public static TestAlarm instance;
    private void Awake()
    {
        TestAlarm.instance = this;
    }
    public System.Action<GameObject> triggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (triggerEvent != null)
        {
            triggerEvent(other.gameObject);
        }

    }
}
