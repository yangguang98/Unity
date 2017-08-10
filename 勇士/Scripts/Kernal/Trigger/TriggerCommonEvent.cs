 using UnityEngine;
using System.Collections;

/// <summary>
/// 核心层：通用触发脚本
/// 功能：1.NPC与敌人对话触发
///      2.存盘/继续
///      3.加载与启用特此那个的脚本
///      4.触发“对话框”
///     
/// </summary>
public class TriggerCommonEvent : MonoBehaviour
{

    //事件
    public static event CommonTriggerDelegate commonTriggerEvent;

    //对话类型
    public CommonTriggerType triggerType = CommonTriggerType.None;

    //英雄标签名称
    public string tagNameByHero = "Player";

    void Awake()
    {
        //产生敌人触发器
        TriggerCommonEvent.commonTriggerEvent += SpwanEnemy;//注册触发事件，，当主角触发某一个触发器后，就自动生成敌人
    }

    //生成敌人
    void SpwanEnemy(CommonTriggerType ctt)
    {
        switch (ctt)
        {
            case CommonTriggerType.Enemy1_Dialog:
                //第一区域动态产生敌人
                SpwanEnemy_A();
                break;
            case CommonTriggerType.Enemy2_Dialog:
                break;
            case CommonTriggerType.Enemy3_Dialog:
                break;
            default:
                break;
        }
    }
    void OnTriggerEnter(Collider con)
    {
        if (con.gameObject.tag == tagNameByHero)
        {
            if (commonTriggerEvent != null)
            {
                commonTriggerEvent(triggerType);
            }
        }
    }

    #region 产生敌人

    //第一区域动态产生敌人
    void SpwanEnemy_A()
    {

    }
    #endregion
}

/// <summary>
/// 通用委托
/// </summary>
/// <param name="Ctt"></param>
public delegate void CommonTriggerDelegate(CommonTriggerType Ctt);

public enum CommonTriggerType
{
    None,
    NPC1_Dialog,
    NPC2_Dialog,
    NPC3_Dialog,
    NPC4_Dialog,
    NPC5_Dialog,
    Enemy1_Dialog,
    Enemy2_Dialog,
    Enemy3_Dialog,
    Enemy4_Dialog,
    Enemy5_Dialog,
    SaveDataOrScenes,
    LoadDataOrScenes,
    EnableScripts1,
    EnableScripts2,
    ActiveConfigWindows,
    ActiveDialogWindows
}
