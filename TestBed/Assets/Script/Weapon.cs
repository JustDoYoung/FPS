using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ����(�ڷ�ƾ)
public class Weapon : MonoBehaviour
{
    public enum Type {  Melee, Range };
    public Type type;
    public int damage;
    public float rate; //���ݼӵ�
    public BoxCollider meleeArea; //���ݹ���
    public TrailRenderer trailEffect;

    public void Use()
    {
        if (type == Type.Melee)
        {
            StopCoroutine("Swing"); //�ڷ�ƾ ���� �Լ�(���� �߿��� ����)
            StartCoroutine("Swing");
        }
    }
    IEnumerator Swing()
    {
        //yield return null; //����� �����ϴ� Ű����, 1������ ���
        //yield break; �ڷ�ƾ Ż��
        yield return new WaitForSeconds(0.1f); //0.1�� ���
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }
    //Use() ���η�ƾ => Swing() �����ƾ => Use() ���η�ƾ : ���� �Լ�����.
    //Use() ���η�ƾ + Swing() �ڷ�ƾ : ���ý���.
}

