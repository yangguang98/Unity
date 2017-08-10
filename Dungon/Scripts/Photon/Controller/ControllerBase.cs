using UnityEngine;
using System.Collections;
using TaidouCommon;
using ExitGames.Client.Photon;
//controller和服务器端的handler是对应的，controller发起请求，handler处理，在分发给controller

public abstract class ControllerBase : MonoBehaviour {

    public abstract OperationCode OpCode
    {
        //有get,set方法，就相当于有了一个属性了，默认为opCode
        get;
    }

    public virtual void Start()
    {
        PhotonEngine.Instance.RegisteController(OpCode, this);//unity启动就会去注册 OpCode 和controller
    }

    public virtual void OnDestroy()
    {
        PhotonEngine.Instance.LogoutController(OpCode);
    }

    public virtual void OnEvent(EventData eventData)
    {

    }
    public abstract void OnOperationResponse(OperationResponse reponse);

    
}
