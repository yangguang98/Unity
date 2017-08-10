using UnityEngine;
using System.Collections;
using TaidouCommon.Model;
using System.Collections.Generic;
//进入到下一个场景，需要保存相关的信息
public enum BattleType
{
    Person,
    Team,
    None
}

public class GameController : MonoBehaviour
{

    private static GameController _instance;
    private TaskDBController taskDBController;
    private BattleController battleController;
    public List<Role> teamRoleList = new List<Role>();//团战角色
    public HashSet<int> dieRoleIdSet = new HashSet<int>();//??????????????这中类型的数据结构？？？？？？？？？？？  团战中 存储所有的已经死亡的主角的ID ，，，当数目等于teamROleList中的数目，，那么游戏就失败了，，boss死亡那么游戏胜利
    public BattleType battleType;//战斗类型
    public int transcriptId = -1;//当前接受的任务Id
    public bool isMaster = false;//当前客户端是否是主机

    private Dictionary<int, GameObject> playerDict = new Dictionary<int, GameObject>();//当时团战的时候，里面存储所有的人物模型，根据人物的roleID进行存储
    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }


    void Awake()
    {
        _instance = this;
        battleType = BattleType.None;
        DontDestroyOnLoad(this.gameObject);//不会被销毁
        Transform posTransform = GameObject.Find("Player_pos").transform;
        string playerPrefabName = "Player_girl";
        if (PhotonEngine.Instance.role.IsMan)
        {
            playerPrefabName = "Player_boy";
        }
        GameObject playerGo = GameObject.Instantiate(Resources.Load("Player/" + playerPrefabName)) as GameObject;
        playerGo.transform.position = posTransform.position;
        taskDBController = this.GetComponent<TaskDBController>();
        battleController = this.GetComponent<BattleController>();
        battleController.OnGetTeam += this.OnGetTeam;
        battleController.OnSyncPositionAndRotation += this.OnSyncPositionAndRotation;
        battleController.OnSyncMoveAnimation += this.OnSyncMoveAnimation;
        battleController.OnSyncPlayerAnimation += this.OnSyncPlayerAnimation;
        battleController.OnGameStateChange += this.OnGameStatChange;
    }
    //该等级下，需要的所有的经验值
    public static int GetRequireExpByLevel(int level)
    {
        return (int)((level - 1) * (100 + (100 + 10 * (level - 2))) / 2);//强制转换
    }

    public void OnPlayerDie(int roleID)
    {
        if (battleType == BattleType.Person)
        {
            GameOverPanel.Instance.Show("游戏失败");
        }
        else
        {
            if (isMaster)
            {
                //只在主机端做失败和胜利的检测，，别的客户端不会去执行，，，团战的时候有一个主机
                dieRoleIdSet.Add(roleID);
                if (dieRoleIdSet.Count == teamRoleList.Count)
                {
                    //死亡数目和treamRoleList中的数目是相同的，则表明游戏失败
                    GameOverPanel.Instance.Show("游戏失败");
                    //显示有习失败的界面，，并且同步
                    battleController.SendGameState(new GameStateModel() { isSuccess = false });//这种语法形式
                }
            }
        }
    }

    public void OnBossDie()
    {
        if (battleType == BattleType.Person)
        {
            OnVictory();
        }
        else
        {
            if (isMaster)
            {
                OnVictory();  //团战中的主机会调用，，别的客户端需要主机去同步
                //向其他客户端发起同步
                battleController.SendGameState(new GameStateModel() { isSuccess = true });
            }
        }
    }
     
    void OnVictory()
    {
        GameOverPanel.Instance.Show("游戏胜利");
        foreach (Task task in TaskManager._instance.GetTaskList())
        {
            //更新奖励
            if (task.TaskProgress1 == TaskProgress.Accept)
            {
                if (task.IdTranscript == transcriptId)
                {
                    task.TaskProgress1 = TaskProgress.Reward;
                    TaskDB taskDB = task.TaskDB;
                    taskDB.State = (int)TaskState.Reward; //TaskProgress和State是对应的关系，只是一个存在于服务器端，一个在客户端
                    taskDBController.UpdateTaskDB(taskDB);
                }
            }
        }
    }

    public void OnGetTeam(List<Role> roleList, int roleMasterId)
    {

    }

    void OnDestroy()
    {
        battleController.OnGetTeam -= this.OnGetTeam;
    }


    public void AddPlayer(int roleID, GameObject playerGameObject)
    {
        playerDict.Add(roleID, playerGameObject);
    }

    public void OnSyncPositionAndRotation(int roleID, Vector3 position, Vector3 eulerAngles)
    {
        //同步时的回调
        GameObject go = null;
        bool isHave = playerDict.TryGetValue(roleID, out go);
        if (isHave)
        {
            go.GetComponent<PlayerMove>().SetPositionAndEulerAngles(position, eulerAngles);//关于物体位置的设置，放到游戏模型本身的身上
        }
        else
        {
            Debug.Log("未找到对应的角色游戏物体");
        }
    }

    public void OnSyncMoveAnimation(int roleID, PlayerMoveAnimationModel model)
    {
        //同步player的移动动画，，，首先通过roleID，寻找到角色，然后在设置
        GameObject go = null;
        bool isHave = playerDict.TryGetValue(roleID, out go);
        if (isHave)
        {
            go.GetComponent<PlayerMove>().SetAnim(model);
        }
        else
        {
            Debug.Log("未找到对应的游戏物体进行更新移动动画状态");
        }
    }

    public void OnSyncPlayerAnimation(PlayerAnimationModel model, int roleID)
    {
        //同步player的移动动画，，，首先通过roleID，寻找到角色，然后在设置
        //不是本地主角的动画通过这个函数同步
        GameObject go = null;
        bool isHave = playerDict.TryGetValue(roleID, out go);
        if (isHave)
        {
            go.GetComponent<PlayerAnimation>().SyncAnimation(model);
            if(model.die )
            {
                //如果主角播放的是死亡动画，就将其加入到死亡队列中，，，，，
                OnPlayerDie(roleID);
            }
        }
        else
        {
            Debug.Log("未找到对应的游戏物体进行更新移动动画状态");
        }
    }

    public int GetRandomRoleID()
    {
        //随机选取一个角色，在获取该角色的iD,将该角色作为敌人攻击的目标，，，在敌人生成的时候调用，作为其攻击的目标
        if(battleType ==BattleType .Person )
        {
            return PhotonEngine.Instance.role.ID;
        }
        int i = Random.Range(0, teamRoleList.Count);
        return teamRoleList[i].ID;
    }

    public GameObject GetPlayerByRoleID(int roleID)
    {
        GameObject go;
        playerDict.TryGetValue(roleID, out go);
        return go;
    }

    void OnGameStatChange(GameStateModel model)
    {
        //团战中不是主机的客户端，通过主机的回调，，调用该方法
        if(model.isSuccess )
        {
            OnVictory();
        }
        else
        {
            GameOverPanel.Instance.Show("游戏失败");
        }
    }

}
