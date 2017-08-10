using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//该游戏物体可以重用，则表明其是可以回收再利用的
namespace Assets.Game.Scripts.FrameWork.Pool
{
    public abstract class ReusableObject:MonoBehaviour ,IResuable 
    {
        public abstract void OnSpawn();//当从池子中拿出该物体后，就调用该方法对物体进行初始化
        public abstract void OnUnspawn();//当将该物体放回到赤字后，就调用该方法对物体身上的数据进行清除操作
    }
}
