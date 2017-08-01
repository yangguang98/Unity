using UnityEngine;
using System.Collections;
/// <summary>
/// 模型层：玩家核心数值代理类
/// 
/// 功能：
///       为了简化数值的开发，我们把数值的直接存取与复杂操作相分离（本质为“代理”设计模式的应用
///       本类必须设计为带有构函数的单例模式
/// </summary>
public class PlayerKernalDataProxy : PlayerKernalData {

    public static PlayerKernalDataProxy _instance=null;         //单例
    public const int ENEMY_MIN_ATTACK=2;                        //敌人最低攻击力

    public PlayerKernalDataProxy(float health, float magic, float attack, float defence, float dexterity,
        float maxHealth, float maxMagic, float maxAttack, float maxDefence, float maxDexterity,
            float attackByPro, float defenceByPro, float DexteriryByPro):base(health ,magic ,attack ,defence ,dexterity ,maxHealth ,maxMagic ,maxAttack ,maxDefence ,maxDexterity ,attackByPro ,defenceByPro ,DexteriryByPro )//调用子类构造函数
    {
        if(_instance==null )
        {
            _instance = this;
        }
        else
        {
            Debug.LogError(GetType() + "/PlayerKernalDataProxy()不能多次调用构造函数去构造实例,请检查");
        }
    }

    public static PlayerKernalDataProxy GetInsance()
    {
        if(_instance !=null)
        {
            return _instance;
        }
        else
        {
            Debug.LogError("请先构造实例！！！！！！");
            return null;
        }

    }


    #region 生命数值操作

    //掉血
    //公式：_Health=_Health -(敌人的攻击力-主角的防御力-主角武器防御力)
    public void DecreaseHealthValue(float enemyAttackValue)
    {
        float enemyReallyAttack=0f;//敌人真实攻击力
        enemyReallyAttack =enemyAttackValue  -base.Defence +base.DefenceByPro;
        if(enemyReallyAttack >0)
        {
            base.Health -=enemyReallyAttack ;
        }else{
            base.Health -=ENEMY_MIN_ATTACK ;
        }

        //更新与生命数值有关的数值
        this.UpdataATKValue();
        this.UpdataDefenceValue();
        this.UpdateDexterityValue();
    }

    //增加血量
    public void IncreaseHealthValue(float healthValue)
    {
        float ReallyIncreaseValue=base.Health +healthValue ;

        //是否超过最大值 
        if(ReallyIncreaseValue<base.MaxHealth)
        {
            base.Health = ReallyIncreaseValue;
        }
        else
        {
            base.Health = base.MaxHealth;
        }

        //更新与生命数值有关的数值
        this.UpdataATKValue();
        this.UpdataDefenceValue();
        this.UpdateDexterityValue();
    }

    //得到当前主角的生命数值
    public float GetCurrentHealthValue()
    {
        return base.Health;
    }

    //增加最大生命数值  升级
    public void IncreaseMaxHealthValue(float increaseHp)
    {
        base.MaxHealth += increaseHp;
    }

    public float GetMaxHealthValue()
    {
        return base.MaxHealth;
    }

    #endregion

    #region 魔法数值操作

    //减少魔法数值（是放大招）
    //公式：_Magic=_Magic-(释放一次“特定魔法”的损耗)
    public void DecreaseMagicValue(float magicValue)
    {
        float reallyMagicValue=base.Magic - Mathf.Abs(magicValue);//实际剩余魔法
        if(reallyMagicValue>=0)
        {
            base.Magic -= Mathf.Abs(magicValue);
        }
        else
        {
            //不能释放，，魔法不够
        }
        
    }

    //增加魔法
    public void IncreaseMagicValue(float magicValue)
    {
        float ReallyIncreaseValue = base.Magic + magicValue;

        //是否超过最大值 
        if (ReallyIncreaseValue < base.MaxMagic)
        {
            base.Magic = ReallyIncreaseValue;
        }
        else
        {
            base.Magic = base.MaxMagic;
        }
    }

    //得到当前主角的魔法数值
    public float GetCurrentMagicValue()
    {
        return base.Magic;
    }

    //增加最大魔法数值  升级
    public void IncreaseMaxMagicValue(float increaseMagic)
    {
        base.MaxMagic += increaseMagic;
    }

    public float GetMaxMagicValue()
    {
        return base.MaxMagic;
    }

    #endregion


    #region 攻击力数值操作


     //更新攻击力（典型应用场景：当主角健康值或者得到新的武器
    //_AttackForce=MaxATK/2*(_Health/MaxHealth)+[“武器攻击力”]；
    public void UpdataATKValue(float newWeaponValue=0)
    {
        float reallyATKValue = 0f;

        //没有获取新的武器道具
        if(newWeaponValue ==0)
        {
            reallyATKValue = base.MaxAttack / 2 * (base.Health / base.MaxHealth) + base.AttackByPro;
        }
        //获取新的武器道具
        else 
        {
            reallyATKValue = base.MaxAttack / 2 * (base.Health / base.MaxHealth) + newWeaponValue;
            base.AttackByPro = newWeaponValue;
        }

        //数值有效性验证
        if(reallyATKValue >base.MaxAttack )
        {
            base.Attack = base.MaxAttack;
        }
        else
        {
            base.Attack = reallyATKValue;
        }
    }

    //得到当前主角的攻击力
    public float GetCurrentATKValue()
    {
        return base.Attack;
    }

    //增加最大攻击力数值  升级
    public void IncreaseMaxATKValue(float increaseATK)
    {
        base.MaxAttack += increaseATK;
    }
     
    //得到当前主角的最大攻击力
    public float GetMaxATKValue()
    {
        return base.MaxAttack;
    }

    #endregion

    #region 防御力数值操作


    //更新防御力（典型应用场景：当主角健康值或者得到新的武器
    //_Defence=MaxDEF/2*(_Health/MaxHealth)+[武器防御力];
    public void UpdataDefenceValue(float newDefenceValue = 0)
    {
        float reallyDefenceValue = 0f;     //实际防御数值

        //没有获取新的武器道具
        if (newDefenceValue == 0)
        {
            reallyDefenceValue = base.MaxDefence / 2 * (base.Health / base.MaxHealth) + base.DefenceByPro;
        }
        //获取新的武器道具
        else
        {
            reallyDefenceValue = base.MaxDefence / 2 * (base.Health / base.MaxHealth) + newDefenceValue;
            base.DefenceByPro = newDefenceValue;
        }

        //数值有效性验证
        if (reallyDefenceValue > base.MaxDefence)
        {
            base.Defence = base.MaxDefence;
        }
        else
        {
            base.Defence = reallyDefenceValue;
        }
    }

    //得到当前主角的防御力
    public float GetCurrentDefenceValue()
    {
        return base.Defence;
    }

    //增加最大防御力数值  升级
    public void IncreaseMaxDefenceValue(float increaseDefence)
    {
        base.MaxDefence += increaseDefence;
    }

    //得到当前主角的最大防御力
    public float GetMaxDefenceValue()
    {
        return base.MaxDefence;
    }

    #endregion

    #region 敏捷度数值操作


    //更新敏捷度（典型应用场景：当主角健康值或者得到新的武器
    //_MoveSpeed=MaxMoveSpeed/2*(_Health/MaxHealth)-_Defence+[道具敏捷力]  
    public void UpdateDexterityValue(float newDexterityValue = 0)
    {
        float reallyDexterityValue = 0f;     //实际敏捷度数值

        //没有获取新的武器道具
        if (newDexterityValue == 0)
        {
            reallyDexterityValue = base.MaxDexterity / 2 * (base.Health / base.MaxHealth) + base.DexterityByPro;
        }
        //获取新的武器道具
        else
        {
            reallyDexterityValue = base.MaxDexterity / 2 * (base.Health / base.MaxHealth) - base.Defence+ newDexterityValue;
            base.DexterityByPro = newDexterityValue;
        }

        //数值有效性验证
        if (reallyDexterityValue > base.MaxDexterity)
        {
            base.Dexterity = base.MaxDexterity;
        }
        else
        {
            base.Dexterity = reallyDexterityValue;
        }
    }

    //得到当前主角的敏捷度
    public float GetCurrentDexterityValue()
    {
        return base.Dexterity;
    }

    //增加最大敏捷度数值  升级
    public void IncreaseMaxDexterityValue(float increaseDexterityValue)
    {
        base.MaxDexterity += increaseDexterityValue;
    }

    //得到当前主角的最大敏捷度
    public float GetMaxDexterityValue()
    {
        return base.MaxDexterity;
    }

    #endregion

 
}
