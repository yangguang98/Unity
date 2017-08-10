using UnityEngine;
using System.Collections;
using TaidouCommon;
using System.Collections.Generic;
using TaidouCommon.Tools;
using TaidouCommon.Model;

public class BattleController : ControllerBase {

	

    public override TaidouCommon.OperationCode OpCode
    {
        get { return OperationCode.Battle; }
    }
    public void SyncPositionAndRotation(Vector3 position,Vector3 eulerAngles)
    {
        //发起同步位置和旋转的请求
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter<Vector3Obj>(parameters, ParameterCode.Position, new Vector3Obj (position));
        ParameterTool.AddParameter<Vector3Obj>(parameters, ParameterCode.EulerAngles,new Vector3Obj (eulerAngles));
        ParameterTool.AddParameter(parameters, ParameterCode.roleId, PhotonEngine.Instance.role.ID, false);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.SyncPositionAndRotation, parameters );
    }

    public void SyncMoveAnimation(PlayerMoveAnimationModel model)
    {
        //发送 移动 动画状态的请求
        Dictionary <byte,object > parameters=new Dictionary<byte,object> ();
        ParameterTool .AddParameter (parameters ,ParameterCode.PlayerMoveAnimationModel ,model);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.SyncMoveAnimation, parameters);
    }

    public void SyncPlayerAnimation(PlayerAnimationModel  model)
    {
        //同步主角动画
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameters, ParameterCode.PlayerAnimationModel, model);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.SyncAnimation, parameters);
    }
    public void SendTeam()
    {
        //发起组队的请求
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.SendTeam, new Dictionary<byte, object>());
    }

    public void CancelTeam()
    {
        //取消组队的请求
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.CancelTeam, new Dictionary<byte, object>());
    }

    public void SendGameState(GameStateModel model)
    {
        //发送游戏状态，，由团战中的主机，，分发给其他的客户端，，，，胜利或者失败
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameters, ParameterCode.GameStateModel, model);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.SendGameState, parameters);
    }

    public override void OnEvent(ExitGames.Client.Photon.EventData eventData)
    {
        SubCode subCode = ParameterTool.GetSubCode(eventData.Parameters);
        switch (subCode )
        {
                //由于OperationResponse中有ReturnCode可以判定是否组队成功，而eventData中没有returnCode来判定，所以添加GetTeam
            case SubCode .GetTeam :
                List<Role> roleList = ParameterTool.GetParameter<List<Role>>(eventData .Parameters, ParameterCode.RoleList);
                int roleMasterId = ParameterTool.GetParameter<int>(eventData.Parameters, ParameterCode.MasterRoleId, false);
                    if(OnGetTeam!=null)
                    {
                        OnGetTeam(roleList,roleMasterId);
                    }
                break;
            case SubCode.SyncPositionAndRotation:
                int roleID = ParameterTool.GetParameter<int>(eventData.Parameters, ParameterCode.roleId,false);//同步的是哪一个角色，，每一个角色都有一个角色ID
                Vector3 position = ParameterTool.GetParameter<Vector3Obj>(eventData.Parameters, ParameterCode.Position).ToVector3 ();//这里的Vector3为什么最后一个参数是True,????????因为其是结构体，也相当于引用类型？？？？？？
                Vector3 eulerAngles = ParameterTool.GetParameter<Vector3Obj>(eventData.Parameters, ParameterCode.EulerAngles).ToVector3 ();
                if(OnSyncPositionAndRotation !=null)
                {
                    OnSyncPositionAndRotation(roleID, position, eulerAngles);
                }
                break;
            case SubCode.SyncMoveAnimation :
                int roleID2 = ParameterTool.GetParameter<int>(eventData.Parameters, ParameterCode.roleId,false);//同步的是哪一个角色，，每一个角色都有一个角色ID
                PlayerMoveAnimationModel model = ParameterTool.GetParameter<PlayerMoveAnimationModel>(eventData.Parameters,ParameterCode.PlayerMoveAnimationModel );
                if(OnSyncMoveAnimation !=null)
                {
                    OnSyncMoveAnimation(roleID2,model);
                }
                break;
            case SubCode .SyncAnimation:
                int roleID3 = ParameterTool.GetParameter<int>(eventData.Parameters, ParameterCode.roleId,false);//同步的是哪一个角色，，每一个角色都有一个角色ID
                PlayerAnimationModel model1 = ParameterTool.GetParameter<PlayerAnimationModel>(eventData.Parameters, ParameterCode.PlayerAnimationModel);
                if(OnSyncPlayerAnimation !=null)
                {
                    OnSyncPlayerAnimation(model1, roleID3);
                }
                break;
            case SubCode.SendGameState:
                GameStateModel model3 = ParameterTool.GetParameter<GameStateModel>(eventData.Parameters, ParameterCode.GameStateModel);
                if(OnGameStateChange!=null)
                {
                    OnGameStateChange(model3);
                }
                break;
        }
    }

    public override void OnOperationResponse(ExitGames.Client.Photon.OperationResponse reponse)
    {
        SubCode subCode = ParameterTool.GetSubCode(reponse.Parameters);
        switch (subCode )
        {
            case  SubCode.SendTeam :
                if(reponse .ReturnCode ==(short)ReturnCode .GetTeam )
                {
                    List<Role> roleList = ParameterTool.GetParameter<List<Role>>(reponse.Parameters, ParameterCode.RoleList);
                    int roleMasterId = ParameterTool.GetParameter<int>(reponse.Parameters, ParameterCode.MasterRoleId, false);
                    if(OnGetTeam!=null)
                    {
                        OnGetTeam(roleList,roleMasterId);
                    }
                }
                else if(reponse .ReturnCode ==(short)ReturnCode .WaitingTeam )
                {
                    if(OnWaitingTeam !=null)
                    {
                        OnWaitingTeam();
                    }
                }
                break;
            case SubCode .CancelTeam :
                if(reponse .ReturnCode ==(short)ReturnCode .Success )
                {
                    if(OnCancelTeam !=null)
                    {
                        OnCancelTeam();
                    }
                }
                break;
        }
    }

    public event OnGetTeamEvent OnGetTeam;
    public event OnWaitingTeamEvent OnWaitingTeam;
    public event OnCancelTeamEvent OnCancelTeam;
    public event OnSyncPositionAndRotationEvent OnSyncPositionAndRotation;
    public event OnSyncMoveAnimationEvent OnSyncMoveAnimation;
    public event OnSyncPlayerAnimationEvent OnSyncPlayerAnimation;
    public event OnGameStateChangeEvent OnGameStateChange;
}
