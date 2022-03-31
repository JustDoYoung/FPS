using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float speed = 5f;
    Vector3 dir;
    System.Action callback;
    public void Move(Vector3 dir, System.Action callback)
    {
        this.callback = callback;
        StartCoroutine("IEMove", dir);
    }
    IEnumerator IEMove(Vector3 dir)
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            transform.position += dir * speed * Time.deltaTime;
            yield return null;
        }
        if (callback != null)
        {
            callback();
        }
    }
}
