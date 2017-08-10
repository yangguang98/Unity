using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//团战的时候，有一个主机，随机挑选的
public class EnemyTriggle : MonoBehaviour
{

    public GameObject[] enemyPrefabs;
    public Transform[] spawnPosArray;//生成位置
    public float time = 0;//多少秒开始生成
    public float repeateRate;//多少秒生成一波
    private bool isSpwaned = false;//只生成一次敌人，，，，当再次穿过这个triggle的时候，不会再产生敌人
    private EnemyController enemyController;//只有在团战的时候才会用到

    void Start()
    {
        if (GameController.Instance.battleType == BattleType.Team && GameController.Instance.isMaster)
        {
            //团战并且是主机的时候
            enemyController = TranscriptManager._instance.GetComponent<EnemyController>();
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if (GameController.Instance.battleType == BattleType.Person)
        {
            //个人战斗模式
            if (col.tag == "Player" && isSpwaned == false)
            {
                isSpwaned = true;
                StartCoroutine(SpwanEnemy());
            }
        }
        else if (GameController.Instance.battleType == BattleType.Team)
        {
            //团队战斗模式
            if (col.tag == "Player" && isSpwaned == false && GameController.Instance.isMaster == true)
            {
                //GameController.Instance.isMaster==true判断当前客户端是否是主机，只有是主机的客户端才会生成敌人，不是主机的不会生成敌人，在团队战斗中，有一个主机，随机挑选的，
                isSpwaned = true;
                StartCoroutine(SpwanEnemy());
            }
        }
    }

    IEnumerator SpwanEnemy()
    {
        yield return new WaitForSeconds(time);
        foreach (GameObject go in enemyPrefabs)
        {
            //每种的敌人，都会产生一波
            List<CreateEnemyProperty> propertyList = new List<CreateEnemyProperty>();
            foreach (Transform t in spawnPosArray)
            {
                //每一个位置都生成一个敌人

                GameObject temp = GameObject.Instantiate(go, t.position, Quaternion.identity) as GameObject;
                    string GUID = Guid.NewGuid().ToString();//为每一个新生成的敌人创建一个GUID，还要将这个数据传递到别的客户端
                    int targetRoleID = GameController.Instance.GetRandomRoleID();
                    //boss和enemy的guid挂在不同的脚本上
                    if (temp.GetComponent<Enemy>() != null)
                    {
                        //普通的敌人
                        Enemy enemy = temp.GetComponent<Enemy>();
                        enemy.guid = GUID;
                        enemy.targetRoleId = targetRoleID;//攻击的目标iD
                    }
                    else
                    {
                        //boss
                        Boss boss = temp.GetComponent<Boss>();
                        boss.guid = GUID;
                        boss.targetRoleId = targetRoleID;//随机生成攻击敌人的id
                    }

                    CreateEnemyProperty property = new CreateEnemyProperty() { guid = GUID, position = new Vector3Obj(t.position), prefabName = go.name, targetRoleId = targetRoleID };//这种语法形式，，应该注意下
                    propertyList.Add(property);
            }

            //生成一波敌人后，在发起同步的请求
            if (GameController.Instance.battleType == BattleType.Team && GameController.Instance.isMaster)
            {
                //主机产生的就要同步
                CreateEnemyModel model = new CreateEnemyModel() { list = propertyList };
                enemyController.SendCreateEnemy(model);
            }
            yield return new WaitForSeconds(repeateRate);
        }
    }
}
