using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class StaticData : Singleton <StaticData>
{
    Dictionary<int, MonsterInfo> m_monsters = new Dictionary<int, MonsterInfo>();
    Dictionary<int, LuoboInfo> m_Luobo = new Dictionary<int, LuoboInfo>();
    Dictionary<int, TowerInfo> m_Towers = new Dictionary<int, TowerInfo>();
    Dictionary<int, BulletInfo> m_Bullets = new Dictionary<int, BulletInfo>();

    protected override void Awake()
    {
        base.Awake();
        InitMonster();
        InitTowers();
        InitBullets();
        InitLuobo();
    }

    void InitMonster()
    {
        /*
        m_monsters.Add(0, new MonsterInfo() { hp = 1, moveSpeed = 1f });
        m_monsters.Add(1, new MonsterInfo() { hp = 2, moveSpeed = 1f });
        m_monsters.Add(2, new MonsterInfo() { hp = 5, moveSpeed = 1f });
        m_monsters.Add(3, new MonsterInfo() { hp = 10, moveSpeed = 1f });
        m_monsters.Add(4, new MonsterInfo() { hp = 10, moveSpeed = 1f });
        m_monsters.Add(5, new MonsterInfo() { hp = 100, moveSpeed = 1f });
        * * */

        m_monsters.Add(0, new MonsterInfo() { ID=0,hp = 1, moveSpeed = 5f });
        m_monsters.Add(1, new MonsterInfo() { ID=1,hp = 2, moveSpeed = 5f });
        m_monsters.Add(2, new MonsterInfo() { ID=2,hp = 5, moveSpeed = 5f });
        m_monsters.Add(3, new MonsterInfo() { ID=3,hp = 10, moveSpeed = 5f });
        m_monsters.Add(4, new MonsterInfo() { ID=4,hp = 10, moveSpeed = 5f });
        m_monsters.Add(5, new MonsterInfo() { ID=5,hp = 100, moveSpeed = 5f });
    }

    void InitLuobo()
    {
        m_Luobo.Add(0, new LuoboInfo() { ID=0,hp = 0 });
    }

    void InitTowers()
    {
        m_Towers.Add(0, new TowerInfo() { ID = 0, PrefabName = "Bottle", NormalIcon = "Bottle/Bottle01", DisableIcon = "Bottle/Bottle00",maxLevel =3,basePrice =1,guardRange =3f,useBulletID =0 });
        m_Towers.Add(1, new TowerInfo() { ID = 1, PrefabName = "Fan", NormalIcon = "Fan/Fan01", DisableIcon = "Fan/Fan00", maxLevel = 3, basePrice = 2, shotRate = 1, guardRange = 3f, useBulletID = 1 });
    }

    void InitBullets()
    {
        m_Bullets.Add(0, new BulletInfo() { ID = 0, prefabName = "BallBullet", baseAttack = 1, baseSpeed = 5f });
        m_Bullets.Add(1, new BulletInfo() { ID = 1, prefabName = "FanBullet", baseSpeed = 2f, baseAttack = 1 });
    }

    public MonsterInfo GetMonsterInfo(int monsterType)
    {
        return m_monsters[monsterType];
    }

    public LuoboInfo GetLuoboInfo()
    {
        return m_Luobo[0];
    }

    public TowerInfo GetTowerInfo(int towerID)
    {
        return m_Towers[towerID];
    }

    public BulletInfo GetBulletInfo(int bulletID)
    {
        return m_Bullets[bulletID];
    }
}
