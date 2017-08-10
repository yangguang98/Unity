using UnityEngine;
using System.Collections;
//在LitJson中，不支持float类型的转型，就是将float类型的数据转换为LitJson数据,会出错
//LitJson中，不支持结构体的数据传送，因此要将结构体转化为对象类型
public class Vector3Obj {

    public double x { get; set; }
    public double y { get; set; }
    public double z { get; set; }

    public Vector3Obj ()
    {

    }
    public Vector3Obj (Vector3 temp)
    {
        x = temp.x;//float转化为double不需要强制转换
        y = temp.y;
        z = temp.z;
    }

    public Vector3 ToVector3()
    {
        Vector3 temp = Vector3.zero;
        temp.x = (float)x;//double转换为float需要强制转换
        temp.y = (float)y;
        temp.z = (float)z;
        return temp;
    }
}
