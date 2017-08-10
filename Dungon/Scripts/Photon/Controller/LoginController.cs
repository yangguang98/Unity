using UnityEngine;
using System.Collections;
using TaidouCommon;
using TaidouCommon.Model;
using LitJson;
using System.Collections.Generic;
public class LoginController : ControllerBase {
    
    private RoleController roleController;
    public override void Start()
    {
        base.Start();
        roleController = StartMenu._instance.GetComponent<RoleController>();
    }
    public override TaidouCommon.OperationCode OpCode
    {
        get { return OperationCode.Login; }
    }

    public void Login(string username,string password)
    {
        //每次的请求操作是何种操作有一个operationCode与之对应，传递的数据是以字典的形式传递的，所以每次传递的数据类型有一个parameterCode与之对应
        User user = new User() { Username = username, Password = password };//get set方法对应的构造函数
        string json = JsonMapper.ToJson(user);
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        parameters.Add((byte)ParameterCode.User, json);
        PhotonEngine.Instance.SendRequest(OperationCode.Login, parameters);//发起请求
    }

    public override void OnOperationResponse(ExitGames.Client.Photon.OperationResponse response)
    {
        switch(response.ReturnCode)
        {
            case (short)ReturnCode.Success:
                //根据登录的用户，加载用户的角色信息

                StartMenu._instance.hideStartPanel();
                roleController.GetRole();
                break;
            case (short)ReturnCode .Fail :
                MessageManager._instance.ShowMessage(response.DebugMessage, 2);
                break;
        }
            
    }
}
