using UnityEngine;
using System.Collections;

public class UpgradePanel : MonoBehaviour {

    UpgradeIcon upgradeIcon;
    SaleIcon saleIcon;

    void Awake()
    {
        upgradeIcon = GetComponentInChildren<UpgradeIcon>();
        saleIcon = GetComponentInChildren<SaleIcon>();
    }
    
	public void Show(GameModel model,Tower tower)
    {
        //位置
        transform.position = tower.transform .position;//tower是一个游戏组件，可以从中获得塔的信息

        //显示
        upgradeIcon.Load(tower);
        saleIcon.Load(tower);

    }

    public void Hide()
    {

    }
}
