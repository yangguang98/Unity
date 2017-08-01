using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI ;
using System.Collections.Generic;
/// <summary>
/// 视图层：场景异步加载
/// </summary>
public class UILoadingScenes : MonoBehaviour {

    public Slider SliLoadingProgress;         //进度条控件
    private float floProgress;                //进度数值
    private AsyncOperation asyOper;


    
   
    IEnumerator Start()
    {
        


        //测试代码
        //测试XML解析
        //参数赋值  对日志进行初始化
        Log.ClearLogAndBufferData();
        DialogDataAnalyze.GetInstance().SetXmlPathAndRootNodeName(KernalParameter.GetDialogXMLPath(), KernalParameter.GetDialogXMLRootNode());
        yield return new WaitForSeconds(0.4f);
        List<DialogDataFormat> dialogDataList = DialogDataAnalyze._instance.GetAllXmlDataArray();
        //Debug.Log(dialogDataList.Count);
        //foreach (DialogDataFormat item in dialogDataList)
        //{
        //    Log.Write("");
        //    Log.Write("DialogSecNum" + item.DialogSecNum);
        //    Log.Write("DialogSecName" + item.DialogSecName);
        //    Log.Write("DialogPerson" + item.DialogPerson);
        //}

        //测试给“对话数据管理器”加载数据
        DialogDataMgr.GetInstance().LoadAllDialogData(dialogDataList);

        GlobalParameterMgr.nextScenesName = ScenesEnum.MajorCity;


        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_1);
        StartCoroutine("LoadingScenesProgress");
    }

    //异步加载协程
	IEnumerator LoadingScenesProgress()
    {
        asyOper = SceneManager.LoadSceneAsync(ConvertEnumToString.GetInstance().GetStrByEnumScenes(GlobalParameterMgr.nextScenesName));
        floProgress = asyOper.progress;
        yield return asyOper;
    }

    //显示进度条
    void Update()
    {
        if (floProgress >= 0.98)
            return;
        if(asyOper ==null)
        {
            return;
        }
        floProgress = asyOper.progress;
        SliLoadingProgress.value = floProgress;
    }
}
