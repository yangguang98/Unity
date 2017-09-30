using UnityEngine;
using UnityEngine.UI ;
using System.Collections;

public class UICountDown : View
{


    

    

    #region 常量
    #endregion

    #region 事件
    #endregion 

    #region 字段
    public Image Count;
    public Sprite[] Numbers;
    #endregion

    #region 属性

    public override string Name
    {
        get
        {
            return Consts.V_CountDown;
        }
    }
    #endregion

    #region 方法

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void StartCountDown()
    {
        Show();
        StartCoroutine("DisplayCount");
    }

    IEnumerator  DisplayCount()
    {
        int count=3;
        while(count >0)
        {

            //显示
            Count.sprite = Numbers[count - 1];//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            yield return new WaitForSeconds(1);//等待一秒
            count--;
        }

        Hide();


        SendEvent(Consts.E_CountDownComplete);//倒计时结束,UIBoard关心了E_ContDownComplete事件，，在其中加载第四个场景
    }
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调

    public override void RegisterEvents()
    {
        this.AttentionEvents.Add(Consts.E_EnterScene);//关心这个事件，这个事件在EncterSceneCommand那里也有注册，说明可以一个事件有两个执行路径，即控制器和视图
    }

    public override void HandlerEvent(string eventName, object data)
    {
        //刚刚开始的时候，这个游戏物体不是active的，在进入一个场景的时候，将这个场景中所有视图都注册了（不管这个物体是active,还是非active,通过transform.find来寻找），在注册视图的同时，也注册该视图所关心的事件。当一个场景下的视图关心了EnterScene事件，在进入场景的时候执行该事件


        //这里利用case 可以处理很多事件，
        switch(eventName )
        {
            case Consts.E_EnterScene:
                SceneArgs e = (SceneArgs)data;
                if(e.SceneIndex ==3)
                {
                    StartCountDown();
                }
                break;
        }
    }
    #endregion

    #region 帮助方法
    #endregion



}
