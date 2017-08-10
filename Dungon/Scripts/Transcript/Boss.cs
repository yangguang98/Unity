using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{

    public float viewAngle = 50;
    private Transform player;
    public float attackDistance = 3;
    public float moveSpeed = 2;
    public float[] attackArray;
    public float timeInvterval = 1;//攻击的时间间隔
    private float timer = 0;
    private bool isAttacking = false;
    private GameObject attack01GameObject;
    private GameObject attack02GameObject;
    public float hp = 1000;
    public string guid;

    public GameObject bosssBulletPrefab;//第三个特效
    private Transform attack03Pos;
    private Renderer render;
    private BossController bossController;
    public int targetRoleId = 0;// boss的攻击目标

    //上一次的位置和旋转
    private Vector3 lastPosition = Vector3.zero;
    private Vector3 lastEulerAngles = Vector3.zero;

    private Animation anim;
    private bool lastStand = false;
    private bool lastAttack01 = false;
    private bool lastAttack02 = false;
    private bool lastAttack03 = false;
    private bool lastDie = false;
    private bool lastHit = false;
    private bool lastRun = false;
    private bool lastWalk = false;


    void Start()
    {
        anim = GetComponent<Animation>();
        player = TranscriptManager._instance.player.transform;
        TranscriptManager._instance.AddEnemy(this.gameObject);//将生成的booss加入到敌人的总队列中
        attack01GameObject = transform.Find("attack01").gameObject;
        attack02GameObject = transform.Find("attack02").gameObject;
        attack03Pos = transform.Find("attack03");
        BossHpProgressBar.Instance.Show(hp);//创建了boss以后，让boss的血条显示出来
        render = transform.Find("Object01").GetComponent<Renderer>();
        
        bossController = GetComponent<BossController>();
        bossController.OnSyncBossAnimation += this.OnSyncBossAnimation;

        if ((GameController.Instance.battleType == BattleType.Team) && GameController.Instance.isMaster)
        {
            InvokeRepeating("CheckPositionAndRotation", 0f, 1f / 30);
            InvokeRepeating("CheckAnimation", 0f, 1f / 30);
        }
    }

    void Update()
    {
        render.material.color = Color.Lerp(render.material.color, Color.white, Time.deltaTime);//出血效果的颜色逐渐变化
        if ((GameController.Instance.battleType == BattleType.Team && GameController.Instance.isMaster) || GameController.Instance.battleType == BattleType.Person)
        {
            //个人战斗，团队战斗并且是主机
            if (hp <= 0)
                return;
            if (this.GetComponent<Animation>().IsPlaying("hit"))//如果在播放受伤动画，则也不进行自主攻击
                return;
            if (isAttacking == true)
                return;
            Vector3 playerPos = player.position;//中间变量，并没有去修改player的y轴
            playerPos.y = transform.position.y;//在一个平面上去计算
            float angle = Vector3.Angle(playerPos - transform.position, transform.forward);//得到一半的角度
            if (angle < viewAngle / 2)
            {
                //在角度攻击范围之内
                float distance = Vector3.Distance(player.position, transform.position);
                if (distance < attackDistance)
                {
                    //距离范围之内,攻击
                    if (isAttacking == false)
                    {
                        //攻击结束才开始计时
                        this.GetComponent<Animation>().CrossFade("stand");//没有到攻击时间就播放stand动画
                        timer += Time.deltaTime;
                        if (timer > timeInvterval)
                        {
                            timer = 0;
                            Attack();
                        }
                    }

                }
                else
                {
                    //距离范围之外进行追赶
                    this.GetComponent<Animation>().CrossFade("walk");
                    this.GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);//利用刚体进行移动，移动到某一个位置
                }
            }
            else
            {
                //在攻击视野之外
                //转向
                this.GetComponent<Animation>().CrossFade("walk");
                Quaternion targetRotation = Quaternion.LookRotation(playerPos - transform.position);//Creates a rotation with the specified forward and upwards directions.目标朝向
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1 * Time.deltaTime);//Interpolates between a and b by t and normalizes the result afterwards. The
                //     parameter t is clamped to the range [0, 1].
            }
        }
    }

    private int attackIndex = 0;
    void Attack()
    {
        //有三个动画轮番的播放
        isAttacking = true;
        attackIndex++;
        if (attackIndex == 4) attackIndex = 1;
        this.GetComponent<Animation>().CrossFade("attack0" + attackIndex);//动画结尾要设置isAttacking 的状态 (通过在动画的结尾添加事件的方式)
    }

    void BackToStand()
    {
        isAttacking = false;
    }

    void PlayAttack01Effect()
    {
        attack01GameObject.SetActive(true);
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < attackDistance)
        {
            player.SendMessage("TakeDamage", attackArray[0], SendMessageOptions.DontRequireReceiver);
        }
    }

    void PlayAttack02Effect()
    {
        attack02GameObject.SetActive(true);
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < attackDistance)
        {
            player.SendMessage("TakeDamage", attackArray[1], SendMessageOptions.DontRequireReceiver);
        }
    }

    void PlayeAttack03Effect()
    {
        GameObject go = GameObject.Instantiate(bosssBulletPrefab, attack03Pos.position, attack03Pos.rotation) as GameObject;//Clones the object original and returns the clone
        BossBullet bb = go.GetComponent<BossBullet>();
        bb.Damage = attackArray[2];
    }


    void TakeDamage(string args)
    {
        //受到攻击后，调用该方法
        SoundManager._instance.Play("Hurt");
        //0.受到多少伤害
        //1.后退的距离
        //2.浮空和

        if (hp <= 0)
            return;
        isAttacking = false;
        Combo._instance.ComboPlus();//显示连击效果
        string[] proArray = args.Split(',');

        //减去伤害值
        int damage = int.Parse(proArray[0]);

        hp -= damage;
        BossHpProgressBar.Instance.UpdateShow(hp);//更新血量的显示
        //hpBarSlider.value = (float)hp / hpTotal;//更新血量的显示
        //TODO 
        //hudText.Add("-" + damage, Color.red, 0.3f);//显示伤害
        //受到攻击的动画
        if (Random.Range(0, 10) == 9)
        {
            this.GetComponent<Animation>().Play("hit");//以一定的概率播放，因为在播放的过程中，无法自主的攻击

        }

        //浮空和后退

        float backDistance = float.Parse(proArray[1]);
        float jumpHeight = float.Parse(proArray[2]);

        if (Random.Range(0, 10) > 7)
        {
            iTween.MoveBy(this.gameObject,
            transform.InverseTransformDirection(TranscriptManager._instance.player.transform.forward) * backDistance + Vector3.up * jumpHeight,
            0.3f);//动画(将主角攻击的前方变为物体运动的局部方向在乘上运动的距离，加上跳跃的高度，运行的时间

            //第二个参数给的是世界坐标还是局部坐标？？？？？？？？？？？？？？？？？？？？？
        }


        //出血特效播放
        render.material.color = Color.red;//身体变为红色

        //GameObject.Instantiate(damageEffectPrefab, bloodPoint.position, Quaternion.identity);//实例化特效
        if (hp <= 0)
        {
            Dead();
        }
    }

    void CheckPositionAndRotation()
    {
        //将所有的需要同步的敌人都收集到一个队列中，然后在集中的一起去同步
        Vector3 position = transform.position;
        Vector3 eulerAngles = transform.eulerAngles;
        if (position.x != lastPosition.x || position.y != lastPosition.y || position.z != lastPosition.z || eulerAngles.x != lastEulerAngles.x || eulerAngles.y != lastEulerAngles.y || eulerAngles.z != lastEulerAngles.z)
        {
            TranscriptManager._instance.AddBossToSync(this);
            lastPosition = position;
            lastEulerAngles = eulerAngles;
        }
    }

    void Dead()
    {
        this.GetComponent<Animation>().Play("die");
        BossHpProgressBar.Instance.Hide();
        TranscriptManager._instance.RemoveEnemy(this.gameObject);//移除物体
        GameController.Instance.OnBossDie();
    }


    void CheckAnimation()
    {
        if (lastStand != anim.IsPlaying("stand") || lastAttack01 != anim.IsPlaying("attack01") || lastAttack02 != anim.IsPlaying("attack02") || lastAttack03 != anim.IsPlaying("attack03") || lastDie != anim.IsPlaying("die") || lastHit != anim.IsPlaying("hit") || lastWalk != anim.IsPlaying("walk") || lastRun != anim.IsPlaying("run"))
        { 
            //同步
            bossController.SyncBossAnimation(new BossAnimationModel() { stand = anim.IsPlaying("stand"), attack01 = anim.IsPlaying("attack01"), attack02 = anim.IsPlaying("attack02"), attack03 = anim.IsPlaying("attack03"), die = anim.IsPlaying("die"), hit = anim.IsPlaying("hit"), walk = anim.IsPlaying("walk"), run = anim.IsPlaying("run") });
            lastStand = anim.IsPlaying("stand");
            lastAttack01 = anim.IsPlaying("attack01");
            lastAttack02 = anim.IsPlaying("attack02");
            lastAttack03 = anim.IsPlaying("attack03");
            lastDie = anim.IsPlaying("die");
            lastHit = anim.IsPlaying("hit");
            lastWalk = anim.IsPlaying("walk");
            lastRun = anim.IsPlaying("run");
        }
    }


    public void OnSyncBossAnimation(BossAnimationModel model)
    {
        if(model.stand)
        {
            anim.Play("stand");
        }
        if(model.attack01 )
        {
            anim.Play("attack01");
        }
        if(model.attack02 )
        {
            anim.Play("attack02");
        }
        if(model.attack03 )
        {
            anim.Play("attack03");
        }
        if(model.die )
        {
            anim.Play("die");
        }
        if(model.hit)
        {
            anim.Play("hit");
        }
        if(model.walk)
        {
            anim.Play("walk");
        }
    }
}
