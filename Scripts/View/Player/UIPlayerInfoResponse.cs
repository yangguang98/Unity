using UnityEngine;
using System.Collections;
/// <summary>
/// 视图层：响应玩家点击处理
/// </summary>
public class UIPlayerInfoResponse : MonoBehaviour
{
    public static UIPlayerInfoResponse _instance;
    public GameObject goPlayerDetailInfoPanel;                //玩家详细信息面板
    public GameObject goET;                                   //ET摇杆对象
    public GameObject goHeroInfo;   //英雄信息

    //定义攻击虚拟按键
    public GameObject goNormalAttack;
    public GameObject goMagicA;
    public GameObject goMagicB;
    public GameObject goMagicC;
    public GameObject goMagicD;
    public GameObject goAddHp;

    void Start()
    {
        //DisplayET();
        
    }
    void Awake()
    {
        _instance = this;
    }
    //现实与隐藏玩家详细信息面板
    //public void DisplayOrHidePlayerDetailInfoPanel()
    //{
    //    goPlayerDetailInfoPanel.SetActive(!goPlayerDetailInfoPanel.activeSelf);
    //}

    //显示玩家角色
    public void DisplayPlayerRoles()
    {
        if (goPlayerDetailInfoPanel!=null)
        {
            BeforeOpenWindow(goPlayerDetailInfoPanel);
        }
        goPlayerDetailInfoPanel.SetActive(true);
    }

    //隐藏玩家角色
    public void HidePlayerRoles()
    {
        if (goPlayerDetailInfoPanel != null)
        {
            BeforeCloseWindow();
        }
        goPlayerDetailInfoPanel.SetActive(false);
    }
    
    public void ExitGame()
    {
        //Application.Quit();
        PlayerUIResCommand.Instance.ExitGame();
    }
    

//#if UNITY_ANDROID||UNITY_IPHONE
    #region 响应玩家虚拟按键

    public void ResponseNormalATK()
    {
        HeroAttackByETCommand.Instance.ResponseATKByNormal();
    }

    public void ResponseMagicA()
    {
        HeroAttackByETCommand.Instance.ResponseATKByA();
    }

    public void ResponseMagicB()
    {
        HeroAttackByETCommand.Instance.ResponseATKByB();
    }

    public void ResponseMagicC()
    {
        HeroAttackByETCommand.Instance.ResponseATKByC();
    }

    public void ResponseMagicD()
    {
        HeroAttackByETCommand.Instance.ResponseATKByD();
    }

    
    #endregion

    //显示ET
    public void DisplayET()
    {
        goET.SetActive(true);
    }

    //隐藏ET
    public void HideET()
    {
        goET.SetActive(false);
    }

    //显示所有的按键
    public void DisplayAllATKKey()
    {
        goNormalAttack.SetActive(true);
        goMagicA.SetActive(true);
        goMagicB.SetActive(true);
        goMagicC.SetActive(true);
        goMagicD.SetActive(true);
        goAddHp.SetActive(true);
    }


    //显示主要的按键
    public void DisplayMainATKKey()
    {
        goNormalAttack.SetActive(true);
        goMagicA.SetActive(false);
        goMagicB.SetActive(false);
        goMagicC.SetActive(false);
        goMagicD.SetActive(false);
        goAddHp.SetActive(false);
    }
    //隐藏所有的按键
    public void HideAllATKKey()
    {
        goNormalAttack.SetActive(false);
        goMagicA.SetActive(false);
        goMagicB.SetActive(false);
        goMagicC.SetActive(false);
        goMagicD.SetActive(false);
        goAddHp.SetActive(false);
    }

    //显示英雄Ui信息
    public void  DisplayHeroInfo()
    {
        goHeroInfo.SetActive(true);
    }

    //隐藏英雄UI信息
    public void HideHeroInfo()
    {
        goHeroInfo.SetActive(false);
    }


    //打开窗体之前的预处理
    private void BeforeOpenWindow(GameObject goDisplay)
    {
        //禁用ET
        HideET();
        //窗体的模态化
        this.gameObject.GetComponent<UIMaskMgr>().SetMaskWindow(goDisplay);
    }

    //关闭窗体的预处理
    private void BeforeCloseWindow()
    {
        //开启ET
        DisplayET();
        //取消窗体的模态化
        this.gameObject.GetComponent<UIMaskMgr>().cancelMaskWindow();
    }


//#endif
}
