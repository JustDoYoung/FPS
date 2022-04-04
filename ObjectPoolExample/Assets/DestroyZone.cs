using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //부딪힌 오브젝트가 ObjectPool에서 관리하는 녀셕이라면
        if (ObjectPool.IsObjectPoolObject(other.gameObject))
        {

            ObjectPool.instance.AddInactiveList(other.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
