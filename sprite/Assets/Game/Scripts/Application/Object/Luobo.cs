using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//damage和die是不一样的，，damage需要外部去调用，当受到了攻击，就调用这个方法，die是属于内部触发，血量减少到0就会死亡
class Luobo:Role
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    Animator m_animator; 
    #endregion

    #region 属性
    #endregion

    #region 方法
    public override void Damage(int hit)
    {
        if (IsDead)
            return;
        base.Damage(hit);//掉血，，掉血可能触发一些事件的发生

        m_animator.SetTrigger("IsDamage");//动画

    }

    public override void OnSpawn()
    {
        //初始化
        base.OnSpawn();
        m_animator = GetComponent<Animator>();
        m_animator.Play("Idle");//播放idle
        LuoboInfo info = Game.Instance.staticData.GetLuoboInfo();
        MaxHp = info.hp;
        Hp = info.hp;
    }

    public override void OnUnspawn()
    {
        //还原
        base.OnUnspawn();
        m_animator.SetBool("IsDead", false);
        m_animator.ResetTrigger("IsDamage");
    }

    protected override void Die(Role role)
    {
        //死亡就会触发，，在base中注册了
        base.Die(role);

        m_animator.SetBool("IsDead",true);//死亡动画
    }

    #endregion

    #region Unity回调

    #endregion

    #region 事件回调

    #endregion

    #region 帮助方法

    #endregion


}
