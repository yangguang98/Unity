using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//进入到一个场景当中，才可以注册场景中对应的视图，因为此时的组件才会被Unity调用
//进入一个场景的时候，会注册场景中所有视图对应的View,注册视图的时候，会注册该视图所关心的事件，
public class EnterSceneCommand:Controller
{

    public override void Execute(object data)
    {
        //进入到场景中就注册所有的视图，在注册视图的过程中，会注册视图关心的事件

        SceneArgs e = data as SceneArgs;
        switch (e.SceneIndex)
        {
            case 0://Init
                break;
            case 1://Start
                RegisterView(GameObject.Find("UIStart").GetComponent<UIStart>());
                break;
            case 2://Select
                RegisterView(GameObject.Find("UISelect").GetComponent<UISelect>());
                break;
            case 3://Level
                RegisterView(GameObject.Find("TowerPopup").GetComponent<TowerPopup>());
                RegisterView(GameObject.Find("Map").GetComponent<Spwaner>());
                RegisterView(GameObject.Find("Canvas").transform.Find ("UIBoard").GetComponent<UIBoard>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UICountDown").GetComponent<UICountDown>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIWin").GetComponent<UIWin>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UILost").GetComponent<UILost>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UISystem").GetComponent<UISystem>());//，GameObject.Find只能寻active物体，如果物体不是active那么就不能用该方法了
                break;
            case 4:
                GameObject.Find("UIComplete").GetComponent<UIComplete>();
                break;
            default :
                break;
        }
    }
}
