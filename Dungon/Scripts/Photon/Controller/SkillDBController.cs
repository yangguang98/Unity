using UnityEngine;
using System.Collections;
using TaidouCommon;
using System.Collections.Generic;
using TaidouCommon.Model;
using TaidouCommon.Tools;

public class SkillDBController : ControllerBase  {


    public override TaidouCommon.OperationCode OpCode
    {
        get { return OperationCode.SkillDB; }
    }

    public void Get()
    {
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.Get, new Dictionary<byte, object>());
    }
    public void Add(SkillDB skillDB)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        skillDB.Role = null;
        ParameterTool.AddParameter<SkillDB>(parameters, ParameterCode.SkillDB, skillDB);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.Add, parameters);
    }
    public void Update1(SkillDB skillDB)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        skillDB.Role = null;
        ParameterTool.AddParameter<SkillDB>(parameters, ParameterCode.SkillDB, skillDB);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.Update, parameters);
    }

    public void UpGrade(SkillDB skillDB)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        Role role = PhotonEngine.Instance.role;
        role.User = null;
        skillDB.Role = null;
        ParameterTool.AddParameter<SkillDB>(parameters, ParameterCode.SkillDB, skillDB);
        ParameterTool.AddParameter<Role>(parameters, ParameterCode.Role, role);
        print("UpGradeUpGrade");
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.Upgrade, parameters);
    }
    public override void OnOperationResponse(ExitGames.Client.Photon.OperationResponse reponse)
    {
        SubCode subcode = ParameterTool.GetSubCode(reponse.Parameters);
        switch (subcode )
        {
            case SubCode .Upgrade :
                SkillDB skillDB1 = ParameterTool.GetParameter<SkillDB>(reponse.Parameters, ParameterCode.SkillDB);
                if(OnUpgradeSkillDB !=null)
                {
                    OnUpgradeSkillDB(skillDB1);
                }
                break;
            case SubCode .Update :
                if(OnUpdateSkillDB !=null)
                {
                    OnUpdateSkillDB();
                }
                break;
            case SubCode.Get:
                List<SkillDB> list = ParameterTool.GetParameter<List<SkillDB>>(reponse.Parameters, ParameterCode.SkillDBList);
                if (OnGetSkillDBList != null)
                {
                    OnGetSkillDBList(list);
                }
                break;
            case SubCode.Add:
                SkillDB skillDB = ParameterTool.GetParameter<SkillDB>(reponse.Parameters, ParameterCode.SkillDB);
                if (OnGetSkillDBList != null)
                {
                    OnAddSkillDB(skillDB);
                }
                break;
        }
    }


    public event OnGetSkillDBListEvent OnGetSkillDBList;
    public event OnAddSkillDBEvent OnAddSkillDB;
    public event OnUpdateSkillDBEvent OnUpdateSkillDB;
    public event OnUpgradeSkillDBEvent OnUpgradeSkillDB;
}
