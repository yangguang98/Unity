using UnityEngine;
using System.Collections;
using TaidouCommon.Model;
using LitJson;
using System.Collections.Generic;
using TaidouCommon;

public class RegisterController : ControllerBase {

    private StartMenu startMenu;
    private User user;
    public override TaidouCommon.OperationCode OpCode
    {
        get { return TaidouCommon.OperationCode.Register; }
    }

    public void Register(string username,string password)
    {
        //发起请求的方法,先准备好数据，然后利用photonEngine中的方法同意的发起请求
        //发起请求为键值对的形式，何种请求加请求的数据，在请求的数据中，又分为键值对，这个键值对中的值是以Json的形式存储的
        user = new User { Username = username, Password = password };
        string json=JsonMapper.ToJson (user);//Json实际为字符串类型的数据
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        parameters.Add((byte)ParameterCode.User, json);
        PhotonEngine.Instance.SendRequest(OperationCode.Register, parameters);
    }

    public override void OnOperationResponse(ExitGames.Client.Photon.OperationResponse response)
    {
        switch (response.ReturnCode )
        {
            case (byte)ReturnCode .Fail :
                MessageManager._instance.ShowMessage(response.DebugMessage, 2);
                break;
            case (byte)ReturnCode .Success :
                MessageManager._instance.ShowMessage("注册成功", 2);
                StartMenu._instance.HideRegisterPanel();
                StartMenu._instance.ShowStartPanel();
                StartMenu._instance.usernameLabelStart.text = user.Username;
                break;
        }
    }
}
