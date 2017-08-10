using UnityEngine;
using System.Collections;
/// <summary>
/// 测试对话UI
/// </summary>
public class TestDialogUI : MonoBehaviour {

    void Start()
    {
        DialogUIMgr._instance.DisplayNextDialog(DialogType.Double, 1);
    }
    //显示下一条对话信息
	public void DisplayNextDialogInfo()
    {
        bool isEnd = DialogUIMgr._instance.DisplayNextDialog(DialogType.Double, 1);
        if(isEnd )
        {

        }
        else
        {

        }
    }
}
