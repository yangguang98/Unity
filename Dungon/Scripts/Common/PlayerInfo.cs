using UnityEngine;
using System.Collections;
using TaidouCommon.Model;
using System.Collections.Generic;
//改变数据，改变UI,同步服务器
//脱下，穿上装备首先更新isDress，然后更新到服务器，更新UI，
//脱下更新UI  InventoryUi._instance.AddInventoryItem(it)
//穿上更新UI 通过触发事件，调用KnapSackRole中的OnPlayerInfoChanged
//在穿上和脱下的过程中我们都修改装备的IsDress属性，在inventoryItemList 中
public enum InfoType{
    Name,
    HeadPortrait,
    Level,
    Power,
    Exp,
    Diamond,
    Coin,
    Energy,
    Toughen,
    Hp,
    Equip,
    Damage,
    All
}

public enum PlayerType
{
    Warrior,
    FemaleAssassin
}

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo _instance;

    #region property
    private string _name;
    private string _headPortrait;
    private int _level = 1;
    private int _power = 1;
    private int _exp = 0;
    private int _diamond = 0;
    private int _coin;
    private int _energy;
    private int _toughen;
    private int _hp;
    private int _damage;
    private PlayerType _playerType;
  /*  private int _helmID=0;
    private int _clothID=0;
    private int _weaponID=0;
    private int _shoesID=0;
    private int _necklaceID=0;
    private int _braceleID=0;
    private int _ringID=0;
    private int _wingID=0;*/

    public InventoryItem helmInventoryItem;
    public InventoryItem clothInventoryItem;
    public InventoryItem weaponInventoryItem;
    public InventoryItem shoesInventoryItem;
    public InventoryItem necklaceInventorItem;
    public InventoryItem ringInventoryItem;
    public InventoryItem wingInventoryItem;
    public InventoryItem braceletInventoryItem;

    #endregion

    public float energyTimer=0;
    public float toughenTimer = 0;

    public delegate void OnPlayerInfoChangeEvent(InfoType type);
    public event OnPlayerInfoChangeEvent OnPlayerInfoChanged;//在这个类当中不需要定义事件对应的函数吗？

    private RoleController roleController;
    private InventoryItemDBController inventoryItemDBController;
    #region get set method
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    public string HeadPortrait
    {
        get
        {
            return _headPortrait;

        }
        set
        {
            _headPortrait = value;
        }
    }
    public int Level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
        }
    }
    public int Exp
    {
        get
        {
            return _exp;
        }
        set
        {
            _exp = value;
        }
    }
    public int Diamond
    {
        get
        {
            return _diamond;
        }
        set
        {
            _diamond = value;
        }
    }
    public int Coin
    {
        get
        {
            return _coin;
        }
        set
        {
            _coin = value;
        }
    }
    public int Power
    {
        get
        {
            return _power;
        }
        set
        {
            _power = value;
        }
    }
    public int Toughen
    {
        get
        {
            return _toughen;
        }
        set
        {
            _toughen = value;
        }
    }
    public int Energy
    {
        get
        {
            return _energy;
        }
        set
        {
            _energy = value;
        }
    }

    public int Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }

    public int Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }

    public PlayerType PlayerType
    {
        get
        {
            return _playerType;
        }
        set
        {
            _playerType = value;
        }
    }

   

    #endregion


    #region unity event
    void Awake()
    {
        _instance = this;
        this.OnPlayerInfoChanged += OnPlayerInfoChange;
        
        roleController = this.GetComponent<RoleController>();
        inventoryItemDBController = this.GetComponent<InventoryItemDBController>();
    }
    #endregion


    void Start()
    {
        Init();
        InventoryManager._instance.OnInventoryChange += this.OnInventoryChange;
    }

    void Update()
    {
        if(this.Energy<100)
        {
            energyTimer += Time.deltaTime;//看看代码
            if(energyTimer>60)
            {
                Energy+=1;
                PhotonEngine.Instance.role.Energy = Energy;
                energyTimer -= 60;
                OnPlayerInfoChanged(InfoType.Energy);
            }
        }
        else
        {
            this.energyTimer = 0;
        }

        if(this.Toughen<50)
        {
            toughenTimer += Time.deltaTime;
            if(toughenTimer >60)
            {
                Toughen += 1;
                PhotonEngine.Instance.role.Toughen = Toughen;
                toughenTimer -= 60;
                OnPlayerInfoChanged(InfoType.Toughen);
            }
        }
        else
        {
            this.toughenTimer = 0;
        }
    }
    void Init()
    {
        print("role");
        Role role = PhotonEngine.Instance.role;//获得当前登录的角色的信息
        this.Coin = role.Coin ;
        this.Diamond = role.Diamond ;
        this.Energy = role.Energy ;
        this.Exp = role.Energy ;
        if(role.IsMan )
        {
            this.HeadPortrait = "头像底板男性";
            _playerType = PlayerType.Warrior;
        }
        else
        {
            this.HeadPortrait = "头像底板女性";
            _playerType = PlayerType.FemaleAssassin;
        }
        
        this.Level = role.Level ;
        this.Name = role.Name ;
        this.Toughen = role.Toughen ;


        //this.BraceletID = 1001;
        //this.wingID = 1002;
        //this._ringID = 1003;
        //this.ClothID = 1004;
        //this.HelmID = 1005;
        //this.WeaponID = 1006;
        //this.NecklaceID = 1007;
        //this.ShoesID = 1008;

        
        //初始化
        InitHpDamagePower();

        //同步更新UI界面
        OnPlayerInfoChanged(InfoType.All);

    }

    public void ChangeName(string newName)
    {
        this.Name = newName;
        PhotonEngine.Instance.role.Name = newName;
        OnPlayerInfoChanged(InfoType.Name);
    }

    public void OnPlayerInfoChange(InfoType infoType)
    {
        //当角色的信息发生变化的时候，要保存到服务器当中
        if(infoType==InfoType .Name ||infoType ==InfoType .Energy ||infoType==InfoType .Toughen||infoType ==InfoType .Coin ||infoType ==InfoType .Diamond )
        {
            roleController.UpdateRole(PhotonEngine.Instance.role);//当一个角色的信息发生改变时，会更新到PhotonEngine.Instance.role上，然后利用此role来更新服务器上的信息
        }
    }

    public void DressOn(InventoryItem it,bool sync=true)
    {
        //穿上装备的操作  穿上脱下某个装备只是修改其中的数值IsDressed，要利用这个数值再去更改已经穿到身上装备的显示

        it.IsDressed = true;
        InventoryItem inventoryItemDressed = null;//已经穿上的装备
        //首先检测有没有穿上相同类型的装备
        bool isDressed = false;
        switch(it.Inventory.EquipTYPe )
        {
            case  EquipType.Bracelet:
                if(braceletInventoryItem !=null)
                {
                    isDressed = true;
                    inventoryItemDressed = braceletInventoryItem;
                    
                }
                braceletInventoryItem = it;
                break;
            case EquipType .Cloth:
                if(clothInventoryItem !=null)
                {
                    isDressed = true ;
                    inventoryItemDressed = clothInventoryItem;
                    
                }
                clothInventoryItem = it;
                break;
            case   EquipType .Helm:
                if(helmInventoryItem !=null)
                {
                    isDressed = true;
                    inventoryItemDressed = helmInventoryItem;
                    
                }
                helmInventoryItem = it;
                break;
            case EquipType .Necklace :
                if(necklaceInventorItem !=null)
                {
                    isDressed = true;
                    inventoryItemDressed = necklaceInventorItem;
                    
                }
                necklaceInventorItem = it;
                break;
            case EquipType .Ring:
                if(ringInventoryItem !=null)
                {
                    isDressed = true;
                    inventoryItemDressed = ringInventoryItem;
                    
                }
                ringInventoryItem = it;
                break;
            case EquipType .Shoes:
                if(shoesInventoryItem !=null)
                {
                    isDressed = true;
                    inventoryItemDressed = shoesInventoryItem;
                    
                }
                shoesInventoryItem = it;
                break;
            case  EquipType .Weapon :
                if(weaponInventoryItem !=null)
                {
                    isDressed = true;
                    inventoryItemDressed = weaponInventoryItem;
                    
                }
                weaponInventoryItem = it;
                break;
            case EquipType .Wing:
                if(weaponInventoryItem !=null)
                {
                    isDressed = true;
                    inventoryItemDressed = weaponInventoryItem;
                    
                }
                weaponInventoryItem = it;
                break;
        }
        //有
        
        if(isDressed)
        { 
            
            inventoryItemDressed.IsDressed = false;//将 穿上的装备脱掉，

            InventoryUi._instance.AddInventoryItem(inventoryItemDressed);//直接将脱下来的装备显示出来，此物品已经存在于List中，只是没有被显示出来  itUI.SetInventoryItem(it);
        }
        if(sync)
        {
            //是否需要更新到服务器
            if (isDressed)
            {
                inventoryItemDBController.UpdateInventoryItemDBList(it.InventoryItemDB, inventoryItemDressed.InventoryItemDB);//将修改的信息更新到服务器，穿上和脱下的物品的信息都要修改，所以为List，这里更新的信息实际就是IsDressed属性
            }
            else
            {
                inventoryItemDBController.UpdateInventoryItemDB(it.InventoryItemDB); //当穿上一个装备后更新到服务器端
            }
        }
        
        //穿上
        OnPlayerInfoChanged(InfoType.Equip);

        //把已经存在的脱掉，放到背包
        //没有
        //直接穿上
    }

    public void DressOff(InventoryItem it)
    {
        //脱下某个装备时  穿上脱下某个装备只是修改其中的数值IsDressed，要利用这个数值再去更改已经穿到身上装备的显示

        switch (it.Inventory.EquipTYPe)
        {
            case EquipType.Bracelet:
                if (braceletInventoryItem != null)
                {
                    braceletInventoryItem = null;
                }
                break;
            case EquipType.Cloth:
                if (clothInventoryItem != null)
                {
                    clothInventoryItem = null;                                        
                }
                break;
            case EquipType.Helm:
                if (helmInventoryItem != null)
                {
                    helmInventoryItem = null;
                }
                break;
            case EquipType.Necklace:
                if (necklaceInventorItem != null)
                {
                    necklaceInventorItem = null;
                }
                break;
            case EquipType.Ring:
                if (ringInventoryItem != null)
                {
                    ringInventoryItem = null;
                }
                break;
            case EquipType.Shoes:
                if (shoesInventoryItem != null)
                {
                    shoesInventoryItem = null;
                }
                break;
            case EquipType.Weapon:
                if (weaponInventoryItem != null)
                {
                    weaponInventoryItem = null;
                }
                break;
            case EquipType.Wing:
                if (weaponInventoryItem != null)
                {
                    weaponInventoryItem = null;
                }
                break;
        }

        it.IsDressed = false;
        inventoryItemDBController.UpdateInventoryItemDB(it.InventoryItemDB);//更新到服务器
        InventoryUi._instance.AddInventoryItem(it);  //直接将脱下来的装备显示出来，此物品已经存在于List中，只是没有被显示出来itUI.SetInventoryItem(it);
        OnPlayerInfoChanged(InfoType.Equip);  //更新战斗值等信息
    }

    public int GetOverAllPower()
    {
        float power = this.Power;
        if(helmInventoryItem !=null)
        {
            power += helmInventoryItem.Inventory.Power * ((helmInventoryItem.Level - 1) / 10f + 1);//装备的基础战斗力+装备级数的加成
        }
        if(clothInventoryItem !=null)
        {
            power += clothInventoryItem.Inventory.Power * ((clothInventoryItem.Level - 1) / 10f + 1);
        }
        if(weaponInventoryItem !=null)
        {
            power += weaponInventoryItem.Inventory.Power * ((weaponInventoryItem.Level - 1) / 10f + 1);
        }
        if (shoesInventoryItem != null)
        {
            power += shoesInventoryItem.Inventory.Power * ((shoesInventoryItem.Level - 1) / 10f + 1);
        }
        if (ringInventoryItem != null)
        {
            power += ringInventoryItem.Inventory.Power * ((ringInventoryItem.Level - 1) / 10f + 1);
        }
        if (wingInventoryItem != null)
        {
            power += wingInventoryItem.Inventory.Power * ((wingInventoryItem.Level - 1) / 10f + 1);
        }
        if (braceletInventoryItem != null)
        {
            power += braceletInventoryItem.Inventory.Power * ((braceletInventoryItem.Level - 1) / 10f + 1);
        }
        return (int)power;
    }

    public void InventoryUse(InventoryItem it,int count)
    {
        //使用某个药品后，对主角信息的改变

        //使用效果

        //处理物品使用后是否还存在
        it.Count -= count;
        if(it.Count<=0)
        {
            InventoryManager._instance.inventoryItemList.Remove(it);
        }

    }
    //取金币也是改变任务的属性，所以将具体的操作放到具体的任务信息类当中
    public bool GetCoin(int count)
    {
        //取得需要的金币数

        if (Coin >= count)
        {
            Coin -= count;
            PhotonEngine.Instance.role.Coin = Coin;//更新金币后，将信息更新到role当中
            OnPlayerInfoChanged(InfoType.Coin);
            return true;
        }
        return false;         
    }

    public void AddCoin(int count)
    {
        this.Coin += count;
        OnPlayerInfoChanged(InfoType.Coin);
    }

    public bool GetEnergy(int energy)
    {
        //得到体力
        if(Energy>energy )
        {
            Energy -= energy;
            PhotonEngine.Instance.role.Energy = Energy;
            OnPlayerInfoChanged(InfoType .Energy);
            return true;
        }
        else
        {
            return false;
        }
    }
    void InitHpDamagePower()
    {
        //初始化damage power
        this.Hp = this.Level * 100;
        this.Damage = this.Level * 50;
        this.Power = this.Hp + this.Damage;
        

    }

    void PutOnEquip(int id)
    {
        if (id == 0) return;//没有穿戴该装备，则对HpDamagePower不做任何的修改
        Inventory inventory = null;
        InventoryManager._instance.inventoryDict.TryGetValue(id, out inventory);
        this.Hp += inventory.Hp;
        this.Damage += inventory.Damage;
        this.Power += inventory.Power;
    }
    void PutOffEquip(int id)
    {
        if (id == 0) return;//没有穿戴该装备，则对HpDamagePower不做任何的修
        Inventory inventory = null;
        InventoryManager._instance.inventoryDict.TryGetValue(id, out inventory);
        this.Hp -= inventory.Hp;
        this.Damage -= inventory.Damage;
        this.Power -= inventory.Power;
    }

    void OnInventoryChange()
    {
        //解决启动的时候，让IsDress为true的物品，装备到人物身上去
        //通过遍历InventoryItemList
        List<InventoryItem> list=InventoryManager ._instance .inventoryItemList ;
        foreach(var temp in list)
        {
            if(temp.Inventory .InventoryTYPE ==InventoryType .Equip &&temp .IsDressed ==true)
            {
                DressOn(temp,false);//这里只是初始化的往主角上装备东西，不需要连接服务器，设置bool为false
            }
        }
    }

    public bool Exchange(int coinChangeCount,int diamondChangeCount)
    {
        //金币和钻石的兑换
        if(((Coin+coinChangeCount )>=0)&&((Diamond +diamondChangeCount) >=0))
        {
            Coin += coinChangeCount;
            Diamond += diamondChangeCount;

            //修改角色，同步服务器，在OnPlayerInfoChanged中注册了OnPlayerInfoChange方法来同步服务器
            PhotonEngine.Instance.role.Coin = Coin;
            PhotonEngine.Instance.role.Diamond = Diamond;


            //修改UI
            OnPlayerInfoChanged(InfoType.Coin);
            OnPlayerInfoChanged(InfoType.Diamond);

            return true;
        }
        else
        {
            return false;
        }
    }
    
    void OnDestroy()
    {
        InventoryManager._instance.OnInventoryChange -= this.OnInventoryChange;
    }
}
