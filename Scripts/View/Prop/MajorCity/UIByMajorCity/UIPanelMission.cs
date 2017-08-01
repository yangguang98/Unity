using UnityEngine;
using System.Collections;
/// <summary>
/// 视图层：主城UI界面——任务
/// </summary>
public class UIPanelMission : MonoBehaviour {

    //进入第二关卡
	public void EnterLevelTwo()
    {
        MissionCommand.Instance.EnterLevelTwo();
    }
}
