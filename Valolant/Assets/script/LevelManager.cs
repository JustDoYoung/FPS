using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//플레이어의 레벨을 표현하고 싶다.
//적의 생성수, 적을 죽인 횟수(경험치) -> PlayerLevelManager.cs가 올바른 방식
//적이 파괴될 때 Killcount를 1 증가시키고 싶다.
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private void Awake()
    {
        LevelManager.instance = this;
    }
    public int maxEnemyCreateCount;
    //적을 생성한 횟수
    public int createCount;
    int killCount;
    int level;
    public Text textLevel;
    public Slider sliderKillCount;
    public Text textKillCountPer;
    public int LEVEL
    {
        get { return level; }
        set
        {
            level = value;
            textLevel.text = "Lv : " + level;
            maxEnemyCreateCount = level;
            sliderKillCount.maxValue = level;
            sliderKillCount.value = killCount;
            createCount = 0;
        }
    }
    public int KILLCOUNT
    {
        get { return killCount; }
        set
        {
            killCount = value;
            textKillCountPer.text = ((float)killCount / maxEnemyCreateCount) * 100 + "%";
            //StartCoroutine(IELevelUp());
            while (killCount >= maxEnemyCreateCount)
            {
                killCount -= maxEnemyCreateCount;
                LEVEL++;
                textKillCountPer.text = (killCount / maxEnemyCreateCount) * 100 + "%";
            }
            sliderKillCount.value = killCount;
        }
        //만약 killCount가 maxEnemyCreateCount 이상이면
        //level을 1 증가시키겠다.
        // maxEnemyCreateCount를 level과 동일하게 하고 싶다.
    }
    // IEnumerator IELevelUp()
    // {
    //     while (killCount >= maxEnemyCreateCount)
    //     {
    //         killCount -= maxEnemyCreateCount;
    //         LEVEL++;
    //         yield return new WaitForSeconds(0.1f);
    //     }
    //     sliderKillCount.value = killCount;
    // }

    public bool CanCreateEnemy()
    {
        return this.createCount < this.maxEnemyCreateCount;
    }
    void Start()
    {
        LEVEL = 1;
        KILLCOUNT = 0;
    }
}
