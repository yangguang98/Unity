using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class Monster:Role
{
    #region 常量
    public const float CLOSE_DISTANCE = 0.1f;
    #endregion

    #region 事件

    public event Action<Monster> Reached;   //达到终点是触发的事件
    #endregion

    #region 字段
    float moveSpeed;
    public MonsterType m_MonsterType = MonsterType.Monster0;
    Vector3[] m_Path;//路径拐点
    int m_PointIndex = -1;//当前经过的最后一个拐点
    bool m_IsReached = false;//是否到达终点
    #endregion

    #region 属性

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    #endregion

    #region 方法

    public void Load(Vector3[] path)
    {
        m_Path = path;
        MoveNext();//加载路径的时候，首先将Monster放在起点位置
    }

    bool HasNext()
    {
        //是否还有点需要走
        return (m_PointIndex + 1) < (m_Path.Length - 1);
    }

    void MoveTo(Vector3 pos)
    {
        transform.position = pos;
    }
    void MoveNext()
    {
        //第一次调用在Load当中
        if(!HasNext ())
        {
            return;
        }

        if(m_PointIndex ==-1)
        {
            //游戏还没有开始玩，直接移动到起点
            m_PointIndex = 0;
            MoveTo(m_Path[m_PointIndex]);
        }
        else
        {
            m_PointIndex++;
        }
    }

    #endregion

    #region Unity回调

    void Update()
    {
        //到达终点
        if (m_IsReached)
            return;

        Vector3 curretPos = transform.position;//当前位置
        Vector3 des = m_Path[m_PointIndex+1];//目标位置；

        float dis = Vector3.Distance(curretPos, des);

        if (dis < CLOSE_DISTANCE)
        {
            //到达拐点,,判断该拐点是否是终点
            MoveTo(des);//移动到拐点

            if (HasNext())
                MoveNext();
            else
            {
                //到达终点
                m_IsReached = true;

                //触发到达终点事件
                if (Reached != null)
                    Reached(this);

            }
        }
        else
        {
            //移动方向
            Vector3 direction = (des - curretPos).normalized;

            //进行移动（米/帧=米/秒*Time.deltaTime）
            transform.Translate(direction*moveSpeed * Time.deltaTime);//direction的大小为一，只是用来指明方向

        }
    }
    #endregion

    #region 事件回调

    public override void OnSpawn()//可以进行两次Override，
    {
        //基本信息的初始化，，在对象池中产生该物体后通过sendMEssage调用
        base.OnSpawn();
        MonsterInfo info = Game.Instance.staticData.GetMonsterInfo((int)m_MonsterType);
        this.MaxHp = info.hp;
        this.Hp = info.hp;
        this.moveSpeed = info.moveSpeed;
    }

    public override void OnUnspawn()
    {
        //回收到对象池中，通过SendMessage调用，清除该脚本上的数据
        base.OnUnspawn();
        this.m_Path = null;
        this.m_PointIndex = -1;
        this.m_IsReached = false;
        this.Reached = null;
        this.moveSpeed = 0;
    }
    #endregion

    #region 帮助方法
    #endregion
}