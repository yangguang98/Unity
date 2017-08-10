using UnityEngine;
using System.Collections;

public class PlayerBarTranscript : MonoBehaviour {

    private UISprite headSprite;
    private UILabel nameLabel;
    private UILabel levelLabel;
    private UISlider HpSlider;
    private UISlider energySlider;

    private UIButton energyPlusButton;
    private UIButton toughenPlusButton;

    private UILabel HpLabel;
    private UILabel energyLabel;

    private UIButton headButton;

    void Awake()
    {
        headSprite = transform.Find("HeadSprite").GetComponent<UISprite>();
        nameLabel = transform.Find("NameLabel").GetComponent<UILabel>();
        levelLabel = transform.Find("LevelLabel").GetComponent<UILabel>();
        energySlider = transform.Find("EnergyProgressBar").GetComponent<UISlider>();
        energyLabel = transform.Find("EnergyProgressBar/Label").GetComponent<UILabel>();//看看
        HpLabel = transform.Find("HpProgressBar/Label").GetComponent<UILabel>();

        HpSlider = transform.Find("HpProgressBar").GetComponent<UISlider>();
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
        UpdateShow();//OnPlayerInfoChanged在刚刚开始的时候没有起作用，因为其处于第三个场景，当跳入到第三个场景是，需要将其初始化
        TranscriptManager._instance.player.GetComponent<PlayerAttack>().OnPlayerHpChange += this.OnPlayerHpChange;
    }

   

    void OnDestroy()//这个函数的调用方式（PlayerBar)被销毁的时候，就将注册的事件函数取消掉
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
        TranscriptManager._instance.player.GetComponent<PlayerAttack>().OnPlayerHpChange -= this.OnPlayerHpChange;
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
        HpSlider.value = info.Hp / info.Hp ;
        HpLabel.text = info.Hp + "/"+info.Hp;
    }

    public void OnHeadButtonClick()
    {
        //调用单例模式中的事件，设计成单例模式容易调用
        PlayerStatus._instance.Show();
    }

    public void OnPlayerHpChange(float hp)
    {
        PlayerInfo info = PlayerInfo._instance;
        HpSlider.value = (float)hp / info.Hp;
        HpLabel.text = hp + "/" + info.Hp;
    }
}
