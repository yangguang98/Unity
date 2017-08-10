using UnityEngine;
using System.Collections;

public class MessageManager : MonoBehaviour {

    public static MessageManager _instance;
    private UILabel messageLabel;
    private TweenAlpha tween;
    private bool isSetActive;
    

    void Awake()
    {
        _instance = this;
        messageLabel = transform.Find("Label").GetComponent<UILabel>();
        tween = this.GetComponent<TweenAlpha>();

        EventDelegate ed = new EventDelegate(this, "OnTweenFinished");
        tween.onFinished.Add(ed);//为动画结束添加响应事件

        gameObject.SetActive(false);
    }

    public void ShowMessage(string message,float time=1)
    {
        gameObject.SetActive(true);
        StartCoroutine(Show(message, time));//开启协程
    }

    IEnumerator Show(string message,float time)
    {
        
        isSetActive = true;
        tween.PlayForward();
        messageLabel.text = message;

        yield return new WaitForSeconds(time);//让显示等待时间time

        isSetActive = false;
        tween.PlayReverse();
    }

    public void OnTweenFinished()
    {
        //监听动画结束（但是动画结束有两种情况 PlayForward和playReverse）要等到plyerever后，将物体设置为false

        if(isSetActive ==false)
        {
            gameObject.SetActive(false);
        }
    }
}
