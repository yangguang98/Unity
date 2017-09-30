using UnityEngine;
using System.Collections;
//通过这个View来产生怪物
public class Spwaner : View
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    Map m_Map = null;
    Luobo luobo = null;
    #endregion

    #region 属性

    public override string Name
    {
        get { return Consts.V_Spwaner; }
    }

    #endregion

    #region 方法

    void SpawnMonster(int MonsterType)
    {
        //创建怪物 ,,,注意要注册上事件
        string prefabName = "Monster" + MonsterType;
        GameObject go = Game.Instance.objectPool.Spawn(prefabName);//利用对象池产生Monster
        Monster monster = go.GetComponent<Monster>();
        monster.Reached += monster_reached;//Monster到达终点，那么就让luobo掉血
        monster.HpChanged += monster_HpChanged;
        monster.Dead += monster_Dead;
        monster.Load(m_Map.Path);
    }

    void SpwanLuobo(Vector3 position)
    {
        GameObject go = Game.Instance.objectPool.Spawn("Luobo");
        luobo = go.GetComponent<Luobo>();
        luobo.Dead += luobo_Dead;//这里注册了两个事件，，一个是用来播放动画，一个是处理别的事情
        luobo.transform.position = position;
    }

    void SpawnTower(int towerID, Vector3 pos)
    {
        Tile tile = m_Map.GetTile(pos);
        TowerInfo info = Game.Instance.staticData.GetTowerInfo(towerID);
        GameObject go = Game.Instance.objectPool.Spawn(info.PrefabName);

        Tower tower = go.GetComponent<Tower>();
        go.transform.position = pos;
        tower.Load(towerID, tile);//数据初始化
    }

    void luobo_Dead(Role obj)
    {
        //回收萝卜
        Game.Instance.objectPool.Unspwan(luobo.gameObject);

        //游戏失败
        GameModel gm = GetModel<GameModel>();
        SendEvent(Consts.E_EndLevel, new EndLevelArgs() { LevelID = gm.PlayLevelIndex, IsSuccess = true });
    }

    void monster_Dead(Role obj)
    {
        //回收死去的monster
        Monster monster = obj as Monster;
        Game.Instance.objectPool.Unspwan(monster.gameObject);

        GameModel gm = GetModel<GameModel>();
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        RoundModel rm = GetModel<RoundModel>();

        //判定游戏是否胜利
        if (!luobo.IsDead                     //萝卜没有死亡
            && monsters.Length <= 0            //场景中没有怪物
            && rm.AllRoundsComplete)          //所有Monster都出完了
        {
            SendEvent(Consts.E_EndLevel, new EndLevelArgs() { LevelID = gm.PlayLevelIndex, IsSuccess = true });
        }
    }

    void monster_HpChanged(int arg1, int arg2)
    {

    }

    void monster_reached(Monster monster)
    {
        //当Monster到达终点后，，，，就让萝卜自动的去掉血,,并且自己的血量都减少到零
        //达到终点后就回收该游戏物体

        monster.Hp = 0;

        //luobo掉血
        luobo.Damage(1);
    }
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_SpwanMonster);
        AttentionEvents.Add(Consts.E_EnterScene);
        AttentionEvents.Add(Consts.E_SpawnTower);
    }

    public override void HandlerEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_EnterScene:
                //进入游戏场景
                SceneArgs e0 = data as SceneArgs;
                if (e0.SceneIndex == 3)
                {

                    //从数据层获取关卡信息
                    GameModel gModel = GetModel<GameModel>();


                    //加载地图
                    m_Map = GetComponent<Map>();//为每一个monster设置行走的路径
                    m_Map.OnTileClick += Map_OnTileClick;
                    m_Map.LoadLevel(gModel.PlayerLevel);//让Map脚本去渲染当前玩的关卡的地图信息

                    //加载萝卜
                    Vector3[] path = m_Map.Path;
                    Vector3 pos = path[path.Length - 1];//获取路径的最后一个点
                    SpwanLuobo(pos);
                }
                break;
            case Consts.E_SpwanMonster:
                SpwanMonsterArgs e1 = data as SpwanMonsterArgs;
                this.SpawnMonster(e1.MonsterType);
                break;
            case Consts.E_SpawnTower:
                SpawnTowerArgs e2 = data as SpawnTowerArgs;
                this.SpawnTower(e2.TowerID, e2.pos);
                break;
            default:
                break;
        }
    }


    //显示创建或者升级面板的起点
    void Map_OnTileClick(object sender, TileClickEventArgs e)
    {
        //弹出创建或者升级面板
        GameModel gm = GetModel<GameModel>();

        //游戏还未开始，那么不弹出菜单
        if (!gm.IsPlaying)
            return;

        //若塔是显示的那么就隐藏塔
        if (TowerPopup.Instance.IsPopupShow )
        {
            SendEvent(Consts.E_HidePopup);
            return;
        }

        //不能放塔的也隐藏
        if(!e.Tile .CanHold )
        {
            SendEvent(Consts.E_HidePopup);
            return;
        }

        //如果有菜单显示
        if (gm.IsPlaying && e.Tile.CanHold)
        {
            Tile tile = e.Tile;

            if (tile.Data == null)
            {
                //显示放塔界面
                ShowSpwanPanelArgs e1 = new ShowSpwanPanelArgs()
                {
                    position = m_Map.GetTileWordPosition(tile),
                    upSide = tile.Y < (Map.RowCount / 2)
                };
                SendEvent(Consts.E_ShowSpwanPanel, e1);
            }
            else
            {
                //显示升级界面
                ShowUpgradePanelArgs e2 = new ShowUpgradePanelArgs()
                {
                    tower = tile.Data as Tower
                };
                SendEvent(Consts.E_ShowUpgradePanel, e2);
            }
        }
    }

    #endregion

    #region 帮助方法
    #endregion



}
