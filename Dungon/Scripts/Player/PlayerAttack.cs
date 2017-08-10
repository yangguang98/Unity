using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 处理攻击特效
/// </summary>
public class PlayerAttack : MonoBehaviour {

    private Dictionary<string, PlayerEffect> effectDict = new Dictionary<string, PlayerEffect>();
    public PlayerEffect[] effectArray;
    public float distanceAttackForward = 10000;
    public float distanceAttackAround = 2;
    public int[] damageArray = new int[]{20,30,30,30};//存储每个技能的伤害值
    public float hp;//主角的血量

    public Animator anim;
    private GameObject hudTextGameObject;
    private HUDText hudText;
    private Transform damageShowPoint;//伤害显示的地方
    private Animator animator;

    private Player player;
    private BattleController battleController;

    private bool isSyncPlayerAnimation = false;//表示是否需要同步动画

    public enum AttackRange
    {
        Forward,
        Around
    }
    void Start()
    {
        
        player = this.GetComponent<Player>();
        if(GameController .Instance .battleType ==BattleType.Team &&player .roleID ==PhotonEngine .Instance .role .ID )
        {
            battleController = GameController.Instance.GetComponent<BattleController>();
            isSyncPlayerAnimation = true;//本地角色，，当动画状态发生改变后，需要同步到其他的客户端
        }

        battleController = GameController.Instance.GetComponent<BattleController>();
        hp = PlayerInfo._instance.Hp;
        PlayerEffect[] peArray = this.GetComponentsInChildren<PlayerEffect>();
        foreach(PlayerEffect  pe in peArray)
        {
            effectDict.Add(pe.gameObject.name, pe);//得到物体(attack1 attack2 attack3)和物体身上的特效组件
        }

        foreach(PlayerEffect pe in effectArray )
        {
            effectDict.Add(pe.gameObject.name, pe);//恶魔之手,飞鸟
        }

        anim = this.GetComponent<Animator>();

        damageShowPoint = transform.Find("DamageShowPoint").GetComponent<Transform>();
        hudTextGameObject = HpBarManager._instance.GetHudText(damageShowPoint.gameObject);
        hudText = hudTextGameObject.GetComponent<HUDText>();
    }
	void Attack(string args)
    {
        //0 normal skill1 skill2 skill3
        //1 effect name
        //2 sound name
        //3 move forward
        //4 jump height

        string[] proArray = args.Split(',');//通过逗号去分割

        //1显示特效
        
        string effectName = proArray[1];
        ShowPlayerEffect(effectName);

        //2.发出声音
        string soundName = proArray[2];
        SoundManager._instance.Play(soundName);

        //3.向前移动
        float moveForward = float.Parse(proArray[3]);
        if(moveForward >0.1f)
        {
            iTween.MoveBy(this.gameObject, Vector3.forward * moveForward, 0.3f);//相对于哪个物体向哪个方向移动多大的距离，，相对的为局部坐标
        }

        string posType = proArray[0];//得到技能的类型
        
        if(posType =="normal")
        {
            //技能为normal时，对敌人产生的影响

            ArrayList array = GetEnemyInAttackRange(AttackRange.Forward);
            foreach(GameObject go in array)
            {
                go.SendMessage("TakeDamage",damageArray [0]+","+proArray[3]+","+proArray [4]);//向敌人发送消息，对敌人被攻击后的效果做一些处理
            }
        }       
   }

    void PlaySound(string soundName)
    {
        SoundManager._instance.Play(soundName);
    }
    void SkillAttack(string args)
    {
        //0 normal skill1 skill2 skill3
        //1 effect name
        //2 sound name
        //3 move forward
        //4 jump height
        string[] proArray = args.Split(',');
        string posType = proArray[0];
        if(posType=="skill1")
        {
            ArrayList array = GetEnemyInAttackRange(AttackRange.Around);//获取在攻击范围内的敌人
            foreach(GameObject go in array)
            {
                go.SendMessage ("TakeDamage",damageArray[1]+","+proArray[1]+","+proArray [2]);
             
            }
        }
        else if (posType == "skill2")
        {
            ArrayList array = GetEnemyInAttackRange(AttackRange.Around);
            foreach (GameObject go in array)
            {
                go.SendMessage("TakeDamage", damageArray[2] + "," + proArray[1] + "," + proArray[2]);
            }
        }

    }

    void ShowPlayerEffect(string effectName)
    {
        PlayerEffect pe;

        if(effectDict .TryGetValue (effectName,out pe))
        {
            pe.Show();
        }
    }

    void ShowEffectDevilHand()
    {
        //显示恶魔之手特效

        string effectName = "DevilHandMobile";
        PlayerEffect pe;
        effectDict.TryGetValue(effectName, out pe);
        ArrayList array = GetEnemyInAttackRange(AttackRange.Forward);
        foreach(GameObject go in array)
        {
            //从敌人的正下方产生特效

            RaycastHit hit;
            bool collider=Physics.Raycast(go.transform .position +Vector3.up ,Vector3.down,out hit,10f,LayerMask.GetMask ("Ground"));//定义一个射线(起始位置，方向，碰撞层)
            if(collider)
            {
                GameObject.Instantiate(pe.gameObject , hit.point, Quaternion.identity);//实例化特效在hit.point地方?????????这里实例化的为特效，怎么会有特效出现，不是应该实例化物体吗？？？？
            }
            
        }
    }

    void ShowEffectSelfToTarget(string effectName)
    {
        PlayerEffect pe;
        effectDict.TryGetValue (effectName,out pe);//没有找到pe
        ArrayList array = GetEnemyInAttackRange(AttackRange.Around);
        foreach (GameObject go in array)
        {
            //从敌人的正下方产生特效
            GameObject goEffect = GameObject.Instantiate(pe.gameObject);
            goEffect.transform.position = transform.position+Vector3.up;//实例化在主角的上面
            goEffect.GetComponent<EffectSettings>().Target = go;//设置目标位置
        }

    }

    void ShowEffectToTarget(string effectName)
    {
        PlayerEffect pe;
        effectDict.TryGetValue(effectName, out pe);//没有找到pe
        ArrayList array = GetEnemyInAttackRange(AttackRange.Around);
        foreach (GameObject go in array)
        {
            //在敌人的正下方实例化特效

            RaycastHit hit;
            bool collider = Physics.Raycast(go.transform.position + Vector3.up, Vector3.down, out hit, 10f, LayerMask.GetMask("Ground"));//定义一个射线(起始位置，方向，碰撞层)
            if(collider)
            {
                GameObject goEffect = GameObject.Instantiate(pe.gameObject);
                goEffect.transform.position = hit.point ;//实例化在主角的上面
            }
            
        }
    }
    ArrayList GetEnemyInAttackRange(AttackRange attackRange)
    {
        //得到在攻击范围内的敌人

        ArrayList arrayList = new ArrayList();
        if (attackRange == AttackRange.Forward)
        {
            foreach (GameObject go in TranscriptManager._instance.GetEnemyList ())
            {

                Vector3 po = transform.InverseTransformPoint(go.transform.position);//将世界坐标转换为transform内的局部坐标（transform组件在主角上）
                if (po.z > -0.5)
                {
                    //保证敌人在主角的前方

                    float distance = Vector3.Distance(Vector3.zero, po);//获得距离
                    if (distance < distanceAttackForward)
                    {
                       
                        arrayList.Add(go);//可以攻击的敌人添加到一个集合里面
                    }
                }
            }
        }
        else
        {
            foreach (GameObject go in TranscriptManager._instance.GetEnemyList ())
            {
                float distance = Vector3.Distance(transform.position, go.transform.position);//获得世界坐标的距离
                
                if (distance < distanceAttackAround)
                {
                    arrayList.Add(go);//可以攻击的敌人添加到一个集合里面
                }

            }
        }
        return arrayList;
    }

    void TakeDamage(int damage)
    {
        if (this.hp <=0)
            return;
        this.hp -= damage;
        if(hp<0)
        {
            hp = 0;
        }
        if(OnPlayerHpChange !=null)
        {
            OnPlayerHpChange(this.hp);
        }

        //播放受到攻击的动画
        int random = Random.Range(0, 100);
        if (random < damage)
        {
            //以一个概率去播放动画
            anim.SetTrigger("TakeDamage");

            if(isSyncPlayerAnimation )
            {
                //需要同步，，就动画同步，，从本地发出，同步到其他的客户端

                //当动画的状态发生了改变，如果是本地角色，那么就要同步到其他的客户端
                PlayerAnimationModel model = new PlayerAnimationModel() { takeDamage = true };
                battleController.SyncPlayerAnimation(model);
            }
        }
        
        //显示血量的减少
        hudText.Add("-" + damage, Color.red, 0.3f);

        //屏幕上血红特效的显示
        BloodScreen.Instance.Show();

        if(hp<=0)
        {
            anim.SetTrigger("Die");
            if(isSyncPlayerAnimation )
            {
                //需要同步，，就动画同步，，从本地发出，同步到其他的客户端

                //当动画的状态发生了改变，如果是本地角色，那么就要同步到其他的客户端
                PlayerAnimationModel model = new PlayerAnimationModel() { die = true };
                battleController.SyncPlayerAnimation(model);
            }
            GameController.Instance.OnPlayerDie(this.GetComponent <Player>().roleID);
        }
    }

    public event OnPlayerHpChangeEvent OnPlayerHpChange;
}
