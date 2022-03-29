using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHP : MonoBehaviour
{
    //체력을 표현하고 싶다.
    int hp;
    public int maxHP = 5;
    public GameObject[] hpObjectList;

    int hpPotion;
    public Text textHpPotion;
    //UI는 프로퍼티로 조작하면 편함
    public int HP_POTION
    {
        get { return hpPotion; }
        set
        {
            hpPotion = value;
            textHpPotion.text = "X " + hpPotion + " 개";
        }
    }
    public int HP
    {
        get { return hp; }
        set
        {
            print("HP set 호출");
            this.hp = value;

            for (int i = 0; i < hpObjectList.Length; i++)
            {
                //비활성화시켜야 빨간 하트 이미지가 UI에 보인다.
                //!(hp > i) false : 현재 hp가 index보다 크면 비활성화
                //!(hp > i) true : 현재 hp가 index보다 작으면 활성화해서 체력이 줄어든 것처럼 보인다.
                hpObjectList[i].SetActive(!(hp > i));
            }
            // hpObjectList[0].SetActive(!(hp > 0));
            // hpObjectList[1].SetActive(!(hp > 1));
            // hpObjectList[2].SetActive(!(hp > 2));
            // hpObjectList[3].SetActive(!(hp > 3));
            // hpObjectList[4].SetActive(!(hp > 4));
        }
    }
    public void AddHP(int value)
    {
        maxHP+=value;
        if(maxHP > hpObjectList.Length){
            maxHP = hpObjectList.Length;
        }
        ResetHpObject();
        //UI를 갱신
        //HP = HP; //유니티 업데이트하면서 
        //HP = maxHP;
    }
    public void ResetHpObject()
    {
        //최대 체력만큼 체력UI를 활성화한다.
        for (int i = 0; i < hpObjectList.Length; i++)
        {
            hpObjectList[i].transform.parent.gameObject.SetActive(i < maxHP);
        }
    }

    void Start()
    {
        HP = maxHP;
        ResetHpObject();
    }

    private void Update()
    {
        //만약 1번 키를 누르면 
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            //포션을 사용해서
            //만약 포션이 1개 이상이라면
            if (HP_POTION > 0)
            {
                //포션을 1감소시키고
                HP_POTION--;
                //체력을 1증가시키고 싶다. 
                HP++;
            }
        }
    }
}
