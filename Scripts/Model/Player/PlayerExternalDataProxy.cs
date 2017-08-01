using UnityEngine;
using System.Collections;
/// <summary>
/// 模型层：玩家扩展数值代理类
/// 
/// 功能：为了简化数值的开发，我们把数值的直接存取与复杂操作相分离（本质为“代理”设计模式的应用
/// </summary>
public class PlayerExternalDataProxy : PlayerExternalData {

    public static PlayerExternalDataProxy _instance = null;         //单例

    //允许到外面去构造
    public PlayerExternalDataProxy(float experience, float killNum, float level, float gold, float diamond)
        : base(experience ,killNum ,level ,gold ,diamond)//调用子类构造函数
    {
        if(_instance==null )
        {
            _instance = this;//第一次构造的时候设置单例
        }
        else
        {
            Debug.LogError(GetType() + "/PlayerKernalDataProxy()不能多次调用构造函数去构造实例,请检查");
        }
    }

    public static PlayerExternalDataProxy GetInsance()
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


    #region 经验

    //增加经验值
    public void AddExp(int expnum)
    {
        base.Experience += expnum;

        //TODO经验值到达一定阶段，会自动提升等级（升级）
        UpgradeRole.GetInstance().UpgradeCondition((int)base.Experience);
    }

    //得到经验值
    public float GetExp()
    {
        return base.Experience;
    }
     
    #endregion

    #region 钻石

    //增加金币
    public void AddDiamond(int diamondNum)
    {
        base.Diamond += Mathf.Abs(diamondNum);
    }

    //减少钻石
    public bool DecreaseDiamond(int diamondNum)
    {
        bool res = false;

        //钻石的余额不能为负数
        if((GetDiamond()-Mathf.Abs (diamondNum))>=0)
        {
            base.Diamond -= Mathf.Abs(diamondNum);
            res = true;
        }
        else
        {
            res = true;
        }
        return res;
    }

    public float GetDiamond()
    {
        return base.Diamond;
    }
    #endregion

    #region 金币

    //增加金币
    public void AddGold(int goldNum)
    {
        base.Gold += Mathf.Abs(goldNum);
    }

    //减少金币
    public bool DecreaseGold(int goldNum)
    {
        bool res = false;

        //钻石的余额不能为负数
        if ((GetGold() - Mathf.Abs(goldNum)) > 0)
        {
            base.Gold -= Mathf.Abs(goldNum);
            res = true;
        }
        else
        {
            res = true;
        }
        return res;
    }

    public float GetGold()
    {
        return base.Gold;
    }
    #endregion

    #region 杀敌数量

    //增加杀敌数量
    public void AddKillNum()
    {
        ++base.KillNum;
    }

    //得到杀敌数量
    public float GetKillNum()
    {
        return base.KillNum ;
    }
    #endregion

    #region 等级

    //升级
    public void AddLevel()
    {
        ++base.Level;

        //    相应的最大核心数值会提升
        UpgradeRole.GetInstance().UpgradeOperation((LevelName)base.Level);//数值转换为枚举类型，前提是枚举已经标记为数值  没有标记为数值，则会出错
    }

    //获得当前等级
    public float GetLevel()
    {
        return base.Level;
    }
    #endregion

    #region
    #endregion


}
