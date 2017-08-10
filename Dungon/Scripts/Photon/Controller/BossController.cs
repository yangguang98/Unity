using UnityEngine;
using System.Collections;
using TaidouCommon;
using System.Collections.Generic;
using TaidouCommon.Tools;

public class BossController :ControllerBase  {


    public override TaidouCommon.OperationCode OpCode
    {
        get { return OperationCode.Boss; }
    }


    public void SyncBossAnimation(BossAnimationModel model)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameters, ParameterCode.BossAnimationModel, model);
        PhotonEngine.Instance.SendRequest(OperationCode.Boss, SubCode.SyncBossAnimation, parameters);
    }
    public override void OnOperationResponse(ExitGames.Client.Photon.OperationResponse reponse)
    {
        throw new System.NotImplementedException();
    }

    public override void OnEvent(ExitGames.Client.Photon.EventData eventData)
    {
        SubCode subCode = ParameterTool.GetSubCode(eventData.Parameters);
        switch (subCode )
        {
            case SubCode.SyncBossAnimation :
                BossAnimationModel model=ParameterTool .GetParameter <BossAnimationModel >(eventData .Parameters ,ParameterCode.BossAnimationModel );
                if(OnSyncBossAnimation !=null)
                {
                    OnSyncBossAnimation(model);
                }
                break;
        }
    }

    public event OnSyncBossAnimationEvent OnSyncBossAnimation;
}
