using UnityEngine;
using System.Collections;
/// <summary>
/// 视图层：第一关卡场景
/// 描述：第一关卡场景控制
/// </summary>
public class UILevelOneScenes : MonoBehaviour {

    public GameObject goUINormalATK;         //普通攻击
    public GameObject goUIMagicATK_A;        //A
    public GameObject goUIMagicATK_B;        //B
    public GameObject goUIMagicATK_C;        //C
    public GameObject goUIMagicATK_D;        //D

    IEnumerator Start()
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT2); 
        /*大招是否启用*/
        goUIMagicATK_A.GetComponent<ATKBtnCDEffectView>().EnableSelf();   //启用
        goUIMagicATK_B.GetComponent<ATKBtnCDEffectView>().EnableSelf();   //启用
        goUIMagicATK_C.GetComponent<ATKBtnCDEffectView>().DisableSelf();  //不启用
        goUIMagicATK_D.GetComponent<ATKBtnCDEffectView>().DisableSelf();  //不启用u
    }
}
