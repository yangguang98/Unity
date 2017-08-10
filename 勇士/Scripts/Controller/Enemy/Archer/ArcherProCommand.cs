using UnityEngine;
using System.Collections;
/// <summary>
/// 控制层：射箭手属性
/// </summary>
public class ArcherProCommand : BaseEnemyProCommand
{

    //这些数值是用来初始化用的，并没有别的作用
    public int maxHp1 = 10;        //最大生命值
    public float currentHp1 = 10;   //当前生命值
    public int heroExp1 = 10;
    public int def1 = 5;           //防御力
    public int atk1 = 0;           //实际攻击力在箭上

    void Stat()
    {
        this.HeroExp = heroExp1;
        this.Atk = atk1;
        this.Def = def1;
        this.MaxHp = maxHp1;
        base.Start();
    }
}
