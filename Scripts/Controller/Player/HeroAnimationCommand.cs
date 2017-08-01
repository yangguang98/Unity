using UnityEngine;
using System.Collections;
using kernel;
/// <summary>
/// 主角动画控制
/// </summary>
public class HeroAnimationCommand : Command
{

    public static HeroAnimationCommand Instance;
    private HeroActionState currentActionState = HeroActionState.None; //默认动画状态

    /*定义动画剪辑*/

    public AnimationClip idleAnim;
    public AnimationClip runingAnim;
    //普通攻击1
    public AnimationClip normalAttack1Anim;
    //普通攻击2
    public AnimationClip normalAttack2Anim;
    //普通攻击3
    public AnimationClip normalAttack3Anim;
    public AnimationClip magicTrickAAnim;
    public AnimationClip magicTrickBAnim;

    //脚步声
    public AudioClip clip;

    //动画句柄
    public Animation animationHandle;

    //定义动画单词开关
    private bool isSinglePlay = true;

    //动画连招
    private NormalATKComboState currentATKCombo = NormalATKComboState.NormalATK1;

    public GameObject goHeroNormalParticalEffect1;  //左右劈砍
    public GameObject goHeroNormalParticalEffect2;  //中间刺


    //主角动画剪辑w
    public AudioClip acBeiJi_DaoJian_3;
    public AudioClip acBeiJi_DaoJian_2;
    public AudioClip acBeiJi_DaoJian_1;
    public AudioClip acSwordHero_MagicA;
    public AudioClip acSwordHero_MagicB;

    void Awake()
    {
        Instance = this;
        animationHandle = this.GetComponent<Animation>();
    }

    void Start()
    {
        currentActionState = HeroActionState.Idle;

        //启动协程
        StartCoroutine("ControlHeroAnimationState");

        //设置动画播放速度
        animationHandle[normalAttack1Anim.name].speed = 2f;
        animationHandle[normalAttack2Anim.name].speed = 2f;
        animationHandle[normalAttack3Anim.name].speed = 2f;

        //主角出现特效
        HeroDisplayParticalEffect();
    }

    //设置当前动画状态
    public void SetActionState(HeroActionState ActionState)
    {
        this.currentActionState = ActionState;
    }

    /*属性：当前（主角)动作状态*/
    public HeroActionState CurrentActionState
    {
        get
        {
            return currentActionState;
        }
    }

    //主角动画控制
    private IEnumerator ControlHeroAnimationState()
    {

        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            switch (currentActionState)
            {
                case HeroActionState.None:

                    break;
                case HeroActionState.Idle:
                    animationHandle.CrossFade(idleAnim.name);
                    break;
                case HeroActionState.Runing:
                    //这句话调用的次数较多，则要让其等待，让脚步声播放完整
                    animationHandle.CrossFade(runingAnim.name);
                    AudioManager.PlayAudioEffectA(clip);
                    yield return new WaitForSeconds(clip.length);
                    break;
                case HeroActionState.NormalAttack:

                    /* 攻击连招处理(自动状态转换)*/
                    switch (currentATKCombo)
                    {
                        case NormalATKComboState.NormalATK1:
                            currentATKCombo = NormalATKComboState.NormalATK2;
                            animationHandle.CrossFade(normalAttack1Anim.name);
                            AudioManager.PlayAudioEffectB(acBeiJi_DaoJian_3);
                            yield return new WaitForSeconds(normalAttack1Anim.length / 2);//让动画播放完整
                            currentActionState = HeroActionState.Idle;

                            break;
                        case NormalATKComboState.NormalATK2:
                            currentATKCombo = NormalATKComboState.NormalATK3;
                            animationHandle.CrossFade(normalAttack2Anim.name);
                            AudioManager.PlayAudioEffectB(acBeiJi_DaoJian_2);
                            yield return new WaitForSeconds(normalAttack2Anim.length / 2);//让动画播放完整
                            currentActionState = HeroActionState.Idle;

                            break;
                        case NormalATKComboState.NormalATK3:
                            currentATKCombo = NormalATKComboState.NormalATK1;
                            animationHandle.CrossFade(normalAttack3Anim.name);
                            AudioManager.PlayAudioEffectB(acBeiJi_DaoJian_1);
                            yield return new WaitForSeconds(normalAttack3Anim.length / 2);//让动画播放完整
                            currentActionState = HeroActionState.Idle;

                            break;
                        default:
                            break;

                    }//Switch_end
                    break;

                case HeroActionState.MagicTrickA:
                    print(GetType() + "/播放动画，MagicTrickA");
                    animationHandle.CrossFade(magicTrickAAnim.name);
                    AudioManager.PlayAudioEffectB(acSwordHero_MagicA);
                    yield return new WaitForSeconds(magicTrickAAnim.length);//让动画播放完整
                    currentActionState = HeroActionState.Idle;
                    
                    break;
                case HeroActionState.MagicTrickB:
                    print(GetType() + "/播放动画，MagicTrickB");
                    animationHandle.CrossFade(magicTrickBAnim.name);
                    AudioManager.PlayAudioEffectB(acSwordHero_MagicB);
                    yield return new WaitForSeconds(magicTrickBAnim.length);//让动画播放完整
                    currentActionState = HeroActionState.Idle;
                    break;
                default:
                    break;

            }//switch_end

        }//while_end
    }

    //主角大招A特效
    public IEnumerator AnimationEvent_HeroMagicA()
    {
        StartCoroutine(LoadParticalEffect(GlobleParameter.INTERVER_TIME_0DOT1, "ParticleProps/Hero_MagicA(bruceSkill)", true, transform.position,transform.rotation, transform, null, 1f));
        yield break;   //yield break 相当于yield return null

        //yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT1);
        //GameObject goMagicA = ResourcesMgr.GetInstance().LoadAsset("ParticleProps/Hero_MagicA(bruceSkill)", true);

        //goMagicA.transform.position = transform.position;
    }

    //主角大招B特效
    public IEnumerator AnimationEvent_HeroMagicB()
    {
        StartCoroutine(LoadParticalEffect(GlobleParameter.INTERVER_TIME_0DOT1, "ParticleProps/Hero_MagicB(groundBrokeRed)", true, transform.position + transform.TransformDirection(new Vector3(0, 1, 3)),transform.rotation, transform, null, 1f));
        yield break;   //yield break 相当于yield return null


        //yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT1);
        //GameObject goMagicB = ResourcesMgr.GetInstance().LoadAsset("ParticleProps/Hero_MagicB(groundBrokeRed)", true);
        ////特效在主角前方5米的位置,,,!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //goMagicB.transform.position = transform.position + transform.TransformDirection(new Vector3(0, 1, 3));
    }

    //定义普通剑法粒子特效(左右劈砍)
    public IEnumerator AnimationEvent_HeroNormalATK_A()
    {
        /*****传统方式****/
        //StartCoroutine(LoadParticalEffect(GlobleParameter.INTERVER_TIME_0DOT1, "ParticleProps/Hero_NormalATK1", true,transform.position ,transform ,null,1f));
        //yield break;   //yield break 相当于yield return null

        //使用对象缓冲池

        goHeroNormalParticalEffect1.transform.position = transform.position;

        //在对象缓冲池中得到一个预设激活体
        PoolManager .PoolsArray ["ParticalSys"].BirthGameObject (goHeroNormalParticalEffect1,goHeroNormalParticalEffect1.transform .position ,Quaternion .identity );

        yield break;   //yield break 相当于yield return null
    }


    //定义普通剑法粒子特效（中间刺）
    public IEnumerator AnimationEvent_HeroNormalATK_B()
    {
        //StartCoroutine(LoadParticalEffect(GlobleParameter.INTERVER_TIME_0DOT1, "ParticleProps/Hero_NormalATK2", true, transform.position,transform, null, 1f));
        //yield break;   //yield break 相当于yield return null

        //使用对象缓冲池

        goHeroNormalParticalEffect2.transform.position = transform.position;

        //在对象缓冲池中得到一个预设激活体
        PoolManager.PoolsArray["ParticalSys"].BirthGameObject(goHeroNormalParticalEffect2, goHeroNormalParticalEffect2.transform.position, Quaternion.identity);

        yield break;   //yield break 相当于yield return null
    }

    //主角出现特效
    private  void HeroDisplayParticalEffect()
    {
        GameObject heroDisplayEffect = ResourcesMgr.GetInstance().LoadAsset("ParticleProps/HeroDisplay", true);
        heroDisplayEffect.transform.position = transform.position;
        heroDisplayEffect.transform.parent = transform;

    }
}
