using UnityEngine;
using System.Collections;

public class PlayerBar : MonoBehaviour {

    private UISprite headSprite;
    private UILabel nameLabel;
    private UILabel levelLabel;
    private UISlider energySlider;
    private UISlider toughenSlider;

    private UIButton energyPlusButton;
    private UIButton toughenPlusButton;

    private UILabel energyLabel;
    private UILabel toughenLabel;

    private UIButton headButton;

    void Awake()
    {
        headSprite = transform.Find("HeadSprite").GetComponent<UISprite>();
        nameLabel = transform.Find("NameLabel").GetComponent<UILabel>();
        levelLabel = transform.Find("LevelLabel").GetComponent<UILabel>();
        energySlider = transform.Find("EnergyProgressBar").GetComponent<UISlider>();
        energyLabel = transform.Find("EnergyProgressBar/Label").GetComponent<UILabel>();//看看
        toughenLabel = transform.Find("ToughenProgressBar/Label").GetComponent<UILabel>();

        toughenSlider = transform.Find("ToughenProgressBar").GetComponent<UISlider>();
        energyPlusButton = transform.Find("EnergyPlusButton").GetComponent<UIButton>();
        toughenPlusButton = transform.Find("ToughenPlusButton").GetComponent<UIButton>();
        headButton = transform.Find("HeadButton").GetComponent<UIButton>();
        PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;

        //给button添加点击的事件
        EventDelegate ed = new EventDelegate(this, "OnHeadButtonClick");//看看
        headButton.onClick.Add(ed);
    }
    void Start()
    {
        //单例模式的初始化的地方
        
    }

    void OnDestroy()//这个函数的调用方式（PlayerBar)被销毁的时候，就将注册的事件函数取消掉
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
    }
    void OnPlayerInfoChanged(InfoType type)
    {
        if(type==InfoType.All||type==InfoType.Name||type==InfoType.HeadPortrait||type==InfoType.Level||type==InfoType.Energy||type==InfoType.Toughen)
        {
            UpdateShow();
        }
    }

    void UpdateShow()
    {
        PlayerInfo info = PlayerInfo._instance;
        headSprite.spriteName = info.HeadPortrait;
        levelLabel.text = info.Level.ToString();
        nameLabel.text = info.Name.ToString();
        energySlider.value = info.Energy / 100f;//小数的原因
        energyLabel.text = info.Energy + "/100";
        toughenSlider.value = info.Toughen / 50f;
        toughenLabel.text = info.Toughen + "/50";
    }

    public void OnHeadButtonClick()
    {
        //调用单例模式中的事件，设计成单例模式容易调用
        PlayerStatus._instance.Show();
    }
}
