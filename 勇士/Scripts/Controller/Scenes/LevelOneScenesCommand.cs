using UnityEngine;
using System.Collections;
using kernel;
/// <summary>
/// 控制层：第一关卡场景控制
/// 
/// 描述：
///    作用：
///          1.负责敌人的动态加载“多出生点测试”
///          
/// </summary>
public class LevelOneScenesCommand :Command {

    public AudioClip AcBackground;
    public Transform spawnPos1;
    public Transform spawnPos2;
    public Transform spawnPos3;
    public Transform spawnPos4;  
    public Transform spawnPos5;
    public Transform spawnPos6;
    public Transform spawnPos7;
    public Transform spawnPos8;
    public Transform spawnPos9;
    public Transform spawnPos10;
    private bool isSingleTime = true;

    //对象缓冲池，复杂对象（敌人）
    public GameObject goWarriorPrefab_Green;

    void Awake()
    {
        //升级事件注册。。。
        PlayerExternalData.PlayerExternalDataEvent += LevelUp;
    }

    IEnumerator  Start()
    {
        AudioManager.SetAudioBackgroundVolumns(0.3f);
        AudioManager.SetAudioEffectVolumns(1f);
        AudioManager.PlayBackground(AcBackground);

        StartCoroutine(SpawnEnemy(2));
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnEnemy(5));
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnEnemy(5));
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnEnemy(5));
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnEnemy(2));
        yield return new WaitForSeconds(3f);


    }


    //生成敌人（传统）
    //IEnumerator SpawnEnemy(int enemyNum)
    //{
    //    yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT5);
    //    for(int i=0;i<enemyNum;i++)
    //    {
    //        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_1 );
    //        //GameObject goEnemy = Resources.Load("Prefabs/Enemys/skeleton_warrior_green", typeof(GameObject)) as GameObject;

    //        //GameObject goEnemyClone = GameObject.Instantiate(goEnemy);
    //        //GameObject goEnemyClone = ResourcesMgr.GetInstance().LoadAsset("Prefabs/Enemys/skeleton_warrior_green", true);

    //        GameObject goEnemyClone=SpawnRandomEnemy();

    //        Transform spawnPos = GetRandomPos();//获得出生任意位置
    //        goEnemyClone.transform.position = spawnPos.position;
    //        goEnemyClone.transform.parent = spawnPos;

    //        //克隆敌人出现特效
    //        EnemySpawnParticalEffect(goEnemyClone);
    //    }
    //}


    //生成敌人（对象缓冲池 ）
    IEnumerator SpawnEnemy(int enemyNum)
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT5);
        for (int i = 0; i < enemyNum; i++)
        {
            yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_1);

            Transform spawnPos = GetRandomPos();//获得出生任意位置
            goWarriorPrefab_Green.transform.position = spawnPos.position;

            //在对象缓冲池中激活指定的对象
            GameObject go=PoolManager.PoolsArray["Enemys"].BirthGameObject(goWarriorPrefab_Green, spawnPos.position, Quaternion.identity);


            //克隆敌人出现特效
            EnemySpawnParticalEffect(go);
        }
    }

    //获得敌人任意出生点
    public Transform GetRandomPos()
    {
        Transform returnPos=null;//返回敌人的位置
        int num = UnityHelper.GetInstance().GetRandomNum(1, 10);
        switch (num)
        {
            case 1:
                returnPos=spawnPos1;
                break;
            case 2:
                returnPos = spawnPos2;
                break;
            case 3:
                returnPos = spawnPos3;
                break;
            case 4:
                returnPos = spawnPos4;
                break;
            case 5:
                returnPos = spawnPos5;
                break;
            case 6:
                returnPos = spawnPos6;
                break;
            case 7:
                returnPos = spawnPos7;
                break;
            case 8:
                returnPos = spawnPos8;
                break;
            case 9:
                returnPos = spawnPos9;
                break;
            case 10:
                returnPos = spawnPos10;
                break;
            default : 
                break;
            
        }
        return returnPos;
    }

    public GameObject  SpawnRandomEnemy()
    {
        GameObject go=null;
        int random = Random.Range(1, 3);
        if(random==1)
        {
            go = ResourcesMgr.GetInstance().LoadAsset("Prefabs/Enemys/skeleton_warrior_green", true);
        }else if(random ==2)
        {
            go = ResourcesMgr.GetInstance().LoadAsset("Prefabs/Enemys/skeleton_warrior_red", true);
        }

        return go;
    }

    //敌人出现粒子特效
    private void EnemySpawnParticalEffect(GameObject go)
    {
        StartCoroutine(LoadParticalEffect(GlobleParameter.INTERVER_TIME_0DOT1, "ParticleProps/EnemyDisplay", true, go.transform.position+go.transform .TransformDirection (0,3,0),go.transform.rotation,transform, "EnemyDisplayEffect", 0));
    }
       
    //主角升级 ，为注册的事件，当升级时会自动调用该方法
    private void  LevelUp(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("Level"))
        {
            //避免刚刚开始赋值0级的时候，就让其有特效出现
            if(isSingleTime )
            {
                isSingleTime = false;
            }
            else
            {
                HeroLevelUp();
            }
            
        }
    }

    private void HeroLevelUp()
    {
        //加载粒子特效
        ResourcesMgr.GetInstance().LoadAsset("ParticleProps/Hero_LvUp", true);

        //播放特效声音
        AudioManager.PlayAudioEffectA("LevelUpD");


    }
}


