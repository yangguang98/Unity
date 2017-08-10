using UnityEngine;
using System.Collections;
/// <summary>
///  控制层：敌人战士属性脚本
/// </summary>
public class RedWarriorProCommand : BaseEnemyProCommand
{

    public int heroExperence = 20;
    public int initATK = 10;
    public int dEF = 3;
    public int maxHealth = 50;

    void Stat()
    {
        this.HeroExp = heroExperence;
        this.Atk = initATK;
        this.Def = dEF;
        this.MaxHp = maxHealth;

        //父类
        base.Start();
    }
}
