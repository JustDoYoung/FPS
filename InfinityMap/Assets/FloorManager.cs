using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    //특정방향으로 이동하고 싶다.
    //가장 최근에 만들어진 Floor의 Docker위치에 일정시간마다 Floor를 붙여주고 싶다.
    //Floor를 붙일 때는 FloorManager의 자식으로 하고 싶다.
    Vector3 dir = Vector3.back;
    public Floor latestFloor;
    public GameObject FloorFactory;
    public float createTime = 1;
    public int count;
    public int maxCount = 20;
    IEnumerator Start()
    {
        while (true)
        {
            if (count < maxCount)
            {
                GameObject floor = Instantiate(FloorFactory);
                floor.transform.position = latestFloor.docker.position;
                floor.transform.parent = transform;
                latestFloor = floor.GetComponent<Floor>();
                latestFloor.floorManager = this;
                count++;
            }
            yield return new WaitForSeconds(createTime);
        }
    }
    public float speed = 5;
    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

}
