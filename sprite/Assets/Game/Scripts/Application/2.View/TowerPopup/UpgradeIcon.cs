using UnityEngine;
using System.Collections;

public class UpgradeIcon : MonoBehaviour {

    SpriteRenderer render;
    Tower tower;

    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

	public void Load(Tower tower)
    {
        //保存塔的数据
        this.tower = tower;

        //显示图片
        TowerInfo info = Game.Instance.staticData.GetTowerInfo(tower.ID);
        string path = "Res/Roles/" + (tower.isTopLevel ? info.DisableIcon : info.NormalIcon);
        render.sprite = Resources.Load<Sprite>(path);
    }

    void OnMouseDown()
    {
        SendMessageUpwards("OnUpgradeTower", tower, SendMessageOptions.RequireReceiver);
    }
}
