using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ����߿� ���Ƿ� �ϳ��� �����ؼ� �� ��ġ�� �����ϰ� �ʹ�.
public class SpawnManager : MonoBehaviour
{
    float curTime;
    float createTime = 1;
    public GameObject enemyFactory;
    public float min = 1;
    public float max = 3;
    public Transform[] spawnList; //���� �������� ���
    int preRandomIndex = -1;
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
            //4. ���� ��� �߿� ������ ��ġ�� ��ġ�ϰ� �ʹ�.
            int randomIndex = Random.Range(0, spawnList.Length);
            //���� randomIndex �� preRandomIndex�� ���ٸ�
            if (randomIndex == preRandomIndex)
            {
                //randomIndex�� �ٽ� ���ϰ� �ʹ�.
                randomIndex = (randomIndex + 1) % spawnList.Length;
                //randomIndex = Random.Range(0, spawnList.Length);
            }
            Vector3 pos = spawnList[randomIndex].position;
            enemy.transform.position = pos;
            //5. ����ð��� 0���� �ʱ�ȭ
            curTime = 0;
            //6. �����ð��� ���� ���Ŀ� �������� ���ϰ� �ʹ�.
            createTime = Random.Range(min, max);
            preRandomIndex = randomIndex;
        }

    }

}
