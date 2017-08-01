using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
///       控制层：主角攻击控制
///Description: 
///      开发思路：
///      1> 把附近的所有敌人放入“敌人数组”
///         1.1> 得到所有敌人，放入“敌人集合”
///         1.2> 判断“敌人集合”，然后找出最近的敌人
///      
///      2> 主角在一定范围内，开始自动“注视”最近的敌人
///      
///      3> 响应输入攻击信号，对于主角“正面”的敌人给予一定的伤害处理
///       
/// </summary>
public class HeroAttackCommand : Command
{


    private List<GameObject> enemyList;      //敌人集合
    private Transform nearestEnemy;          //最近敌人
    private float maxDistance = 10f;         //敌我最大距离     小于这个距离，并且在或者的敌人放入到敌人集合当中
    private float minAttackDistance = 5f;    //最小（关注）距离   +关注距离应该大于实际的攻击距离，先关注在靠近攻击
    private float realAttackArea = 2f;       //最小攻击距离
    private float rotateSpeed = 1f;        //旋转速度

    //大招攻击范围参数定义
    public float attackDisMagicA = 5;     //大招A攻击范围
    public float attackDisMagicB = 8;     //大招B攻击范围
    public int powerMagicA = 3;           //大招A攻击倍率  攻击的伤害倍数，在普通攻击上乘
    public int powerMagicB = 3;           //大招B攻击倍率


    void Awake()
    {
        //事件注册:键盘输入
        //#if UNITY_STANDALONE_WIN||UNITY_EDITOR
        HeroAttackBykeyCommand.playerControlEvent += ResponseNormalAttack;
        HeroAttackBykeyCommand.playerControlEvent += ResponseMagicTrickA;
        HeroAttackBykeyCommand.playerControlEvent += ResponseMagicTrickB;
        //#endif

        //主角攻击输入（虚拟按键的事件）
        //#if UNITY_ANDROID||UNITY_IPHONE   //预编译指令
        HeroAttackByETCommand.playerControlEvent += ResponseNormalAttack;
        HeroAttackByETCommand.playerControlEvent += ResponseMagicTrickA;
        HeroAttackByETCommand.playerControlEvent += ResponseMagicTrickB;
        HeroAttackByETCommand.playerControlEvent += ResponseMagicTrickC;
        HeroAttackByETCommand.playerControlEvent += ResponseMagicTrickD;
        //#endif
    }

    void Start()
    {
        enemyList = new List<GameObject>();  //初始化敌人集合

        //把附近的敌人放入到敌人数组中，，得到最近的敌人，，会不断的执行
        StartCoroutine("RecordNearbyEnemysToArray");

        //主角在一定范围内，开始自动“注视”最近的敌人
        StartCoroutine("LookAtEnemy");

    }

    //响应普通攻击
    public void ResponseNormalAttack(string controlType)
    {
        if (controlType == "NormalAttack")
        {
            //播放攻击动画
            HeroAnimationCommand.Instance.SetActionState(HeroActionState.NormalAttack);

            //给特定敌人伤害

            //if (UnityHelper.GetInstance().GetSmallTime(0.1f))
            //{
            //当用户狂点的时候，也是0.1秒执行一次，，如何去理解？？？？？？？？？
            AttackEnemyByNormal();
            //}
        }
    }


    //响应大招A
    public void ResponseMagicTrickA(string controlType)
    {
        if (controlType == "MagicTrickA")
        {
            HeroAnimationCommand.Instance.SetActionState(HeroActionState.MagicTrickA);
            StartCoroutine("AttackEnemyByMagicA");
        }
    }

    //响应大招B
    public void ResponseMagicTrickB(string controlType)
    {
        if (controlType == "MagicTrickB")
        {
            HeroAnimationCommand.Instance.SetActionState(HeroActionState.MagicTrickB);
            StartCoroutine("AttackEnemyByMagicB");
        }
    }

    public void ResponseMagicTrickC(string controlType)
    {
        if (controlType == "MagicTrickC")
        {

        }
    }

    public void ResponseMagicTrickD(string controlType)
    {
        if (controlType == "MagicTrickD")
        {

        }
    }

    //把附近敌人放入到“敌人数组”
    IEnumerator RecordNearbyEnemysToArray()
    {
        while (true)
        {
            yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_2);

            //得到所有敌人，放入“敌人集合”
            GetEnemysToArray();
            //判断“敌人集合”，然后找出最近的敌人
            GetNearestEnemy();
        }
    }

    //得到所有敌人(活着的敌人)，放入“敌人集合”
    public void GetEnemysToArray()
    {
        //清空敌人集合
        enemyList.Clear();

        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag(Tag.Enemy);
        foreach (GameObject go in enemyArray)
        {
            //判断敌人是否存活？？？？？？？
            //EnemyCommand enemy=go.GetComponent <EnemyCommand >();
            BaseEnemyProCommand enemy = go.GetComponent<BaseEnemyProCommand>();
            if (enemy != null && enemy.CurrentState != EnemyState.death)
            {
                enemyList.Add(go);
            }
        }
    }

    //判断“敌人集合”，然后找出最近的敌人
    private void GetNearestEnemy()
    {
        if (enemyList != null && (enemyList.Count >= 1))
        {
            foreach (GameObject go in enemyList)
            {
                float distance = Vector3.Distance(transform.position, go.transform.position);
                if (distance < maxDistance)
                {
                    maxDistance = distance;
                    nearestEnemy = go.transform;   //最近敌人
                }
            }//foreach_end
        }
    }//GetNearestEnemy()_end

    //主角在一定范围内，开始自动“注视”最近的敌人
    IEnumerator LookAtEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT5);
            if (nearestEnemy != null && HeroAnimationCommand.Instance.CurrentActionState == HeroActionState.Idle)
            {
                //最近敌人不为空，并且处于idle状态才会去开始注视（避免抽风）
                float distance = Vector3.Distance(this.gameObject.transform.position, nearestEnemy.position);
                if (distance < minAttackDistance)
                {
                    //小于最小关注距离才会去关注
                    //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(nearestEnemy.position.x, 0, nearestEnemy.position.z) - new Vector3(transform.position.x, 0, transform.position.z)), rotateSpeed); //四元数    在y轴方向不需要旋转
                    UnityHelper.GetInstance().FaceToGoal(this.gameObject.transform, nearestEnemy, rotateSpeed);
                }

            }
        }//while_end
    }

    //攻击敌人_普通招数
    private void AttackEnemyByNormal()
    {
        AttackEnemy(enemyList, nearestEnemy, realAttackArea, 1);
        //print(GetType() + "AttackEnemyByNormal()()");
        ////敌人不存在
        //if (enemyList == null || enemyList.Count <= 0)
        //{
        //    nearestEnemy = null;
        //    return;
        //}

        //foreach (GameObject go in enemyList)
        //{
        //    //每两秒才更新一次enemyList，而敌人的生命值是1秒，所有存在敌人已经死了，而列表还没有更新的问题 ，因此加if判断
        //    //if(go&&go.GetComponent <EnemyCommand >().IsAlive)//有些已经死掉的敌人可以执行这个代码吗？？？？即敌人被销毁，还可以获取其身上的脚本？？？？？？
        //    if (go && go.GetComponent<BaseEnemyProCommand>().CurrentState != EnemyState.death)//有些已经死掉的敌人可以执行这个代码吗？？？？即敌人被销毁，还可以获取其身上的脚本？？？？？？
        //    {
        //        print(GetType() + "AttackEnemyByNormal()()/foreach");
        //        float distance = Vector3.Distance(go.transform.position, this.gameObject.transform.position);
        //        Vector3 dir = (go.transform.position - this.gameObject.transform.position).normalized;
        //        float floDirection = Vector3.Dot(dir, this.gameObject.transform.forward);
        //        if (floDirection > 0 && realAttackArea >= distance)
        //        {
        //            //在攻击范围内
        //            go.SendMessage("OnHurt", HeroPropertyCommand.Instance.GetCurrentATKValue(), SendMessageOptions.DontRequireReceiver);
        //        }
        //    }
        //}
    }

    //使用大招A攻击敌人
    //功能：主角周围的敌人，都造成一定伤害
    IEnumerator  AttackEnemyByMagicA()
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_1);
        AttackEnemy(enemyList, nearestEnemy, attackDisMagicA, powerMagicA, false);
        //print(GetType() + "AttackEnemyByNormal()()");
        ////敌人不存在
        //if (enemyList == null || enemyList.Count <= 0)
        //{
        //    nearestEnemy = null;
        //    return;
        //}

        //foreach (GameObject go in enemyList)
        //{
        //    //每两秒才更新一次enemyList，而敌人的生命值是1秒，所有存在敌人已经死了，而列表还没有更新的问题 ，因此加if判断
        //    //if(go&&go.GetComponent <EnemyCommand >().IsAlive)//有些已经死掉的敌人可以执行这个代码吗？？？？即敌人被销毁，还可以获取其身上的脚本？？？？？？
        //    if (go && go.GetComponent<BaseEnemyProCommand>().CurrentState != EnemyState.death)//有些已经死掉的敌人可以执行这个代码吗？？？？即敌人被销毁，还可以获取其身上的脚本？？？？？？
        //    {
        //        print(GetType() + "AttackEnemyByNormal()()/foreach");
        //        float distance = Vector3.Distance(go.transform.position, this.gameObject.transform.position);

        //        //给敌人伤害判断
        //        if (attackDisMagicA >= distance)
        //        {
        //            //在攻击范围内
        //            go.SendMessage("OnHurt", HeroPropertyCommand.Instance.GetCurrentATKValue() * powerMagicA, SendMessageOptions.DontRequireReceiver);
        //        }
        //    }
        //}
    }


    //使用大招B攻击敌人
    //功能：主角正对面方向，都造成一定伤害
    IEnumerator  AttackEnemyByMagicB()
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_1);
        AttackEnemy(enemyList, nearestEnemy,attackDisMagicB, powerMagicB);
        //print(GetType() + "AttackEnemyByNormal()()");
        ////敌人不存在
        //if (enemyList == null || enemyList.Count <= 0)
        //{
        //    nearestEnemy = null;
        //    return;
        //}

        //foreach (GameObject go in enemyList)
        //{
        //    //每两秒才更新一次enemyList，而敌人的生命值是1秒，所有存在敌人已经死了，而列表还没有更新的问题 ，因此加if判断
        //    //if(go&&go.GetComponent <EnemyCommand >().IsAlive)//有些已经死掉的敌人可以执行这个代码吗？？？？即敌人被销毁，还可以获取其身上的脚本？？？？？？
        //    if (go && go.GetComponent<BaseEnemyProCommand>().CurrentState != EnemyState.death)//有些已经死掉的敌人可以执行这个代码吗？？？？即敌人被销毁，还可以获取其身上的脚本？？？？？？
        //    {
        //        print(GetType() + "AttackEnemyByNormal()()/foreach");
        //        float distance = Vector3.Distance(go.transform.position, this.gameObject.transform.position);
        //        Vector3 dir = (go.transform.position - this.gameObject.transform.position).normalized;
        //        float floDirection = Vector3.Dot(dir, this.gameObject.transform.forward);
        //        if (floDirection > 0 && attackDisMagicB >= distance)
        //        {
        //            //在攻击范围内
        //            go.SendMessage("OnHurt", HeroPropertyCommand.Instance.GetCurrentATKValue() * powerMagicB, SendMessageOptions.DontRequireReceiver);
        //        }
        //    }
        //}
    }

    //公共方法，攻击敌人
    
}
