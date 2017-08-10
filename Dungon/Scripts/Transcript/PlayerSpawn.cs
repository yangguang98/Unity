using UnityEngine;
using System.Collections;
using TaidouCommon.Model;
//每一个本底角色都有一个RoleID，这个是关键
public class PlayerSpawn : MonoBehaviour {


    public Transform[] positionTransformArray;
    void Start()
    {
        
    }

    void Awake()
    {
        SpwanPlayer();
    }



    void SpwanPlayer()
    {
        if(GameController .Instance .battleType ==BattleType.Person )
        {
            //个人战斗角色加载
            Role role = PhotonEngine.Instance.role;
            GameObject playerPrefab;
            if(role.IsMan )
            {
                playerPrefab=Resources.Load("Player-battle/Player_Boy") as GameObject;
            }
            else
            {
                playerPrefab = Resources.Load("Player-battle/Player_Girl") as GameObject;
            }
            //通过加载的游戏物体，实例化游戏物体
            GameObject go=GameObject.Instantiate(playerPrefab, positionTransformArray[0].position, Quaternion.identity) as GameObject ;
            TranscriptManager._instance.player = go;//直接进行赋值，这里用到了_instance，所以要注意脚本的执行顺序

            go.GetComponent<Player>().roleID = role.ID;
        }
        else if (GameController .Instance .battleType ==BattleType .Team )
        {
            //团队战斗   在每一个客户端都会生成二个人物模型
            for(int i=0;i<2;i++)
            {
                Role role = GameController.Instance.teamRoleList[i];
                Vector3 pos = positionTransformArray[i].position;
                GameObject playerPrefab;
                if (role.IsMan)
                {
                    playerPrefab = Resources.Load("Player-battle/Player_Boy") as GameObject;
                }
                else
                {
                    playerPrefab = Resources.Load("Player-battle/Player_Girl") as GameObject;
                }
                GameObject go=GameObject.Instantiate(playerPrefab, pos, Quaternion.identity) as GameObject;
                go.GetComponent<Player>().roleID = role.ID;//为每一个模型设置roleID,便于区别，体现每一个模型的差异性
                GameController.Instance.AddPlayer(role.ID,go);//添加到字典中，所有人物进行管理，根据role.ID进行管理
                if(role.ID ==PhotonEngine .Instance.role.ID )
                {
                    //当点击按钮时，只让本地角色播放技能动画
                    TranscriptManager._instance.player = go;//当点击按钮释放技能的时候，只让本地的角色播放动画，只控制本地角色的动画,还有摄像机只更随本底的角色移动
                }
                else
                {
                    //只控制本地角色的移动
                    go.GetComponent<PlayerMove>().isCanControl = false;//该角色不是用户登录的角色，则不能够控制该角色。
                }
            }
        }
    }
}
