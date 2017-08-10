using UnityEngine;
using System.Collections;
/// <summary>
///InventoryPopup
/// 处理InventoryPopup的事件
/// </summary>
public class InventoryPopup : MonoBehaviour {

    private UILabel nameLabel;
    private UISprite inventorySprite;
    private UILabel desLabel;
    private UILabel btnLabel;
    private InventoryItem it;
    private InventoryItemUI itUI;//当批量使用的时候，要对背包中的物品进行处理

    private UIButton closeButton;
    private UIButton useButton;
    private UIButton useBatchingButton;

    void Awake()
    {
        nameLabel = transform.Find("Bg/NameLabel").GetComponent<UILabel>();
        inventorySprite = transform.Find("Bg/Sprite/Sprite").GetComponent<UISprite>();
        desLabel = transform.Find("Bg/Label").GetComponent<UILabel>();
        btnLabel = transform.Find("Bg/ButtonUsebatch/Label").GetComponent<UILabel>();
        closeButton = transform.Find("ButtonClose").GetComponent<UIButton>();
        useButton = transform.Find("Bg/ButtonUse").GetComponent<UIButton>();
        useBatchingButton = transform.Find("Bg/ButtonUsebatch").GetComponent<UIButton>();

        EventDelegate ed1 = new EventDelegate(this, "OnClose");
        closeButton.onClick.Add(ed1);

        EventDelegate ed2 = new EventDelegate(this, "OnUse");
        useButton.onClick.Add(ed2);

        EventDelegate ed3 = new EventDelegate(this, "OnUseBatch");
        useBatchingButton.onClick.Add(ed3);
    }

    public void Show(InventoryItem it,InventoryItemUI itUI)
    {
        transform.gameObject.SetActive(true);
        this.it = it;
        this.itUI = itUI;
        nameLabel.text = it.Inventory.Name;
        inventorySprite.spriteName = it.Inventory.Icon;
        desLabel.text = it.Inventory.Des;
        btnLabel.text="批量使用("+it.Count+")";
    }


    public void OnClose()
    {
        //closeButton的响应事件

        Close();
        transform.parent.SendMessage("DisableButton");
    }

    public void Close()
    {
        //点击出售之后，关闭InventoryPopup

        Clear();
        gameObject.SetActive(false);
    }

    //将时间上传到playerInfo上处理
    public void OnUse()
    {
        //使用某个药品

        itUI.ChangeCount(1);//将改变的东西放到InventoryItemUI中去处理（处理面板中的显示）
        PlayerInfo._instance.InventoryUse(it, 1);//对主角信息的改变，放到PlayerInfo中去处理
        OnClose();
    }

    public void OnUseBatch()
    {
        itUI.ChangeCount(it.Count);
        PlayerInfo._instance.InventoryUse(it, it.Count);
    }

    void Clear()
    {
        this.it = null;
        this.itUI =null;
    }
}
