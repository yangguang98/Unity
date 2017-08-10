using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {


    public static PlayerStatus _instance;
    private UISprite headSprite;
    private UILabel levelLabel;
    private UILabel nameLabel;
    private UILabel powerLabel;
    private UISlider expSlider;
    private UILabel expLabel;
    private UILabel diamondLabel;
    private UILabel coinLabel;
    private UILabel energyLabel;
    private UILabel energyRestorePartLabel;
    private UILabel energyRestoreAllLabel;
    private UILabel toughenLabel;
    private UILabel toughenRestorePartLabel;
    private UILabel toughenRestoreAllLabel;

    private TweenPosition tween;
    private UIButton closeButton;

    private UIButton changeNameButton;
    private GameObject changeNameGo;
    private UIInput nameInput;
    private UIButton sureButton;
    private UIButton cancelButton;

    void Awake()
    {
        headSprite = transform.Find("HeadSprite").GetComponent<UISprite>();
        levelLabel = transform.Find("LevelLabel").GetComponent<UILabel>();
        nameLabel = transform.Find("NameLabel").GetComponent<UILabel>();
        powerLabel = transform.Find("PowerLabel/Label").GetComponent<UILabel>();
        expSlider = transform.Find("ExpProgressBar").GetComponent<UISlider>();
        expLabel=transform.Find("ExpProgressBar/Label").GetComponent <UILabel>();
        diamondLabel=transform.Find("Diamond/Label1").GetComponent <UILabel>();
        coinLabel = transform.Find("Coin/Label1").GetComponent<UILabel>();
        energyLabel = transform.Find("EnergyLabel/NumberLabel").GetComponent<UILabel>();
        energyRestorePartLabel = transform.Find("EnergyLabel/Label/RestorePartTime").GetComponent<UILabel>();
        energyRestoreAllLabel = transform.Find("EnergyLabel/Label1/RestoreAllTime").GetComponent<UILabel>();
        toughenLabel = transform.Find("ToughenLabel/NumberLabel").GetComponent<UILabel>();
        toughenRestorePartLabel = transform.Find("ToughenLabel/Label/RestorePartTime").GetComponent<UILabel>();
        toughenRestoreAllLabel = transform.Find("ToughenLabel/Label1/RestoreAllTime").GetComponent<UILabel>();
        closeButton = transform.Find("ButtonClose").GetComponent<UIButton>();
        changeNameButton = transform.Find("ChangeNameButton").GetComponent<UIButton>();
        changeNameGo = transform.Find("ChangeNameBg").gameObject;
        nameInput = transform.Find("ChangeNameBg/NameInput").GetComponent<UIInput>();
        sureButton = transform.Find("ChangeNameBg/SureButton").GetComponent<UIButton>();
        cancelButton = transform.Find("ChangeNameBg/CancelButton").GetComponent<UIButton>();

        changeNameGo.SetActive(false);
        EventDelegate ed = new EventDelegate(this, "OnButtonCloseClick"); 
        closeButton.onClick.Add(ed);

        EventDelegate ed2 = new EventDelegate(this, "OnButtonChangeNameClick");
        changeNameButton.onClick.Add(ed2);

        EventDelegate ed3 = new EventDelegate(this, "OnButtonSureClick");
        sureButton.onClick.Add(ed3);

        EventDelegate ed4 = new EventDelegate(this, "OnButtonCancelClick");
        cancelButton.onClick.Add(ed4);

        PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
        _instance = this;
        tween = this.GetComponent<TweenPosition>();

    }
    void Update()
    {
        UpdateEnergyAndToughenShow();
    }

    void OnDestroy()
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
    }


    void OnPlayerInfoChanged(InfoType type)
    {
        //每一帧都去调用更新，去冬天的更新
        UpdateShow();
    }

    void UpdateShow()
    {
        PlayerInfo info = PlayerInfo._instance;
        headSprite.spriteName = info.HeadPortrait;
        levelLabel.text = info.Level.ToString();
        nameLabel.text = info.Name;
        int requireExp = GameController.GetRequireExpByLevel(info.Level + 1);
        expSlider.value = (float)info.Exp / requireExp;
        expLabel.text = info.Exp + "/" + requireExp;
        diamondLabel.text = info.Diamond.ToString();
        coinLabel.text = info.Coin.ToString();

        UpdateEnergyAndToughenShow();//修改体力和历练的倒计时
    }

    void UpdateEnergyAndToughenShow()
    {
        PlayerInfo info = PlayerInfo._instance;
        energyLabel.text = info.Energy + "/100";
        if(info.Energy>=100)
        {
            energyRestorePartLabel.text = "00:00:00";
            energyRestoreAllLabel.text = "00:00:00";
        }
        else
        {
            int remainTime = 60 - (int)info.energyTimer;
            string str = remainTime <= 9 ? "0" + remainTime : remainTime.ToString();
            energyRestorePartLabel.text ="00:00" +":"+ str;;


            int minutes = 99 - info.Energy;
            int hours = minutes / 60;
            minutes = minutes % 60;

            string hoursStr = hours <= 9 ? "0" + hours : hours.ToString();
            string minutesStr = minutes <= 9 ? "0" + minutes : minutes.ToString();
            energyRestoreAllLabel.text = hoursStr + ":" + minutesStr + ":" + str;

        }

        toughenLabel.text = info.Toughen + "/50";
        if (info.Toughen >= 50)
        {
            toughenRestorePartLabel.text = "00:00:00";
            toughenRestoreAllLabel.text = "00:00:00";
        }
        else
        {
            int remainTime = 60 - (int)info.toughenTimer;
            string str = remainTime <= 9 ? "0" + remainTime : remainTime.ToString();
            toughenRestorePartLabel.text = "00:00" + ":" + str; ;
            

            int minutes = 49 - info.Toughen;
            int hours = minutes / 60;
            minutes = minutes % 60;

            string hoursStr = hours <= 9 ? "0" + hours : hours.ToString();
            string minutesStr = minutes <= 9 ? "0" + minutes : minutes.ToString();
            toughenRestoreAllLabel.text = hoursStr + ":" + minutesStr + ":" + str;

        }


    }

    public void Show()
    {
        tween.PlayForward();
    }

    public void OnButtonCloseClick()
    {
        tween.PlayReverse();
    }

    public void OnButtonChangeNameClick()
    {
        changeNameGo.SetActive(true);
    }

    public void OnButtonSureClick()
    {
        //1 联网校验名字是否争正确
        //TODO

        PlayerInfo._instance.ChangeName(nameInput.value);
        changeNameGo.SetActive(false);
    }

    public void OnButtonCancelClick()
    {
        changeNameGo.SetActive(false);
    }

}
