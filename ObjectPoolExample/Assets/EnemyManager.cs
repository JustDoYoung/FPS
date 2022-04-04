using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float curTime = 0;
    float createTime = 3;
    public Transform enemyParent;
    //일정시간마다 적공장에서 적을 만들어서 내 위치에 배치하고 싶다.
    IEnumerator Start()
    {
        ObjectPool.instance.CreateInstance("Enemy", enemyParent, 10);
        while (true)
        {
            GameObject enemy = ObjectPool.instance.GetInactiveBulletNew("Enemy");
            enemy.transform.position = transform.position;
            yield return new WaitForSeconds(createTime);
        }
    }

    void Update()
    {

    }
}
