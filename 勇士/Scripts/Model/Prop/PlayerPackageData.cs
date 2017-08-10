using UnityEngine;
using System.Collections;
using System;
/// <summary>
/// 模型层：玩家背包数据
/// 
/// </summary>
[Serializable]
public class PlayerPackageData  {

	//定义事件
    public static event PlayerKernalDataDelegate playerPackageDataEvent;   //玩家背包数据事件
    private int iBooldBottleNum;  //血瓶数量
    private int iMagicBottleNum;  //魔法瓶数量
    private int iPropATKNum;      //攻击力道具数量
    private int iPropDEFNum;      //防御力道具数量
    private int iPropDEXNum;      //敏捷度道具数量

    /* 属性定义*/

    public int BooldBottleNum
    {
        get { return iBooldBottleNum; }
        set 
        { 
            iBooldBottleNum = value;
            
            if(playerPackageDataEvent!=null)
            {
                KeyValuesUpdate kv = new KeyValuesUpdate("IBooldBottleNum", iBooldBottleNum);
                playerPackageDataEvent(kv);
                Debug.Log(GetType() + "/IBooldBottleNum");
            }
        }
    }

    public int MagicBottleNum
    {
        get { return iMagicBottleNum; }
        set 
        { 
            iMagicBottleNum = value;
            if (playerPackageDataEvent != null)
            {
                KeyValuesUpdate kv = new KeyValuesUpdate("IMagicBottleNum", MagicBottleNum);
                playerPackageDataEvent(kv);
            }
        }
    }

    public int PropATKNum
    {
        get { return iPropATKNum; }
        set 
        { 
            iPropATKNum = value;
            if (playerPackageDataEvent != null)
            {
                KeyValuesUpdate kv = new KeyValuesUpdate("IPropATKNum", PropATKNum);
                playerPackageDataEvent(kv);
            }
        }
    }

    public int PropDEFNum
    {
        get { return iPropDEFNum; }
        set 
        { 
            iPropDEFNum = value;
            if (playerPackageDataEvent != null)
            {
                KeyValuesUpdate kv = new KeyValuesUpdate("IPropDEFNum", PropDEFNum);
                playerPackageDataEvent(kv);
            }
        }
    }

    public int PropDEXNum
    {
        get { return iPropDEXNum; }
        set 
        { 
            iPropDEXNum = value;
            if (playerPackageDataEvent != null)
            {
                KeyValuesUpdate kv = new KeyValuesUpdate("IPropDEXNum", PropDEXNum);
                playerPackageDataEvent(kv);
            }
        }
    }

    //定义私有的构造函数
    private PlayerPackageData() { }

    //公共构造函数
    public PlayerPackageData(int bloodBottleNum,int magicBottleNum,int DEFNum,int DEXNum,int ATKNum)
    {
        this.BooldBottleNum = bloodBottleNum;
        this.MagicBottleNum = magicBottleNum;
        this.PropATKNum = ATKNum;
        this.PropDEFNum = DEFNum;
        this.PropDEXNum = DEXNum;
    }
    //public void Add(Object obj)
    //{

    //}
}
