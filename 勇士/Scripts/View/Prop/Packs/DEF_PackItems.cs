using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
/// <summary>
/// 视图层：防御力道具
/// 
/// </summary>
public class DEF_PackItems : BasePackages, IEndDragHandler, IDragHandler, IBeginDragHandler
{

    //定义目标格子名称
    public string TargetName = "Img_DEF";
    //主角增加防御力
    public float floAddDEF = 20;

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
        //主角增加防御力 最大攻击力值发生了改变，其攻击力也会发生改变
        PlayerKernalDataProxy.GetInsance().IncreaseMaxDefenceValue(floAddDEF);
        PlayerKernalDataProxy.GetInsance().UpdataDefenceValue();
    }
}
