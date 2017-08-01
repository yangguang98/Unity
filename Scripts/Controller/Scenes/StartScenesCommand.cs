using UnityEngine;
using System.Collections;
using kernel;
using UnityEngine.SceneManagement;

public class StartScenesCommand : Command {

    public static StartScenesCommand Instance;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //设置音频音量
        AudioManager.SetAudioBackgroundVolumns(0.5f);
        AudioManager.SetAudioEffectVolumns(1f);

        //播放背景音乐
        AudioManager.PlayBackground("StartScenes");
    }

    //新游戏
	internal void ClickNewGame()
    {
        print(GetType() + "/ClickNewGame()");

        //进入“登录场景”
        StartCoroutine("EnterNextScenes");

        //Application.LoadLevel("2_LogonScenes");
    }

    //游戏继续
    internal void ClickGameContinue()
    {
        print(GetType() + "/ClickGameContinue()");
        //读取游戏的进度
        StartCoroutine("ContinueGame");

    }


    IEnumerator EnterNextScenes()
    {
        //设置场景为淡出效果
        FadeInAndOut.Instance.SetScenesToBlack();
        yield return new WaitForSeconds(1.5f);//给淡出留一些时间
        base.EnterNextScenes(ScenesEnum.LoginScenes);
    }

    IEnumerator  ContinueGame()
    {
        //读取单机的进度
        SaveAndLoading.GetInstance().LoadGlobalParameter();

        //设置场景为淡出效果
        FadeInAndOut.Instance.SetScenesToBlack();
        yield return new WaitForSeconds(1.5f);//给淡出留一些时间
        base.EnterNextScenes(GlobalParameterMgr.nextScenesName);
    }

}
