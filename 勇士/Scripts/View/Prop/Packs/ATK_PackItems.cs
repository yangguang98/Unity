using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
/// <summary>
/// 视图层:背包系统子类
///       “攻击力道具”
/// </summary>
public class ATK_PackItems : BasePackages ,IEndDragHandler ,IDragHandler ,IBeginDragHandler {

    //定义目标格子名称
    public string TargetName = "Img_ATK";
    //主角增加攻击力
    public float floAddATK = 20;
 
    void Start()
    {
        base.strTargetName = TargetName;
        base.BaseStart();
        //父类初始化
        base.RunInstanceByChild();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        base.BaseOnEndDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        base.BaseOnDrag(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        base.BaseOnBeginDrag(eventData);
    }

    protected override void InvokeMethodByEndDrag()
    {
        base.InvokeMethodByEndDrag();
        //主角增加攻击力 最大攻击力值发生了改变，其攻击力也会发生改变
        PlayerKernalDataProxy.GetInsance().IncreaseMaxATKValue(floAddATK);
        PlayerKernalDataProxy.GetInsance().UpdataATKValue();
    }
}
