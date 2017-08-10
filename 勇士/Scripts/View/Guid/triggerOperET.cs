using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 视图层:新手引导模块--"触发虚拟摇杆"（ET）
/// </summary>
public class triggerOperET : MonoBehaviour,IGuidTrigger {

    public static triggerOperET Instance;
    public GameObject goBackground;   // 背景游戏对象（对话界面）
    private bool isExitNextDialog = false;  //是否存在下一条对话记录
    private Image bgGuideET;                //引导ET贴图  在这个游戏对象上注册了点击事件

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //引导ET贴图
        bgGuideET = transform.parent.Find("ImgET").GetComponent<Image>();

        //注册引导ET贴图
        RegisterGuidET();
    }

    private void RegisterGuidET()
    {
        if (bgGuideET != null)
        {
            EventTriggerListener.Get(bgGuideET.gameObject).onClick += GuidETOp;
        }
    }

    private void UnRegisterGuidET()
    {
        EventTriggerListener.Get(bgGuideET.gameObject).onClick -= GuidETOp;
    }

    //引导ET操作
    private void GuidETOp(GameObject go)
    {
        if(go==bgGuideET.gameObject)
        {
            isExitNextDialog = true; 
        }
    }
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
        isExitNextDialog = false;
        Log.Write(GetType() + "/RunOperation");

        //影藏对话界面
        goBackground.SetActive(false);

        //影藏“引导ET贴图”
        bgGuideET.gameObject.SetActive(false);

        //激活ET
        UIPlayerInfoResponse._instance.DisplayET();

        //隐藏攻击按键
        UIPlayerInfoResponse._instance.HideAllATKKey();

        //恢复对话系统 继续会话
        StartCoroutine("ResumeDialogs");

        return true;
    }


    //恢复对话系统 继续会话
    IEnumerator ResumeDialogs()
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_3);

        //隐藏ET
        UIPlayerInfoResponse._instance.HideET();

        //注册会话系统  允许会话
        TriggerDialogs.Instance.RegisterDialogs();

        //运行对话系统，直接显示下一条对话  ,不同点击直接去显示
        TriggerDialogs.Instance.RunOperation();

        //显示对话界面
        goBackground.SetActive(true);
    }

    //显示引导ET贴图
    public void DisplayET()
    {
        bgGuideET.gameObject.SetActive(true);
    }
}
