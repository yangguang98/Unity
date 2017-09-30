using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Game.Scripts.FrameWork.Pool;
using UnityEngine;
//这个脚本会绑定在一个实际的塔上
public abstract class Tower : ReusableObject, IResuable
{
    //动画组件
    protected Animator animator;

    //攻击目标
    protected Monster target;

    //所在Tile
    Tile tile;

    //上次攻击时间
    float lastAttackTime = 0;

    //等级
    int currentLevel = 0;//当前等级可以为0


    public int ID { get; private set; }//这个ID用来从静态的数据中读取实际的数据

    public int Price
    {
        get
        {
            return basePrice * currentLevel;
        }
    }

    public int basePrice { get; private set; }

    public int maxLevel { get; private set; }

    public float guardRange { get; private set; }

    public float shotRate { get; private set; }

    public int useBulletID { get; private set; }//使用子弹的ID


    public Rect MapRect { get; private set; }

    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }
        set
        {
            currentLevel = Mathf.Clamp(value, 0, maxLevel);

            //根据级别设置大小
            transform.localScale = Vector3.one * (1 + currentLevel * 0.25f);
        }
    }

    public bool isTopLevel
    {
        get { return CurrentLevel >= maxLevel; }
    }

    //给塔加载静态数据
    public void Load(int towerID, Tile tile)
    {
        TowerInfo info = Game.Instance.staticData.GetTowerInfo(towerID);
        this.ID = info.ID;
        this.maxLevel = info.maxLevel;
        this.basePrice = info.basePrice;
        this.guardRange = info.guardRange;
        this.shotRate = info.shotRate;
        this.useBulletID = info.useBulletID;
        this.currentLevel = 1;
        this.tile = tile;
    }

    //攻击
    protected virtual void Attack()
    {
        animator.SetTrigger("IsAttack");
    }

    protected virtual void Awake()
    {

    }

    void Update()//?????????????????????会被子类继承吗
    {
        //搜索目标
        if (target == null)
        {
            //没有目标就去寻找目标
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
            foreach (GameObject monster in monsters)
            {
                Monster m = monster.GetComponent<Monster>();
                float dis = Vector3.Distance(m.transform.position, transform.position);
                if (!m.IsDead && this.guardRange >= dis)
                {
                    target = m;
                    break;
                }
            }
        }
        else//目标存在
        {
            //这一帧还要做一个判定，看那个target是否满足条件
            float dis = Vector3.Distance(target.transform.position, transform.position);
            if (target.IsDead || this.guardRange < dis)
            {
                target = null;
                return;
            }

            //计算攻击时间点
            float attackTime = lastAttackTime + 1f / shotRate;
            if (Time.time >= attackTime)
            {

                //攻击
                Attack();
                
                //记录攻击时间
                lastAttackTime = Time.time;
            }
        }

        //看着目标
        LookAt();//时刻盯着目标
    }

    void LookAt()
    {
        //2D游戏炮塔旋转     多多理解！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        if (target == null)
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.z = 0;
            transform.eulerAngles = eulerAngles;
        }
        else
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
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


    //这个方法在父类中是一个抽象的方法，在子类中可以将其改为虚方法
    public override void OnSpawn()
    {
        animator = GetComponent<Animator>();
        animator.Play("Idle");
    }

    public override void OnUnspawn()
    {
        //回收游戏物体
        animator.ResetTrigger("IsAttack");
        animator = null;
        target = null;
        currentLevel = 0;
        ID = -1;
        basePrice = 0;
        maxLevel = 0;
        guardRange = 0;
        shotRate = 0;
        useBulletID = 0;
    }
}
