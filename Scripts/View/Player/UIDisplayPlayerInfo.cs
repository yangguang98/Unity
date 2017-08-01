using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 视图层:显示玩家信息
/// 
/// </summary>
public class UIDisplayPlayerInfo : MonoBehaviour {

	//屏幕上的玩家信息
    public Text txtLevel;
    public Text txtHp;
    public Text txtMaxHp;
    public Text txtMp;
    public Text txtMaxMp;
    public Text txtExp;
    public Text txtGold;
    public Text txtDiamond;

    public Text txtPlayerName;
    public Text txtPlayerNameByDetailPanel;   //详细面板上的任务名称
    public Slider SliHp;
    public Slider SliMp;


    //详细信息
    public Text txtLevel1;
    public Text txtHp1;
    public Text txtMaxHp1;
    public Text txtMp1;
    public Text txtMaxMp1;
    public Text txtDEF;
    public Text txtMaxDEF;
    public Text txtDEX;
    public Text txtMaxDEX;
    public Text txtATK;
    public Text txtMaxATK;
    public Text txtDiamond1;
    public Text txtGold1;
    public Text txtKillNum;
    public Text txtExp1;

     

    void Awake()
    {
        //核心数值 事件注册
        PlayerKernalData.PlayerKernalDataEvent += DisplayDEX;
        PlayerKernalData.PlayerKernalDataEvent += DisplayMaxDEX;
        PlayerKernalData.PlayerKernalDataEvent += DisPlayHp;
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

    void Start()
    {
        
        if (!string.IsNullOrEmpty(GlobalParameterMgr.playerName))
        {
            txtPlayerName.text = GlobalParameterMgr.playerName;
            txtPlayerNameByDetailPanel.text = GlobalParameterMgr.playerName;
        }
    }


    #region 事件注册

    /* 核心数值*/
    void DisPlayHp(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("Health"))
        {
            txtHp.text = kv.Value.ToString();
            txtHp1.text = kv.Value.ToString();
            SliHp.value = (float)kv.Value;
        }
    }

    void DisplayMaxHp(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("MaxHealth"))
        {
            txtMaxHp.text = kv.Value.ToString();
            txtMaxHp1.text = kv.Value.ToString();
            //滑动条处理
            SliHp.maxValue = (float)kv.Value;
            SliHp.minValue = 0;
        }
    }

    void DisplayMaxMagic(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("MaxMagic"))
        {
            txtMaxMp.text = kv.Value.ToString();
            txtMaxMp1.text = kv.Value.ToString();
            //滑动条处理
            SliMp.maxValue = (float)kv.Value;
            SliHp.minValue = 0;
        }
    }

    void DisPlayMagic(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("Magic"))
        {
            txtMp.text = kv.Value.ToString();
            txtMp1.text = kv.Value.ToString();
            //滑动条处理
            SliMp.value = (float)kv.Value;
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
            txtExp.text = kv.Value.ToString();
            txtExp1.text = kv.Value.ToString();
        }
    }

    void DisplayGold(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("Gold"))
        {
            txtGold.text = kv.Value.ToString();
            txtGold1.text = kv.Value.ToString();
        }
    }

    //等级
    void DisplayLevel(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("Level"))
        {
            txtLevel.text = kv.Value.ToString();
            txtLevel1.text = kv.Value.ToString();
        }
    }

    void DisplayDiamond(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("Diamond"))
        {
            txtDiamond.text = kv.Value.ToString();
            txtDiamond1.text = kv.Value.ToString();
        }
    }

    void DisplayKillNum(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("KillNum"))
        {
            txtKillNum.text = kv.Value.ToString();
        }
    }

    #endregion

}
