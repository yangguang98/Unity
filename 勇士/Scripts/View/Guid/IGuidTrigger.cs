using UnityEngine;
using System.Collections;
/// <summary>
/// 视图层:新手引导模块--"引导触发接口"
/// </summary>
public interface IGuidTrigger  {


	//检查触发条件 true ：表示条件成立，出发后续业务逻辑
    bool CheckCondition();

    //运行业务逻辑  true:表示业务逻辑执行完毕
    bool RunOperation();
}
