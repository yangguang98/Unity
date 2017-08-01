using UnityEngine;
using System.Collections;
/// <summary>
/// 公共层：全局参数
/// 
/// 1：定义整个项目的常量
/// 2：定义整个项目的委托
/// 3：定义整个项目的系统变量
/// 4: 定义系统所有的Tag
/// </summary>
public class GlobleParameter
{

    /// <summary>
    /// * 定义系统常量
    /// </summary>

    //摇杆名称
    public const string JOYSTICK_NAME = "Herojoystick";
    //输入管理器定义_攻击名称_普通攻击
    public const string INPUT_MGR_ATTACK_NORMAL = "NormalAttack";
    //输入管理器定义_攻击名称_大招A
    public const string INPUT_MGR_ATTACK_MAGICTRICK_A = "MagicTrickA";
    //输入管理器定义_攻击名称_大招B
    public const string INPUT_MGR_ATTACK_MAGICTRICK_B = "MagicTrickB";
    //输入管理器定义_攻击名称_大招C
    public const string INPUT_MGR_ATTACK_MAGICTRICK_C = "MagicTrickC";
    //输入管理器定义_攻击名称_大招D
    public const string INPUT_MGR_ATTACK_MAGICTRICK_D = "MagicTrickD";

    //时间间隔
    public const float INTERVER_TIME_0DOT02 = 0.02f;
    public const float INTERVER_TIME_0DOT1 = 0.1f;
    public const float INTERVER_TIME_0DOT2 = 0.2f;
    public const float INTERVER_TIME_0DOT3 = 0.3f;
    public const float INTERVER_TIME_0DOT4 = 0.4f;
    public const float INTERVER_TIME_0DOT5 = 0.5f;
    public const float INTERVER_TIME_1 = 1f;
    public const float INTERVER_TIME_2 = 1.5f;
    public const float INTERVER_TIME_3 = 3f;
    public const float INTERVER_TIME_4 = 2.5f;
    public const float INTERVER_TIME_5 = 5f;
}

public class Tag
{
    public static string Enemy = "Enemy";
    public static string Player = "Player";
    public static string MajorCity_Up = "MajorCity_Up";
    public static string MajorCity_Down = "MajorCity_Down";
    public static string UIPlayerInfo = "Tag_UIPlayerInfo";
}

#region 枚举类型

public enum PlayerType
{
    None,
    Sword,
    Magic,
    Other
}

public enum ScenesEnum
{
    StartScenes,
    LoadingScenes,
    LoginScenes,
    LevelOne,
    LevelTwo,
    MajorCity,                 //主城
    TestScenes
}

//主角的动作状态
public enum HeroActionState
{
    None,
    Idle,
    Runing,
    NormalAttack,
    MagicTrickA,
    MagicTrickB
}


public enum CurrentGameType
{
    None,
    NewGame,    //新游戏
    Continue   //继续
}

//普通攻击连招
public enum NormalATKComboState
{
    NormalATK1,
    NormalATK2,
    NormalATK3
}

//等级名称
public enum LevelName
{
    //将枚举类型后面标记为数字，方便数字转化为枚举类型 
    Level_0=0,
    Level_1=1,
    Level_2=2,
    Level_3= 3,
    Level_4 = 4,
    Level_5 = 5,
    Level_6 = 6,
    Level_7 = 7,
    Level_8 = 8,
    Level_9 = 9,
    Level_10 =10,
}

public enum EnemyState
{
    idle,
    walking,
    attack,
    hurt,
    death
}
#endregion

#region 委托
//主角控制委托
public delegate void PlayerCtrByStrDelegate(string controlType);
//委托：玩家核心模型数值
public delegate void PlayerKernalDataDelegate(KeyValuesUpdate e);

//键值更新
public class KeyValuesUpdate
{
    private string key;     //键
      
    private object value;   //值

    public string Key
    {
        get { return key; }
    }

    public object Value
    {
        get { return this.value; }
    }

    public KeyValuesUpdate (string key,object vlaues)
    {
        this.key = key;
        this.value = vlaues;
    }
}

#endregion


