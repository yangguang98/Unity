using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Consts
{

    public static string LevelDir = Application.dataPath + @"\Game\Resources\Res\Levels";//前面的@是忽略转意字符的含义，因为\在字符串中有表示转意字符的含义，因此这个表示的就是字符串   Game\Res\Levels ？？？？？？？？？？？？？？？？为什么这个路径的斜杠不同？？？？？？？？？？？？
    public static string MapDir = Application.dataPath + @"\Game\Resources\Res\Maps";//application.dataPath得到的路径为：G:\project\unity\luobo\Assets\
    public static string CardDir = Application.dataPath + @"\Game\Resources\Res\Cards";

    //常量
    public const float DotClosedDistance = 0.1f;
    public const float RangeClosedDistance = 0.7f;

    //Controller
    
    public const string E_EnterScene = "E_EnterScene";//SceneArgs
    public const string E_ExitScene = "E_ExitScene";//SceneArgs
    public const string E_StartUp = "E_StartUp";

    public const string E_StartLevel = "E_StartLevel";//StartLevelArgs
    public const string E_EndLevel = "E_EndLevel";//StartLevelArgs

    public const string E_CountDownComplete = "E_ContDownComplete";//StartLevelArgs
    public const string E_StartRound = "E_StartRound";//StartRoundArgs
    public const string E_SpwanMonster = "E_SpwanMonster";//SpwanMonsterArgs
    public const string E_ShowSpwanPanel = "E_ShowSpwanPanel";//ShowSpawnPanelArgs
    public const string E_ShowUpgradePanel = "E_ShowUpgradePanel";//ShowUpgradePanelArgs
    public const string E_HidePopup = "E_HidePopup";


    public const string E_SpawnTower = "E_SpawnTower";// SpawnTowerArgs
    public const string E_UpgradeTower = "E_UpgradeTower";//UpgradeTowerArgs
    public const string E_SaleTower = "E_SaleTower";//SaleTowerArgs
    
    
    
    //View
    public static string V_Start = "V_Start";
    public static string v_Select = "V_Select";
    public static string v_Board = "V_UIBoard";
    public static string V_CountDown = "V_CountDown";
    public static string V_Win = "V_Win";
    public static string V_Lost = "V_Lost";
    public static string V_System = "V_System";
    public static string V_Complete = "V_Complete";
    public static string V_Spwaner = "V_Spwaner";
    public static string V_TowerPopup = "V_TowerPopup";

    //Model
    public const string M_GameModel = "M_GameModel";  //这里的const和static有相同的作用吗？？？？？
    public const string M_RoundModel = "M_RoundModel";


    //存档
    public const string GameProgress = "GameProgress";
}

public enum GameSpeed
{
    One,
    Two
}

public enum MonsterType
{
    Monster0,
    Monster1,
    Monster2,
    Monster3,
    Monster4,
    Monster5
}

