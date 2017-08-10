using UnityEngine;
using System.Collections;
/// <summary>
/// 控制层：国王Animation
/// </summary>
public class KingAnimationCommand : Command {

    private BaseEnemyProCommand pro;                      //属性控制器
    private Animator animator;
    private HeroPropertyCommand heroPro;                //主角属性控制器
    private bool isSingleTimes = true;                  //单次执行开关
    //漂字预设

    public GameObject goMoveUpLabelPrefab;
    GameObject goHero;

    private GameObject goUIPlayerInfo;    //玩家UI

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

        goHero = GameObject.FindGameObjectWithTag(Tags.player);
        heroPro = goHero.GetComponent<HeroPropertyCommand>();
        goUIPlayerInfo = GameObject.FindGameObjectWithTag(Tag.UIPlayerInfo);
    }


    //播放战士动画,,,通过当前的状态去调节动画
    IEnumerator PlayAnimation()
    {
        yield return new WaitForEndOfFrame();
        print(GetType() + "/PlayAnimation()");
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
                    //如果离敌人很近，则其会攻击敌人，在攻击敌人的过程中如果受到伤害，那么就要播放受伤动画，因为敌人的AI会不断的调整敌人的currentState，有可能敌人的受伤动画没有播放完整，敌人的currentState又发生了变化，这是敌人会出现抽搐的情况，因此需要加播放的等待时间
                    print(GetType() + "/PlayAnimation()/EnemyState.hurt");
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
    public void AttackHeroByAnimationEvent()
    {
        //通过调用主角的控制层去修改主角的各种属性
        heroPro.DecreaseHealthValue(pro.Atk);//让主角掉血

        //漂字特效
        StartCoroutine(LoadParticalEffectInPool_MoveUpLabel(GlobleParameter.INTERVER_TIME_0DOT1, goMoveUpLabelPrefab, goHero.transform.position, goHero, pro.Atk, goUIPlayerInfo.transform));
    }

    //战士受伤动画效果
    public IEnumerator AnimationEvent_KingHurt()
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT1);
        GameObject WarriorHurtEffect = ResourcesMgr.GetInstance().LoadAsset("ParticleProps/Enemy_HurtedEffect", true);
        WarriorHurtEffect.transform.position = transform.position;

        //定义特效的父子对象
        WarriorHurtEffect.transform.parent = transform;

        Destroy(WarriorHurtEffect, 1f);
    }
}
