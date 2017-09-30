﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISystem : View
{

    #region 常量

    public Button btnResume;
    public Button btnRestart;
    public Button btnSelect;

    #endregion

    #region 事件
    #endregion

    #region 字段
    #endregion

    #region 属性

    public override string Name
    {
        get { return Consts.V_System; }
    }

    #endregion

    #region 方法

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调

    public override void HandlerEvent(string eventName, object data)
    {

    }

    public void onResumeClick()
    {

    }

    public void OnRestartClick()
    {

    }

    public void OnSelectClick()
    {

    }

    #endregion

    #region 帮助方法
    #endregion
    

    
}
