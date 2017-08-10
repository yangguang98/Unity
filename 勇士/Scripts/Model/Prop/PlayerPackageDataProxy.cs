using UnityEngine;
using System.Collections;
/// <summary>
/// 模型层：玩家背包数据代理类
/// 
/// 作用：封装核心背包数据，向外提供各种方法
/// </summary>
public class PlayerPackageDataProxy : PlayerPackageData
{

    private static PlayerPackageDataProxy _Instance = null;  //本类实例

    public PlayerPackageDataProxy(int bloodBottleNum, int magicBottleNum, int defNum, int dexNum, int atkNum):base(bloodBottleNum ,magicBottleNum ,defNum ,dexNum ,atkNum)
    { 
        if(_Instance ==null)
        {
            _Instance = this;
        }
        else
        {
            Debug.LogError(GetType() + "/PlayerPackageDataProxy()/ 不允许构造函数的重复实例化");
        }
    }

    //得到本类实例
    public static PlayerPackageDataProxy GetInstance()
    {
        if(_Instance !=null)
        {
            return _Instance ;
        }
        else
        {
            return null;
        }
    }

    #region/*血瓶*/

    //增加血瓶数量
    public void InCreaseBloodBottleNum(int num)
    {
        Debug.Log(GetType() + "/InCreaseBloodBottleNum()");
        base.BooldBottleNum += Mathf.Abs(num);
    }

    //减少血瓶数量
    public void DecreaseBloodBottleNum(int num)
    {
        if((base.BooldBottleNum -Mathf.Abs (num))>=0)
        {
            base.BooldBottleNum -= Mathf.Abs(num);
        }
    }

    //显示当前血瓶数量
    public int DisplayBloodBottleNum( )
    {
        return base.BooldBottleNum;
    }
    #endregion

    #region/*魔法瓶数量*/

    //增加魔法数量
    public void InCreaseMagicBottleNum(int num)
    {
        base.MagicBottleNum += Mathf.Abs(num);
    }

    //减少魔法数量
    public void DecreaseMagicBottleNum(int num)
    {
        if ((base.MagicBottleNum - Mathf.Abs(num)) >= 0)
        {
            base.MagicBottleNum -= Mathf.Abs(num);
        }
    }

    //显示当前魔法数量
    public int DisplayMagicBottleNum()
    {
        return base.MagicBottleNum;
    }
#endregion

    #region/*攻击力道具数量*/

    //增加攻击力道具数量
    public void InCreaseATKNum(int num)
    {
        base.PropATKNum += Mathf.Abs(num);
    }

    //减少攻击力道具数量
    public void DecreaseATKNum(int num)
    {
        if ((base.PropATKNum - Mathf.Abs(num)) >= 0)
        {
            base.PropATKNum -= Mathf.Abs(num);
        }
    }

    //显示当前攻击力道具数量
    public int DisplayATKNum()
    {
        return base.PropATKNum;
    }
    #endregion

    #region/*防御力道具数量*/

    //增加防御力道具数量
    public void InCreaseDEFNum(int num)
    {
        base.PropDEFNum += Mathf.Abs(num);
    }

    //减少防御力道具数量
    public void DecreaseDEFNum(int num)
    {
        if ((base.PropDEFNum - Mathf.Abs(num)) >= 0)
        {
            base.PropDEFNum -= Mathf.Abs(num);
        }
    }

    //显示当前防御力道具数量
    public int DisplayDEFNum()
    {
        return base.PropDEFNum;
    }
    #endregion

    #region/*敏捷度道具数量*/

    //增加敏捷度道具数量
    public void InCreaseDEXNum(int num)
    {
        base.PropDEXNum += Mathf.Abs(num);
    }

    //减少敏捷度道具数量
    public void DecreaseDEXNum(int num)
    {
        if ((base.PropDEXNum - Mathf.Abs(num)) >= 0)
        {
            base.PropDEXNum -= Mathf.Abs(num);
        }
    }

    //显示当前敏捷度道具数量
    public int DisplayDEXNum()
    {
        return base.PropDEXNum;
    }
    #endregion
}
