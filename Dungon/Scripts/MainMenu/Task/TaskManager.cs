using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TaidouCommon.Model;

//关于Task有两个类与其对应，一个是与数据库对应的TaskDB，用来存储任务的状态，进度，属于哪个角色，另外一个类是task,用来负责显示与UI进行交互

public class TaskManager : MonoBehaviour {

    public static TaskManager _instance;
    public TextAsset taskInfoText;

    private ArrayList taskList = new ArrayList();
    private Dictionary <int,Task > taskDict=new Dictionary <int,Task>();

    private Task currentTask;

    private PlayerAutoMove playerAutoMove;

    private PlayerAutoMove PlayerAutoMove
    {
        //私有的方法，去得到playerAutoMove get方法
        get
        {
             if(playerAutoMove ==null)
             {
                 
                 playerAutoMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAutoMove>();
             }
             return playerAutoMove;
        }
    }

    public TaskDBController taskDBController;
    public event OnSyncTaskCompleteEvent OnSyncTaskComplete;

    void Awake()
    {
        _instance = this;
        taskDBController = this.GetComponent<TaskDBController>();
        InitTask();

        taskDBController.GetTaskDBList();//向服务器发起请求，得到任务列表


        taskDBController.OnAddTaskDB += this.OnAddTaskDB;
        taskDBController.OnGetTaskDBList += this.OnGetTaskDBList;
        taskDBController.OnUpdateTaskDB += this.OnUpdateTaskDB;


       // playerAutoMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAutoMove>();
    }

    public void OnGetTaskDBList(List<TaskDB> list)
    {
        if(list==null) return;
        foreach(TaskDB taskDB in list)
        {
            //每一个TaskDB对应于一个Task，通过TaskDb.TaskID来对应，则服务器对于每一个任务都能够保存其进度信息
            Task task = null;
            if(taskDict.TryGetValue(taskDB.TaskID, out task))
            {
                task.SyncTask (taskDB);
            }
        }

        if(OnSyncTaskComplete!=null)
        {
            //当与服务器交互完成后，就与UI交互，完成显示的更新
            OnSyncTaskComplete();
        }

    }

    public void OnAddTaskDB(TaskDB taskDB)
    {
        //每一个TaskDB对应于一个Task，通过TaskDb.TaskID来对应，则服务器对于每一个任务都能够保存其进度信息
        Task task = null;
        if (taskDict.TryGetValue(taskDB.TaskID, out task))
        {
            task.SyncTask(taskDB);
        }
    }

    public void OnUpdateTaskDB()
    {

    }
   
    /// <summary>
    /// 初始化任务信息
    /// </summary>
    public void InitTask()
    {
        string[] taskInfoArray = taskInfoText.ToString().Split('\n');
        foreach(string str in taskInfoArray )
        {
            string []proArray = str.Split('|');
            Task task = new Task();
            task.Id = int.Parse(proArray[0]);
            switch(proArray[1])
            {
                case"Main":
                    task.TaskType1 = TaskType.Main;
                    break;
                case"Reward":
                    task.TaskType1 = TaskType.Reward;
                    break;
                case"Daliy":
                    task.TaskType1 = TaskType.Daliy;
                    break;
            }



            task.Name = proArray[2];
            task.Icon = proArray[3];
            task.Des = proArray[4];
            task.Coin = int.Parse(proArray[5]);
            task.Diamond = int.Parse(proArray[6]);
            task.TalkNpc = proArray[7];
            task.IdNpc =int.Parse(proArray [8]);
            task.IdTranscript =int.Parse(proArray [9]);

            taskList.Add(task);
            taskDict.Add(task.Id, task);
        }
    }
     public ArrayList GetTaskList()
    {
        return taskList;
    }


     public void OnExecuteTask(Task task)
     {
         //执行某个任务 ,在UI上点击下一步某个任务，设置currentTask，这个时候还没有将信息同步到服务器端，然后移动到NPC,如果某个任务已经被接受，则移动到副本出口处

         currentTask = task;
         if(task.TaskProgress1 ==TaskProgress .NoStart)
         {
             //导航到NPC那里去接受任务
             PlayerAutoMove.SetDestination(NPCManager._instance.GetNpcById(task.IdNpc).transform .position);//这里的物体如何不可以用GetComponent<>
         }else if(task.TaskProgress1 ==TaskProgress.Accept )
         {
             PlayerAutoMove.SetDestination(NPCManager._instance.transcriptGo .transform .position);
         }
     }

    public void OnAcceptTask()
     {
        //接受了某个任务
         currentTask.TaskProgress1 = TaskProgress.Accept;//属性发生了改变
         currentTask.UpdateTask(this);//与服务器交互，当接受了一个任务之后，将相应的信息存储到服务器端
         PlayerAutoMove.SetDestination(NPCManager._instance.transcriptGo.transform.position);//接受任务之后，寻路到副本入口
     }

    public void OnArriveDestination()
    {
        //到达了目的地
        if(currentTask ==null)
        {
            TranscriptMapUI._instance.Show();
        }
        else
        {
            if (currentTask.TaskProgress1 == TaskProgress.NoStart)
            {
                //到达NPC

                //currentTask.TaskProgress1 = TaskProgress.Accept;
                NPCDialogUI._instance.Show(currentTask.TalkNpc);
            }
            else
            {
                //到达副本入口
                TranscriptMapUI._instance.Show();//显示地图
                TranscriptMapUI._instance.ShowTranscriptEnter(currentTask.IdTranscript);//自动显示进入哪一个副本地图，根据当前任务的transcriptid
            }
        }

        
        
    }
}
