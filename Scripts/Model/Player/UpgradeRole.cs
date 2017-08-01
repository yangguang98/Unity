using UnityEngine;
using System.Collections;
/// <summary>
/// 模型层：升级规则
/// 描述：描述项目中的升级
/// </summary>
public class UpgradeRole
{

    private static UpgradeRole _instance;

    private UpgradeRole()
    {

    }

    public static UpgradeRole GetInstance()
    {
        if (_instance == null)
        {
            _instance = new UpgradeRole(); //内部去构造

        }
        return _instance;
    }

    public void UpgradeCondition(int exp)
    {
        int currentLevel = (int)PlayerExternalDataProxy.GetInsance().GetLevel();
        if (exp > 100 && exp < 300 && currentLevel == 0)
        {
            PlayerExternalDataProxy.GetInsance().AddLevel();
        }
        else if (exp >= 300 && exp < 500 && currentLevel == 1)
        {
            PlayerExternalDataProxy.GetInsance().AddLevel();
        }
        else if (exp >= 500 && exp < 1000 && currentLevel == 2)
        {
            PlayerExternalDataProxy.GetInsance().AddLevel();
        }
        else if (exp >= 1000 && exp < 3000 && currentLevel == 3)
        {
            PlayerExternalDataProxy.GetInsance().AddLevel();
        }
        else if (exp >= 3000 && exp > 5000 && currentLevel == 4)
        {
            PlayerExternalDataProxy.GetInsance().AddLevel();
        }
        else if (exp >= 5000 && exp < 10000 && currentLevel == 5)
        {
            PlayerExternalDataProxy.GetInsance().AddLevel();
        }

    }

    //升级操作
    //1.所有的核心最大数值
    //2.对应的“生命数值”，“魔法数值”，加满为最大值
    public void UpgradeOperation(LevelName levelName)
    {
        switch (levelName)
        {
            case LevelName .Level_0 :
                //Level_1:     +10    +10      +2       +1          +10
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_1:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_2:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_3:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_4:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_5:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_6:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_7:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_8:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_9:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            case LevelName.Level_10:
                UpgradeRuleOperation(10, 10, 2, 1, 10);
                break;
            default :
                break;
        }
    }

    //所有的核心最大数值增加
    //对应的“生命数值”，“魔法数值”，加满为最大值
    private void UpgradeRuleOperation(float hp,float mp,float ATK,float DEF,float DEX)
    {
        //所有的核心最大数值增加
        PlayerKernalDataProxy.GetInsance().IncreaseMaxHealthValue(hp);
        PlayerKernalDataProxy.GetInsance().IncreaseMaxDexterityValue(DEX);
        PlayerKernalDataProxy.GetInsance().IncreaseMaxDefenceValue(DEF);
        PlayerKernalDataProxy.GetInsance().IncreaseMaxMagicValue(mp);
        PlayerKernalDataProxy.GetInsance().IncreaseMaxATKValue(ATK);


        //对应的“生命数值”，“魔法数值”，加满为最大值
        PlayerKernalDataProxy.GetInsance().IncreaseHealthValue(PlayerKernalDataProxy.GetInsance().GetMaxHealthValue());
        PlayerKernalDataProxy.GetInsance().IncreaseMagicValue(PlayerKernalDataProxy.GetInsance().GetMaxMagicValue());
    }
}
