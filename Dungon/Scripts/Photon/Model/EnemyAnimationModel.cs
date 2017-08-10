using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAnimationModel  {

   public  List<EnemyAnimationProperty> propertyList = new List<EnemyAnimationProperty>();
}

public class EnemyAnimationProperty
{
    public string guid;
    public bool isIdel;
    public bool isWalk;
    public bool isAttack;
    public bool isDie;
    public bool isTakeDamage;
}
