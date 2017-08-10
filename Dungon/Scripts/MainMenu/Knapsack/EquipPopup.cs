using UnityEngine;
using System.Collections;
/// <summary>
/// EquipPopup
/// 当某一个装备被点击之后，就会显示该信息框（装备的详细信息）
/// </summary>
public class EquipPopup : MonoBehaviour {

    public PowerShow powerShow;

    private InventoryItem it;//存储传递过来要显示的装备
    private InventoryItemUI itUI;//在OnEquip()中使用
    private KnapsackRoleEquip roleEquip;


    private UISprite equipSprite;
    private UILabel nameLabel;
    private UILabel qualityLabel;
    private UILabel damageLabel;
    private UILabel hpLabel;
    private UILabel powerLabel;
    private UILabel desLabel;
    private UILabel levelLabel;
    private UILabel btnLabel;
    private UIButton upgradeButton;

    private UIButton closeButton;
    private UIButton equipButton;

    private bool isLeft = true;
    void Awake()
    {
        equipSprite=transform.Find ("Bg/Sprite").GetComponent<UISprite>();
        nameLabel=transform.Find ("NameLabel").GetComponent<UILabel>();
        qualityLabel=transform.Find ("QualityLabel/Label").GetComponent <UILabel>();
        damageLabel = transform.Find("DamageLabel/Label").GetComponent<UILabel>();
        hpLabel = transform.Find("HpLabel/Label").GetComponent<UILabel>();
        powerLabel = transform.Find("PowerLabel/Label").GetComponent<UILabel>();
        desLabel = transform.Find("DesLabel").GetComponent<UILabel>();
        levelLabel = transform.Find("LevelLabel/Label").GetComponent<UILabel>();
        closeButton = transform.Find("CloseButton").GetComponent<UIButton>();
        equipButton = transform.Find("EquipButton").GetComponent<UIButton>();
        btnLabel = transform.Find("EquipButton/Label").GetComponent<UILabel>();
        upgradeButton = transform.Find("UpgradeButton").GetComponent<UIButton>();

        EventDelegate ed1 = new EventDelegate(this, "OnClose");
        closeButton.onClick.Add(ed1);

        EventDelegate ed2 = new EventDelegate(this, "OnEquip");
        equipButton.onClick.Add(ed2);

        EventDelegate ed3 = new EventDelegate(this, "OnUpgrade");
        upgradeButton.onClick.Add(ed3);
    }

    public void Show(InventoryItem it,bool isleft,KnapsackRoleEquip roleEquip,InventoryItemUI itUI)
    {
        gameObject.SetActive(true);
        this.it = it;
        this.itUI = itUI;
        this.roleEquip = roleEquip;
        Vector3 pos = transform.localPosition;
        this.isLeft = isleft;
        if(isleft)
        {
            btnLabel.text = "装备";
            transform.localPosition =new Vector3(-Mathf.Abs(pos.x),pos.y,pos.z);
        }
        else
        {
            transform.localPosition = new Vector3(Mathf.Abs(pos.x), pos.y, pos.z);
            
            btnLabel.text = "卸下";
        }

        equipSprite.spriteName= it.Inventory.Icon;
        nameLabel.text = it.Inventory.Name;
        qualityLabel.text =it.Inventory.Quality.ToString();
        damageLabel.text = it.Inventory.Damage.ToString();
        hpLabel.text = it.Inventory.Hp.ToString();
        powerLabel.text = it.Inventory.Power.ToString();
        desLabel.text = it.Inventory.Des;
        levelLabel.text = it.Level.ToString();
    }

    public void OnClose()
    {
        Close();

        //关闭按钮时也要禁用出售按钮

        transform.parent.SendMessage("DisableButton");
    }

    public void Close()
    {
        ClearObject();
        gameObject.SetActive(false);
    }

    public void OnEquip()
    {
        //点击卸下按钮和装备按钮的时候触发

        int startValue = PlayerInfo._instance.GetOverAllPower();//获得角色当前具有的战斗力
        if(isLeft)
        {
            //从背包到身上
            itUI.Clear();//穿上装备的时候，要使背包中的该装备为空，
            PlayerInfo._instance.DressOn(it);
            
        }
        else
        {
            
            //从身上到背包
            roleEquip.Clear();//清空身上装备的显示
            PlayerInfo._instance.DressOff(it);
           
        }
      

        int endValue = PlayerInfo._instance.GetOverAllPower();//穿上装备和卸下装备时所具有的power
        powerShow.ShowPowerChange(startValue, endValue);

        InventoryUi._instance.SendMessage("UpdateCount");//向InventoryUi中的方法发送消息
        OnClose();
        
    }

    public void OnUpgrade()
    {
        //对升级按钮的响应

        int coinNeed = (it.Level + 1) * it.Inventory.Price;//获得需要的金币数
        bool isSuccess = PlayerInfo._instance.GetCoin(coinNeed);//巧妙的设置bool类型
        if (isSuccess)
        {
            it.Level += 1;//InventoryItemDB.level也被改变了
            levelLabel.text = it.Level.ToString();//更新level的显示，
            print("123");
            InventoryManager._instance.UpgradeEquip(it);//降数据更新到服务器
        }
        else
        {
            //给出提示信息
            MessageManager._instance.ShowMessage("金币不足，无法升级");
        }
    }

    void ClearObject()
    {
        //这个it是要在面板中显示的信息，每次点击不通的背包系统就会有不通的显示结果，那么存储的it也不同，每次清空
        it = null;
        itUI = null;
    }
}
