using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float kAdjust = 0.5f;

    // Update is called once per frame
    void Update()
    {
        //스페이스바를 누르면 카메라를 1초동안 흔들고 싶다.(코루틴)
        if (Input.GetButtonDown("Jump"))
        {
            Shake(1);
        }
    }

    public void Shake(float time)
    {
        StartCoroutine("IEShake", time);
    }
    IEnumerator IEShake(float time)
    {
        Vector3 origin = new Vector3(0, 0, 0);
        //time초 동안 화면을 흔들고 싶다.
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            transform.localPosition = origin + Random.insideUnitSphere * kAdjust; //1m짜리 구의 랜덤 점 하나를 찍는다.
            yield return 0;
        }
        //다 흔들고 나면 원래 위치로 돌려놓고 싶다.
        transform.localPosition = origin;
    }
}
