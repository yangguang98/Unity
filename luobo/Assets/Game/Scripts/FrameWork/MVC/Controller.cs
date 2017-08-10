using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//控制器中也可以注册视图 控制层是常驻内存的
public abstract class Controller
{
    //处理系统消息
    public abstract void Execute(object data);


    
    protected T GetModel<T>() where T : Model
    {
        //获取模型
        return MVC.GetModel<T>() as T;
    }


    
    protected T GetView<T>() where T : View
    {
        //获取视图
        return MVC.GetView<T>() as T;
    }


    protected void RegisterModel(Model model)
    {
        //注册模型
        MVC.RegisterModel(model);
    }

    protected void RegisterView(View view)
    {
        //注册视图
        MVC.RegisterView(view);
    }

    protected void RegisterController(string eventName,Type controllerType)
    {
        //注册控制器
        MVC.RegisterController(eventName, controllerType);
    }

    


    
}
