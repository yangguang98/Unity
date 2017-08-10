using UnityEngine;
using System.Collections;
/// <summary>
/// 场景加载，进度条实时更新类,
/// </summary>
public class LoadSceneProgressBar : MonoBehaviour {

    public static LoadSceneProgressBar _instance;
    private GameObject bg;
    private UISlider progressBar;
    private AsyncOperation ao = null;

    private bool isAsyn = false;
    void Awake()
    {
        _instance = this;
        bg = transform.Find("Bg").gameObject;
        progressBar = transform.Find("Bg/ProgressBar").GetComponent<UISlider>();
        gameObject.SetActive(false);

        //Application.LoadLevelAsync(2);//从后台开始异步的加载第二个场景
    }


    void Update()
    {
        if(isAsyn)
        {
            //在update中不断的去检测加载的进度，动态的更新progressBar
            progressBar.value = ao.progress;//加载的进度
        }
    }
    public void Show(AsyncOperation ao)
    {
        this.gameObject.SetActive(true);
        bg.SetActive(true);
        isAsyn = true;
        this.ao = ao;

    }
}
