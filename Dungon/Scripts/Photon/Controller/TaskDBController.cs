using UnityEngine;
using System.Collections;
using TaidouCommon;
using System.Collections.Generic;
using TaidouCommon.Model;
using TaidouCommon.Tools;
//服务器端存储任务的进度信息
public class TaskDBController : ControllerBase  {


    public override TaidouCommon.OperationCode OpCode
    {
        get { return OperationCode.TaskDB; }
    }

    public void GetTaskDBList()
    {
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.GetTaskDB, new Dictionary<byte, object>());
    }

    public void AddTaskDB(TaskDB taskDB)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        taskDB.Role = null;
        ParameterTool.AddParameter<TaskDB>(parameters, ParameterCode.TaskDB, taskDB);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.AddTaskDB, parameters);
    }

    public void UpdateTaskDB(TaskDB taskDB)
    {
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        taskDB.Role = null;
        ParameterTool.AddParameter<TaskDB>(parameters, ParameterCode.TaskDB, taskDB);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.UpdateTaskDB, parameters);
    }

    public override void OnOperationResponse(ExitGames.Client.Photon.OperationResponse reponse)
    {
        SubCode subcode=ParameterTool .GetParameter <SubCode >(reponse .Parameters ,ParameterCode.SubCode,false);
        switch(subcode)
        {
            case SubCode .AddTaskDB :
                TaskDB taskDB = ParameterTool.GetParameter<TaskDB>(reponse.Parameters, ParameterCode.TaskDB);
                if (OnAddTaskDB != null)
                {
                    OnAddTaskDB(taskDB);
                }
                break;
            case SubCode .UpdateTaskDB :
                if(OnUpdateTaskDB !=null)
                {
                    OnUpdateTaskDB();
                }
                break;
            case  SubCode.GetTaskDB :
                List<TaskDB> list = ParameterTool.GetParameter<List<TaskDB>>(reponse.Parameters, ParameterCode.TaskDBList);
                if(OnGetTaskDBList !=null)
                {
                    OnGetTaskDBList(list);
                }
                break;
        }
    }

    public event OnGetTaskDBListEvent OnGetTaskDBList;//OnGetTaskDBList相当于一个变量，然后在这个变量声明注册函数
    public event OnAddTaskDBEvent OnAddTaskDB;
    public event OnUpdateTaskDBEvent OnUpdateTaskDB;
}
