using UnityEngine;
using System.Collections;

public class CharacterShow : MonoBehaviour {

	// Use this for initialization
	public void OnPress(bool isPress)//这个函数为什么不能调用
    {
       
        if(isPress==false)
        {
            //点击的为girl游戏物体(胶囊碰撞器在起作用)，，要要传递 gril_showselect
            StartMenu._instance.OnCharacterClick(transform.parent.gameObject);//看看
        }
    }
}
  