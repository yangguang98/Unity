using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 场景异步加载 ，后台逻辑处理，相当于一个全局的管理
/// </summary>
public class LoadingScenesCommand : MonoBehaviour {

	IEnumerator  Start()
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT2);

        //关卡预处理逻辑
        StartCoroutine("SceneSPreProgressing");

        //垃圾收集工作
        StartCoroutine("HandleGC");
    }


    //关卡预处理逻辑
    IEnumerator SceneSPreProgressing()
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT2);
        switch (GlobalParameterMgr.nextScenesName )
        {
            case ScenesEnum.StartScenes:
                break;
            case ScenesEnum.LoginScenes:
                break;
            case ScenesEnum.LevelOne:
                StartCoroutine("ScenesProgressing_LevelOne");
                break;
            case ScenesEnum.LevelTwo:
                break;
            case ScenesEnum.MajorCity:
                break;
            case ScenesEnum.TestScenes:
                break;
            default:
                break;
        }

    }

    //第一关卡预处理
    IEnumerator ScenesProgressing_LevelOne()
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT1);
        //参数赋值  对日志进行初始化
        DialogDataAnalyze.GetInstance().SetXmlPathAndRootNodeName(KernalParameter.GetDialogXMLPath(), KernalParameter.GetDialogXMLRootNode());
        yield return new WaitForSeconds(0.4f);
        List<DialogDataFormat> dialogDataList = DialogDataAnalyze._instance.GetAllXmlDataArray();
        
        //测试给“对话数据管理器”加载数据
        DialogDataMgr.GetInstance().LoadAllDialogData(dialogDataList);
    }

    //垃圾收集工作
    IEnumerator HandleGC()
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT2);
        //卸载不用的资源
        Resources.UnloadUnusedAssets();
        //强制垃圾收集
        System.GC.Collect();//这个垃圾收集比较消耗资源，因此在加载场景的时候进行收集
    }
}
