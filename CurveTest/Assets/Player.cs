using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //로켓은 그 점을 이용해서 커브로 이동하고 싶다
    //어딘가에 부딪히면 폭발하고 싶다.
    //그 때 부딪힌 것이 Enemy라면 데미지를 입히고 싶다.
    public GameObject rocketFactory;
    public GameObject target;
    public LineRenderer lr;
    Vector3[] path;
    public int maxPosition = 20;
    void Start()
    {
        // path = new Vector3[lr.positionCount];
        // lr.GetPositions(path);
    }

    float curTime = 0;
    float createTime = 1;
    void Update()
    {
        // if (isMakeRocket)
        // {
        //     curTime += Time.deltaTime;
        //     //시간이 흐르다가 로켓생성시간이 되면
        //     if (curTime > createTime)
        //     {
        //         //MakeRocket을 호출하고 싶다.
        //         MakeRocket();
        //         //시간을 0으로 초기화하고 싶다.
        //         curTime = 0;
        //         //count를 1차감하고 싶다.
        //         count--;
        //     }
        //     if (count < 0)
        //     {
        //         //만약 count가 0이하라면 isMakeRocket을 false로 하고싶다.
        //         isMakeRocket = false;
        //     }
        // }
        if (Input.GetButtonDown("Jump"))
        {
            count = 5;
            //Invoke("MakeRocket", 0.2f);
            //순차적으로 0.2초마다 호출하고 싶다.
            //isMakeRocket = true;
            StartCoroutine("MakeRocket");
        }
    }
    bool isMakeRocket = false;
    int count;
    // void MakeRocket()
    // {
    //     if (count < 0)
    //     {
    //         return;
    //     }
    //     count--;
    //     //스페이스바를 누르면 플레이어가 로켓을 생성해서 로켓에게 
    //     GameObject rocket = Instantiate(rocketFactory);
    //     rocket.transform.position = transform.position;

    //     //커브에 관련된 점배열을 알려주고 싶다.
    //     Rocket rocketCompo = rocket.GetComponent<Rocket>();
    //     rocketCompo.SetPath(MakePath());
    //     Invoke("MakeRocket", 0.2f);
    // }
    IEnumerator MakeRocket()
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(0.2f);
            //스페이스바를 누르면 플레이어가 로켓을 생성해서 로켓에게 
            GameObject rocket = Instantiate(rocketFactory);
            rocket.transform.position = transform.position;

            //커브에 관련된 점배열을 알려주고 싶다.
            Rocket rocketCompo = rocket.GetComponent<Rocket>();
            rocketCompo.SetPath(MakePath());
        }
    }
    Vector3 GetCurvePosition(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(ab, bc, t);
    }

    Vector3[] MakePath()
    {
        //Quaternion dir = Vector3.up * Quaternion.Euler(0,0,0);
        Vector3 dir = new Vector3(Random.Range(-1f, 1f), 1, 0);
        dir.Normalize();
        dir *= 3.58f;
        dir += new Vector3(0, 0, -3.08999991f);
        Vector3[] path = new Vector3[maxPosition];
        Vector3 p1 = transform.position;
        Vector3 p2 = transform.position + dir;
        Vector3 p3 = target.transform.position;
        for (int i = 0; i < maxPosition; i++)
        {
            float t = (float)i / (maxPosition - 1);
            path[i] = GetCurvePosition(p1, p2, p3, t);
        }
        return path;
    }
}
