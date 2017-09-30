using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class Bottle:Tower
{
    Transform shotPoint;

    protected override void Awake()
    {
        base.Awake();
        shotPoint = transform.Find("ShotPoint").transform;
    }
    protected override void Attack()
    {
        //动画
        base.Attack();
        GameObject go = Game.Instance.objectPool.Spawn("BallBullet");
        BallBullet bullet = go.GetComponent<BallBullet>();
        bullet.transform.position = shotPoint.position;
        bullet.Load(this.useBulletID, this.CurrentLevel, this.MapRect, this.target);
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnspawn()
    {
        base.OnUnspawn();
    }
}
