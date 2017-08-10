using UnityEngine;
using System.Collections;

public enum InventoryType
{
    Equip,
    Drug,
    Box

}

public enum EquipType
{
    Helm,
    Cloth,
    Weapon,
    Shoes,
    Necklace,
    Bracelet,
    Ring,
    Wing
}

public class Inventory  {

    private int id;//ID
    private string name;//名称
    private string icon;//在图集中的名称
    private InventoryType inventoryType;//类型
    private EquipType equipType;//装备类型
    private int price = 0;//价格
    private int startLevel = 1;//星级
    private int quality = 1;//品质
    private int damage = 0;//伤害
    private int hp = 0;//生命
    private int power = 0;//战斗力
    private InfoType infoType;//作用类型，表示作用在哪个属性之上
    private int applyValue;//作用值
    private string des;//描述

    #region get set
    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    public string Icon
    {
        get
        {
            return icon;
        }
        set
        {
            icon = value;
        }
    }

    public InventoryType InventoryTYPE
    {
        get
        {
            return inventoryType;
        }
        set
        {
            inventoryType = value;
        }
    }

    public EquipType EquipTYPe
    {
        get
        {
            return equipType;
        }
        set
        {
            equipType = value;
        }
    }

    public int Price
    {
        get
        {
            return price;
        }
        set
        {
            price = value;
        }
    }

    public int StartLevel
    {
        get
        {
            return startLevel;
        }
        set
        {
            startLevel = value;
        }
    }

    public int Quality
    {
        get
        {
            return quality;
        }
        set
        {
            quality = value;
        }
    }

    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }

    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }

    public int Power
    {
        get
        {
            return power;
        }
        set
        {
            power = value;
        }
    }

    public InfoType InfoTYPE
    {
        get
        {
            return infoType;
        }
        set
        {
            infoType = value;
        }
    }

    public int ApplyValue
    {
        get
        {
            return applyValue;
        }
        set
        {
            applyValue = value;
        }
    }

    public string Des
    {
        get
        {
            return des;
        }
        set
        {
            des = value;
        }
    }
    #endregion
}