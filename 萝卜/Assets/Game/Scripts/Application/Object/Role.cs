using Assets.Game.Scripts.FrameWork.Pool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Role : ReusableObject
{
    

    #region 常量

    #endregion

    #region 事件
    public event Action<int, int> HpChanged;//血量变化，，还没有死亡的时候调用
    public event Action<Role> Dead;//死亡事件
    #endregion

    #region 字段
    int m_hp;
    int m_maxHp;
    #endregion

    #region 属性

    public bool IsDead
    {
        get { return m_hp == 0; }//通过判定m_hp是否等于零
    }

    public int MaxHp
    {
        get { return m_maxHp; }
        set
        {
            if (value < 0)
                value = 0;
            m_maxHp = value;
        }
    }

    public int Hp
    {
        get { return m_hp; }
        set 
        {
            value = Mathf.Clamp(value,0, m_maxHp);//限定范围[0,m_maxHp];
            if (value == m_hp)
                return;

            m_hp = value; 

            //血量变化
            if(HpChanged !=null)
            {
                //当血量变化后需要触发一些事件，可能是UI的显示
                HpChanged(m_hp, m_maxHp);
            }

            //死亡
            if(Dead !=null&&m_hp ==0)
            {
                Dead(this);
            }
        }
    }
    #endregion

    #region 方法

    public virtual void Damage(int hit)
    {
        //是一个被动的过程，需要别的调用
        if (IsDead)
            return;

        Hp -= hit;
    }

    protected virtual void Die(Role role)
    {

    }
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调

    public override void OnSpawn()
    {
        this.Dead += Die;
    }

    public override void OnUnspawn()
    {
        //回收游戏对象，，清空其中的数据
        Hp = 0;
        MaxHp = 0;
        while (HpChanged != null)
            HpChanged -= HpChanged;
        while (Dead != null)
            Dead -= Dead;
    }

    #endregion

    #region 帮助方法
    #endregion
}
