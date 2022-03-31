using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChangeColor : MonoBehaviour
{
    void Start()
    {
        TestAlarm.instance.triggerEvent = OnChangeColor;
    }

    private void OnChangeColor(GameObject obj)
    {
        print("sfd");
        GetComponent<MeshRenderer>().material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }
}
