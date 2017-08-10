using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 静态数据，，，，格子信息  ，，包含格子的位置，哪一行，那一列，还包含这个格子是否可以放置炮塔
/// 代表地图中的某一块，并不是实际的游戏物体，在Map中存储所有的tile信息
/// </summary>
public class Tile
{
    public int X;//行数
    public int Y;//列数

    public bool CanHold;//是否可以放置塔
    public object Data;//格子所保存的数据

    public Tile(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public override string ToString()
    {
        return string.Format("[X:{0},Y:{1},CanHold:{2}", this.X, this.Y, this.CanHold);
    }
}
