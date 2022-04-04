using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//일정시간마다 적공장에서 적을 만들어서 내 위치에 배치하고 싶다.
//생성시간을 생성 직후에 랜덤으로 정하고 싶다.
public class CloudSpawn : MonoBehaviour
{
    float curTime;
    float createTime;
    public GameObject enemyFactory;
    public float min = 1;
    public float max = 3;
    BoxCollider col;

    void Start()
    {
        //태어날 때 생성시간을 랜덤으로 정하고 싶다.
        //생성시간 = 랜덤(min, max)
        createTime = Random.Range(min, max);
        col = GetComponent<BoxCollider>();
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
            //4. 내 위치에 배치하고 싶다.
            if (GetPosition(out pos))
            {
                enemy.transform.position = pos;
                //5. 현재시간을 0으로 초기화
                curTime = 0;
                //6. 생성시간을 생성 직후에 랜덤으로 정하고 싶다.
                createTime = Random.Range(min, max);
            }
        }
    }
    Vector3 pos;
    private bool GetPosition(out Vector3 pos)
    {
        //내 위치를 기준으로 x, z 임의의 점을 찍고 싶다.
        pos = transform.position;
        float lx = -col.bounds.size.x / 2; //Vector3 lx = col.bounds.min.x;
        float rx = col.bounds.size.x / 2; //Vector3 rx = col.bounds.max.x;
        float fz = col.bounds.size.z / 2;
        float bz = -col.bounds.size.z / 2;

        pos.x += Random.Range(lx, rx);
        pos.z += Random.Range(bz, fz);

        //그 점에서 땅을 향해서 Ray를 발사!!
        Ray ray = new Ray(pos, Vector3.down);
        RaycastHit hitInfo;
        //닿은 것이 있다면 그 위치를 반환하고 싶다.
        if (Physics.Raycast(ray, out hitInfo))
        {
            pos = hitInfo.point; //enemy가 바닥에 파묻힐 수 있음. enemy의 기준축을 발바닥으로 지정해 줘야 함.
            return true;
        }
        pos = Vector3.zero;
        return false;
    }
}
