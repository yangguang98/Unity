using UnityEngine;
using System.Collections;
/// <summary>
/// 模型层：全局参数数据
/// 作用：为了做“全局参数持久化”
/// </summary>
public class GlobalParameterData  {

    //下一场景名称
    private ScenesEnum nextScenesName = ScenesEnum.LoginScenes;

    //玩家的姓名
    private string playerName = "";



    public ScenesEnum NextScenesName
    {
        get { return nextScenesName; }
        set { nextScenesName = value; }
    }

    

    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }

    //无参构造函数  序列化时必须要有
    private  GlobalParameterData()
    {

    }

    public GlobalParameterData (ScenesEnum scenesEnum,string playerName)
    {
        NextScenesName = scenesEnum;
        PlayerName = playerName;
    }
}
