using UnityEngine;
using System.Collections;

public class KnapsackRole : MonoBehaviour {

    private KnapsackRoleEquip helmEquip;
    private KnapsackRoleEquip clothEquip;
    private KnapsackRoleEquip weaponEquip;
    private KnapsackRoleEquip shoesEquip;
    private KnapsackRoleEquip necklaceEquip;
    private KnapsackRoleEquip braceletEquip;
    private KnapsackRoleEquip ringEquip;
    private KnapsackRoleEquip wingEquip;

    private UILabel hpLabel;
    private UILabel damageLabel;
    private UILabel expLabel;
    private UISlider expSlider;

    void Awake()
    {
        helmEquip = transform.Find("HelmSprite").GetComponent<KnapsackRoleEquip >();
        clothEquip = transform.Find("ClothSprite").GetComponent<KnapsackRoleEquip >();
        weaponEquip = transform.Find("WeaponSprite").GetComponent<KnapsackRoleEquip >();
        shoesEquip = transform.Find("ShoesSprite").GetComponent<KnapsackRoleEquip >();
        necklaceEquip = transform.Find("NecklaceSprite").GetComponent<KnapsackRoleEquip >();
        braceletEquip = transform.Find("BraceletSprite").GetComponent<KnapsackRoleEquip >();
        ringEquip = transform.Find("RingSprite").GetComponent<KnapsackRoleEquip >();
        wingEquip = transform.Find("WingSprite").GetComponent<KnapsackRoleEquip >();


        hpLabel = transform.Find("HpBg/Label").GetComponent<UILabel>();
        damageLabel = transform.Find("DamageBg/Label").GetComponent<UILabel>();
        expSlider = transform.Find("ExpSlider").GetComponent<UISlider>();
        expLabel = transform.Find("ExpSlider/Label").GetComponent<UILabel>();
        PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
    }


    void OnDestroy()
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
    }
    void OnPlayerInfoChanged(InfoType type)
    {
        if(type==InfoType.All||type==InfoType.Damage||type==InfoType .Exp||type==InfoType .Hp||type==InfoType.Equip )
        {
            UpdateShow();
        }
    }

    void UpdateShow()
    {
        PlayerInfo info = PlayerInfo._instance;
        //helmEquip.SetId(info.HelmID);
        //clothEquip.SetId(info.ClothID);
        //weaponEquip.SetId(info.WeaponID);
        //shoesEquip.SetId(info.ShoesID);
        //necklaceEquip.SetId(info.NecklaceID);
        //braceletEquip.SetId(info.BraceletID);
        //weaponEquip.SetId(info.WeaponID);
        //ringEquip.SetId(info.RingID);
        //wingEquip.SetId(info.wingID);

        helmEquip.SetInventoryItem(info.helmInventoryItem);
        clothEquip.SetInventoryItem(info.clothInventoryItem);
        weaponEquip.SetInventoryItem(info.weaponInventoryItem);
        shoesEquip.SetInventoryItem(info.shoesInventoryItem);
        braceletEquip.SetInventoryItem(info.braceletInventoryItem);
        ringEquip.SetInventoryItem(info.ringInventoryItem);
        wingEquip.SetInventoryItem(info.wingInventoryItem);
        necklaceEquip.SetInventoryItem(info.necklaceInventorItem);

        hpLabel.text = info.Hp.ToString();
        damageLabel.text = info.Damage.ToString();
        expSlider.value = (float)info.Exp / GameController.GetRequireExpByLevel(info.Level + 1);//当前的经验值除以到达下一级所需要的经验值
        expLabel.text = info.Exp +"/" + GameController.GetRequireExpByLevel(info.Level + 1);
    }
}
