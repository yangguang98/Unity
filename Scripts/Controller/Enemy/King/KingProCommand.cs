using UnityEngine;
using System.Collections;
/// <summary>
/// 控制层：国王属性 
/// </summary>
public class KingProCommand : BaseEnemyProCommand
{

    //这些数值是用来初始化用的，并没有别的作用
    public int maxHp1 = 50;        //最大生命值
    public float currentHp1 = 0;   //当前生命值
    public int heroExp1 = 50;
    public int def1 = 20;           //防御力
    public int atk1 = 30;           //攻击力

    void Stat()
    {
        this.HeroExp = heroExp1;
        this.Atk = atk1;
        this.Def = def1;
        this.MaxHp = maxHp1;
        base.Start();
    }
}
