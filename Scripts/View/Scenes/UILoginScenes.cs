using UnityEngine;
using System.Collections;
using UnityEngine.UI ;
using Controller;
using kernel;

/// <summary>
/// 视图层：
/// </summary>
public class UILoginScenes : MonoBehaviour {

    public GameObject goSwordHero;             //少年剑侠
    public GameObject goMagicHero;        //魔杖真人
    public GameObject swordHeroInfo;      
    public GameObject magicHeroInfo;
    public InputField inputFiled;

    void Start()
    {
        //获取玩家的类型（系统默认值）
        GlobalParameterMgr.playerType = PlayerType.Magic;
        inputFiled.text = "李逍遥";
    }

    //选择少年剑侠
    public void ChangeToSwordHero()
    {
        //显示对象
        goSwordHero.SetActive(true);
        goMagicHero.SetActive(false);

        //显示UI
        swordHeroInfo.SetActive(true);
        magicHeroInfo.SetActive(false);

        //修改玩家类型
        GlobalParameterMgr.playerType = PlayerType.Sword;

        //音效
        LoginScenesCommand.Instance.PlayAudioEffectOfSword();
    }

    //选择魔杖真人
    public void ChangeToMagicHero()
    {
        //显示对象
        goSwordHero.SetActive(false);
        goMagicHero.SetActive(true);

        //显示UI 
        swordHeroInfo.SetActive(false);
        magicHeroInfo.SetActive(true);

        //修改玩家类型
        GlobalParameterMgr.playerType = PlayerType.Magic;

        //音效
        LoginScenesCommand.Instance.PlayAudioEffectOfMagic();
    }

    //提交信息
    public void SubmitInfo()
    {
        //获取名称
        GlobalParameterMgr.playerName = inputFiled.text;

        //跳转到下一个场景
        //调用控制层
        LoginScenesCommand.Instance.EnterNextScenes();

    }
}
