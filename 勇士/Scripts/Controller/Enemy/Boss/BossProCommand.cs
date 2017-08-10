using UnityEngine;
using System.Collections;

public class BossProCommand : BaseEnemyProCommand
{

    //这些数值是用来初始化用的，并没有别的作用
    public int maxHp1 = 500;        //最大生命值
    public float currentHp1 = 500;   //当前生命值
    public int heroExp1 = 1500;
    public int def1 = 30;           //防御力
    public int atk1 = 50;           //攻击力

    void Stat()
    {
        this.HeroExp = heroExp1;
        this.Atk = atk1;
        this.Def = def1;
        this.MaxHp = maxHp1;
        base.Start();
    }

}
