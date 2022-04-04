using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletPosition;

    public Transform bulletParent;
    private void Start()
    {
        //ObjectPool을 이용해서 총알을 20개 만들고 싶다.
        ObjectPool.instance.CreateInstance("Bullet", bulletParent, 1);
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = ObjectPool.instance.GetInactiveBulletNew("Bullet");

            bullet.transform.position = bulletPosition.transform.position;
            bullet.transform.up = bulletPosition.transform.up;
        }
    }
}
