using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ExitSceneCommand:Controller 
{
    public override void Execute(object data)
    {
        //离开场景回收所有可回收的对象
        Game.Instance .objectPool .UnSpwanAll();
    }
}
