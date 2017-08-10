using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 对被背包系统的管理
/// InventoryItemUi中存有InventoryItem,
/// 
/// 背包系统中的所有物品itemUIList，若某个物品被穿上则其Isdress为true,在更新的时候不会显示在背包中
/// </summary>
public class InventoryUi : MonoBehaviour {

    public static InventoryUi _instance;
    public List<InventoryItemUI> itemUIList = new List<InventoryItemUI>();//获得背包系统中每个道具的引用,
    private UIButton clearupButton;
    private UILabel inventoryLabel;

    private int count;//有装备的格子的数目

    void Awake()
    {
        _instance = this;
        InventoryManager._instance.OnInventoryChange += this.OnInventoryChange;
        clearupButton = transform.Find("ButtonClearUp").GetComponent<UIButton>();
        inventoryLabel = transform.Find("InventoryLabel").GetComponent<UILabel>();

        EventDelegate ed=new EventDelegate (this,"OnClearup");
        clearupButton.onClick.Add(ed);
    }

    void OnDestroy()
    {
        InventoryManager._instance.OnInventoryChange -= this.OnInventoryChange;
    }

    void OnInventoryChange()
    {
        UpdateShow();
    }

    void UpdateShow()
    {
        //控制背包的显示,将InventoryManager._instance.inventoryItemList中的每个数据存储到InventoryItemUI当中，并且让其显示（如果某个装备已经被穿上，则不显示该装备）

        int temp = 0;
        for(int i=0;i<InventoryManager._instance.inventoryItemList.Count;i++)//如果是列表的话，，就要使用count获得元素的个数
        {
            InventoryItem it = InventoryManager._instance.inventoryItemList[i];
            if(it.IsDressed==false)
            {
                //若物品的IsDressed属性为被穿上，则不需要将其显示在背包中
                
                itemUIList[temp].SetInventoryItem(it);
                temp++;
            }
            
        }
        count = temp;

        for(int i=temp;i<itemUIList.Count;i++)
        {
            //剩余的格子都置于空
            itemUIList[i].Clear();//这个方法看看
        }
        inventoryLabel.text = count + "/32";
    }


    public void AddInventoryItem(InventoryItem it)
    {
        
        //TODO
        //向背包中添加一个物品 ，这个物品已经存在于List中，现在只是让其显示出来，并没有将其添加到队列中
        
        foreach(InventoryItemUI itUI in itemUIList)
        {
            
            if(itUI.it==null)
            {
               
                itUI.SetInventoryItem(it);
                count++;//添加一个物品之后，让拥有物品的个子数目加一个
                break;
            }
            
        }
        inventoryLabel.text = count + "/32";
    }

    public void OnClearup()
    {
        //整理背包
        UpdateShow();
    }

    public void UpdateCount()
    {
        count = 0;
        foreach(InventoryItemUI itUI in itemUIList)
        {
            if(itUI.it!=null)
            {
                count++;
            }
        }
        inventoryLabel.text = count + "/32";
    }
}
