using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvent : MonoBehaviour
{
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>(); //���� �θ𿡰� �ִ� Enemy ������Ʈ�� �������� �ʹ�.
    }

    public void OnEnemyAttackHit()
    {
        //���� Hit�� �Ǵ� ����
        enemy.OnEnemyAttackHit(); //alt + enter �޼ҵ� ����, Enemy�� �����̾�
    }
    public void OnEnemyAttackFinished()
    {
        //���� �ִϸ��̼��� ����Ǵ� ����
        enemy.OnEnemyAttackFinished();
    }
    public void OnEnemyReactFinished()
    {
        //���׼� �ִϸ��̼��� ����Ǵ� ����
        enemy.OnEnemyReactFinished();
    }
    public void OnEnemyDeathFinished()
    {
        //���� �ִϸ��̼��� ����Ǵ� ����
        enemy.OnEnemyDeathFinished();
    }
}
