using UnityEngine;
using System.Collections;

public class Fan :Tower {

    public int BulletCount = 6;

    protected override void Attack()
    {
        base.Attack();

        for(int i=0;i<BulletCount ;i++)
        {
            //发射方向
            float radians = ((Mathf.PI * 2) / BulletCount) * i;
            Vector3 dir = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 1f);

            //产生子弹
            GameObject go = Game.Instance.objectPool.Spawn("FanBullet");
            FanBullet bullet = go.GetComponent<FanBullet>();
            bullet.transform.position = transform.position;
            bullet.Load(this.useBulletID, this.CurrentLevel, this.MapRect, dir);
        }
    }
}
