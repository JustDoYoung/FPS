using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����ð����� �����忡�� ���� ���� �� ��ġ�� ��ġ�ϰ� �ʹ�.
//�����ð��� ���� ���Ŀ� �������� ���ϰ� �ʹ�.
public class EnemyManagerStep1 : MonoBehaviour
{
    float curTime;
    float createTime;
    public GameObject enemyFactory;
    public float min = 1;
    public float max = 3;

    void Start()
    {
        //�¾ �� �����ð��� �������� ���ϰ� �ʹ�.
        //�����ð� = ����(min, max)
        createTime = Random.Range(min, max);
    }

    void Update()
    {
        //1. ����ð��� �帣�ٰ� �����ð����� �����忡�� ���� ���� �� ��ġ�� ��ġ�ϰ� �ʹ�.
        curTime += Time.deltaTime;
        //2. ����ð��� �����ð��� �ʰ��ϸ�
        if (curTime > createTime)
        {
        //3, �����忡�� ���� ����
            GameObject enemy = Instantiate(enemyFactory);
        //4. �� ��ġ�� ��ġ�ϰ� �ʹ�.
            enemy.transform.position = transform.position;
        //5. ����ð��� 0���� �ʱ�ȭ
            curTime = 0;
        //6. �����ð��� ���� ���Ŀ� �������� ���ϰ� �ʹ�.
            createTime = Random.Range(min, max);
        }

    }
}
