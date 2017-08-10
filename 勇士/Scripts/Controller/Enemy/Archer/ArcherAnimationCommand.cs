using UnityEngine;
using System.Collections;
/// <summary>
/// 控制层：射箭手-动画系统控制
/// 注意：谁的动画就调用谁的事件，便于管理
/// </summary>
public class ArcherAnimationCommand : Command {

    private BaseEnemyProCommand pro;                      //属性控制器
    private Animator animator;
    private HeroPropertyCommand heroPro;                //主角属性控制器
    private bool isSingleTimes = true;                  //单次执行开关


    //这个变量十分特殊，其值等于射箭手手上那把看不见的箭，其坐标属性会随着射箭手的移动而改变，设置这个变量为了在实例化箭的时候获得箭的旋转信息和位置信息，其坐标属性会随着射箭手的移动而改变这点非常重要
    public GameObject goArrow;       
                   
    void OnEnable()
    {
        StartCoroutine("PlayAnimation");

        //开启单次模式
        isSingleTimes = true;
    }

    void OnDisable()
    {
        StopCoroutine("PlayAnimation");

        //让敌人动画恢复idle状态  敌人死亡动画播放完整后，没有别的转台可以跳转，停留在末端
        if (animator)
        {
            animator.SetTrigger("RecoverLife");
        }
    }


    void Start()
    {
        //StartCoroutine("PlayWarriorAnimation");
        pro = this.GetComponent<BaseEnemyProCommand>();
        animator = this.GetComponent<Animator>();

        GameObject go = GameObject.FindGameObjectWithTag(Tags.player);
        heroPro = go.GetComponent<HeroPropertyCommand>();
    }


    //播放战士动画,,,通过当前的状态去调节动画
    IEnumerator PlayAnimation()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT1);
            switch (pro.CurrentState)
            {
                case EnemyState.idle:
                    animator.SetFloat("MoveSpeed", 0);
                    animator.SetBool("Attack", false);
                    break;
                case EnemyState.walking:
                    animator.SetFloat("MoveSpeed", 1);
                    animator.SetBool("Attack", false);
                    break;
                case EnemyState.attack:
                    animator.SetFloat("MoveSpeed", 0);
                    animator.SetBool("Attack", true);
                    break;
                case EnemyState.hurt:
                    animator.SetTrigger("Hurt");
                    yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_1);//让动画播放完整
                    break;
                case EnemyState.death:
                    if (isSingleTimes)
                    {
                        //死亡动画只能执行一次
                        //这个协程协程会循环执行，GlobleParameter.INTERVER_TIME_0DOT1，为等待的事件，当死亡后，过这么多时间又会执行，所以加上单次开关，让其只执行一次就行
                        isSingleTimes = false;
                        animator.SetTrigger("Dead");
                    }
                    break;
                default:
                    break;
            }
        }
    }

    //攻击主角  (动画事件）
    //功能：放箭
    public IEnumerator  AttackHeroByAnimationEvent()
    {
        StartCoroutine(LoadParticalEffect(GlobleParameter.INTERVER_TIME_0DOT1, "Prefabs/Prop/Arrow", true, goArrow.transform.position, goArrow.transform.rotation, transform.parent, null, 10));//倒数第三个参数为transform时，则箭的父对象为射箭手。由于射箭手会随着主角的位置信息而改变自己的位置和旋转信息，当父对象的旋转发生了变化，因此射出去的箭会随着父对象的旋转而旋转。当属第三个参数为transform.parent时，由于整个过程中父对象位置和旋转信息不会有任何改版，则射出去的箭会直线运动
        yield break;
    }

    //战士受伤动画效果
    public IEnumerator AnimationEvent_ArcherHurt()
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT1);
        GameObject WarriorHurtEffect = ResourcesMgr.GetInstance().LoadAsset("ParticleProps/Enemy_HurtedEffect", true);
        WarriorHurtEffect.transform.position = transform.position;

        //定义特效的父子对象
        WarriorHurtEffect.transform.parent = transform;

        Destroy(WarriorHurtEffect, 1f);
    }
}
