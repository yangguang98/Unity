using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 视图层:新手引导模块--“新手引导管理器”
/// 作用：
///     控制与协调所有具体新手引导业务脚本的检查与执行
/// </summary>
public class GuidMgr : MonoBehaviour {

    //所有“新手引导”业务
    private List<IGuidTrigger> guideTriggerList = new List<IGuidTrigger>();

    IEnumerator Start()
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT2);

        //加入所有的“业务逻辑”脚本
        IGuidTrigger iTri_1 = TriggerDialogs.Instance;
        IGuidTrigger iTri_2 = triggerOperET.Instance;
        IGuidTrigger iTri_3 = TriggerOperVirtualKey.Instance;
        guideTriggerList.Add(iTri_1);
        guideTriggerList.Add(iTri_2);
        guideTriggerList.Add(iTri_3);

        //启动业务逻辑的检查
        StartCoroutine("CheckGuideState");
    }

    IEnumerator CheckGuideState()
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT2);
        while(true)
        {
            yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT5);
            for(int i=0;i<guideTriggerList.Count ;i++)
            {
                IGuidTrigger iTrigger=guideTriggerList [i];
                //检查每个业务脚本是否可以运行
                if(iTrigger .CheckCondition ())
                {
                    //每个业务脚本，执行业务逻辑
                    if (iTrigger.RunOperation())  //RunOperation运行结束后，就会将该IGuidTrigger移除
                    {
                        guideTriggerList.Remove(iTrigger);
                    }
                }
            }
        }
    }


}
