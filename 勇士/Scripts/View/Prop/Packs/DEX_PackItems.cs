﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
/// <summary>
/// 视图层：敏捷度
/// </summary>
public class DEX_PackItems : BasePackages, IEndDragHandler, IDragHandler, IBeginDragHandler
{

    //定义目标格子名称
    public string TargetName = "Img_DEX";
    //主角增加攻击力
    public float floAddDEX = 20;

    void Start()
    {
        base.strTargetName = TargetName;
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
        PlayerKernalDataProxy.GetInsance().IncreaseMaxDexterityValue(floAddDEX);
        PlayerKernalDataProxy.GetInsance().UpdateDexterityValue();
    }
}
