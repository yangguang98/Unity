using UnityEngine;
using System.Collections;

public class TaskUI : MonoBehaviour {

    public static TaskUI _instance;
    private UIGrid tasklistGrid;
    public GameObject taskItemPrefab;
    private TweenPosition tween;
    
    private UIButton closeButton;


    void Awake()
    {
        _instance = this;
        tween = this.gameObject.GetComponent<TweenPosition>();
        closeButton = transform.Find("CloseButton").GetComponent<UIButton>();
        tasklistGrid = transform.Find("ScrollView/Grid").GetComponent<UIGrid>();

        EventDelegate ed = new EventDelegate(this, "OnClose");
        closeButton.onClick.Add(ed);
    }
    void Start()
    {
        TaskManager._instance.OnSyncTaskComplete += this.OnSyncTaskComplete;
        //InitTaskList();
    }

    //这个函数用来初始化Task的UI信息，TaskManager._instance.GetTaskList中的信息已经是从服务器端得到的数据
    public void OnSyncTaskComplete()
    {
        InitTaskList();
    }
    /// <summary>
    ///初始化任务列表信息
    /// </summary>
    void InitTaskList()
    {
        ArrayList taskList = TaskManager._instance.GetTaskList();

        
        foreach(Task task in taskList)
        {
            //利用task的信息去给一个物体初始化

            GameObject go=NGUITools.AddChild(tasklistGrid.gameObject,taskItemPrefab);
            tasklistGrid.AddChild(go.transform);//让Grid对新添加的物体排序
            TaskItemUI ti=go.GetComponent <TaskItemUI>();
            ti.SetTask (task);
        }
    }

    public void Show()
    {
        tween.PlayForward();
    }

    public void Hide()
    {
        tween.PlayReverse();
    }

    void OnClose()
    {
        Hide();
    }
}
