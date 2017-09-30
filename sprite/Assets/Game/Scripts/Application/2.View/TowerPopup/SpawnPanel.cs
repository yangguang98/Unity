using UnityEngine;
using System.Collections;

public class SpawnPanel : MonoBehaviour {

    TowerIcon[] towerIcons;

    void Awake()
    {
        towerIcons = transform.GetComponentsInChildren<TowerIcon>();
    }

    public void Show(GameModel gm,Vector3 pos,bool upSide)
    {
        //动态加载图标
        for (int i = 0; i < towerIcons.Length;i++ )
        {
            TowerInfo info = Game.Instance.staticData.GetTowerInfo(i);
            towerIcons[i].Load(gm, info, pos, upSide);
        }
        //设置位置
        transform.position = pos;

        //显示
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
