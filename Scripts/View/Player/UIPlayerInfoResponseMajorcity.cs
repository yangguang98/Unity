using UnityEngine;
using System.Collections;
/// <summary>
/// 视图层：玩家主城信息响应
/// 描述：主城场景中，关于玩家各种面板的显示与隐藏
/// </summary>
public class UIPlayerInfoResponseMajorcity : MonoBehaviour {

    public GameObject goPlayerSkillPanel;
    public GameObject goPlayerMission;
    public GameObject goPlayerPurchase;
    public GameObject goPlayerPackage;

    private GameObject currentDis=null;

    //public void Display
    void Awake()
    {

    }

    //显示角色
    public void DisplayRoles()
    {
        UIPlayerInfoResponse._instance.DisplayPlayerRoles();
    }

    //隐藏角色
    public void HideRoles()
    {
        UIPlayerInfoResponse._instance.HidePlayerRoles();
    }

    //显示技能
    public void DisplaySkillInfo()
    {
        //预处理
        if (goPlayerSkillPanel!=null)
        {
            BeforeOpenWindow(goPlayerSkillPanel);
        }
        
        goPlayerSkillPanel.SetActive(true);
    }

    //隐藏技能
    public void HideSkillInfo()
    {
        //预处理
        if (goPlayerSkillPanel != null)
        {
            BeforeCloseWindow();
        }
        goPlayerSkillPanel.SetActive(false);
    }

    //显示任务
    public void DisplayMission()
    {
        //预处理
        if (goPlayerMission != null)
        {
            BeforeOpenWindow(goPlayerMission);
        }
        goPlayerMission.SetActive(true);
    }

    //隐藏任务
    public void HideMission()
    {
        //预处理
        if (goPlayerMission != null)
        {
            BeforeCloseWindow();
        }
        goPlayerMission.SetActive(false);
    }

    //显示商城
    public void DisplayMarket()
    {
        //预处理
        if (goPlayerPurchase != null)
        {
            BeforeOpenWindow(goPlayerPurchase);
        }
        goPlayerPurchase.SetActive(true);
    }

    //隐藏商城
    public void HideMarket()
    {
        //预处理
        if (goPlayerPurchase != null)
        {
            BeforeCloseWindow();
        }
        goPlayerPurchase.SetActive(false);
    }

    //显示背包
    public void DisplayPackage()
    {
        //预处理
        if (goPlayerPackage != null)
        {
            BeforeOpenWindow(goPlayerPackage);
        }
        goPlayerPackage.SetActive(true);
    }

    //隐藏背包
    public void HidePackage()
    {
        //预处理
        if (goPlayerPackage != null)
        {
            BeforeCloseWindow();
        }
        goPlayerPackage.SetActive(false);
    }

    //打开窗体之前的预处理
    private void BeforeOpenWindow(GameObject goDisplay)
    {
        //禁用ET
        UIPlayerInfoResponse._instance.HideET();
        //窗体的模态化
        this.gameObject.GetComponent<UIMaskMgr>().SetMaskWindow(goDisplay);
    }

    //关闭窗体的预处理
    private void BeforeCloseWindow()
    {
        //开启ET
        UIPlayerInfoResponse._instance.DisplayET();
        //取消窗体的模态化
        this.gameObject.GetComponent<UIMaskMgr>().cancelMaskWindow();
    }
}
