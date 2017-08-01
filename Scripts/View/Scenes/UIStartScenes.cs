using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// 视图层：开始场景
/// </summary>

public class UIStartScenes : MonoBehaviour {

    //新的游戏
	public void ClickNewGame()
    {
        print(GetType() + "/ClickNewGame");
        StartScenesCommand.Instance.ClickNewGame();

        
    }


    //游戏继续
    public void ClickGameContinue()
    {
        print(GetType() + "ClickGameContinye");
        StartScenesCommand.Instance.ClickGameContinue();
    }
    
}
