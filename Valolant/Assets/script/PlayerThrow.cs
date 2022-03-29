using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//사용자가 G키를 누르면 공장에서 폭탄을 만들어서 내 위치에 배치하고 싶다.
//폭탄의 앞방향을 내가 던지고자 하는 방향(grenadeThrowPosition)으로 회전시키고 싶다.(내 기준 50도 방향)
//
public class PlayerThrow : MonoBehaviour
{
    public GameObject grenadeFactory;
    public Transform grenadeThrowPosition;

    // Update is called once per frame
    void Update()
    {
        //사용자가 G키를 누르면
        if (Input.GetKeyDown(KeyCode.G))
        {
        //공장에서 폭탄을 만들어서
            GameObject grenade = Instantiate(grenadeFactory);
        //내 위치에 배치하고 싶다.
            grenade.transform.position = grenadeThrowPosition.position;
            //폭탄의 앞방향을 내가 던지고자 하는 방향으로 회전시키고 싶다.(내 기준 45도 방향)
            //Vector3 dir = transform.forward + transform.up; //내 기준 45도 방향
            //grenade.transform.foward = dir;

            //폭탄의 앞방향을 내가 던지고자 하는 방향으로 회전시키고 싶다.(내 기준 50도 방향)
            //Quaternion q = grenadeThrowPosition.rotation * Quaternion.Euler(-50, 0, 0); //기준의 회전좌표 * Quaternion.Euler(회전각)
            //grenade.transform.rotation = q;

            grenade.transform.forward = grenadeThrowPosition.forward; //일반적인 FPS
        }
    }
}
