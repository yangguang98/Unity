using UnityEngine;
using System.Collections;

public class SkillUI : MonoBehaviour {

    public static SkillUI _instance;
    private UILabel skillNameLabel;
    private UILabel skillDesLabel;
    private UIButton closeButton;
    private UIButton upgradeButton;
    private UILabel upgradeButtonLabel;
    private TweenPosition tween;

    private Skill skill;

    void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenPosition>();
        skillNameLabel = transform.Find("SkillNameLabel").GetComponent<UILabel>();
        skillDesLabel = transform.Find("Bg/DesLabel").GetComponent<UILabel>();
        closeButton = transform.Find("CloseButton").GetComponent<UIButton>();
        upgradeButtonLabel = transform.Find("UpgradeButton/Label").GetComponent<UILabel>();
        upgradeButton = transform.Find("UpgradeButton").GetComponent<UIButton>();

        skillNameLabel.text = "";
        skillDesLabel.text = "";
        DisableUpgradeButton("选择技能");

        EventDelegate ed=new EventDelegate (this,"OnUpgrade");
        upgradeButton .onClick .Add(ed);

        EventDelegate ed1 = new EventDelegate(this, "OnClose");
        closeButton.onClick.Add(ed1);


    }


    void DisableUpgradeButton(string label="")
    {
        //禁用按钮

        upgradeButton.SetState(UIButton.State.Disabled,true);//禁用Button按钮
        upgradeButton.gameObject.GetComponent<Collider>().enabled = false;
        if(label!=" ")
        {
            upgradeButtonLabel.text = label;
        }
    }

    void EnabelUpgradeButton(string label="")
    {
        //启用按钮

        upgradeButton.SetState(UIButton.State.Normal , true);//禁用Button按钮
        upgradeButton.gameObject.GetComponent<Collider>().enabled = true;
        if (label != " ")
        {
            upgradeButtonLabel.text = label;
        }
    }

    void OnSkillClick(Skill skill)
    {
        //当点击了技能后，通过判定金币的数量，看是否能进行升级，从而设置升级按钮的状态
        this.skill = skill;
        PlayerInfo info = PlayerInfo._instance;
        if(500*(skill.Level +1)<=info.Coin)
        {
            //金币数够了，可以升级

            if(skill.Level <info.Level )
            {
                //只是改变显示，没有改变具体的数据

                EnabelUpgradeButton("升级");
            }
            else
            {
                DisableUpgradeButton("最大等级");
            }
            
        }
        else
        {
            DisableUpgradeButton("金币不足");
        }
        skillNameLabel.text = skill.Name + " Lv." + skill.Level;
        skillDesLabel.text = "当前技能的攻击力为：" + (skill.Damage * skill.Level); 
    }

    void OnUpgrade()
    {
        //升级按钮点击事件
        PlayerInfo info=PlayerInfo._instance ;
        if(skill.Level <info.Level )
        {
            int coinNeed = (500 * (skill.Level + 1));
            bool isSuccess = info.GetCoin(coinNeed);//减去金币的操作也是放到任务信息中午处理（金币是任务的属性）11111111

            if(isSuccess)
            {
                skill.Upgrade();//技能的升级操作放到技能中去处理（自己的属性）11111111


                SkillManager._instance.SyncDataBase(skill);//同步到数据库

                OnSkillClick(skill);//升级之后，更新显示
            }
            else
            {
                DisableUpgradeButton("金币不足");
            }
        }
        else
        {
            DisableUpgradeButton("最大等级");
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
        //关闭的时候去调用Hide()方法

        Hide();
    }

}
