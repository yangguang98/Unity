using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System.Collections .Generic ;
using TaidouCommon;
using TaidouCommon.Model;
using TaidouCommon.Tools;
public class PhotonEngine : MonoBehaviour,IPhotonPeerListener {

    public ConnectionProtocol protocol = ConnectionProtocol.Tcp;
    public string serverAdress = "127.0.0.1:4530";
    public string applicationName = "TaidouServer";//服务器的名称
    private bool isConnected = false;
    private PhotonPeer peer;
    private Dictionary<byte, ControllerBase> controllers = new Dictionary<byte, ControllerBase>();//存储所有的controller

    private static PhotonEngine _instance;

    public static PhotonEngine Instance
    {
        get
        {
            return _instance;
        }
    }

    public  Role role;//当前用户选择的角色，就是展示的那个角色

    public delegate void OnConnectToServerEvent();
    public event OnConnectToServerEvent OnConnectToServer;

    void Awake()
    {
        _instance = this;
        peer = new PhotonPeer(this, protocol);
        peer.Connect(serverAdress, applicationName);//异步执行,向服务器发送链接请求
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if(peer!=null)
        {
            peer.Service();
        }
    }

    public void RegisteController(OperationCode opCode,ControllerBase controller)
    {
        controllers.Add((byte)opCode, controller);
    }

    public void LogoutController(OperationCode  opCode)
    {
        controllers.Remove((byte)opCode);
    }

    public void SendRequest(OperationCode opCode,Dictionary <byte,object > parameters)
    {
        //总的发起一个opCode类型的请求
        Debug.Log("sendrequest to server,opCode:" + opCode);
        peer.OpCustom((byte)opCode, parameters, true);
    }

    public void SendRequest(OperationCode opCode,SubCode subCode,Dictionary <byte,object > parameters)
    {
        //sendRquest的重载方法，有subcode的时候使用该方法较方便,subCode的相关操作在这里进行
        Debug.Log("sendrequest to server,opCode:" + opCode);
        parameters.Add((byte)ParameterCode.SubCode, subCode);
        peer.OpCustom((byte)opCode, parameters, true);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log(level + ":" + message);
    }

    public void OnEvent(EventData eventData)
    {
        //服务器主动给客户端发送消息时，调用
        ControllerBase controller;
        OperationCode opCode=ParameterTool.GetParameter<OperationCode>(eventData.Parameters, ParameterCode.OperationCode, false);
        controllers.TryGetValue((byte)opCode, out controller);
        if(controller!=null)
        {
            controller.OnEvent(eventData);
        }
        else
        {
            Debug.LogWarning("Receive a unknown Event:" + opCode);
        }
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        //客户端给服务器发送消息，然后客户端响应服务器端的消息时，调用
        //服务器响应的参数会传到这个函数中的参数来，总的服务器响应
        ControllerBase controller;
        controllers.TryGetValue(operationResponse.OperationCode, out controller);//在列表中得到对应的controllers,由controller对对应的响应进行处理
        if (controller != null)
        {
            controller.OnOperationResponse(operationResponse);//对服务器中的响应做相应的处理
        }
        else
        {
            Debug.LogWarning("Receive a unknown response:" + operationResponse.OperationCode);
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log("OnStatusChange" + statusCode);
        switch(statusCode)
        {
            case  StatusCode .Connect :
                isConnected = true;
                OnConnectToServer();//连接成功后就触发事件
                break;
            default:
                isConnected = false;
                break;
        }
    }
}
