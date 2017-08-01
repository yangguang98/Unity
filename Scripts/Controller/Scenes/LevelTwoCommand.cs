using UnityEngine;
using System.Collections;
using kernel;
/// <summary>
/// 控制层：第二关卡
/// 副本：战斗场景
/// </summary>
public class LevelTwoCommand : Command
{

    //背景音乐与音效

    public AudioClip backgroundMusic;  //背景音乐
    public AudioClip backgroundMusic_Fighting;   //战斗的背景音效
    public AudioClip acPlayerLevelUp; //主角升级音效

    public string[] tagNameByHide;   //隐藏的游戏物体

    /*敌人预设*/
    public GameObject goArcher;
    public GameObject goMagic;
    public GameObject goKing;
    public GameObject goWarrior;

    public GameObject goBoss;


    /*敌人的生成地点*/
    //区域A
    public Transform[] traSpawnEnemyPos_AreaA_Archer;
    public Transform[] traSpwanEnemyPos_AreaA_Magic;

    //区域B
    public Transform[] traSpawnEnemyPos_AreaB_Archer;
    public Transform[] traSpawnEnemyPos_AreaB_Warrior;
    public Transform[] traSpawnEnemyPos_AreaB_King;

    //区域C
    public Transform[] traSpawnEnemyPos_AreaC_King;

    //区域D (boss核心战斗区域)
    public Transform[] spawnBossTransform;
    public Transform[] traSpawnEnemyPos_AreaD_Archer;
    public Transform[] traSpawnEnemyPos_AreaD_King;
    public Transform[] traSpawnEnemyPos_AreaD_Warrior;
    public Transform[] traSpawnEnemyPos_AreaD_Magic;

    /*敌人的单次出生控制*/
    public bool isSingleSpwan_AreaA = true;
    public bool isSingleSpwan_AreaB = true;
    public bool isSingleSpwan_AreaC = true;
    public bool isSingleSpwan_AreaD = true;
    public bool isSingleSpwan_AreaE = true;
    public bool isSingleSpawnBoss = true;

    public GameObject goParticalWall;//粒子墙

    void Awake()
    {
        TriggerCommonEvent.commonTriggerEvent += SpawnEnemy;
        
    }
    void Start()
    {
        AudioManager.SetAudioBackgroundVolumns(0.5f);
        AudioManager.SetAudioEffectVolumns(0.7f);
        AudioManager.PlayBackground(backgroundMusic);
        //AudioManager.PlayBackground(backgroundMusic_Fighting);
        PlayerExternalData.PlayerExternalDataEvent += PlayerLevelUp;

        //StartCoroutine("HideArea");
        goParticalWall.SetActive(false);
    }

    //主角升级
    void PlayerLevelUp(KeyValuesUpdate e)
    {
         base.LevelUp(e, acPlayerLevelUp);
    }

    //产生敌人
    void SpawnEnemy(CommonTriggerType ctt)
    {
        switch (ctt)
        {
            case CommonTriggerType.Enemy1_Dialog:
                //第一区域产生敌人
                if(isSingleSpwan_AreaA )
                {
                    isSingleSpwan_AreaA = false;
                    SpawnEnemyArea_A();
                }
               
                break;
            case CommonTriggerType.Enemy2_Dialog:
                //第二区域产生敌人
                if(isSingleSpwan_AreaB )
                {
                    isSingleSpwan_AreaB = false;
                    SpawnEnemyArea_B();
                }
                
                break;
            case CommonTriggerType.Enemy3_Dialog:
                //第三区域产生敌人
                if(isSingleSpwan_AreaC )
                {
                    isSingleSpwan_AreaC = false;
                    SpawnEnemyArea_C();
                }
                
                break;
            case CommonTriggerType.Enemy4_Dialog:
                //显示粒子墙
                if (isSingleSpwan_AreaD)
                {
                    isSingleSpwan_AreaD= false;
                    DisplayParticalWall();
                }

                break;
            case CommonTriggerType.Enemy5_Dialog:
                //boos核心打斗
                if (isSingleSpwan_AreaE)
                {
                    isSingleSpwan_AreaE = false;
                    //产生核心boss战斗
                    StartCoroutine("SpwanEnemy_BossArea");
                }

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 场景优化管理：默认隐藏不活动的区域
    /// </summary>
    /// <returns></returns>
    private IEnumerator HideArea()
    {
        yield return new WaitForEndOfFrame();
        foreach (string temp in tagNameByHide)
        {
            //得到游戏对象
            GameObject[] hideGo = GameObject.FindGameObjectsWithTag(temp);
            foreach (GameObject go in hideGo)
            {
                go.SetActive(false);
            }
        }
    }

    #region 产生敌人

    //产生第一区域敌人
    void SpawnEnemyArea_A()
    {
        if (goArcher && goMagic)
        {
            print(GetType() + "/SpawnEnemyArea_A()");
            //产生1个射箭手
            StartCoroutine(SpawnEnemy(1, goArcher, traSpawnEnemyPos_AreaA_Archer));

            //产生2个魔法小怪
            StartCoroutine(SpawnEnemy(2, goMagic, traSpwanEnemyPos_AreaA_Magic));
        }
    }

    //第二区域产生敌人
    void SpawnEnemyArea_B()
    {
        if (goArcher && goWarrior && goKing)
        {
            print(GetType() + "/SpawnEnemyArea_A()");
            //产生1个射箭手
            StartCoroutine(SpawnEnemy(2, goArcher, traSpawnEnemyPos_AreaB_Archer));

            //产生2个战士怪物
            StartCoroutine(SpawnEnemy(2, goWarrior, traSpawnEnemyPos_AreaB_Warrior));

            //产生1个国王
            StartCoroutine(SpawnEnemy(1, goKing, traSpawnEnemyPos_AreaB_King));
        }
    }

    //第三区域产生敌人
    void SpawnEnemyArea_C()
    {
        if (goKing)
        {
            print(GetType() + "/SpawnEnemyArea_A()");

            //产生2个魔法小怪
            StartCoroutine(SpawnEnemy(2, goKing, traSpawnEnemyPos_AreaC_King));
        }
    }

    #endregion

    //显示例子墙，更新背景音乐
    void DisplayParticalWall()
    {
        //开启粒子墙，阻止玩家
        goParticalWall.SetActive(true);
        AudioManager.SetAudioBackgroundVolumns(0.6f);
        AudioManager.SetAudioEffectVolumns(0.8f);
        AudioManager.PlayBackground(backgroundMusic_Fighting);
    }

    //核心战斗系统
    IEnumerator SpwanEnemy_BossArea()
    {
        yield return new WaitForSeconds (0.1f);
        //产生boss
        if(isSingleSpawnBoss )
        {
            isSingleSpawnBoss = false;
            StartCoroutine(SpawnEnemy(1, goBoss, spawnBossTransform));
        }
        //产生更多的怪物

        while(true)
        {
            yield return new WaitForSeconds(10f);

            //如果敌人的数量小于等于3个，则产生新的敌人
            if (CountEnemyNumberAtBossArea()<=3)
            {
                yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_1);
                StartCoroutine(SpawnEnemy(2, goArcher, traSpawnEnemyPos_AreaD_Archer));
                StartCoroutine(SpawnEnemy(1, goKing, traSpawnEnemyPos_AreaD_King));
                StartCoroutine(SpawnEnemy(2, goMagic, traSpawnEnemyPos_AreaD_Magic));
                StartCoroutine(SpawnEnemy(2, goWarrior, traSpawnEnemyPos_AreaD_Warrior));
            }
        }
    }

    //统计敌人数量  再别的战斗场景中，可能还有没有被消灭的敌人，这是一个小问题
    private int CountEnemyNumberAtBossArea()
    {
        GameObject[] goEnemyArray = GameObject.FindGameObjectsWithTag(Tag.Enemy);
        if(goEnemyArray !=null)
        {
            return goEnemyArray.Length;
        }
        return 0;
    }
}
