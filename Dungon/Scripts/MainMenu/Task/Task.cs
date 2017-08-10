using UnityEngine;
using System.Collections;
using TaidouCommon.Model;
using System;


public enum TaskType
{
    Main=0,
    Reward=1,
    Daliy=2,
}


public enum TaskProgress
{
    NoStart=0,
    Accept=1,
    Complete=2,
    Reward=3
}
/// <summary>
/// 服务器端存储的TaskDb和这里的task是不一样的，这里存储的是task的一些基本的信息
/// 加上task的状态（是否完成），服务器端需要存储这个任务的进度和一些其他的信息，下次进入游戏的时候，
/// 直接从服务器端读取这些游戏进度到本地，然后修改对应task的状态
/// </summary>
public class Task {

    
   


    private int id;
    private TaskType taskType;
    private string name;
    private string icon;
    private string des;
    private int coin;
    private int diamond;
    private string talkNpc;
    private int idNpc;
    private int idTranscript;//副本的iD
    private TaskProgress taskProgress = TaskProgress.NoStart;

    public TaskDB TaskDB
    {
        //与数据库进行交互的类，数据库存储的一些信息，要反映到界面上
        get;
        set;
    }

    public delegate void OnTaskChangeEvent();
    public event OnTaskChangeEvent onTaskChange;//用来改变UI的显示，，由于一个系统中含有多个任务，没人任务的显示不同，则为每一个UI都注册一个改变UI的事件

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    

    public TaskType TaskType1
    {
        get { return taskType; }
        set { taskType = value; }
    }
    

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    

    public string Icon
    {
        get { return icon; }
        set { icon = value; }
    }
    

    public string Des
    {
        get { return des; }
        set { des = value; }
    }
    

    public int Coin
    {
        get { return coin; }
        set { coin = value; }
    }
    

    public int Diamond
    {
        get { return diamond; }
        set { diamond = value; }
    }
    

    public string TalkNpc
    {
        get { return talkNpc; }
        set { talkNpc = value; }
    }
    

    public int IdNpc
    {
        get { return idNpc; }
        set { idNpc = value; }
    }
    

    public int IdTranscript
    {
        get { return idTranscript; }
        set { idTranscript = value; }
    }


    public TaskProgress TaskProgress1
    {
        get { return taskProgress; }
        set
        {
            if (taskProgress != value)
            {
                //若任务的进度发生变化，则触发事件

                taskProgress = value;
                onTaskChange();
            }
        }
    }

    public void SyncTask(TaskDB taskDB)
    {
        //将服务器信息同步到本地
        this.TaskDB = taskDB;
        taskProgress = (TaskProgress)taskDB.State;
    }

    public void UpdateTask(TaskManager taskManager)
    {
        //当接受一个任务后，对应的task中的属性就被改变，将更新的信息传递到TaskDB中，便于与服务器交互
        if(TaskDB==null)
        {
            TaskDB = new TaskDB();
            TaskDB.TaskID = id;
            TaskDB.State = (int)taskProgress;
            TaskDB.LastUpdateTime = new DateTime();
            TaskDB.Type = (int)taskType;
            taskManager.taskDBController.AddTaskDB(TaskDB);//发起添加任务的请求
        }
        else
        {
            this.TaskDB.State = (int)taskProgress;
            taskManager.taskDBController.AddTaskDB(this.TaskDB);
        }
    }
}
