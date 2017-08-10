using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public GameObject damageEffectPrefab;
    public int hp = 200;
    public float downSpeed = 2;//下降的速度
    private float downDistance = 0;//下降的距离
    public float speed = 2;//速度
    public string guid = "";//这个是GUID,是每一个敌人的唯一标识
    public float damage = 20;//攻击力
    public int attackRate = 2;//攻击速率,多少秒攻击一次
    private float attackTimer = 0;
    private int hpTotal = 0;//总的血量
    public float attackDistance = 2;
    private float distance;
    private Transform bloodPoint;
    private CharacterController cc;
    private GameObject hpBarGameObject;//血条
    private UISlider hpBarSlider;
    private GameObject hudTextGameObject;
    private HUDText hudText;
    private Animation anim;

    public int targetRoleId = -1;//团战的时候，表示这个敌人要攻击的目标，，在生成敌人的时候，随机产生的攻击目标
    private GameObject targetGameObject;//敌人攻击的游戏物体，由targetRoleID来得到，，

    //上一次的位置和旋转
    private Vector3 lastPosition = Vector3.zero;
    private Vector3 lastEulerAngles = Vector3.zero;


    private bool lastIsIdel = true;
    private bool lastIsWalk = false;
    private bool lastIsAttack = false;
    private bool lastIsTakeDamage = false;
    private bool lastIsDie = false;

    void Awake()
    {
        anim = this.GetComponent<Animation>();
    }
    void Start()
    {
        TranscriptManager._instance.AddEnemy(this.gameObject);//创建该敌人的时候，就将其加入到队列中
        hpTotal = hp;
        bloodPoint = transform.Find("BloodPoint").gameObject.GetComponent<Transform>();
        cc = this.GetComponent<CharacterController>();

        InvokeRepeating("CalcDistance", 0, 0.1f);//Invokes the method methodName in time seconds, then repeatedly every repeatRate

        Transform hpBarPoint = transform.Find("HpBarPoint").GetComponent<Transform>();
        hpBarGameObject = HpBarManager._instance.GetHpBar(hpBarPoint.gameObject);//创建敌人的时候，创建一个血条，参数为更随的目标

        hpBarSlider = hpBarGameObject.transform.Find("Bg").GetComponent<UISlider>();
        hudTextGameObject = HpBarManager._instance.GetHudText(hpBarPoint.gameObject);
        hudText = hudTextGameObject.GetComponent<HUDText>();

        if(GameController .Instance .battleType ==BattleType.Team &&GameController.Instance .isMaster )
        {
            //通过主机的去检测敌人是否移动，，，，然后在同步到其他的客户端
            InvokeRepeating("CheckPositionAndRotation", 0f, 1f / 30);//同步位置
            InvokeRepeating("CheckAnimation", 0f, 1f / 30);//同步动画
        }
    }

    void Update()
    {
        if (hp <= 0)
        {
            //移到地下
            downDistance += downSpeed * Time.deltaTime;
            transform.Translate(-transform.up * downSpeed * Time.deltaTime);//Moves the transform in the direction and distance of translation
            if (downDistance > 4)
            {
                Destroy(this.gameObject);//销毁物体
            }
            return;
        }

        if (GameController.Instance.battleType == BattleType.Person || (GameController.Instance.battleType == BattleType.Team && GameController.Instance.isMaster))
        {
            //个人战斗，或团队战斗并且是主机，才能够自主的控制，，团队战斗不是主机时各种动作需要主机去同步
            if (distance < attackDistance)
            {
                //在攻击范围内

                attackTimer += Time.deltaTime;
                if (attackTimer > attackRate)
                {
                    //进行攻击
                    Transform player=null;
                    if(GameController .Instance .battleType ==BattleType .Person )
                    {
                        //个人战斗那么就是本地的角色
                        player = TranscriptManager._instance.player.transform;
                    }
                    if(GameController .Instance .battleType ==BattleType .Team )
                    {
                        //如果是团队战斗那么，就要根据攻击的目标的Id去选取了,攻击的目标的ID是在生成敌人的时候，自动随机生成的
                        player = GameController.Instance.GetPlayerByRoleID(targetRoleId).transform;
                    }
                    Vector3 targetPos = player.position;
                    targetPos.y = transform.position.y;
                    transform.LookAt(targetPos);//控制攻击时的朝向

                    GetComponent<Animation>().Play("attack01");
                    attackTimer = 0;
                }
                else
                {
                    //this.GetComponent<Animation>().CrossFade("idle");

                    //还没有到攻击的时候
                    if (!this.GetComponent<Animation>().IsPlaying("attack01"))
                    {
                        this.GetComponent<Animation>().CrossFade("idle");//Fades the animation with name animation in over a period of time seconds
                        //     and fades other animations out.
                    }
                }
            }
            else
            {
                this.GetComponent<Animation>().Play("walk");
                Move();
            }
        }

    }


    void Move()
    {
        //按player的方向移动

        Transform player = TranscriptManager._instance.player.transform;
        Vector3 targetPos = player.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        cc.SimpleMove(transform.forward * speed);//Moves the character with speed，向某个方向移动
    }


    void CalcDistance()
    {
        Transform player = TranscriptManager._instance.player.transform;
        distance = Vector3.Distance(player.position, transform.position);
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

        Combo._instance.ComboPlus();//显示连击效果
        string[] proArray = args.Split(',');

        //减去伤害值
        int damage = int.Parse(proArray[0]);

        hp -= damage;
        hpBarSlider.value = (float)hp / hpTotal;//更新血量的显示

        hudText.Add("-" + damage, Color.red, 0.3f);//显示伤害
        //受到攻击的动画

        this.GetComponent<Animation>().Play("takedamage");

        //浮空和后退

        float backDistance = float.Parse(proArray[1]);
        float jumpHeight = float.Parse(proArray[2]);

        iTween.MoveBy(this.gameObject,
            transform.InverseTransformDirection(TranscriptManager._instance.player.transform.forward) * backDistance + Vector3.up * jumpHeight,
            0.3f);//动画(将主角攻击的前方变为物体运动的局部方向在乘上运动的距离，加上跳跃的高度，运行的时间

        //出血特效播放

        GameObject.Instantiate(damageEffectPrefab, bloodPoint.position, Quaternion.identity);//实例化特效
        if (hp <= 0)
        {
            Dead();
        }
    }


    void Attack()
    {
        Transform player = TranscriptManager._instance.player.transform;
        distance = Vector3.Distance(player.position, transform.position);//计算距离
        if (distance < attackDistance)
        {
            player.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);//Calls the method named methodName on every MonoBehaviour in this game object.  Should an error be raised if the target object doesn't implement the method for the message? 

        }
    }


    void Dead()
    {

        TranscriptManager._instance.RemoveEnemy(this.gameObject);//敌人死亡之后从list中移除
        this.GetComponent<CharacterController>().enabled = false;//CharacterController : Collider
        Destroy(hpBarGameObject);//销毁血条
        Destroy(hudTextGameObject);//伤害条
        //死亡动画
        //第一种死亡方式：播放死亡动画
        //第二种死亡方式：使用破碎效果
        int random = Random.Range(0, 10);
        if (random <= 7)
        {
            this.GetComponent<Animation>().Play("die");
        }
        else
        {
            this.GetComponentInChildren<MeshExploder>().Explode();//破碎效果
            this.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;//破碎之后不需要渲染
        }
    }

    void CheckPositionAndRotation()
    {
        //将所有的需要同步的敌人都收集到一个队列中，然后在集中的一起去同步
        Vector3 position = transform.position;
        Vector3 eulerAngles = transform.eulerAngles;
        if(position .x !=lastPosition .x||position .y !=lastPosition .y||position .z!=lastPosition .z ||eulerAngles .x!=lastEulerAngles .x||eulerAngles .y!=lastEulerAngles .y||eulerAngles .z!=lastEulerAngles .z)
        {
            TranscriptManager._instance.AddEnemyToSync(this);
            lastPosition = position;
            lastEulerAngles = eulerAngles;
        }
    }


    void CheckAnimation()
    {
        if(lastIsAttack !=anim.IsPlaying ("attack01")||lastIsDie !=anim.IsPlaying ("die")||lastIsIdel !=anim .IsPlaying("idle")||lastIsTakeDamage !=anim.IsPlaying ("takedamage")||lastIsWalk !=anim.IsPlaying ("walk"))
        {
            TranscriptManager._instance.AddEnemyToSyncAnimation(this);//把自身传递过去 ，敌人太多了，需要统一的去同步
            lastIsAttack = anim.IsPlaying("attack01");//判断是否在播放
            lastIsDie = anim.IsPlaying("die");
            lastIsIdel = anim.IsPlaying("idle");
            lastIsTakeDamage = anim.IsPlaying("takedamage");
            lastIsWalk = anim.IsPlaying("walk");
        }
    }
}
