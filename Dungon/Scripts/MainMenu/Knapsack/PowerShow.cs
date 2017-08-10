using UnityEngine;
using System.Collections;

public class PowerShow : MonoBehaviour {

    private float startValue = 0;
    private int endValue =0;

    private bool isStart =false;
    private TweenAlpha tween;
    private UILabel numLabel;

    public int speed = 100;

    private bool isUp = true;

    void Awake()
    {
        numLabel = transform.Find("Label").GetComponent<UILabel>();
        tween = this.gameObject.GetComponent<TweenAlpha>();

        EventDelegate ed = new EventDelegate(this, "OnTweenFinished");
        tween.onFinished.Add(ed);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if(isStart)
        {
            if(isUp)
            {
                startValue += speed * Time.deltaTime;//看看这个代码。。。。。。。一秒可以变化这么多，一帧的事件就是相应的相乘
                
                if(startValue>endValue)
                {
                    startValue = endValue;//达到最后需要的数值，不然可能有误差
                    isStart = false;
                    tween.PlayReverse();
                }
            }
            else
            {
                startValue -= speed * Time.deltaTime;
               
                if (startValue < endValue)
                {
                    startValue = endValue;
                    isStart = false;
                    tween.PlayReverse();
                }
            }

            numLabel.text = (int)startValue + "";

        }
    }

    public void ShowPowerChange(int startValue,int endValue)
    {
        gameObject.SetActive(true);
        tween.PlayForward();
        this.startValue = startValue;
        this.endValue = endValue;
        if(endValue >startValue )
        {
            isUp = true;
        }
        else
        {
            isUp = false;
        }
        isStart = true;
    }

    public void OnTweenFinished()
    {
        //动画显示和隐藏的播放完，都会触发这个方法，因此要去判断这个isStart
        if(isStart==false)
        {
            gameObject.SetActive(false);
        }
    }
}
