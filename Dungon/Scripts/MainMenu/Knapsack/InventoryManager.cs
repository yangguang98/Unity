using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TaidouCommon.Model;


//OnInventoryChange;  这个事件只是当背包中有增加和减少物品的时候去触发UI的变化，不涉及到人物信息的变更（如：战斗值等）,背包中的物品都在List中，穿上了其isDress就为true,否则就为false,
public class InventoryManager : MonoBehaviour {


    public static InventoryManager _instance;
    public TextAsset listinfo;

    //创建一个字典，将ID作为索引
    public Dictionary<int, Inventory> inventoryDict = new Dictionary<int, Inventory>();

    //public Dictionary<int,InventoryItem> inventoryItemDict=new Dictionary<int,InventoryItem>();
    public List<InventoryItem> inventoryItemList = new List<InventoryItem>();//背包中物品

    public delegate void OnInventoryChangeEvent();
    public event OnInventoryChangeEvent OnInventoryChange;//这个事件只是当背包中有增加和减少物品的时候去触发UI的变化，

    private InventoryItemDBController inventoryItemDBController;
    void Awake()
    {
        _instance = this;//在这个创造单例模式的时候，在这个单例模式中，事件应该都被注册上了(OnInventoryChange)。。
        ReadInventoryInfo();//读入装备的基本信息
        inventoryItemDBController = this.GetComponent<InventoryItemDBController>();
        inventoryItemDBController.OnGetInventoryItemDBList += this.OnGetInventoryItemDBList;
        inventoryItemDBController.OnAddInventoryItemDB += this.OnAddInventoryItemDB;
        
    }

    void Start()
    {
        ReadInventoryItemInfo();//初始化背包系统
    }

    void Update()
    {
        PickUp();
    }


    void ReadInventoryInfo()
    {
        //装备的基本信息
        string str=listinfo.ToString();
        string[] itemStrArray = str.Split('\n');
        foreach(string itemStr in itemStrArray)
        {
            //ID 名称 图标 类型（Equip，Drug） 装备类型(Helm,Cloth,Weapon,Shoes,Necklace,Bracelet,Ring,Wing) 
            //售价 星级 品质 伤害 生命 战斗力 作用类型 作用值 描述

            string[] proArray = itemStr.Split('|');
            Inventory inventory = new Inventory();
            inventory.Id = int.Parse(proArray[0]);
            inventory.Name = proArray[1];
            inventory.Icon = proArray[2];
            switch(proArray[3])
            {
                case "Equip":
                    inventory.InventoryTYPE = InventoryType.Equip;
                    break;
                case "Drug":
                    inventory.InventoryTYPE = InventoryType.Drug;
                    break;
                case "Box":
                    inventory.InventoryTYPE = InventoryType.Box;
                    break;
            }

            if(inventory.InventoryTYPE==InventoryType.Equip)
            {
                switch(proArray[4])
                {
                    case "Helm":
                        inventory.EquipTYPe = EquipType.Helm;
                        break;
                    case "Cloth":
                        inventory.EquipTYPe = EquipType.Cloth;
                        break;
                    case "Weapon":
                        inventory.EquipTYPe = EquipType.Weapon;
                        break;
                    case "Shoes":
                        inventory.EquipTYPe = EquipType.Shoes;
                        break;
                    case "Necklace":
                        inventory.EquipTYPe = EquipType.Necklace;
                        break;
                    case "Bracelet":
                        inventory.EquipTYPe = EquipType.Bracelet;
                        break;
                    case "Ring":
                        inventory.EquipTYPe = EquipType.Ring;
                        break;
                    case "Wing":
                        inventory.EquipTYPe = EquipType.Wing;
                        break;
                }
            }

            
            inventory.Price = int.Parse(proArray[5]);

            //售价 星级 品质 伤害 生命 战斗力 作用类型 作用值 描述
            if(inventory.InventoryTYPE ==InventoryType.Equip )
            {
                inventory.StartLevel = int.Parse(proArray[6]);
                inventory.Quality = int.Parse(proArray[7]);
                inventory.Damage = int.Parse(proArray[8]);
                inventory.Hp = int.Parse(proArray[9]);
                inventory.Power = int.Parse(proArray[10]);
            }

            if(inventory.InventoryTYPE ==InventoryType.Drug)
            {
                inventory.ApplyValue = int.Parse(proArray[12]);
            }

            inventory.Des = proArray[13];
            inventoryDict.Add(inventory.Id, inventory);//添加到字典中
        }
    }
    void ReadInventoryItemInfo()
    {
        //InventoryItem继承自Inventory，其中含有装备的等级，数量等
        //TODO 需要连接服务器，取得当前角色拥有的物品信息
        //随机生成主角拥有的物品


        //完成角色背包信息的初始化，获得拥有的物品
        //for(int i=0;i<20;i++)
        //{
        //    int id=Random.Range(1001,1020);
        //    Inventory j = null;
        //    inventoryDict.TryGetValue(id,out j);
        //    if(j.InventoryTYPE ==InventoryType .Equip )
        //    {
        //        //得到的物品为equip
        //        InventoryItem it = new InventoryItem();
        //        it.Inventory = j;
        //        it.Level = Random.Range(1, 10);
        //        it.Count = 1;
        //        inventoryItemList.Add(it);//装备就直接加到其中，可能有几个装备，但是其等级不一定相同
        //    }
        //    else
        //    {
        //        //先判断背包里是否存在
        //        InventoryItem it = null;
        //        bool isExit = false;
        //        foreach(InventoryItem temp in inventoryItemList )
        //        {
        //            if(temp.Inventory.Id==id)
        //            {
        //                isExit = true;
        //                it = temp;
        //                break;
        //            }
        //        }
        //        if(isExit)
        //        {
        //            it.Count++;
        //        }
        //        else
        //        {
        //            it = new InventoryItem();
        //            it.Inventory = j;
        //            it.Count = 1;
        //            inventoryItemList.Add(it);
        //        }
        //    }
            
            
        //}

        inventoryItemDBController.GetInventoryItemDB();     //从服务器得到背包数据
        
    }

    public void PickUp()
    {
        //模拟捡起物品
        if(Input.GetKeyDown (KeyCode.P))
        {
            int id = Random.Range(1001, 1020);
            Inventory j = null;
            inventoryDict.TryGetValue(id, out j);
            if(j.InventoryTYPE ==InventoryType .Equip )
            {
                //先创建IventoryItemDB存储到数据库中，然后将其存储到数据库中，并且将此数据传回来，在利用此数据去更新下UI(先修改服务器端的数据，然后在修改本底的数据)
            //    InventoryItem it = new InventoryItem();
            //    it.Inventory = j;
            //    it.Level = Random.Range(1, 10);
            //    it.Count = 1;
            //    InventoryItemDB itemDB = it.CreateInventoryItemDB();
                InventoryItemDB itemDB = new InventoryItemDB();
                itemDB.Level = Random.Range (1,10);
                itemDB.InventoryID = id;
                itemDB.IsDress = false;
                itemDB.Count = 1;
                inventoryItemDBController.AddInventoryItemDB(itemDB);
                //由得到的InventoryItem去生成对应的InventoryItemDB，存储到数据库当中
            }
            else
            {
                //先判断背包里是否存在
                InventoryItem it = null;
                bool isExit = false;
                foreach(InventoryItem temp in inventoryItemList )
                {
                    if(temp.Inventory.Id==id)
                    {
                        isExit = true;
                        it = temp;
                        break;
                    }
                }

                if (isExit)
                {
                    Debug.Log("isExit=true");
                    it.Count++;//当count增加的时候，其对应的InventoryItemDB中的count数目也要增加，然后将InventoryItemDB更新到数据库当中(先修改本底的数据，然后在修改服务器端的数据)
                    //创建InventoryItemDB，将数据更新到服务器端 Update
                    OnInventoryChange();//事件,去修改UI
                    inventoryItemDBController.UpdateInventoryItemDB(it.InventoryItemDB);
                }
                else
                {
                    Debug.Log("isExit=false");
                    InventoryItemDB itemDB = new InventoryItemDB();
                    itemDB.Level = Random.Range(1, 10);
                    itemDB.InventoryID = id;
                    itemDB.IsDress = false;
                    itemDB.Count = 1;
                    inventoryItemDBController.AddInventoryItemDB(itemDB);
                }
            }
        }
    }

    public void RemoveInventoryItem(InventoryItem it)
    {
        inventoryItemList.Remove(it);
    }

    public void OnGetInventoryItemDBList(List<InventoryItemDB> list)
    {
        //当请求服务器端关于背包信息的响应后的回调事件
        foreach(var itemDB in list)
        {
            InventoryItem it = new InventoryItem(itemDB);
            inventoryItemList.Add(it);
        }
        OnInventoryChange();//事件,去修改UI
    }

    public void OnAddInventoryItemDB(InventoryItemDB itemDB)
    {
        //通过itemDB，创建InventoryItemDB,然后显示到UI上
        InventoryItem it = new InventoryItem(itemDB);
        inventoryItemList.Add(it);

        OnInventoryChange();//更新UI
    }


    void OnDestroy()
    {
        inventoryItemDBController.OnGetInventoryItemDBList -= OnGetInventoryItemDBList;
        inventoryItemDBController.OnAddInventoryItemDB -= OnAddInventoryItemDB;
    }

    public void UpgradeEquip(InventoryItem it)
    {
        //升级装备后与服务器的同步
        inventoryItemDBController.UpgradeEquip(it.InventoryItemDB);
    }


}
