using UnityEngine;
using System.Collections;
/// <summary>
/// 处理点击事件
/// 
/// </summary>
public class Knapsack : MonoBehaviour {

    public static Knapsack _instance;

    private EquipPopup equipPopup;
    private InventoryPopup inventoryPopup;

    private UIButton saleButton;
    private UILabel priceLabel;
    private TweenPosition tween;
    private UIButton closeKnapsackButton;
    
    private InventoryItemUI itUI;

    void Awake()
    {
        _instance = this;
        equipPopup = transform.Find("EquipPopUp").GetComponent<EquipPopup>();
        inventoryPopup = transform.Find("InventoryPopUp").GetComponent<InventoryPopup>();
        saleButton = transform.Find("Inventory/ButtonSale").GetComponent<UIButton>();
        priceLabel = transform.Find("Inventory/PriceBg/Label").GetComponent<UILabel>();
        tween = this.GetComponent<TweenPosition>();
        closeKnapsackButton = transform.Find("CloseButton").GetComponent<UIButton>();

        EventDelegate ed = new EventDelegate(this, "OnSale");
        saleButton.onClick.Add(ed);

        EventDelegate ed2 = new EventDelegate(this, "OnKnapsackClose");
        closeKnapsackButton.onClick.Add(ed2);

        DisableButton();
        
    }
	public void OnInventoryClick(object[] objectArray)
    {
        //控制背包中每个物体的点击响应（将点击的响应事件传递到Knapsack上处理）

        InventoryItem it = objectArray[0] as InventoryItem;
        bool isLeft =(bool)objectArray[1];//强制转换
        

        if(it.Inventory.InventoryTYPE ==InventoryType.Equip )
        {
            InventoryItemUI itUI = null;
            KnapsackRoleEquip roleEquip = null;
            if(isLeft==true)
            {
                itUI = objectArray[2] as InventoryItemUI;

            }
            else
            {
                 roleEquip = objectArray[2] as KnapsackRoleEquip;
            }
            inventoryPopup.Close();
            equipPopup.Show(it, isLeft,roleEquip, itUI);

            
        }
        else 
        {
            InventoryItemUI itUI = objectArray[2] as InventoryItemUI;
            equipPopup.Close();
            inventoryPopup.Show(it,itUI);
        }

        if((it.Inventory.InventoryTYPE ==InventoryType .Equip&&isLeft==true)||it.Inventory.InventoryTYPE !=InventoryType .Equip )
        {
            //当点击的为背包中的物品，就让出售按钮具有交互功能

            this.itUI=objectArray[2] as InventoryItemUI;
            EnableButton();

            priceLabel.text = (this.itUI.it.Inventory.Price*this.itUI.it.Count).ToString();
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

    void DisableButton()
    {
        //禁用一个按钮

        saleButton.SetState(UIButtonColor.State.Disabled, true);
        saleButton.GetComponent<Collider>().enabled = false;
        priceLabel.text = "";
    }

    void EnableButton()
    {
        //启用一个按钮

        saleButton.SetState(UIButtonColor.State.Normal, true);
        saleButton.GetComponent<Collider>().enabled=true;

    }

    void OnSale()
    {
        int price = int.Parse(priceLabel.text);
        PlayerInfo._instance.AddCoin(price);
        InventoryManager._instance.RemoveInventoryItem(itUI.it);
        itUI.Clear();

        equipPopup.Close();
        DisableButton();
    }

    void OnKnapsackClose()
    {
        tween.PlayReverse();
    }
        
}
