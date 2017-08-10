using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TranscriptManager : MonoBehaviour
{

    public static TranscriptManager _instance;
    public GameObject player;//这个是本地的角色，在游戏中可能出现好多相同的角色，这个代表本玩家的角色，游戏的模型都是一样，只能通过数据进行不同的控制

    private List<GameObject> enemyList = new List<GameObject>();//用来存放场景中存在的所有敌人

    private Dictionary<string, GameObject> enemyDict = new Dictionary<string, GameObject>();//用字典存储所有的敌人

    private List<Enemy> enemyToSyncList = new List<Enemy>();//需要同步的敌人的集合，，位置和旋转同步，，由于敌人太多了，需要在一定的事件段内，去统一的同步
    private Boss bossToSync = null;//如果boss需要同步，就将boss传递到这里，然后在去同步

    private List<Enemy> enemyToSyncAnimationLsit = new List<Enemy>();//同步敌人动画

    private EnemyController enemyController;

    void Awake()
    {
        _instance = this;

    }

    void Start()
    {
        if (GameController.Instance.battleType == BattleType.Team)
        {

            enemyController = this.GetComponent<EnemyController>();
            enemyController.OnCreateEnemy += this.OnCreateEnemy;
            enemyController.OnSyncEnemyPositionAndRotation += this.OnSyncEnemyPositionAndRotation;
            enemyController.OnSyncEnemyAnimation += this.OnSyncEnemyAnimation;
        }

        if (GameController.Instance.battleType == BattleType.Team && GameController.Instance.isMaster)
        {
            //只有在主机中才会去发起这个同步的请求，，，团战有一个主机
            InvokeRepeating("SyncEnemyPositionAndRotation", 1f, 1f / 30);//定时去同步队列中需要同步的敌人 位置 旋转
            InvokeRepeating("SyncEnemyAnimation", 1f, 1f / 30);//定时去同步队列中需要同步的敌人  动画
        }
    }



    public void OnCreateEnemy(CreateEnemyModel model)
    {
        foreach (CreateEnemyProperty property in model.list)
        {
            GameObject enemyPrefab = Resources.Load("enemy/" + property.prefabName) as GameObject;
            GameObject go = GameObject.Instantiate(enemyPrefab, property.position.ToVector3(), Quaternion.identity) as GameObject;
            Enemy enemy=go.GetComponent<Enemy>();
            if(enemy!=null)
            {
                enemy.guid = property.guid;
                enemy.targetRoleId = property.targetRoleId;
            }
            else
            {
                Boss boss = go.GetComponent<Boss>();
                boss.guid = property.guid;
                boss.targetRoleId = property.targetRoleId;
            }
        }
    }


    void OnDestroy()
    {
        if (enemyController != null)
        {
            enemyController.OnCreateEnemy -= OnCreateEnemy;
        }
    }


    public void AddEnemy(GameObject enemyGo)
    {
        //向enemyList和enemyDict中添加游戏物体
        enemyList.Add(enemyGo);
        string guid = null;//guid实际为字符串类型的

        //boss和enemy的guid挂在不同的脚本上
        if (enemyGo.GetComponent<Enemy>() != null)
        {
            guid = enemyGo.GetComponent<Enemy>().guid;
        }
        else
        {
            guid = enemyGo.GetComponent<Boss>().guid;
        }
        enemyDict.Add(guid, enemyGo);
    }

    public void RemoveEnemy(GameObject enemyGo)
    {
        //删除游戏物体  enemyList和enemyDict中都要删除游戏物体
        enemyList.Remove(enemyGo);
        string guid = null;//guid实际为字符串类型的

        //boss和enemy的guid挂在不同的脚本上
        if (enemyGo.GetComponent<Enemy>() != null)
        {
            guid = enemyGo.GetComponent<Enemy>().guid;
        }
        else
        {
            guid = enemyGo.GetComponent<Boss>().guid;
        }
        enemyDict.Remove(guid);
    }

    public List<GameObject> GetEnemyList()
    {
        return enemyList;
    }

    public Dictionary<string, GameObject> GetEnemyDict()
    {
        return enemyDict;
    }

    public void AddEnemyToSync(Enemy enemy)
    {
        //需要同步的敌人添加到队列中，，动画同步
        enemyToSyncList.Add(enemy);
    }

    public void AddBossToSync(Boss boss)
    {
        //需要同步的boss添加到BossToSync中，，动画同步
        this.bossToSync = boss;
    }

    public void AddEnemyToSyncAnimation(Enemy enemy)
    {
        enemyToSyncAnimationLsit.Add(enemy);
    }

    void SyncEnemyPositionAndRotation()
    {
        //发起同步请求  位置和旋转  boss位置的同步也放在这里
        if (enemyToSyncList != null && enemyToSyncList.Count > 0)
        {
            //判断enemyToSyncList中是否有需要同步的敌人，，，
            EnemyPositionModel model = new EnemyPositionModel();
            foreach (Enemy temp in enemyToSyncList)
            {
                if(temp!=null)
                {
                    //这里得到的enemy有可能为空，因为检测到同步到，invoke方法发起执行该方法时，中间有时间间隔，，在这个时间间隔内，敌人可能已经被杀死
                    EnemyPositionProperty property = new EnemyPositionProperty()
                    {
                        guid = temp.guid,
                        position = new Vector3Obj(temp.gameObject.transform.position),
                        eulerAngles = new Vector3Obj(temp.gameObject.transform.eulerAngles)
                    };//这种语法形式要注意。。。。。。。。。。。。。
                    model.propertyList.Add(property);
                }
            }
            if (bossToSync != null)
            {
                //boss的同步
                EnemyPositionProperty property = new EnemyPositionProperty()
                {
                    guid = bossToSync.guid,
                    position = new Vector3Obj(bossToSync.gameObject.transform.position),
                    eulerAngles = new Vector3Obj(bossToSync.gameObject.transform.eulerAngles)
                };//这种语法形式要注意。。。。。。。。。。。。。
            }
            bossToSync = null;
            enemyController.SyncEnemyPosition(model); //在enemyTOSyncList中的元素都发起同步的请求
            enemyToSyncList.Clear();//清空队列中元素

        }
    }

    void OnSyncEnemyPositionAndRotation(EnemyPositionModel model)
    {
        //同步敌人的回调，，，，其中可能有boss,但是由guid统一起来了  位置和旋转
        foreach (EnemyPositionProperty temp in model.propertyList)
        {
            GameObject enemyGo;
            bool isHave = enemyDict.TryGetValue(temp.guid, out enemyGo);//查看哪些敌人需要同步
            if (isHave)
            {
                enemyGo.transform.position = temp.position.ToVector3();
                enemyGo.transform.eulerAngles = temp.eulerAngles.ToVector3();
            }
        }
    }

    void SyncEnemyAnimation()
    {
        //动画同步请求
        if (enemyToSyncAnimationLsit != null && enemyToSyncAnimationLsit.Count > 0)
        {
            EnemyAnimationModel model = new EnemyAnimationModel();
            foreach (Enemy temp in enemyToSyncAnimationLsit)
            {
                Animation anim = temp.GetComponent<Animation>();
                EnemyAnimationProperty property = new EnemyAnimationProperty()
                {
                    guid = temp.guid,
                    isAttack = anim.IsPlaying("attack01"),
                    isDie = anim.IsPlaying("die"),
                    isTakeDamage = anim.IsPlaying("takedamage"),
                    isIdel = anim.IsPlaying("idle"),
                    isWalk = anim.IsPlaying("walk")
                };//这种语法结构，，，，，，需要注意下
                model.propertyList.Add(property);
            }

            enemyController.SyncEnemyAnimation(model);//同步动画请求
            enemyToSyncAnimationLsit.Clear();//清空队列
        }
    }

    void OnSyncEnemyAnimation(EnemyAnimationModel model)
    {
        //动画同步回调
        foreach (EnemyAnimationProperty temp in model.propertyList)
        {

            GameObject enemyGo;
            bool isHave = enemyDict.TryGetValue(temp.guid, out enemyGo);//查看哪些敌人需要同步
            if (isHave)
            {
                Animation anim = enemyGo.GetComponent<Animation>();
                //这个控制和player的控制是有区别的，这个是老式的动画控制，，animation
                if (temp.isIdel)
                {
                    //本底角色在播放，那么同步到其他的客户端就也可以播放
                    anim.Play("idle");
                }
                if (temp.isAttack)
                {
                    anim.Play("attack01");
                }
                if (temp.isDie)
                {
                    anim.Play("die");
                }
                if (temp.isTakeDamage)
                {
                    anim.Play("takedamage");
                }
                if (temp.isWalk)
                {
                    anim.Play("walk");
                }
            }
        }
    }
}
