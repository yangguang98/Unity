using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


[RequireComponent (typeof(ObjectPool))]//!!!!!!!!!!!!!!!!!!!!!!!!!自动添加脚本
[RequireComponent(typeof(Sound))]
[RequireComponent(typeof(StaticData))]
public class Game : ApplicationBase<Game> 
{

    //全局访问功能
    [HideInInspector]
    public ObjectPool objectPool = null;
    [HideInInspector ]
    public Sound sound = null;
    [HideInInspector ]
    public StaticData staticData = null;

    //全局方法
    public void LoadScene(int level)
    {
        //加载新的场景，在加载之前需要调用事件，对旧的场景做一些处理

        //----退出场旧场景
        SceneArgs e = new SceneArgs();//参数
        e.SceneIndex = SceneManager.GetActiveScene().buildIndex;//!!!!!!!!!!!!!!!!
        SendEvent(Consts.E_ExitScene, e);//发布事件

        //---加载新场景
        SceneManager.LoadScene(level, LoadSceneMode.Single);

    }

    void OnLevelWasLoaded(int level)
    {
        //在加载完新的场景后，自动调用触发事件
        Debug.Log("OnLevelWasLoaded:" + level);
        SceneArgs e = new SceneArgs();//事件参数
        e.SceneIndex = level;
        SendEvent(Consts.E_EnterScene, e);//发布事件
    }

    //游戏入口
	void Start()
    {

        Object.DontDestroyOnLoad(this.gameObject);//确保Game对象一直存在
        //全局单例赋值
        objectPool = ObjectPool.Instance;
        sound = Sound.Instance;
        staticData = StaticData.Instance;

        RegisterController(Consts.E_StartUp, typeof(StartUpCommand));//注册启动命令

        //启动游戏
        SendEvent(Consts.E_StartUp);//每次的SendEvent实际就是执行控制器中的execut函数
    }
}
