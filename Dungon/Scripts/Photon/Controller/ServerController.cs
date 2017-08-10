using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TaidouCommon;
using LitJson;
using TaidouCommon.Model;

public class ServerController : ControllerBase {

    public override void Start()
    {
        base.Start();
        PhotonEngine.Instance.OnConnectToServer += GetServerList;
    }

    public void GetServerList()
    {
        //连接上了服务器就会去调用这个方法
        PhotonEngine.Instance.SendRequest(TaidouCommon.OperationCode.GetServer, new Dictionary<byte, object>());
    }
    public override void OnOperationResponse(ExitGames.Client.Photon.OperationResponse reponse)
    {
        Dictionary<byte, object> parameters = reponse.Parameters;
        object jsonObject = null;
        parameters.TryGetValue((byte)ParameterCode.ServerList,out jsonObject);
        List<TaidouCommon.Model.ServerProperty> serverList = JsonMapper.ToObject<List<ServerProperty>>(jsonObject.ToString());//将object类型的json转化为list类型，泛型为ServerProperty

        ServerProperty1 spDefault = null;
        int index = 0;
        foreach(ServerProperty spTemp in serverList)
        {
            string ip = spTemp.IP + ":4530";
            string name = spTemp.Name ;
            int count = spTemp.Count ;
            GameObject go = null;
            if (count > 50)
            {
                //火爆
                go = NGUITools.AddChild(StartMenu._instance.serverlistGrid.gameObject, StartMenu._instance.serverItemRed);//如何有格子得到物体

            }
            else
            {
                //流畅
                go = NGUITools.AddChild(StartMenu._instance.serverlistGrid.gameObject, StartMenu._instance.serverItemGreen);

            }
            ServerProperty1 sp = go.GetComponent<ServerProperty1>();
            sp.ip = ip;
            sp.name = name;
            sp.count = count;
            if(index==0)
            {
                spDefault = sp;
                //让默认选择的服务器为第一个服务器
                StartMenu._instance.serverSelectedGo.GetComponent<UISprite>().spriteName = go.GetComponent<UISprite>().spriteName;
                StartMenu._instance.serverSelectedGo.transform.Find("Label").GetComponent<UILabel>().text = sp.name;
                StartMenu._instance.serverSelectedGo.transform.Find("Label").GetComponent<UILabel>().color = go.transform.Find("Label").GetComponent<UILabel>().color;
            }

            StartMenu._instance.serverlistGrid.AddChild(go.transform);//把孩子的坐标传给grid
            index++;
        }
        StartMenu.sp = spDefault;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        PhotonEngine.Instance.OnConnectToServer -= GetServerList;
    }


    public override OperationCode OpCode
    {
        //有get,set方法，就相当于有了一个属性了，默认为opCode
        get { return OperationCode .GetServer ; }
    }
}
