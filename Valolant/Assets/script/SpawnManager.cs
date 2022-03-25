using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스폰 목록중에 임의로 하나를 선택해서 그 위치에 생성하고 싶다.
public class SpawnManager : MonoBehaviour
{
    float curTime;
    float createTime = 1;
    public GameObject enemyFactory;
    public float min = 1;
    public float max = 3;
    public Transform[] spawnList; //스폰 지점들의 목록
    int preRandomIndex = -1;
    void Start()
    {
        //태어날 때 생성시간을 랜덤으로 정하고 싶다.
        //생성시간 = 랜덤(min, max)
        createTime = Random.Range(min, max);
    }

    void Update()
    {
        //1. 현재시간이 흐르다가 일정시간마다 적공장에서 적을 만들어서 내 위치에 배치하고 싶다.
        curTime += Time.deltaTime;
        //2. 현재시간이 생성시간을 초과하면
        if (curTime > createTime)
        {
            //3, 적공장에서 적을 만들어서
            GameObject enemy = Instantiate(enemyFactory);
            //4. 스폰 목록 중에 랜덤한 위치에 배치하고 싶다.
            int randomIndex = Random.Range(0, spawnList.Length);
            //만약 randomIndex 와 preRandomIndex와 같다면
            if (randomIndex == preRandomIndex)
            {
                //randomIndex를 다시 정하고 싶다.
                randomIndex = (randomIndex + 1) % spawnList.Length;
                //randomIndex = Random.Range(0, spawnList.Length);
            }
            Vector3 pos = spawnList[randomIndex].position;
            enemy.transform.position = pos;
            //5. 현재시간을 0으로 초기화
            curTime = 0;
            //6. 생성시간을 생성 직후에 랜덤으로 정하고 싶다.
            createTime = Random.Range(min, max);
            preRandomIndex = randomIndex;
        }

    }

}
