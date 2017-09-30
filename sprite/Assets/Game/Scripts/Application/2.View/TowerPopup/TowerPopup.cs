using UnityEngine;
using System.Collections;

public class TowerPopup : View{

    public SpawnPanel spwanPanel;
    public UpgradePanel upgradePanel;

    public override string Name
    {
        get { return Consts.V_TowerPopup; }
    }
    private static TowerPopup m_Instance = null;

    public static TowerPopup Instance
    {
        get
        {
            return m_Instance;
        }
    }
    void Awake()
    {
        m_Instance = this; 
    }

    public bool IsPopupShow
    {
        get
        {
            foreach(Transform child in transform)
            {
                if (child.gameObject.activeSelf)
                    return true;
            }
            return false;
        }
    }

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_ShowSpwanPanel);
        AttentionEvents.Add(Consts.E_ShowUpgradePanel);
        AttentionEvents.Add(Consts.E_HidePopup);
    }

    public override void HandlerEvent(string eventName, object data)
    {
        switch (eventName )
        {
            case Consts.E_HidePopup :
                HideAllPopups();
                break;
            case Consts .E_ShowSpwanPanel :
                ShowSpwanPanelArgs e1 = data as ShowSpwanPanelArgs;
                ShowSpwanPanel(e1.position, e1.upSide);
                break;
            case Consts.E_ShowUpgradePanel :
                ShowUpgradePanelArgs e2 = data as ShowUpgradePanelArgs;
                ShowUpgradepanel(e2.tower);
                break;
            default :
                break;
        }
    }

    void OnSpawnTower(object [] args)
    {
        int towerID = (int)args[0];
        Vector3 position = (Vector3)args[1];

        //发送事件TODO
        SendEvent(Consts.E_SpawnTower, new SpawnTowerArgs() { TowerID = towerID, pos = position });
    }

    void OnUpgradeTower(Tower tower)
    {
        SendEvent(Consts.E_UpgradeTower, new UpgradeTowerArgs() { tower = tower });
    }

    void OnSaleTower(Tower tower)
    {
        SendEvent(Consts.E_SaleTower, new SaleTowerArgs() { tower = tower });
    }

    void ShowSpwanPanel(Vector3 pos,bool upSide)
    {
        HideAllPopups();//不可能同时两个面板，先将这两个面板都隐藏
        GameModel gm=GetModel <GameModel>();
        spwanPanel.Show(gm, pos, upSide);
    }

    void ShowUpgradepanel(Tower tower)
    {
        HideAllPopups();

        GameModel gm = GetModel<GameModel>();
        upgradePanel.Show(gm,tower);
    }

    void HideAllPopups()
    {
        spwanPanel.Hide();
        upgradePanel.Hide();
    }
}
