using UnityEngine;
using System.Collections;
using System;
/// <summary>
/// 公共层：全局数值传递
/// </summary>
[Serializable]
public static class GlobalParameterMgr  {

    //下一场景名称
    public static ScenesEnum nextScenesName = ScenesEnum.LoginScenes;

    //玩家的姓名
    public static string playerName = "";

    //玩家类型
    public static PlayerType playerType = PlayerType.None;

    //游戏类型
    public static CurrentGameType curGameType = CurrentGameType.NewGame;

    //public static void Add(Object obj)
    //{

    //}
}
