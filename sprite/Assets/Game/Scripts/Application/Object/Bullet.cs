using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Game.Scripts.FrameWork.Pool;
using UnityEngine;
using System.Collections;
public class Bullet:ReusableObject,IResuable
{
    public int ID { get; private set; }
    public int Level { get; private set; }
    public float BaseSpeed { get; private set; }
    public int BaseAttack { get; private set; }
    public float Speed { get { return BaseSpeed *Level ;} }
    public int Attack{get{return BaseAttack *Level;}}
    public Rect MapRect{get;private set;}
    public float DelayToDestroy=1f;
    protected bool isExpolted=false ;//是否爆炸，当碰到敌人时就让其爆炸，播放完爆炸动画然后在将其回收
    Animator animator;

    protected virtual void Awake()
    {
        animator=GetComponent<Animator>();
    }

    protected  virtual void Update()
    {

    }


    //外部进行初始化
    public void Load(int bulletID,int level,Rect mapRect)
    {
        this.MapRect = mapRect;
        this.ID = ID;
        this.Level = level;
        BulletInfo info = Game.Instance.staticData.GetBulletInfo(bulletID);
        this.BaseAttack = info.baseAttack;
        this.BaseSpeed = info.baseSpeed;
    }

    public void Explode()
    {
        //标记已经爆炸
        isExpolted = true;

        //播放爆炸动画
        animator.SetTrigger("IsExplode");

        //延迟回收
        StartCoroutine("DestroyCoroutine");
    }

    IEnumerator DestroyCoroutine()
    {
        //延迟
        yield return new WaitForSeconds(DelayToDestroy);

        //回收
        Game.Instance.objectPool.Unspwan(this.gameObject);
    }

    public override void OnSpawn()
    {

    }

    public override void OnUnspawn()
    {
        isExpolted = false;
        animator.Play("Play");
        animator.ResetTrigger("IsExplode");
    }
}