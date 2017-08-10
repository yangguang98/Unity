using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using kernel;
/// <summary>
/// 控制层：父类控制层
/// </summary>
public class Command : MonoBehaviour {

    private bool isSingleTime_LevelUp = true; //主角升级单次开关

    
	protected void EnterNextScenes(ScenesEnum scenesEnumName)
    {
        GlobalParameterMgr.nextScenesName = scenesEnumName;
        SceneManager.LoadScene(ConvertEnumToString.GetInstance().GetStrByEnumScenes(ScenesEnum.LoadingScenes));
    }


    public void AttackEnemy(List<GameObject> enemyList, Transform nearestEnemy, float attackArea, int powerMult, bool isDirection = true)
    {


        //在攻击范围内，没有敌人存在
        if (enemyList == null || enemyList.Count <= 0)
        {
            nearestEnemy = null;
            return;
        }

        foreach (GameObject go in enemyList)
        {
            //每两秒才更新一次enemyList，而敌人的生命值是1秒，所以存在敌人已经死了，而列表还没有更新的问题 ，因此加if判断
            //if(go&&go.GetComponent <EnemyCommand >().IsAlive)//有些已经死掉的敌人可以执行这个代码吗？？？？即敌人被销毁，还可以获取其身上的脚本？？？？？？
            if (go && go.GetComponent<BaseEnemyProCommand>().CurrentState != EnemyState.death)//有些已经死掉的敌人可以执行这个代码吗？？？？即敌人被销毁，还可以获取其身上的脚本？？？？？？
            {
                float distance = Vector3.Distance(go.transform.position, this.gameObject.transform.position);

                //攻击具有方向性
                if (isDirection)
                {
                    Vector3 dir = (go.transform.position - this.gameObject.transform.position).normalized;
                    float floDirection = Vector3.Dot(dir, this.gameObject.transform.forward);
                    if (floDirection > 0 && attackArea >= distance)
                    {
                        //在攻击范围内
                        go.SendMessage("OnHurt", HeroPropertyCommand.Instance.GetCurrentATKValue() * powerMult, SendMessageOptions.DontRequireReceiver);
                    }
                }
                //攻击没有方向性
                else
                {
                    if (attackArea >= distance)
                    {
                        //在攻击范围内
                        go.SendMessage("OnHurt", HeroPropertyCommand.Instance.GetCurrentATKValue() * powerMult, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
    }

    
    /// <summary>
    /// //粒子特效加载公共方法
    /// </summary>
    /// <param name="interverTime">间隔时间</param>
    /// <param name="path">路径</param>
    /// <param name="isCatch">是否使用缓存</param>
    /// <param name="pos">位置</param>
    /// <param name="quaParticalEffect">旋转</param>
    /// <param name="transform1">父对象</param>
    /// <param name="audioEffect">音效</param>
    /// <param name="desTime">销毁时间</param>
    /// <returns></returns>
    public IEnumerator LoadParticalEffect(float interverTime,string path,bool isCatch,Vector3 pos,Quaternion quaParticalEffect,Transform transform1,string audioEffect=null,float desTime=0)
    {
        //需要间隔时间
        yield return new WaitForSeconds(interverTime);

        //粒子预设
        GameObject prefab = ResourcesMgr.GetInstance().LoadAsset(path, isCatch);

        //位置
        prefab.transform.position  = pos;

        //例子预设的旋转
        prefab.transform.rotation = quaParticalEffect;

        //父子对象
       if(transform1 !=null)
       {
           prefab.transform.parent = transform1;
       }

        //特效音频
        if(!string.IsNullOrEmpty (audioEffect ))
        {
            AudioManager.PlayAudioEffectA(audioEffect);
        }
        //销毁时间
        if(desTime >0)
        {
            Destroy(prefab,desTime);
        }
    }

    
    /// <summary>
    /// //生成敌人（对象缓冲池 ）
    /// </summary>
    /// <param name="enemyNum">生成敌人的数量</param>
    /// <param name="enemyPrefab">生成敌人的类型</param>
    /// <param name="spwanPos">敌人位置</param>
    /// <returns></returns>
    public IEnumerator SpawnEnemy(int enemyNum,GameObject enemyPrefab,Transform [] spwanPos)
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT5);
        for (int i = 0; i < enemyNum; i++)
        {

            Transform randomPos = GetRandomPos(spwanPos);//获得出生任意位置
            enemyPrefab.transform.position = randomPos.position;

            //在对象缓冲池中激活指定的对象
            GameObject go = PoolManager.PoolsArray["Enemys"].BirthGameObject(enemyPrefab, enemyPrefab.transform.position, Quaternion.identity);

            /*敌人的血条*/
            //调用预设
            GameObject goEnemyHp = ResourcesMgr.GetInstance().LoadAsset("Prefabs/UI/Enemy", true);

            //确定父节点
            goEnemyHp.transform.parent = GameObject.FindGameObjectWithTag(Tag.UIPlayerInfo).transform;
            //参数赋值
            goEnemyHp.GetComponent<EnemyHpBar>().SetTargetEnemy(go);

            //克隆敌人出现特效
            //EnemySpawnParticalEffect(go);
        }
    }

    //获得敌人任意出生点 (从gei定的Transform中选一个)
    public Transform GetRandomPos(Transform[] enemyCreatePos)
    {
        int iRandomNum = UnityHelper.GetInstance().GetRandomNum(0, enemyCreatePos.Length - 1);
        return enemyCreatePos [iRandomNum];
    }

    //主角升级 ，为注册的事件，当升级时会自动调用该方法
    protected void LevelUp(KeyValuesUpdate kv,AudioClip audioClip)
    {
        if (kv.Key.Equals("Level"))
        {
            //避免刚刚开始赋值0级的时候，就让其有特效出现
            if (isSingleTime_LevelUp)
            {
                isSingleTime_LevelUp = false;
            }
            else
            {
                HeroLevelUp(audioClip);
            }

        }
    }

    private void HeroLevelUp(AudioClip audioClip)
    {
        //加载粒子特效
        ResourcesMgr.GetInstance().LoadAsset("ParticleProps/Hero_LvUp", true);

        //播放特效声音
        AudioManager.PlayAudioEffectA(audioClip);


    }

    
    /// <summary>
    /// //漂字特效缓冲池加载
    /// </summary>
    /// <param name="internalTime">间隔时间</param>
    /// <param name="goPrefab">特效预设</param>
    /// <param name="pos">显示位置</param>
    /// <param name="goTarget">目标对象</param>
    /// <param name="num">漂字数值</param>
    /// <param name="parent">父节点</param>
    /// <param name="audioClip">音频剪辑</param>
    /// <returns></returns>
    protected IEnumerator  LoadParticalEffectInPool_MoveUpLabel(float internalTime,GameObject goPrefab,Vector3 pos,GameObject goTarget,int num,Transform parent,AudioClip audioClip=null)
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT5);

        //在对象缓冲池中激活指定的对象
        GameObject go = PoolManager.PoolsArray["ParticalSys"].BirthGameObject(goPrefab, pos, Quaternion.identity);

        //目标参数赋值
        if(go)
        {
            go.GetComponent<MoveUpLabel>().SetTargetEnemy(goTarget);
            go.GetComponent<MoveUpLabel>().SetReduceHp(num);
        }

        //确定父节点
        if(parent !=null)
        {
            go.transform.parent = parent;
        }

        //音效
        if(audioClip !=null)
        {
            AudioManager.PlayAudioEffectB(audioClip);
        }
    }
}
