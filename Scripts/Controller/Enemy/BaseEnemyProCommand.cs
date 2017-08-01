using UnityEngine;
using System.Collections;
/// <summary>
/// 控制层：敌人属性父类
/// 描述：
///   运用重构的思想，来构造更加灵活与低耦合度的系统
///   1.包含所有敌人的公共属性
/// </summary>
public class BaseEnemyProCommand : Command
{

    //private bool isAlive = true;  //是否生存
    private int maxHp = 20;        //最大生命值
    private float currentHp = 0;   //当前生命值
    private int heroExp = 5; 
    private int def = 5;           //防御力
    private int atk = 2;           //攻击力
    private EnemyState currentState = EnemyState.idle;

    //当前状态
    public EnemyState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }

    public float CurrentHp
    {
        get { return currentHp; }
        set { currentHp = value; }
    }

    public int HeroExp
    {
        get { return heroExp; }
        set { heroExp = value; }
    }

    public int Def
    {
        get { return def; }
        set { def = value; }
    }

    public int Atk
    {
        get { return atk; }
        set { atk = value; }
    }

    void OnEnable()
    {
        StartCoroutine("CheckLifeContinue");
    }

    void OnDisable()
    {
        StopCoroutine("CheckLifeContinue");
    }
    protected void Start()
    {
        currentHp = maxHp;
        //判断是否存活
        //StartCoroutine("CheckLifeContinue");
    }

    void update()
    {

    }


    //协程需要循环执行  确定敌人是否死亡
    IEnumerator CheckLifeContinue()
    {
        while (true)
        {
            yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT1);
            if (currentHp <= maxHp * 0.01)
            {
                //isAlive = false;
                CurrentState = EnemyState.death;
                //关于英雄增加经验值
                HeroPropertyCommand.Instance.AddExp(heroExp);
                //增加杀敌数量
                HeroPropertyCommand.Instance.AddKillNum();

                CurrentState = EnemyState.death;//播放死亡动画

                //Destroy(this.gameObject, 3f);//销毁游戏物体,,让动画播放完

                StartCoroutine("RecoverEnemy");
            }
        }
    }

    public void OnHurt(int hurtValue)
    {
        print("收到了攻击");

        //播放动画
        currentState = EnemyState.hurt;

        int hurtValues = 0;
        hurtValues = Mathf.Abs(hurtValue);//保证大于零
        if (hurtValues > 0 && CurrentState != EnemyState.death)
        {
            currentHp -= hurtValues;
        }
    }


    IEnumerator RecoverEnemy()
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_3);

        //敌人回收前状态重置
        currentHp = MaxHp;

        PoolManager.PoolsArray["Enemys"].RecoverGameObject(this.gameObject);//回收敌人
    }
}
