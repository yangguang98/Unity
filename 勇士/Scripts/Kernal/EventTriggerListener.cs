using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
/// <summary>
/// 核心层：事件监听器
/// 作用：可以监听任何一个指定的游戏对象
/// </summary>
public class EventTriggerListener : EventTrigger  {

	public delegate void VoidDelegate(GameObject go);
    public event VoidDelegate onClick;
    public event VoidDelegate onDown;
    public event VoidDelegate onEnter;
    public event VoidDelegate onExit;
    public event VoidDelegate onUp;
    public event VoidDelegate onSelect;
    public event VoidDelegate onUpdateSelect;

    //得到监听器组件  监听的游戏对象
    public static EventTriggerListener Get(GameObject go)
    {
        EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
        if(listener==null)
        {
            //若游戏物体没有监听组件，那么就给其加上监听组件
            listener = go.AddComponent<EventTriggerListener>();
        }
        return listener;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if(onClick!=null)
        {
            onClick(gameObject);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null)
        {
            onDown(gameObject);
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null)
        {
            onEnter(gameObject);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null)
        {
            onExit(gameObject);
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null)
        {
            onUp(gameObject);
        }
    }

    //public override void OnUpdateSelected(BaseEventData eventData)
    //{
    //    if (onClick != null)
    //    {
    //        onClick(gameObject);
    //    }
    //}

    public override void OnSelect(BaseEventData eventData)
    {
        if (onClick != null)
        {
            onClick(gameObject);
        }
    }

  

   

}
