using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    float speed = 5;
    Vector3[] path;
    public void SetPath(Vector3[] path)
    {
        this.path = path.Clone() as Vector3[];
    }

    // Update is called once per frame
    int index = 0;
    float t;
    public float timeSpeed = 5;
    void Update()
    {
        if (path == null)
        {
            return;
        }
        if (index >= path.Length - 1)
        {
            index = 0;
        }

        print(index);
        Vector3 p1 = path[index];
        Vector3 p2 = path[index + 1];
        transform.position = Vector3.Lerp(p1, p2, t);
        Vector3 dir = p2 - p1;
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);
        if (index < path.Length - 1)
        {
            t += Time.deltaTime * timeSpeed;
            if (t > 1)
            {
                index++;
                t = 0;
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        //Destroy(gameObject);
    }
}
