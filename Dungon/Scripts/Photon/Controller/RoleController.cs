using UnityEngine;
using System.Collections;
using TaidouCommon;
using System.Collections.Generic;
using TaidouCommon.Tools;
using TaidouCommon.Model;
using LitJson;

public class RoleController : ControllerBase {


    public override TaidouCommon.OperationCode OpCode
    {
        get { return OperationCode.Role ; }
    }

    public void GetRole()
    {

        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        parameters.Add((byte)ParameterCode.SubCode, SubCode.GetRole);//数据的类型为ParameterCode.SubCode，具体的值为SubCode.GetRole
        PhotonEngine .Instance .SendRequest (OperationCode.Role ,parameters);
    }

    public void AddRole(Role role)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();

        parameters.Add((byte)ParameterCode.SubCode, SubCode.AddRole );

        parameters.Add((byte)ParameterCode.Role, JsonMapper.ToJson(role));
        PhotonEngine.Instance.SendRequest(OperationCode.Role, parameters);
    }
    public  void SelectRole(Role role)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool .AddParameter <Role>(parameters ,ParameterCode.Role ,role);
        PhotonEngine.Instance.SendRequest(OperationCode.Role, SubCode.SelectRole, parameters);
    }
    public void UpdateRole(Role role)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter<Role>(parameters, ParameterCode.Role, role);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.UpdateRole, parameters);
    }

    public override void OnOperationResponse(ExitGames.Client.Photon.OperationResponse reponse)
    {
        //parameter中含有两个参数，一个是parameterCode.SubCode具体对应的值，
         //                     一个是parameterCode.SubCode具体对应的值和具体的数据组成的键值对
        SubCode subcode = ParameterTool.GetParameter<SubCode>(reponse.Parameters, ParameterCode.SubCode,false);
        switch(subcode)
        {
            case SubCode.GetRole :  //getRole对应的为RoleList
                List<Role> list = ParameterTool.GetParameter<List<Role>>(reponse.Parameters, ParameterCode.RoleList);
                OnGetRole(list);//回调做相应的处理，在startMenu中的awake中注册和定义的的
                break;
            case SubCode.AddRole  ://AddRole对应的为Role
                Role role=ParameterTool .GetParameter <Role>(reponse.Parameters , ParameterCode.Role );
                OnAddRole(role);//回调做相应的处理，在startMenu中的awake中注册和定义的
                break;
            case SubCode .SelectRole :
                if(OnSelectRole!=null)
                {
                    OnSelectRole();
                }
                break;
        }
    }

    public event OnGetRoleEvent OnGetRole;
    public event OnAddRoleEvent OnAddRole;
    public event OnSelectRoleEvent OnSelectRole;
}
