using UnityEngine;
using System.Collections;
using TaidouCommon.Model;
/// <summary>
/// InventoryItem中包含Inventory中装备的一些基本属性，在加上一些等级的属性
/// </summary>
public class InventoryItem{

    private Inventory inventory;
    private int level;
    private int count;
    private bool isDressed = false;
    private InventoryItemDB inventoryItemDB;

    public InventoryItem()
    {

    }

    public InventoryItem(InventoryItemDB itemDB)
    {
        //将数据库当中传过来的数据添加转换到客户端对应的数据
        this.inventoryItemDB =itemDB;
        Inventory inventoryTemp;
        InventoryManager ._instance .inventoryDict .TryGetValue (itemDB.InventoryID ,out inventoryTemp);
        inventory =inventoryTemp ;
        level=itemDB.Level ;
        count=itemDB .Count ;
        isDressed =itemDB .IsDress ;
    }

    public InventoryItemDB CreateInventoryItemDB()
    {
        //由InventoryItem创建InventoryItemDB,以便将数据存储到数据库当中
        InventoryItemDB inventoryItemDB = new InventoryItemDB();
        inventoryItemDB.Level = this.level;
        inventoryItemDB.IsDress = this.isDressed;
        inventoryItemDB.Count = this.count;
        inventoryItemDB.InventoryID = this.inventory.Id;
        return inventoryItemDB;
    }
    public Inventory Inventory
    {
        get
        {
            return inventory;
        }
        set
        {
            inventory = value;
        }
    }

    public InventoryItemDB InventoryItemDB
    {
        get
        {
            return inventoryItemDB;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            InventoryItemDB.Level = level;
        }
    }

    public int Count
    {
        get
        {
            return count;
        }
        set
        {
            count = value;
            inventoryItemDB.Count =Count;//修改InventoryItemDB中的数据
        }
    }


    public bool IsDressed
    {
        get
        {
            return isDressed;
        }
        set
        {
            isDressed = value;
            inventoryItemDB.IsDress = isDressed;//对应的inventoryItemDB中的属性也做响应的修改，便于将信息更新到服务器上
        }
    }
      

}
