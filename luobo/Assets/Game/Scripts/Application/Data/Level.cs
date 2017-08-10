using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level 
{
    public string Name;//名字
    public string cardImage;//卡片信息
    public string Background;//背景
    public string Road;//路径
    public int InitScore;//金币
    public List<Point> Holder=new List<Point>();//炮塔可放置的位置,,存放的是索引
    public List<Point> Path = new List<Point>();//怪物行走的路径，，存放的是索引
    public List<Round> Rounds=new List<Round >();//出怪回合信息
	
}
