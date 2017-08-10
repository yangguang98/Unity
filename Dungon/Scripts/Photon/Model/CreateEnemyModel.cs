using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//用这个类来传递 敌人创建的时候的一些数据

public class CreateEnemyModel {

    public List<CreateEnemyProperty> list = new List<CreateEnemyProperty>();
}



public class CreateEnemyProperty
{
    //存储创建一个敌人所需的属性
    public string guid;//标识一个敌人的guid
    public string prefabName;//根据prefab的名字找到prefab进行生成
    public Vector3Obj position;//表示生成的位置
    public int targetRoleId;//攻击敌人的ID
}
