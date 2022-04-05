using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform lookTarget;


    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = target.transform.position;
        //lookTarget에서 target방향으로 바라보고 부딪힌 것이 있다면 그 위치를 targetPosition으로 하고 싶다.

        Ray ray = new Ray(lookTarget.position, targetPosition - lookTarget.position); //(원점, 레이를 쏘는 방향)
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            targetPosition = hitInfo.point;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);
        //transform.position = Vector3.SmoothDamp(transform.position,target.transform.position, Time.deltaTime * 10);
        transform.rotation = target.transform.rotation;
    }
}
