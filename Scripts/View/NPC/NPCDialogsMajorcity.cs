using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 描述：主城对话脚本
/// </summary>
public class NPCDialogsMajorcity : MonoBehaviour
{

    public GameObject goDialogPanel; //对话面板
    private Image imgBgDialog;  //对话背景贴图

    private CommonTriggerType commonTriggerType = CommonTriggerType.None;


    void Start()
    {
        imgBgDialog = this.transform.parent.Find("BackGround").GetComponent<Image>();

        //注册“触发器，对话系统”（目的是准备对话）
        RigisterTriggerDialog();
        //注册“背景贴图”（目的是发起对话）
        RigisterBgTexture();

        imgBgDialog.gameObject.SetActive(false);//开始不显示

    }

    #region 对话准备

    // 注册“触发器，对话系统”
    private void RigisterTriggerDialog()
    {
        TriggerCommonEvent.commonTriggerEvent += StartDialog;
    }

    //准备对话
    private void StartDialog(CommonTriggerType ctt)
    {
        switch (ctt)
        {
            case CommonTriggerType.None:
                break;
            case CommonTriggerType.NPC1_Dialog:
                ActiveNpc1Dia();
                break;
            case CommonTriggerType.NPC2_Dialog:
                ActiveNpc2Dia();
                break;

            default:
                break;
        }
    }

    private void ActiveNpc1Dia()
    {
        //动态加载贴图
        LoadNpc1Tex();

        //当前状态
        commonTriggerType = CommonTriggerType.NPC1_Dialog;

        //禁用ET
        UIPlayerInfoResponse._instance.HideET();

        //显示UI
        imgBgDialog.gameObject.SetActive(true);

        //显示首句对话对话
        DisplayNextDialog(5);
    }

    private void ActiveNpc2Dia()
    {
        //动态加载贴图
        LoadNpc2Tex();
        //当前状态
        commonTriggerType = CommonTriggerType.NPC2_Dialog;

        //禁用ET
        UIPlayerInfoResponse._instance.HideET();

        //显示UI
        imgBgDialog.gameObject.SetActive(true);

        DisplayNextDialog(6);
    }


    //动态加载贴图
    void LoadNpc1Tex()
    {
        DialogUIMgr._instance.sprNPC_Right[0] = ResourcesMgr.GetInstance().LoadResource<Sprite>("/Texture/BigScales/NPCTrue_1",true);
        DialogUIMgr._instance.sprNPC_Right[1] = ResourcesMgr.GetInstance().LoadResource<Sprite>("/Texture/BigScales/NPCBW_1", true);
    }

    //动态加载贴图
    void LoadNpc2Tex()
    {
        DialogUIMgr._instance.sprNPC_Right[0] = ResourcesMgr.GetInstance().LoadResource<Sprite>("/Texture/BigScales/NPCTrue_2", true);
        DialogUIMgr._instance.sprNPC_Right[1] = ResourcesMgr.GetInstance().LoadResource<Sprite>("/Texture/BigScales/NPCBW_2", true);
    }
    #endregion 

    #region

    //注册“背景贴图”（目的是发起对话）
    private void RigisterBgTexture()
    {
        if (imgBgDialog != null)
        {
            EventTriggerListener.Get(imgBgDialog.gameObject).onClick += DisplayDialogByNpc;
        }
    }

    void DisplayDialogByNpc(GameObject go)
    {
        switch (commonTriggerType)
        {
            case CommonTriggerType.None:
                break;
            case CommonTriggerType.NPC1_Dialog:
                DisplayNextDialog(5);
                break;
            case CommonTriggerType.NPC2_Dialog:
                DisplayNextDialog(6);
                break;
            default:
                break;
        }
    }

    //显示下一句对话
    private void DisplayNextDialog(int secNum)
    {
        bool res = DialogUIMgr._instance.DisplayNextDialog(DialogType.Double, secNum);//获取下段对话

        //对话结束
        if(res)
        {
            //关闭对话面板，启用ET

            goDialogPanel.gameObject.SetActive(false);

            UIPlayerInfoResponse._instance.DisplayET();
        }
    }
    #endregion

}
