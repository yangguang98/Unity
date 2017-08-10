using UnityEngine;
using System.Collections;
using kernel;
/// <summary>
/// 控制层：主城场景控制
/// </summary>
public class MajorCityCommand : Command {

    public AudioClip AcBackground;

	// Use this for initialization
	IEnumerator  Start () {

        //播放背景音乐
	    if(AcBackground !=null)
        {
            AudioManager.PlayBackground(AcBackground);
        }

        //读取单机玩家数据进度
        if(GlobalParameterMgr .curGameType ==CurrentGameType.Continue )
        {
            yield return new WaitForSeconds(2f);
            SaveAndLoading.GetInstance().LoadPlayerData();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
