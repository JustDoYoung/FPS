using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    int hp;
    public int maxHP = 2;

    public Slider sliderHP;

    public int HP
    {
        get { return hp; }
        set
        {
            this.hp = value;
            sliderHP.value = hp;
        }
    }
    void Start()
    {
        //�¾ �� ü���� �ִ�ü������ �ϰ�ʹ�.
        sliderHP.maxValue = maxHP;
        HP = maxHP;
    }
}
