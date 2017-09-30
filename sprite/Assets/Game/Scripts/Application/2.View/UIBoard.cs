using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//每一个UI可以有自己关心的事件，event可以有Controller来执行也可以由View本身来执行（HandleEvent)
public class UIBoard : View
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    public Text txtScore;
    public Image imgRoundInfo;
    public Text txtCurrent;
    public Text txtTotal;
    public Image imgPauseInfo;
    public Button btnSpeed1;
    public Button btnSpeed2;
    public Button BtnResume;
    public Button btnPause;
    public Button btnSystem;

    bool isPlaying = false;//是否正在运行，
    GameSpeed speed = GameSpeed.One;//游戏速度
    int score = 0;

    
    
    
    #endregion 

    #region 属性

    public int Score
    {
        get { return score; }
        set 
        { 
            score = value;
            txtScore.text = value.ToString();//修改UI显示
        }
    }

    public override string Name
    {
        get
        {
            return Consts.v_Board;
        }
    }

    public bool IsPlaying
    {
        get { return isPlaying; }
        set 
        {
            isPlaying = value;

            //修改UI显示
            imgRoundInfo.gameObject.SetActive(value);
            imgPauseInfo.gameObject.SetActive(!value);
        }
    }

    public GameSpeed Speed
    {
        get { return speed; }
        set { 
            speed = value;

            //修改UI显示
            btnSpeed1.gameObject.SetActive(speed == GameSpeed.One);
            btnSpeed2.gameObject.SetActive(speed == GameSpeed.Two);
        }
    }
    #endregion

    #region 方法
    public void UpateRoundInfo(int currentRound,int totalRound)
    {
        txtCurrent.text = currentRound.ToString("D2");//始终保留两位整数，不够则前面一位为零
    }

    public override void RegisterEvents()
    {
        //注册关心的事件
      //  this.AttentionEvents.Add(Consts.E_CountDownComplete);//注册关心的事件
    }
    #endregion

    #region Unity回调
    void awake()
    {
        this.Score = 0;
        this.IsPlaying = true;
        this.Speed = GameSpeed.One;
    }
    #endregion

    #region 事件回调
    public override void HandlerEvent(string eventName, object data)
    {
        //处理本View所关心的事件
        switch (eventName)
        {
            //case Consts.E_CountDownComplete :
            //    Game.Instance.LoadScene(4);
            //    break;
        }
    }

    public void OnSpeed1Click()
    {
        Speed = GameSpeed.One;
    }

    public void OnSpeed2Click()
    {
        Speed = GameSpeed.Two;
    }

    public void OnPauseClick()
    {
        IsPlaying = false;
    }

    public void OnResumeClick()
    {
        IsPlaying = true;
    }

    public void OnSystemClick()
    {

    }

    #endregion

    #region 帮助方法
    #endregion


    

    
}
