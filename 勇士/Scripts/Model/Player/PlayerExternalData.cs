using UnityEngine;
using System.Collections;
using System;
/// <summary>
/// 模型层：玩家扩展数值代理类
/// 功能：本类提供玩家扩展数据的存取
[Serializable]
public class PlayerExternalData 
{

    public static event PlayerKernalDataDelegate PlayerExternalDataEvent;            //玩家扩展数值

    #region 字段
    private float experience;              //经验值
    private float killNum;                 //杀敌数量
    private float level;                   //当前等级
    private float gold;                    //当前金币
    private float diamond;                 //钻石

    #endregion

    #region 属性
    public float Experience
    {
        get { return experience; }
        set
        {
            experience = value;
            if (PlayerExternalDataEvent != null)
            {
                KeyValuesUpdate e = new KeyValuesUpdate("Experience", experience);
                PlayerExternalDataEvent(e);
            }
        }
    }


    public float KillNum
    {
        get { return killNum; }
        set
        {
            killNum = value;
            if (PlayerExternalDataEvent != null)
            {
                KeyValuesUpdate e = new KeyValuesUpdate("KillNum", killNum);
                PlayerExternalDataEvent(e);
            }
        }
    }


    public float Level
    {
        get { return level; }
        set
        {
            level = value;
            if (PlayerExternalDataEvent != null)
            {
                KeyValuesUpdate e = new KeyValuesUpdate("Level", Level);
                PlayerExternalDataEvent(e);
            }
        }
    }


    public float Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            if (PlayerExternalDataEvent != null)
            {
                KeyValuesUpdate e = new KeyValuesUpdate("Gold", Gold);
                PlayerExternalDataEvent(e);
            }
        }
    }


    public float Diamond
    {
        get { return diamond; }
        set
        {
            diamond = value;
            if (PlayerExternalDataEvent != null)
            {
                KeyValuesUpdate e = new KeyValuesUpdate("Diamond", Diamond);
                PlayerExternalDataEvent(e);
            }
        }

    }

    #endregion

    public PlayerExternalData ()
    {

    }

    public PlayerExternalData (float experience,float killNum,float level,float gold,float diamond)
    {
        this.Experience = experience;
        this.KillNum = killNum;
        this.Level = level;
        this.Gold = gold;
        this.Diamond = diamond;
    }
    //public void Add(Object obj)
    //{

    //}
}
