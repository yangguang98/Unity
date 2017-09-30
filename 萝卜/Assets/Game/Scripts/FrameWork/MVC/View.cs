using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
//视图也可以处理事件
public abstract class View:MonoBehaviour 
{
    public abstract string Name { get; }//视图列表

    [HideInInspector]
    public List<string> AttentionEvents = new List<string>();//每一个视图关心的事件，在注册视图的时候，就调用RegiterEvents来注册关心得事件

    public virtual void RegisterEvents()
    {
        //注册View关心的事件
    }

    public abstract void HandlerEvent(string eventName, object data);//事件处理函数

    protected T GetModel<T>() where T : Model
    {
        return MVC.GetModel<T>() as T;
    }

    protected void SendEvent(string eventName,object data=null)
    {
        MVC.SendEvent(eventName, data);
    }
}