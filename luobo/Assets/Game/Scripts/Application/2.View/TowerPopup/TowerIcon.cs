using UnityEngine;
using System.Collections;
//创建塔的时候，只需要TowerInfo，，即塔的一些基本信息就好
public class TowerIcon : MonoBehaviour {

    SpriteRenderer render;
    TowerInfo info;
    Vector3 pos;
    bool isEnough = false;

	public void Load(GameModel gm,TowerInfo info,Vector3 pos,bool upSide)
    {
        //保存必要信息
        this.info = info;
        this.pos = pos;

        //判断金币是否足够
        isEnough = gm.Gold >= info.basePrice;

        //加载图片
        string path = "Res/Roles/" + (isEnough ? info.NormalIcon : info.DisableIcon);
        render.sprite = Resources.Load<Sprite>(path);
        
        //摆放位置  放在上面还是下面
        Vector3 localPos = transform.localPosition;
        localPos.y = upSide ? Mathf.Abs(localPos.y) : -Mathf.Abs(localPos.y);
        transform.localPosition = localPos;
    }

    void OnMouseDown()
    {
        //金币是否足够TODO
        if (!isEnough)
            return;

        //创建塔的类型TowerID
        int towerID = info.ID;

        //创建位置
        Vector3 pos = this.pos;

        //参数
        object[] args={towerID ,pos};//??????????????
        //消息冒泡
        SendMessageUpwards("OnSpawnTower",args,SendMessageOptions.RequireReceiver );
    }


}
