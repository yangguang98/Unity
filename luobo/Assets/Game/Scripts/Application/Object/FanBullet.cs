using UnityEngine;
using System.Collections;

public class FanBullet : Bullet {

    //旋转速度（度/秒）
    public float RotateSpeed = 180f;


    public Vector2 Direction { get; private set; }

    public void Load(int bulletID,int level,Rect mapRect,Vector3 direction)
    {
        Load(bulletID, level, mapRect);
        Direction = direction;
    }

    protected override void Update()
    {
        if (isExpolted)
            return;

        //移动
        transform.Translate (Direction *Speed*Time.deltaTime ,Space.World );

        //旋转
        transform.Rotate(Vector3.forward, RotateSpeed * Time.deltaTime);
    
        //检测(存活/死亡)
        GameObject[] monsterObjects = GameObject.FindGameObjectsWithTag("Monster");
        foreach(GameObject monsterObject in monsterObjects)
        {
            Monster monster=monsterObject .GetComponent<Monster>();

            //忽略已经死亡的怪物
            if (monster.IsDead)
                continue;
            if(Vector3 .Distance (transform .position,monsterObject .transform .position )<Consts.DotClosedDistance )
            {
                //敌人受伤
                monster.Damage(this.Attack);

                //爆炸
                Explode();

                //退出,,不需要再去检测别的敌人了
                break;
            }
        }

        if (!isExpolted && MapRect.Contains(transform.position))
        {
            //没有碰撞到敌人
            Explode();
        }
    }
}
