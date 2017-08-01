using UnityEngine;
using System.Collections;
/// <summary>
/// 公共层：UI遮罩管理器
/// 作用：实现弹出“模态”窗体
/// </summary>
public class UIMaskMgr : MonoBehaviour {

    public GameObject goTopPanel;    //顶层面板
    public GameObject goMaskPanel;   //遮罩面板
    private Camera uiCamera;         //UI摄像机

    private float originalUICameraDepth;  //原始Ui摄像机层深

    void Start()
    {
        uiCamera = this.gameObject.transform.parent.FindChild("UICamera").GetComponent<Camera>();
        if(uiCamera !=null)
        {
            originalUICameraDepth = uiCamera.depth;//得到层深
        }
        else
        {
            Debug.Log(GetType() + "/Start()/uiCamera is Null,please check");
        }
    }

    //设置遮罩窗体  
    public void SetMaskWindow(GameObject go)
    {
        //顶层Ui下移
        goTopPanel.transform.SetAsLastSibling();
        //启用遮罩窗体
        goMaskPanel.SetActive(true);
        //遮罩窗体下移
        goMaskPanel.transform.SetAsLastSibling();
        //显示窗体下移
        go.transform.SetAsLastSibling();
        //增加当前UI摄像机的“层深
        if(uiCamera !=null)
        {
            uiCamera.depth = uiCamera.depth + 20;
        }
    }

    public void cancelMaskWindow()
    {
        //顶层窗体上移
        goTopPanel.transform.SetAsFirstSibling();
        //禁用遮罩船体
        goMaskPanel.SetActive(false);
        //恢复UI摄像机的原来的层深
        uiCamera.depth = originalUICameraDepth;
    }
}
