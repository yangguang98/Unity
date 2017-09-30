using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIComplete : View
{

    #region 常量

    public Button btnSelect;
    public Button btnClear;

    #endregion

    #region 事件
    #endregion

    #region 字段
    #endregion

    #region 属性

    public override string Name
    {
        get
        {
            return Consts.V_Complete;
        }
    }

    #endregion

    #region 方法
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调

    public override void RegisterEvents()
    {

    }

    public override void HandlerEvent(string eventName, object data)
    {

    }

    public void OnSelectClick()
    {
        Game.Instance.LoadScene(1);
    }

    public void OnClearClick()
    {
        GameModel gm = GetModel<GameModel>();
        gm.ClearProgress();
    }

    #endregion

    #region 帮助方法
    #endregion
    

    
}
