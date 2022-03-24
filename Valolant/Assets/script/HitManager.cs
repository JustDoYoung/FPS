using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ȭ���� ��½�Ÿ��� �ϰ� �ʹ�.
//ImageHit ������Ʈ�� �����ٰ� 0.1�� �Ŀ� �� ���̰� �ϰ� �ʹ�.
public class HitManager : MonoBehaviour
{
    public static HitManager instance; //static : instance ������ class ScoreManager�� ��! ��ü�� ���� �ƴ�

    private void Awake()
    {
        HitManager.instance = this; //class�� ������� ��ü�� �ڱ��ڽ��� �θ� �� : this
    }

    public GameObject imageHit;

    void Start()
    {
        imageHit.SetActive(false);
    }
    public void DoHitPlz()
    {
        StopCoroutine(IEDoHit());
        StartCoroutine(IEDoHit());
    }

    IEnumerator IEDoHit()
    {
        //���̰� �Ѵ�.
        imageHit.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        //�� ���̰� �Ѵ�.
        imageHit.SetActive(false);
        yield return new WaitForSeconds(0.1f);
    }
}
