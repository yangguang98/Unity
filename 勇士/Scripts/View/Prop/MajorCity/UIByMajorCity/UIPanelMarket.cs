using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 视图层：主城UI界面——商城
/// </summary>
public class UIPanelMarket : MonoBehaviour
{

    //贴图显示
    public Text txtDiamond;
    public Text txtGold;
    public Text txtBoolBottle;
    public Text txtMagicBottle;
    public Text txtAttackPro;
    public Text txtDefencePro;
    public Text txtDexterityPro;

    //响应按键 
    public Button BtnDiamond;
    public Button BtnGold;
    public Button BtnBoolBottle;
    public Button BtnMagicBottle;
    public Button BtnAttackPro;
    public Button BtnDefencePro;
    public Button BtnDexterityPro;


    //具体的文字说明
    public Text goodsDes;

    void Start()
    {

    }
    void Awake()
    {
        //注册相关按钮
        RigisterTxtAndBtn();
    }

    //相关按钮注册
    private void RigisterTxtAndBtn()
    {
        //文字注册
        if (txtDiamond != null)
        {
            EventTriggerListener.Get(txtDiamond.gameObject).onClick += Display_Diamond;
        }
        if (txtGold != null)
        {
            EventTriggerListener.Get(txtGold.gameObject).onClick += Display_Gold;
        }
        if (txtBoolBottle != null)
        {
            EventTriggerListener.Get(txtBoolBottle.gameObject).onClick += DisplayBoolBottle;
        }
        if (txtMagicBottle != null)
        {
            EventTriggerListener.Get(txtMagicBottle.gameObject).onClick += DisplayMagicBottle;
        }
        if (txtAttackPro != null)
        {
            EventTriggerListener.Get(txtAttackPro.gameObject).onClick += DisplayAttackPro;
        }
        if (txtDefencePro != null)
        {
            EventTriggerListener.Get(txtDefencePro.gameObject).onClick += DisplayDefencePro;
        }
        if (txtDexterityPro != null)
        {
            EventTriggerListener.Get(txtDexterityPro.gameObject).onClick += DisplayDexterityPro;
        }
        //按钮注册

        if (BtnDiamond != null)
        {
            EventTriggerListener.Get(BtnDiamond.gameObject).onClick += PurchaseDiamond;
        }
        if (BtnGold != null)
        {
            EventTriggerListener.Get(BtnGold.gameObject).onClick += PurchaseGold;
        }
        if (BtnBoolBottle != null)
        {
            EventTriggerListener.Get(BtnBoolBottle.gameObject).onClick += PurchaseBloodBottle;
        }
        if (BtnMagicBottle != null)
        {
            EventTriggerListener.Get(BtnMagicBottle.gameObject).onClick += PurchaseMagicBottle;
        }
        if (BtnAttackPro != null)
        {
            EventTriggerListener.Get(BtnAttackPro.gameObject).onClick += PurchaseAttackPro;
        }
        if (BtnDefencePro != null)
        {
            EventTriggerListener.Get(BtnDefencePro.gameObject).onClick += PurchaseDefencePro;
        }
        if (BtnDexterityPro != null)
        {
            EventTriggerListener.Get(BtnDexterityPro.gameObject).onClick += PurchaseDexterityPro;
        }

    }

    #region 商品的显示信息

    //钻石
    private void Display_Diamond(GameObject go)
    {
        if (go == txtDiamond.gameObject)
        {
            goodsDes.text = "充值10个钻石，1颗钻石等于1人民币";
        }
    }

    //金币
    private void Display_Gold(GameObject go)
    {
        if (go == txtGold.gameObject)
        {
            goodsDes.text = "1颗钻石可以购买10金币";
        }
    }

    private void DisplayBoolBottle(GameObject go)
    {
        if (go == txtBoolBottle.gameObject)
        {
            goodsDes.text = "5个血瓶，需要50个金币";
        }
    }

    private void DisplayMagicBottle(GameObject go)
    {
        if (go == txtMagicBottle.gameObject)
        {
            goodsDes.text = "5个魔法瓶，需要100个金币";
        }
    }

    private void DisplayAttackPro(GameObject go)
    {
        if (go == txtAttackPro.gameObject)
        {
            goodsDes.text = "倚天剑，需要50金币";
        }
    }

    private void DisplayDefencePro(GameObject go)
    {
        if (go == txtDefencePro.gameObject)
        {
            goodsDes.text = "超级护盾，需要30金币";
        }
    }

    private void DisplayDexterityPro(GameObject go)
    {
        if (go == txtDexterityPro.gameObject)
        {
            goodsDes.text = "千里金靴，需要20金币";
        }
    }
    #endregion

    #region 商品的点击响应
    //购买钻石
    private void PurchaseDiamond(GameObject go)
    {
        if (go = BtnDiamond.gameObject)
        {
            bool res = false;
            //调用商城的逻辑层脚本
            res = MarketCommand.Instance.AddDiamonds();
            if (res)
            {
                goodsDes.text = "钻石充实成功";
            }
            else
            {
                goodsDes.text = "充值不成功，请联系工作人员";
            }
        }
    }

    //购买金币
    private void PurchaseGold(GameObject go)
    {
        if (go = BtnGold.gameObject)
        {
            bool res = false;
            //调用商城的逻辑层脚本
            res = MarketCommand.Instance.AddGolds();
            if (res)
            {
                goodsDes.text = "购买10枚金币成功！";
            }
            else
            {
                goodsDes.text = "购买不成功，请联系工作人员";
            }
        }
    }

    //购买血瓶
    private void PurchaseBloodBottle(GameObject go)
    {
        if (go = BtnBoolBottle.gameObject)
        {
            bool res = false;
            //调用商城的逻辑层脚本
            res = MarketCommand.Instance.AddBoolBottle();
            if (res)
            {
                goodsDes.text = "购买5个血瓶成功";
            }
            else
            {
                goodsDes.text = "购买不成功，请联系工作人员";
            }
        }
    }

    //购买魔法瓶
    private void PurchaseMagicBottle(GameObject go)
    {
        if (go = BtnMagicBottle.gameObject)
        {
            bool res = false;
            //调用商城的逻辑层脚本
            res = MarketCommand.Instance.AddMagicBottle();
            if (res)
            {
                goodsDes.text = "购买5个魔法瓶成功";
            }
            else
            {
                goodsDes.text = "购买不成功，请联系工作人员";
            }
        }
    }

    //购买攻击力道具
    private void PurchaseAttackPro(GameObject go)
    {
        if (go = BtnAttackPro.gameObject)
        {
            bool res = false;
            //调用商城的逻辑层脚本
            res = MarketCommand.Instance.AddAtk();
            if (res)
            {
                goodsDes.text = "购买攻击力道具成功";
            }
            else
            {
                goodsDes.text = "购买不成功，请联系工作人员";
            }
        }
    }

    //购买防御力道具
    private void PurchaseDefencePro(GameObject go)
    {
        if (go = BtnDefencePro.gameObject)
        {
            bool res = false;
            //调用商城的逻辑层脚本
            res = MarketCommand.Instance.AddDef();
            if (res)
            {
                goodsDes.text = "购买防御力道具成功";
            }
            else
            {
                goodsDes.text = "购买不成功，请联系工作人员";
            }
        }
    }

    //购买敏捷度道具
    private void PurchaseDexterityPro(GameObject go)
    {
        if (go = BtnDexterityPro.gameObject)
        {
            bool res = false;
            //调用商城的逻辑层脚本
            res = MarketCommand.Instance.AddDex();
            if (res)
            {
                goodsDes.text = "购买敏捷度道具成功";
            }
            else
            {
                goodsDes.text = "购买成功，请联系工作人员";
            }
        }
    }

    
    #endregion
}
