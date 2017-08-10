using UnityEngine;
using System.Collections;


/// <summary>
/// 通过一个类去管理对一个道具的显示操作
/// </summary>
public class InventoryItemUI : MonoBehaviour {


    private UISprite sprite;
    private UILabel label;
    public InventoryItem it;

    private UISprite Sprite//不需要显示的去赋值了
    {
        get
        {
            if(sprite==null)
            {
                sprite = transform.Find("Sprite").GetComponent<UISprite>();
            } return sprite;
        }
    }

    private UILabel Label
    {
        get
        {
            if(label==null)
            {
                label = transform.Find("Label").GetComponent<UILabel>();
            }
            return label;
        }
    }
	public void SetInventoryItem(InventoryItem it)
    {
        this.it = it;
        Sprite.spriteName = it.Inventory.Icon;
        if(it.Count<=1)
        {
            Label.text = "";
        }
        else
        {
            Label.text = it.Count.ToString();
        }
    }

    public void Clear()
    {
        it = null;
        Label.text = "";
        Sprite.spriteName = "bg_道具";
    }
	

    public void OnClick()
    {
        //点击按钮时触发该事件
       
        if(it!=null)
        {
            object[] objectArray=new object[3];
            objectArray[0]=it;
            objectArray[1]=true;
            objectArray[2] = this;//在OnEquip()中使用，当装备之后，要使之前的背包系统中的物品都为空
            transform.parent.parent.parent.SendMessage("OnInventoryClick", objectArray);
        }
        
    }

    public void ChangeCount(int count)
    {
        //背包中某个物品被使用后(如药品),改变显示

        if(it.Count -count<0)
        {
            Clear();
        }
        else if(it.Count-count==1)
        {
            label.text = "";
        }
        else
        {
            label.text = (it.Count - count).ToString();
        }
       

    }
}
