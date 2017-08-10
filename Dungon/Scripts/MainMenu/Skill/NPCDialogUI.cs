using UnityEngine;
using System.Collections;

public class NPCDialogUI : MonoBehaviour {

    public static NPCDialogUI _instance;
    private TweenPosition tween;
    private UILabel npcTalkLabel;
    private UIButton acceptButton;


    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        tween = this.GetComponent<TweenPosition>();
        npcTalkLabel = transform.Find("Label").GetComponent<UILabel>();
        acceptButton = transform.Find("AcceptButton").GetComponent<UIButton>();

        EventDelegate ed = new EventDelegate(this, "OnAccept");
        acceptButton.onClick.Add(ed);
    }

    public void Show(string npcTalk)
    {
        npcTalkLabel.text = npcTalk;
        tween.PlayForward();
    }

    public void Hide()
    {
      
    }

    void OnAccept()
    {

        TaskManager._instance.OnAcceptTask();//通知任务管理器已经接受任务
        tween.PlayReverse();
    }
}
