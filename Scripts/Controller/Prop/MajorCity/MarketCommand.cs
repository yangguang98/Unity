using UnityEngine;
using System.Collections;

/// <summary>
///  控制层：主城UI界面——商城
/// </summary>
public class MarketCommand : MonoBehaviour {

    public static MarketCommand Instance;

    void Awake()
    {
        Instance = this;
    }

    //购买钻石
    public bool AddDiamonds()
    {
        //说明：对接qpp_store的收费接口，收费成功就给返回值

        //本地角色数据的改变
        PlayerExternalDataProxy.GetInsance().AddDiamond(10);
        return true;
    }

    //购买金币
    public bool AddGolds()
    {
        bool res;

        //购买10个金币，需要扣除账户中的1个钻石
        bool resFlag = PlayerExternalDataProxy.GetInsance().DecreaseDiamond(1);
        if (resFlag)
        {
            PlayerExternalDataProxy.GetInsance().AddGold(10);
            res = true;
        }
        else
        {
            res = false;
        }

        return res;
    }

    //购买血瓶
    public bool AddBoolBottle()
    {
        bool res;

        //购买5个血瓶，需要扣除账户中的x个金币
        bool resFlag = PlayerExternalDataProxy.GetInsance().DecreaseGold (50);
        if (resFlag)
        {
            //编写专门的模型层
            PlayerPackageDataProxy.GetInstance().InCreaseBloodBottleNum(5);
            print("增加血瓶数量");
            res = true;
        }
        else
        {
            res = false;
        }

        return res;
    }

    //购买魔法瓶
    public bool AddMagicBottle()
    {
        bool res;

        //购买5个魔法瓶，需要扣除账户中的x个金币
        bool resFlag = PlayerExternalDataProxy.GetInsance().DecreaseGold(100);
        if (resFlag)
        {
            //编写专门的模型层
            PlayerPackageDataProxy.GetInstance().InCreaseMagicBottleNum(5);
            res = true;
        }
        else
        {
            res = false;
        }

        return res;
    }

    //敏捷度道具
    public bool AddDex()
    {
        bool res;

        //购买5个血瓶，需要扣除账户中的x个金币
        bool resFlag = PlayerExternalDataProxy.GetInsance().DecreaseGold(20);
        if (resFlag)
        {
            //编写专门的模型层
            PlayerPackageDataProxy.GetInstance().InCreaseDEXNum(1);
            res = true;
        }
        else
        {
            res = false;
        }

        return res;
    }

    //攻击力道具
    public bool AddAtk()
    {
        bool res;

        //购买攻击力道具，需要扣除账户中的x个金币
        bool resFlag = PlayerExternalDataProxy.GetInsance().DecreaseGold(50);
        if (resFlag)
        {
            //编写专门的模型层
            PlayerPackageDataProxy.GetInstance().InCreaseATKNum(1);
            res = true;
        }
        else
        {
            res = false;
        }

        return res;
    }

    //防御力道具
    public bool AddDef()
    {
        bool res;

        //购买5个血瓶，需要扣除账户中的x个金币
        bool resFlag = PlayerExternalDataProxy.GetInsance().DecreaseGold(30);
        if (resFlag)
        {
            //编写专门的模型层
            PlayerPackageDataProxy.GetInstance().InCreaseDEFNum(1);
            res = true;
        }
        else
        {
            res = false;
        }

        return res;
    }
}
