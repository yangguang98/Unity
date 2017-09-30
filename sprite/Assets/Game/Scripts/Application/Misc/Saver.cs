using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//存档
public class Saver
{
    public static int GetProgress()
    {
        //如果玩家没有玩过这个游戏，那么默认的获取数字就为1
        return PlayerPrefs.GetInt(Consts.GameProgress, -1);
    }

    public static void SetProgress(int levelIndex)
    {
        PlayerPrefs.SetInt(Consts.GameProgress, levelIndex);
    }
}
