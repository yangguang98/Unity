using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 视图层：英雄技能窗体
/// 描述：
/// </summary>
public class PanelSkill : MonoBehaviour {

    //查看项目
    public Image imgNormalATK;
    public Image imgCloseATK;
    public Image imgJumpATK;
    public Image imgFireATK;
    public Image imgThunderATK;

    public Image imgExit;

    //显示文字控件
    public Text txtSkillName;
    public Text txtSkillDescription;

    void Awake()
    {
        //攻击贴图注册
        RigisterAttack();
    }
    void Start()
    {
        //默认显示
        txtSkillName.text = "普通技能";
        txtSkillDescription.text = "普通连招大几，当升级不同等级时候，给敌人的大几会响应提高";
    }
    //攻击贴图注册
    public void RigisterAttack()
    {
        if(imgNormalATK !=null)
        {
            EventTriggerListener .Get (imgNormalATK .gameObject).onClick +=NormalATk;
        }
        if(imgCloseATK !=null)
        {
            EventTriggerListener .Get (imgCloseATK .gameObject).onClick +=CloseATk;
        }
        if(imgJumpATK !=null)
        {
            EventTriggerListener .Get (imgJumpATK .gameObject).onClick +=JumpATK;
        }
        if(imgFireATK !=null)
        {
            EventTriggerListener .Get (imgFireATK .gameObject).onClick +=FireATK;
        }
        if(imgThunderATK !=null)
        {
            EventTriggerListener.Get(imgThunderATK.gameObject).onClick += ThunderATK;
        }
    }

    //普通攻击
    private void NormalATk(GameObject go)
    {
        if(go==imgNormalATK .gameObject )
        {
            txtSkillName.text = "普通技能";
            txtSkillDescription.text = "普通连招大几，当升级不同等级时候，给敌人的大几会响应提高";
        }
    }

    //近距离攻击
    private void CloseATk(GameObject go)
    {
        if (go == imgCloseATK.gameObject)
        {
            txtSkillName.text = "近距离技能";
            txtSkillDescription.text = "近距离技能大招，给敌人以激烈打击，没有方向性！";
        }
    }

    //跳跃攻击
    private void JumpATK(GameObject go)
    {
        if (go == imgJumpATK.gameObject)
        {
            txtSkillName.text = "跳跃技能";
            txtSkillDescription.text = "跳跃大招技能，给敌人非常强烈的打击，具有方向性！";
        }
    }

    //火攻击
    private void FireATK(GameObject go)
    {
        if (go == imgFireATK.gameObject)
        {
            txtSkillName.text = "火攻技能";
            txtSkillDescription.text = "火攻大招技能";
        }
    }

    //雷电攻击
    private void ThunderATK(GameObject go)
    {
        if (go == imgThunderATK.gameObject)
        {
            txtSkillName.text = "雷电技能";
            txtSkillDescription.text = "雷电大招技能";
        }
    }
}
