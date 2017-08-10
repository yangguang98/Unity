using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//若一个类是static类型，，那么其所有的方法都要是static 类型的

//事件的注册，有两种情况，一种是事件对应一个控制器，一种是事件对应视图当中的一个方法
public static class MVC
{
    //解决存储MVC
    public static Dictionary <string,Model> Models=new Dictionary <string,Model>();//模型名字--模型
    public static Dictionary<string, View> Views = new Dictionary<string, View>();//视图名字--视图  进入到某个场景的时候就注册View,其实在系统中一种存在的，不是进入到下一个场景就会注销掉

    public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();//事件名字--控制器类型，，，，，控制器当中的事件


    //注册
    public static void RegisterModel(Model model)
    {
        Models[model.Name] = model;//这种语法结构！！！！！！！！！！！！！！！！！！！！！！！！！！！！
    }

    public static void RegisterView(View view)
    {
        if(Views.ContainsKey (view.Name))
        {
            //防止重复注册
            Views.Remove(view.Name);
        }

        //注册视图的时候，调用该视图的RegisterEvents方法，来注册该视图所关心的方法
        view.RegisterEvents();//调用注册AttentoinEvent,,若这里的view为继承自View那么气调用的方法就为view中重写的RegisterEvents

        Views[view.Name] = view;
    }

    public static void RegisterController(string eventName,Type controllerType)
    {
        CommandMap[eventName] = controllerType;
    }


    //获取
    public static Model GetModel<T>() where T:Model
    {
        //获取模型
        foreach (Model m in Models.Values)//这里的Models是字典类型，因此其语法结构有所不同
        {
            if(m is T)
            {
                return m;
            }
        }
        return null;
    }

    public static View GetView<T>() where T : View
    {
        //获取视图
        foreach(View v in Views .Values )
        {
            if(v is T)
            {
                return v;
            }
        }
        return null;
    }

    //发送事件
    public static void SendEvent(string eventName, object data = null)
    {
        //这里SendEvent可能触发两种不同的情况，，一个是Controller中的Execut方法
        //一个是视图当中的  HandleEvent

        //控制器响应事件
        if(CommandMap .ContainsKey (eventName ))
        {
            Type t = CommandMap[eventName];
            Controller c = Activator.CreateInstance(t) as Controller;

            //控制器执行
            c.Execute(data);
        }

        //视图响应事件
        foreach (View v in Views.Values )
        {
            if(v.AttentionEvents .Contains (eventName))
            {
                //视图响应事件
                v.HandlerEvent(eventName, data);
            }
        }
    }
}
