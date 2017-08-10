using UnityEngine;
using System.Collections;

public class TimerDialog : MonoBehaviour {

    private UILabel stateLabel;
    private UILabel timerLabel;
    private UIButton cancelButton;
    private TweenScale tweenScale;
    public int time=10;
    private float timer = 0;
    private bool isStart = false;

    void Start()
    {
        stateLabel = transform.Find("StateLabel").GetComponent<UILabel>();
        timerLabel = transform.Find("TimerLabel").GetComponent<UILabel>();
        cancelButton = transform.Find("CancelButton").GetComponent<UIButton>();
        tweenScale = this.GetComponent<TweenScale>();
        EventDelegate ed = new EventDelegate(this, "OnCancelButtonClick");
        cancelButton.onClick.Add(ed);
    }

    void Update()
    {
        if(isStart)
        {
            timer += Time.deltaTime;
            int remainTime = (int)(time - timer);
            timerLabel.text = remainTime.ToString();
            if(timer>time)
            {
                timer = 0;
                isStart = false;
                OnTimeEnd();
            }
        }
    }

    public void StartTimer()
    {
        //显示组队的倒计时器
        tweenScale.PlayForward();
        timer = 0;
        isStart = true;
    }

    public void HideTimer()
    {
        tweenScale.PlayReverse();
        isStart = false;
    }

    void OnCancelButtonClick()
    {
        //点击取消组队按钮
        HideTimer();
        transform.parent.gameObject.SendMessage("OnCancelTeam");//发送消息
    }

    void OnTimeEnd()
    {
        //时间结束
        HideTimer();
        transform.parent.gameObject.SendMessage("OnCancelTeam");//发送消息
    }
}
