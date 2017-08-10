using UnityEngine;
using System.Collections;
using kernel;
/// <summary>
/// 控制层：boss动画控制脚本
/// </summary>
public class BossAnimationCommand : Command {

    private BaseEnemyProCommand pro;                      //属性控制器
    private Animator animator;
    private HeroPropertyCommand heroPro;                //主角属性控制器
    private bool isSingleTimes = true;                  //单次执行开关

    /*音效处理*/
    public AudioClip acNormalAttack;  //普通攻击
    public AudioClip acJumpAttack;    //大招
    public AudioClip acBlare;         //大吼
    public AudioClip acDead;          //死亡
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


    //播放动画,,,通过当前的状态去调节动画
    IEnumerator PlayAnimation()
    {
        yield return new WaitForEndOfFrame();
        //出场音效
        AudioManager.PlayAudioEffectA(acBlare);
        yield return new WaitForSeconds(acBlare.length);
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

    //攻击主角普通攻击
    IEnumerator  AttackHeroNormalByBoss()
    {
        //攻击音效
        AudioManager.PlayAudioEffectA(acNormalAttack);

        //这里不需要等待，当播放一个声音紧接着去播放另外一个声音的时候，才需要去等待
        //yield return new WaitForSeconds(acNormalAttack.length);

        ////通过调用主角的控制层去修改主角的各种属性
        heroPro.DecreaseHealthValue(pro.Atk);//让主角掉血

        //播放例子效果
        StartCoroutine(LoadParticalEffect(GlobleParameter.INTERVER_TIME_0DOT1, "ParticleProps/Bosss", true, transform.position + transform.TransformDirection(new Vector3(0, 0, -10)), transform.rotation, transform, null, 1));
        yield break;
    }


    //攻击主角跳跃攻击
    IEnumerator AttackHeroJumpByBoss()
    {
        //攻击音效
        AudioManager.PlayAudioEffectA(acJumpAttack);
        //yield return new WaitForSeconds(acNormalAttack.length);

        ////通过调用主角的控制层去修改主角的各种属性
        heroPro.DecreaseHealthValue(pro.Atk*2);//让主角掉血

        //播放例子效果
        StartCoroutine(LoadParticalEffect(GlobleParameter.INTERVER_TIME_0DOT1, "ParticleProps/Bosss", true, transform.position + transform.TransformDirection(new Vector3(0, 0, -10)), transform.rotation, transform, null, 1));
        yield break;
    }

    //Boss受伤动画效果
    public IEnumerator AnimationEvent_WarriorHurt()
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT1);
        GameObject WarriorHurtEffect = ResourcesMgr.GetInstance().LoadAsset("ParticleProps/Enemy_HurtedEffect", true);
        WarriorHurtEffect.transform.position = transform.position;

        //定义特效的父子对象
        WarriorHurtEffect.transform.parent = transform;

        Destroy(WarriorHurtEffect, 1f);
    }

    IEnumerator  AnimationEvent_BurceDead()
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT1);

        AudioManager.PlayAudioEffectA(acDead);
        yield return new WaitForSeconds(acDead.length);
    }
}
