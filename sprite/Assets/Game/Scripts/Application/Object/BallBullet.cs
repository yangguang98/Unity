using UnityEngine;
using System.Collections;

public class BallBullet : Bullet
{

    //目标
    public Monster Target { get; private set; }

    //移动方向////移动方向就是其朝向
    public Vector3 Direction { get; private set; }


    public void Load(int bulletId, int level, Rect mapRect, Monster monster)
    {
        Load(bulletId, level, mapRect);

        Target = monster;
        Direction = (Target.transform.position - transform.position).normalized;
    }

    protected override void Update()
    {
        if (isExpolted)
            return;

        //检测目标
        if (Target != null)
        {
            //目标存在
            if (!Target.IsDead)
            {
                Direction = (Target.transform.position - transform.position).normalized;
            }

            //朝向
            LookAt();

            //移动
            transform.Translate(Direction * Speed * Time.deltaTime, Space.World);


            if(Vector3 .Distance (transform .position ,Target .transform .position )<=Consts.DotClosedDistance)
            {
                //敌人受伤
                Target.Damage(this.Attack);

                //爆炸
                Explode();
            }
        }
        else //目标不存在
        {
            //移动
            transform.Translate(Direction * Speed * Time.deltaTime, Space.World);

            //边界检测
            if (!isExpolted && !MapRect.Contains(transform.position))
                Explode();
        }
    }

    private void LookAt()
    {
        Vector3 dir = (Target.transform.position - transform.position).normalized;
        float dy = dir.y;
        float dx = dir.x;

        //计算夹角
        float angles = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;//[-180,180]幅度值

        //让炮塔旋转
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.z = angles - 90f;
        transform.eulerAngles = eulerAngles;
    }
}
