using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPositionModel {

    //一串敌人的信息
    public List<EnemyPositionProperty> propertyList = new List<EnemyPositionProperty>();
}

public class EnemyPositionProperty
{
    //每一个敌人的信息，，位置信息和旋转信息
    public string guid;
    public Vector3Obj position;
    public Vector3Obj eulerAngles;
}
