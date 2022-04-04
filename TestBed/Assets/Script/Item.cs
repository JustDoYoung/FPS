using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Ammo, Coin, Grenade, Heart, Weapon };
    public Type type;
    public int value;

    Rigidbody rigid;
    SphereCollider sphereCollider;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>(); //첫 번째 sphereCollider만 가져옴.(컴포넌트 순서 유의해서 배치 trigger를 밑으로)
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * 30 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            rigid.isKinematic = true; //외부물리효과 무시
            sphereCollider.enabled = false;
        }
    }
}
