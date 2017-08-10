using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 测试类：测试模型数据
/// </summary>
public class TestModelLayer : MonoBehaviour
{
    public Text txtHp;
    public Text txtMaxHp;
    public Text txtMp;
    public Text txtMaxMp;
    public Text txtATK;
    public Text txtMaxATK;
    public Text txtDEF;
    public Text txtMaxDEF;
    public Text txtDEX;
    public Text txtMaxDEX;

    //扩展数值
    public Text TxtExp;
    public Text TxtKillNum;
    public Text TxtLevel;
    public Text TxtGold;
    public Text TxtDiamond;

    void Awake()
    {
        //核心数值 事件注册
        PlayerKernalData.PlayerKernalDataEvent += DisplayDEX;
        PlayerKernalData.PlayerKernalDataEvent += DisplayMaxDEX;
        PlayerKernalData.PlayerKernalDataEvent += DisPlayHp ;
        PlayerKernalData.PlayerKernalDataEvent += DisplayMaxHp;
        PlayerKernalData.PlayerKernalDataEvent += DisPlayMagic;
        PlayerKernalData.PlayerKernalDataEvent += DisplayMaxMagic;
        PlayerKernalData.PlayerKernalDataEvent += DisplayATK;
        PlayerKernalData.PlayerKernalDataEvent += DisplayMaxATK;
        PlayerKernalData.PlayerKernalDataEvent += DisplayDEF;
        PlayerKernalData.PlayerKernalDataEvent += DisplayMaxDEF;

        //扩展数值 事件注册
        PlayerExternalData.PlayerExternalDataEvent += DisplayKillNum;
        PlayerExternalData.PlayerExternalDataEvent += DisplayDiamond;
        PlayerExternalData.PlayerExternalDataEvent += DisplayExp;
        PlayerExternalData.PlayerExternalDataEvent += DisplayLevel;
        PlayerExternalData.PlayerExternalDataEvent += DisplayGold;

    }

    void PlayerKernalData_PlayerKernalDataEvent(KeyValuesUpdate e)
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        PlayerKernalDataProxy playerKernalDataObj = new PlayerKernalDataProxy(100, 100, 10, 5, 45, 100, 100, 10, 5, 50, 0, 0, 0);

        PlayerExternalDataProxy playerExternalDataObj = new PlayerExternalDataProxy(0, 0, 0, 0, 0);
        //显示初始值
        //PlayerKernalDataProxy.GetInsance().DisplayeAllOrigianlValues();
    }

    #region 事件用户点击
    public void IncreaseHp()
    {
        PlayerKernalDataProxy.GetInsance().IncreaseHealthValue(30);
    }

    public void DecreaseHp()
    {
        PlayerKernalDataProxy.GetInsance().DecreaseHealthValue(10);
    }

    public void IncreaseMagic()
    {
        PlayerKernalDataProxy.GetInsance().IncreaseMagicValue(40);
    }

    public void DecreaseMagic()
    {
        PlayerKernalDataProxy.GetInsance().DecreaseMagicValue(15);
    }

    public void IncreaseExp()
    {
        PlayerExternalDataProxy.GetInsance().AddExp(30);
    }

    #endregion

    #region 事件注册的方法 

    /* 核心数值*/
    void DisPlayHp(KeyValuesUpdate kv)
    {
        if(kv.Key .Equals ("Health"))
        {
            txtHp.text = kv.Value.ToString();
        }
    }

    void DisplayMaxHp(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("MaxHealth"))
        {
            txtMaxHp.text = kv.Value.ToString();
        }
    }

    void DisplayMaxMagic(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("MaxMagic"))
        {
            txtMaxMp.text = kv.Value.ToString();
        }
    }

    void DisPlayMagic(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("Magic"))
        {
            txtMp.text = kv.Value.ToString();
        }
    }

    void DisplayATK(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("Attack"))
        {
            txtATK.text = kv.Value.ToString();
        }
    }

    void DisplayMaxATK(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("MaxAttack"))
        {
            txtMaxATK.text = kv.Value.ToString();
        }
    }

    void DisplayDEX(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("Dexterity"))
        {
            txtDEX.text = kv.Value.ToString();
        }
    }

    void DisplayMaxDEX(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("MaxDexterity"))
        {
            txtMaxDEX.text = kv.Value.ToString();
        }
    }

    void DisplayDEF(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("Defence"))
        {
            txtDEF.text = kv.Value.ToString();
        }
    }

    void DisplayMaxDEF(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("MaxDefence"))
        {
            txtMaxDEF.text = kv.Value.ToString();
        }
    }


    /*扩展数值*/

    void DisplayExp(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("Experience"))
        {
            TxtExp.text = kv.Value.ToString();
        }
    }

    void DisplayGold(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("Gold"))
        {
            TxtGold.text = kv.Value.ToString();
        }
    }

    void DisplayLevel(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("Level"))
        {
            TxtLevel.text = kv.Value.ToString();
        }
    }

    void DisplayDiamond(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("Diamond"))
        {
            TxtDiamond.text = kv.Value.ToString();
        }
    }

    void DisplayKillNum(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("KillNum"))
        {
            TxtKillNum.text = kv.Value.ToString();
        }
    }
    #endregion


}
