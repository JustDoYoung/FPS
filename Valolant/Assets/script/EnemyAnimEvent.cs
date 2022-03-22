using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvent : MonoBehaviour
{
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>(); //나의 부모에게 있는 Enemy 컴포넌트를 가져오고 싶다.
    }

    public void OnEnemyAttackHit()
    {
        //공격 Hit가 되는 순간
        enemy.OnEnemyAttackHit(); //alt + enter 메소드 생성, Enemy야 지금이야
    }
    public void OnEnemyAttackFinished()
    {
        //공격 애니메이션이 종료되는 순간
        enemy.OnEnemyAttackFinished();
    }
}
