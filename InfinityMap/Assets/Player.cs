using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //사용자가 좌우키를 누르면 좌우로 이동하고 싶다.(Lerp)
    //목적지
    Vector3 targetPosition;
    //목적지 리스트
    public Transform[] list;
    //현재 목적지 목록의 인덱스
    public int index;
    Vector3 pos;
    void Start()
    {
        pos = transform.position;
        pos.x = list[index].position.x;
        targetPosition.x = pos.x;
    }


    void Update()
    {
        //만약 index >0 이고 왼쪽 키를 누르면
        if (index > 0 && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            index--;
        }
        //만약 index < list.Length - 1이고 오른쪽 키를 누르면
        if (index < list.Length - 1 && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            index++;
        }
        pos.x = list[index].position.x;
        targetPosition.x = pos.x;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5);
    }
}
