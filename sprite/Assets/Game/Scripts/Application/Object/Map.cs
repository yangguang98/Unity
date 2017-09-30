using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TileClickEventArgs:EventArgs//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
{
    //鼠标点击参数类
    public int MouseButton;
    public Tile Tile;

    public TileClickEventArgs (int mouseButton,Tile tile)
    {
        this.MouseButton = mouseButton;
        this.Tile = tile;
    }
}

public class Map : MonoBehaviour
{
    #region 常量
    public const int RowCount = 8;
    public const int ColumnCount = 12;
    #endregion

    #region  事件
    public event EventHandler<TileClickEventArgs> OnTileClick;//!!!!!!!!!!!!!!!!!!!!!!!!!
    #endregion

    #region 字段
    float MapWidth;//地图宽 
    float MapHeight;//地图高

    float TileWidth;//格子宽 
    float TileHeight;//格子高


    //初始化时这两个List中的数据都是m_level中的信息，，当我们对地图编辑之后，，首先修改这两个List中的值，，然后再将其存储到m_level中去，，m_level相当于一个中间的数据
    List<Tile> m_grid = new List<Tile>();//格子集合，，，就是地图中所有格子，，游戏启动就会被创建，
    List<Tile> m_road = new List<Tile>();//路径集合

    Level m_level;//关卡数据,，，，m_grid   m_road相当于本地数据

    public bool DrawGizmos = true;
    #endregion

    #region 事件回调

    //只有在关卡编辑器中才可以运行这个回调函数
    private void Map_OnTileClick(object sender, TileClickEventArgs e)
    {
        //当前场景不是LevelBuilder不能编辑
        if (gameObject.scene.name != "LevelBuilder")
            return;
         
        if(Level==null)
        {
            return;
        }

        if(e.MouseButton ==0&&!m_road .Contains (e.Tile ))
        {
            //鼠标左键，并且不是寻路点
            e.Tile.CanHold = !e.Tile.CanHold;
        }

        if(e.MouseButton ==1&&!e.Tile.CanHold )
        {
            //鼠标右键，并且不是放塔点
            if(m_road .Contains (e.Tile ))
            {
                //包含在寻路中，则移除
                m_road.Remove(e.Tile);
            }
            else
            {
                //不包含在寻路中，则添加
                m_road.Add(e.Tile);
            }
        }
    }
    #endregion

    #region








    public Vector3[] Path
    {
        //通过路径获取，，路径的世界坐标序列
        get
        {
            List<Vector3> m_path = new List<Vector3>();
            for (int i = 0; i < m_road.Count; i++)
            {
                Tile t = m_road[i];
                Vector3 point = GetTileWordPosition(t);//通过块去获取其世界坐标
                m_path.Add(point);
            } 
            return m_path.ToArray(); //将List类型转化为数组类型
        }
    }

    #endregion

    #region

    public void LoadLevel(Level level)
    {
        //通过关卡信息Level,将具体的数据显示到界面上

        Clear();//清除当前装状态
        this.m_level = level;//保存


        //加载图片
        this.BackgroundImage = "file://" + Consts.MapDir + level.Background;//本地加载的方式
        this.RoadImage = "file://" + Consts.MapDir + level.Road;

        //寻路路径
        for (int i = 0; i < level.Path.Count; i++)
        {
            Point p = level.Path[i];
            Tile t = GetTile(p.X, p.Y);
            m_road.Add(t);
        }

        //炮塔空地
        for (int i = 0; i < level.Holder.Count; i++)
        {
            Point p = level.Holder[i];
            Tile t = GetTile(p.X, p.Y);
            t.CanHold = true;
        }

    }
    #endregion

    public void ClearHolder()
    {
        //清除塔位信息
        foreach (Tile t in m_grid)
        {
            if (t.CanHold)
            {
                t.CanHold = false;
            }
        }
    }

    public void ClearRoad()
    {
        //清除寻路格子
        m_road.Clear();
    }

    public void Clear()
    {
        m_level = null;
        ClearHolder();
        ClearRoad();
    }


    #region 常用方法

    void CalculateSize()
    {
        //计算块在世界坐标中的大小，，原理，所有的视口大小都是（1，1），通过视口的倍数放大投影到屏幕上，则，可以通过视口在世界坐标中的大小，反应屏幕在世界坐标中的大小，通过世界坐标去计算块的大小
        Vector3 p1 = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
        Vector3 p2 = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));

        MapWidth = (p2.x - p1.x);
        MapHeight = (p2.y - p1.y);

        TileHeight = MapHeight / RowCount;
        TileWidth = MapWidth / ColumnCount;
    }

    public Vector3 GetTileWordPosition(Tile t)
    {
        //通过格子在那一行，哪一列， 去计算格子在世界坐标中的位置
        return new Vector3(-MapWidth / 2 + (t.X + 0.5f) * TileWidth,
            -MapHeight / 2 + (t.Y + 0.5f) * TileHeight,
            0);
    }

    public Tile GetTile(int indexX, int indexY)
    {
        //通过索引值，获取格子，所有的格子都在m_grid中，，通过公式得到是按照行来存储的，
        int index = indexX + indexY * ColumnCount;
        if (index < 0 || index >= m_grid.Count)
        {
            return null;
        }
        return m_grid[index];
    }

    public Tile GetTile(Vector3 worldPos)
    {
        int col = (int)((worldPos.x + MapWidth / 2) / TileWidth);//这里转化为int，如果是小数，是否有进位！！！！！！！！！！！！！！！！！！！！！！！11
        int row = (int)((worldPos.y + MapWidth / 2) / TileHeight);
        return GetTile(row, col);
    }

    Vector3 GetMouseWordPosition()
    {
        //获取鼠标的世界坐标，，原理，鼠标在屏幕上，先将屏幕坐标转换为视口坐标，再将视口坐标转化为世界坐标
        Vector3 viewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 worlPos = Camera.main.ViewportToWorldPoint(viewPos);
        return worlPos;
    }

    Tile GetTileUnderMouse()
    {
        //获取鼠标下的块，，先获取鼠标的世界坐标，在获取块的索引，在通过索引查找块
        Vector2 worldPos = GetMouseWordPosition();
        return GetTile(worldPos);
    }

    #endregion

    #region unity回调

    void Awake()
    {
        //只在运行期起作用
        CalculateSize();//计算格子大小

        //创建所有格子
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; i < ColumnCount; j++)
            {
                m_grid.Add(new Tile(j, i));
            }
        }

        //监听鼠标点击事件
        OnTileClick +=Map_OnTileClick;
    }

    

    void Update()
    {
        if(Input.GetMouseButtonDown (0))
        {
            //鼠标左键点击
            Tile t = GetTileUnderMouse();

            if(t!=null)
            {
                //触发鼠标左键点击事件
                TileClickEventArgs e = new TileClickEventArgs(0, t);//事件参数
                if (OnTileClick !=null)
                {
                    OnTileClick(this, e);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            //鼠标左键点击
            Tile t = GetTileUnderMouse();

            if (t != null)
            {
                //触发鼠标右键点击事件
                TileClickEventArgs e = new TileClickEventArgs(1, t);//事件参数
                if (OnTileClick != null)
                {
                    OnTileClick(this, e);
                }
            }
        }
    }


    void OnDrawGizmos()
    {
        //只在编辑器中起作用，，即Scene视图中，正式游戏运行的时候，，不会执行
        if (!DrawGizmos)
        {
            return;
        }

        CalculateSize();//计算地图格子的大小

        Gizmos.color = Color.green;

        //绘制行  
        for (int row = 0; row < RowCount; row++)
        {
            Vector2 from = new Vector2(-MapWidth / 2, -MapHeight / 2 + row * TileHeight);
            Vector2 to = new Vector2(MapWidth / 2, -MapHeight / 2 + row * TileHeight);
            Gizmos.DrawLine(from, to);
        }

        //绘制列
        for (int column = 0; column < ColumnCount; column++)
        {
            Vector2 from = new Vector2(-MapWidth / 2 + column * TileWidth, -MapHeight / 2);
            Vector2 to = new Vector2(-MapWidth / 2 + column * TileWidth, MapHeight / 2);
            Gizmos.DrawLine(from, to);
        }

        foreach (Tile t in m_grid)
        {
            if (t.CanHold)
            {
                Vector3 pos = GetTileWordPosition(t);
                Gizmos.DrawIcon(pos, "holder.png", true);//这个图片存放的路径是有要求的，在Gizmos目录下
            }
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < m_road.Count; i++)
        {
            if (i == 0)
            {
                //起点
                Gizmos.DrawIcon(GetTileWordPosition(m_road[i]), "start.png", true);//这个图画的大小如何控制的和格子的大小相同？？？？？？？？？？？？？？？？
            }

            if (m_road.Count > 1 && i == m_road.Count - 1)
            {
                //终点
                Gizmos.DrawIcon(GetTileWordPosition(m_road[i]), "end.png", true);
            }

            if (m_road.Count > 1 && i != 0)
            {
                Vector3 from = GetTileWordPosition(m_road[i - 1]);
                Vector3 to = GetTileWordPosition(m_road[i]);
                Gizmos.DrawLine(from, to);
            }


        }


    }
    #endregion

    #region 属性


    public string BackgroundImage
    {
        //通过指定的路径去加载不同的sprite,,这里通过修改不同的sprite的SpriteRender来达到修改图片的目的
        set
        {
            SpriteRenderer render = transform.Find("Background").GetComponent<SpriteRenderer>();
            StartCoroutine(Tools.LoadImage(value, render));
        }
    }

    public string RoadImage
    {
        //通过指定的路径去加载不同的sprite,然后反应到画面上去,这里通过修改不同的sprite的SpriteRender来达到修改图片的目的 
        set
        {
            SpriteRenderer render = transform.Find("Road").GetComponent<SpriteRenderer>();
            StartCoroutine(Tools.LoadImage(value, render));
        }
    }

    public Level Level
    {
        get
        {
            return m_level;
        }
    }

    public List<Tile> Grid
    {
        //获取所有的格子
        get
        {
            return m_grid;
        }
    }


    public List<Tile> Road
    {
        //获取路径的格子
        get
        {
            return m_road;
        }
    }

    #endregion

}
