using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 视图层:新手引导模块--"触发对话引导"
/// </summary>
public class TriggerDialogs : MonoBehaviour, IGuidTrigger
{
    public enum DialogState
    {
        None,
        Step1_doublePersonDialog,   //双人对话
        Step2_AliceET,              //介绍ET
        Step3_AliceSpeakVirtualKey, //介绍虚拟按键
        Step4_AliceEnd              //祝福
    }
    public static TriggerDialogs Instance;

    public GameObject goBackground;   // 背景
    private bool isExitNextDialog = false;  //是否存在下一条对话记录
    private Image bgDialogs;                //背景对话贴图
    private DialogState dialogState = DialogState.None;  //当前对话状态

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Log.Write(GetType() + "/Start");

        //当前状态
        dialogState = DialogState.Step1_doublePersonDialog;

        //背景贴图
        bgDialogs = transform.parent.Find("BackGround").GetComponent<Image>();

        //注册背景贴图
        RegisterDialogs();

        DialogUIMgr._instance.DisplayNextDialog(DialogType.Double, 1);
    }

    ////注册背景贴图  点击事件
    public void RegisterDialogs()
    {
        if (bgDialogs != null)
        {
            EventTriggerListener.Get(bgDialogs.gameObject).onClick += DisplayNextDialogRecord;
            
        }
    }

    ////取消注册背景贴图
    public   void UnRegisterDialogs()
    {
        if (bgDialogs != null)
        {
            EventTriggerListener.Get(bgDialogs.gameObject).onClick -= DisplayNextDialogRecord;
        }
    }

    //显示下一条记录
    private void DisplayNextDialogRecord(GameObject go)
    {
        Log.Write(GetType() + "/###########DisplayNextDialogRecord");
        if (go == bgDialogs.gameObject)//???????????????????
        {
            isExitNextDialog = true;  //点击一次那么这个值就设置为true
        }
    }

    //检查状态，点击了这个才会返回True
    public bool CheckCondition()
    {
        Log.Write(GetType() + "/CheckCondition");
        if (isExitNextDialog)
        {
            return true;
        }
        return false;
    }

    public bool RunOperation()
    {
        Log.Write(GetType() + "!!!!!!!/RunOperation");
        bool result = false;                   //本方法是否运行结束标志位  该方法运行结束后，此IGuidTrigger会从GuidMgr中移除   if (iTrigger.RunOperation())  //RunOperation运行结束后，就会将该
        bool currentDialogIsEnd = false;       //当前对话是否结束标志位
        isExitNextDialog = false;              // 用户点击一次，这个方法就运行一次


        //业务逻辑判断
        switch (dialogState)
        {
            case DialogState.None:
                break;
            case DialogState.Step1_doublePersonDialog:
                currentDialogIsEnd = DialogUIMgr._instance.DisplayNextDialog(DialogType.Double, 1);
                break;
            case DialogState.Step2_AliceET:
                Log.Write(GetType() + "/RunOperation()/##### Alice开始介绍ET");
                currentDialogIsEnd = DialogUIMgr._instance.DisplayNextDialog(DialogType.Single, 2);
                break;
            case DialogState.Step3_AliceSpeakVirtualKey:
                currentDialogIsEnd = DialogUIMgr._instance.DisplayNextDialog(DialogType.Single, 3);
                break;
            case DialogState.Step4_AliceEnd:
                currentDialogIsEnd = DialogUIMgr._instance.DisplayNextDialog(DialogType.Single, 4);
                break;
            default:
                break;
        }

        //当前对话是否结束
        if (currentDialogIsEnd)
        {
            switch (dialogState)
            {
                case DialogState.None:
                    break;
                case DialogState.Step1_doublePersonDialog:
                    //无
                    break;

                case DialogState.Step2_AliceET:  //介绍ET完毕，发生后台处理

                    //显示“引导ET贴图”,控制权暂时移交到TriggerET.cs
                    triggerOperET.Instance.DisplayET();

                    //暂停会话
                    UnRegisterDialogs();

                    //要做相应的处理
                    break;
                case DialogState.Step3_AliceSpeakVirtualKey://介绍虚拟按键完毕，

                    //显示“引导虚拟按键贴图”,控制权暂时移交到TriggerVirtualKey.cs
                    TriggerOperVirtualKey.Instance.DisplayGuideVirtualKey();

                    //暂停会话
                    UnRegisterDialogs();
                    break;
                case DialogState.Step4_AliceEnd://全部介绍结束

                    //显示ET
                    UIPlayerInfoResponse._instance.DisplayET();


                    //显示所有的虚拟按键
                    UIPlayerInfoResponse._instance.DisplayAllATKKey();

                    //信息英雄信息
                    UIPlayerInfoResponse._instance.DisplayHeroInfo();

                    //允许生成敌人
                    GameObject.Find("GameMgr/ScenesController").GetComponent<LevelOneScenesCommand>().enabled = true;

                    GameObject.Find("GameMgr/ScenesController").GetComponent<LevelOneScenesCommand >().enabled = true;

                    //隐藏本对话界面
                    goBackground.SetActive(false);

                    result = true;//所有的代码运行结束
                    break;
                default:
                    break;
            }
            //进入下一个状态
            EnterNextState();
        }
        return result;
    }

    private void EnterNextState()
    {
        switch (dialogState)
        {
            case DialogState.None:
                break;
            case DialogState.Step1_doublePersonDialog:
                dialogState = DialogState.Step2_AliceET;
                break;
            case DialogState.Step2_AliceET:
                dialogState = DialogState.Step3_AliceSpeakVirtualKey;
                break;
            case DialogState.Step3_AliceSpeakVirtualKey:
                dialogState = DialogState.Step4_AliceEnd;
                break;
            case DialogState.Step4_AliceEnd:
                dialogState = DialogState.None;
                break;
            default:
                break;
        }
    }
}
