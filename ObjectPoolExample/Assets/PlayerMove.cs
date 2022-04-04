using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float speed = 5;
    public float rotSpeed = 0.3f;
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = transform.up * v;
        dir.Normalize();

        transform.position += dir * speed * Time.deltaTime;
        transform.Rotate(Vector3.forward, -h * Time.deltaTime * 360 * rotSpeed);
    }
}
