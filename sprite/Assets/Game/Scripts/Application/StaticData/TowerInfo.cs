using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//基本信息，，静态
public class TowerInfo
{
    public int ID;
    public string PrefabName;
    public int maxLevel;
    public float guardRange;
    public float shotRate;//一秒钟发射几颗子弹
    public int useBulletID;//发射子弹类型
    public string NormalIcon;
    public int basePrice;
    public string DisableIcon;
}
