using UnityEngine;
using System.Collections;
/// <summary>
/// 控制层：英雄属性脚本
/// 
/// 描述：
///   1.实例化对应模型层且初始化数据
///   2.整合模型层关于“玩家”模块的核心方法，供本控制层其他脚本调用
///   3.通过控制层去调用模型层
/// </summary>
public class HeroPropertyCommand : Command {

	//玩家核心数值
    public float playerCurrentHp=1000f;
    public float playerMaxHp=1000f;
    public float playerCurrentMp=1000f;
    public float playerMaxMp=1000f;
    public float playerCurrentATK=10f;
    public float playerMaxATK=10f;
    public float playerCurrentDEF = 5f;
    public float playerMaxDEF=5f;
    public float playerCurrentDEX=45;
    public float playerMaxDEX=50;

    public float ATKByPro=0f;
    public float DEFByPro=0f;
    public float DEXByPro=0f;

    //玩家背包数值  用来初始化
    public int iBloodBottleNum = 0;  //血瓶数量
    public int iMagicBottleNum = 0;  //魔法瓶数量
    public int iATKNum = 0;          //攻击力道具
    public int iDEFNum = 0;          //防御力道具
    public int iDEXNum = 0;          //敏捷度道具



    //玩家扩展数值
    public int exp = 0;
    public int level = 0;
    public int gold = 0;
    public int kllNum = 0;
    public int diamond = 0;

    public static HeroPropertyCommand Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //初始化模型层数据
        PlayerKernalDataProxy playerKernalDataObj = new PlayerKernalDataProxy(playerCurrentHp, playerCurrentMp, playerCurrentATK, playerCurrentDEF, playerCurrentDEX, playerMaxHp, playerMaxMp, playerMaxATK, playerMaxDEF, playerMaxDEX, ATKByPro, DEFByPro, DEXByPro);

        PlayerExternalDataProxy playerExternalDataObj = new PlayerExternalDataProxy(exp, kllNum, level, gold, diamond);

        //Add By 2017
        PlayerPackageDataProxy playerPakcageData = new PlayerPackageDataProxy(iBloodBottleNum, iMagicBottleNum, iATKNum, iDEFNum, iDEXNum);
    }


    #region 生命数值操作

    //掉血
    //公式：_Health=_Health -(敌人的攻击力-主角的防御力-主角武器防御力)
    public void DecreaseHealthValue(float enemyAttackValue)
    {
        if(enemyAttackValue >0)
        {
            PlayerKernalDataProxy.GetInsance().DecreaseHealthValue(enemyAttackValue);
        }
    }

    //增加血量
    public void IncreaseHealthValue(float healthValue)
    {
        if(healthValue >0)
        {
            PlayerKernalDataProxy.GetInsance().IncreaseHealthValue(healthValue);
        }
        
    }

    //得到当前主角的生命数值
    public float GetCurrentHealthValue()
    {
        return PlayerKernalDataProxy.GetInsance().GetCurrentHealthValue();
    }

    //增加最大生命数值  升级
    public void IncreaseMaxHealthValue(float increaseHp)
    {
        if(increaseHp>0)
        {
            PlayerKernalDataProxy.GetInsance().IncreaseMaxHealthValue(increaseHp); 
        }
        
    }

    public float GetMaxHealthValue()
    {
        return PlayerKernalDataProxy.GetInsance().GetMaxHealthValue();
    }

    #endregion

    #region 魔法数值操作

    //减少魔法数值（是放大招）
    //公式：_Magic=_Magic-(释放一次“特定魔法”的损耗)
    public void DecreaseMagicValue(float magicValue)
    {
        if (magicValue > 0)
        {
            PlayerKernalDataProxy.GetInsance().DecreaseMagicValue(magicValue);
        }
    }

    //增加魔法
    public void IncreaseMagicValue(float magicValue)
    {
        if(magicValue >0)
        {
            PlayerKernalDataProxy.GetInsance().IncreaseMagicValue(magicValue);
        }
    }

    //得到当前主角的魔法数值
    public float GetCurrentMagicValue()
    {
        return PlayerKernalDataProxy.GetInsance().GetCurrentMagicValue();
    }

    //增加最大魔法数值  升级
    public void IncreaseMaxMagicValue(float increaseMagic)
    {
        if(increaseMagic >0)
        {
            PlayerKernalDataProxy.GetInsance().IncreaseMaxMagicValue(increaseMagic);
        }
        
    }

    public float GetMaxMagicValue()
    {
        return PlayerKernalDataProxy.GetInsance().GetMaxMagicValue();
    }

    #endregion


    #region 攻击力数值操作


    //更新攻击力（典型应用场景：当主角健康值或者得到新的武器
    //_AttackForce=MaxATK/2*(_Health/MaxHealth)+[“武器攻击力”]；
    public void UpdataATKValue(float newWeaponValue = 0)
    {
        if(newWeaponValue>0)
        {
            PlayerKernalDataProxy.GetInsance().UpdataATKValue(newWeaponValue);
        }
    }

    //得到当前主角的攻击力
    public float GetCurrentATKValue()
    {
        return PlayerKernalDataProxy.GetInsance().GetCurrentATKValue();
    }

    //增加最大攻击力数值  升级
    public void IncreaseMaxATKValue(float increaseATK)
    {
        if(increaseATK>0)
        {
            PlayerKernalDataProxy.GetInsance().IncreaseMaxATKValue(increaseATK);
        }
        
    }

    //得到当前主角的最大攻击力
    public float GetMaxATKValue()
    {
        return PlayerKernalDataProxy.GetInsance().GetMaxATKValue();
    }

    #endregion

    #region 防御力数值操作


    //更新防御力（典型应用场景：当主角健康值或者得到新的武器
    //_Defence=MaxDEF/2*(_Health/MaxHealth)+[武器防御力];
    public void UpdataDefenceValue(float newDefenceValue = 0)
    {
       if(newDefenceValue >0)
       {
           PlayerKernalDataProxy.GetInsance().UpdataDefenceValue(newDefenceValue);
       }
    }

    //得到当前主角的防御力
    public float GetCurrentDefenceValue()
    {
        return PlayerKernalDataProxy.GetInsance().GetCurrentDefenceValue();
    }

    //增加最大防御力数值  升级
    public void IncreaseMaxDefenceValue(float increaseDefence)
    {
        if(increaseDefence >0)
        {
            PlayerKernalDataProxy.GetInsance().IncreaseMaxDefenceValue(increaseDefence);
        }
        
    }

    //得到当前主角的最大防御力
    public float GetMaxDefenceValue()
    {
        return PlayerKernalDataProxy.GetInsance().GetMaxDefenceValue();
    }

    #endregion

    #region 敏捷度数值操作


    //更新敏捷度（典型应用场景：当主角健康值或者得到新的武器
    //_MoveSpeed=MaxMoveSpeed/2*(_Health/MaxHealth)-_Defence+[道具敏捷力]  
    public void UpdateDexterityValue(float newDexterityValue = 0)
    {
        if(newDexterityValue >0)
        {
            PlayerKernalDataProxy.GetInsance().UpdateDexterityValue(newDexterityValue);
        }
    }

    //得到当前主角的敏捷度
    public float GetCurrentDexterityValue()
    {
        return PlayerKernalDataProxy.GetInsance().GetCurrentDexterityValue();
    }

    //增加最大敏捷度数值  升级
    public void IncreaseMaxDexterityValue(float increaseDexterityValue)
    {
        if(increaseDexterityValue >0)
        {
            PlayerKernalDataProxy.GetInsance().IncreaseMaxDexterityValue(increaseDexterityValue);
        }
    }

    //得到当前主角的最大敏捷度
    public float GetMaxDexterityValue()
    {
        return PlayerKernalDataProxy.GetInsance().GetMaxDexterityValue();
    }

    #endregion


    #region 经验

    //增加经验值
    public void AddExp(int expNum)
    {
        if(expNum>0)
        {
            PlayerExternalDataProxy.GetInsance().AddExp(expNum);
        }
    }

    //得到经验值
    public float GetExp()
    {
        return PlayerExternalDataProxy.GetInsance().GetExp();
    }

    #endregion

    #region 钻石

    //增加金币
    public void AddDiamond(int diamondNum)
    {
        if(diamondNum>0)
        {
            PlayerExternalDataProxy.GetInsance().AddDiamond(diamondNum);
        }
        
    }

    public float GetDiamond()
    {
        return PlayerExternalDataProxy.GetInsance().GetDiamond();
    }
    #endregion

    #region 金币

    //增加金币
    public void AddGold(int goldNum)
    {
        if(goldNum >0)
        {
            PlayerExternalDataProxy.GetInsance().AddGold(goldNum);
        }
        
    }

    public float GetGold()
    {
        return PlayerExternalDataProxy.GetInsance().GetGold();
    }
    #endregion

    #region 杀敌数量

    //增加杀敌数量
    public void AddKillNum()
    {
        PlayerExternalDataProxy.GetInsance().AddKillNum();
    }

    //得到杀敌数量
    public float GetKillNum()
    {
        return PlayerExternalDataProxy.GetInsance().GetKillNum();
    }
    #endregion

    #region 等级

    //升级
    public void AddLevel()
    {
        PlayerExternalDataProxy.GetInsance().AddLevel();
    }

    //获得当前等级
    public float GetLevel()
    {
        return PlayerExternalDataProxy.GetInsance().GetLevel();
    }
    #endregion


}
