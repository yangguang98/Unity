using UnityEngine;
using System.Collections;
/// <summary>
/// 对应的八个装备框上，都有这个类，用来处理装备的改变响应
/// </summary>
public class KnapsackRoleEquip : MonoBehaviour {

    private UISprite _sprite;
    private InventoryItem it;
    private UISprite Sprite
    {
        get
        {
            //防止出现空指针
            if(_sprite==null)
            {
                _sprite = this.GetComponent<UISprite>();
            }

            return _sprite;
        }
    }
	public void SetId(int id)
    {
        Inventory inventory = null;
        bool isExit = InventoryManager._instance.inventoryDict.TryGetValue(id, out inventory);
        if(isExit)
        {
            Sprite.spriteName = inventory.Icon;
        }
    
    }

    public void SetInventoryItem(InventoryItem it)
    {
        if (it == null)
            return;
        this.it = it;
        Sprite.spriteName = it.Inventory.Icon;
    }

    public void Clear()
    {
        it = null;
        Sprite.spriteName = "bg_道具";
    }

    public void OnPress(bool isPress)
    {
        if(isPress&&it!=null)
        {
            object[] objectArray=new object[3];
            objectArray[0]=it;
            objectArray[1]=false;
            objectArray[2] = this;
            transform.parent.parent.SendMessage("OnInventoryClick", objectArray);
        }
        
    }
}
