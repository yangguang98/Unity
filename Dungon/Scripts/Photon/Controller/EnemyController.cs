using UnityEngine;
using System.Collections;
using TaidouCommon;
using System.Collections.Generic;
using TaidouCommon.Tools;
//敌人的是，，，主机先产生敌人，然后把敌人的guid和位置信息传递到别的客户端，别的客户端然后在根据这些信息去实例化敌人
public class EnemyController : ControllerBase {


    public override TaidouCommon.OperationCode OpCode
    {
        get { return OperationCode.Enemy; }
    }

    public void SendCreateEnemy(CreateEnemyModel model)
    {
        //产生敌人的同步  一般写的时候，是先将这个函数写好，和服务器端的响应函数写好，再去决定CreateEnemyModel中存储什么样的参数
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameters, ParameterCode.CreateEnemyModel, model);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.CreateEnemy, parameters);
    }

    public void SyncEnemyPosition(EnemyPositionModel model)
    {
        //同步敌人的位置和旋转
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameters, ParameterCode.EnemyPositionModel, model);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.SyncPositionAndRotation, parameters);
    }

    public void SyncEnemyAnimation(EnemyAnimationModel model)
    {
        //同步敌人的动画
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameters, ParameterCode.EnemyAnimationModel, model);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.SyncAnimation, parameters);
    }

    public override void OnEvent(ExitGames.Client.Photon.EventData eventData)
    {
        SubCode subCode = ParameterTool.GetSubCode(eventData.Parameters);
        switch (subCode )
        {
            case SubCode.CreateEnemy :
                CreateEnemyModel model = ParameterTool.GetParameter<CreateEnemyModel>(eventData.Parameters, ParameterCode.CreateEnemyModel);
                if(OnCreateEnemy !=null)
                {
                    OnCreateEnemy(model);
                }
                break;
            case SubCode.SyncPositionAndRotation :
                EnemyPositionModel model2 = ParameterTool.GetParameter<EnemyPositionModel>(eventData.Parameters, ParameterCode.EnemyPositionModel);
                if(OnSyncEnemyPositionAndRotation!=null)
                {
                    OnSyncEnemyPositionAndRotation(model2);
                }
                break;
            case SubCode.SyncAnimation :
                EnemyAnimationModel model3 = ParameterTool.GetParameter<EnemyAnimationModel>(eventData.Parameters, ParameterCode.EnemyAnimationModel);
                if(OnSyncEnemyAnimation !=null)
                {
                    OnSyncEnemyAnimation(model3);
                }
                break;
        }
    }

    public override void OnOperationResponse(ExitGames.Client.Photon.OperationResponse reponse)
    {

    }

    public event OnCreateEnemyEvent OnCreateEnemy;
    public event OnSyncEnemyPositionRotationEvent OnSyncEnemyPositionAndRotation;
    public event OnSyncEnemyAnimationEvent OnSyncEnemyAnimation;
}
