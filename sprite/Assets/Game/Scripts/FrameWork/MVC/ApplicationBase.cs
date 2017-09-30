using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class ApplicationBase<T>:Singleton <T> where T:MonoBehaviour 
{
    protected void RegisterController(string eventName ,Type controllerType)
    {
        //注册控制器  ，，只有一个作用就是提供注册启动的命令
        MVC.RegisterController(eventName, controllerType);
    }

    protected void SendEvent(string eventName,object data=null)
    {
        MVC.SendEvent(eventName,data);
    }
}
