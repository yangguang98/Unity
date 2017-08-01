using UnityEngine;
using System.Collections;

/// <summary>
/// 控制层：主城UI界面_任务系统的功能实现
/// </summary>
public class MissionCommand : Command {


    public static MissionCommand Instance;

    void Awake()
    {
        Instance = this;
    }

    //进入第二关卡
	public void EnterLevelTwo()
    {
        base.EnterNextScenes(ScenesEnum.LevelTwo);
    }
}
