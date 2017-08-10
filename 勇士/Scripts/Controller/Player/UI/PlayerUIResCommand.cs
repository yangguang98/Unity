using UnityEngine;
using System.Collections;
/// <summary>
/// 控制层：玩家Ui界面响应处理
/// </summary>
public class PlayerUIResCommand : Command  {

    public static PlayerUIResCommand Instance;

    void Awake()
    {
        Instance = this;
    }

    public void ExitGame()
    {
        StartCoroutine("SavingGame");
    }

    //处理退出
    IEnumerator SavingGame()
    {
        SaveAndLoading.GetInstance().SaveGameProgress();
        Debug.Log(GetType() + "/SavingGame()");
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
}
