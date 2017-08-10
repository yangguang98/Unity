using UnityEngine;
using System.Collections;

public class SkillItemUI : MonoBehaviour {

    public PosType posType;
    private Skill skill;
    private UIButton button;
    private UISprite sprite;
    public bool isSelected = false;

    private UIButton Button
    {
        get
        {
            if(button==null)
            {
                button = this.GetComponent<UIButton>();
                
            }
            return button;
        }
    }
    private UISprite Sprite
    {
        get
        {
            if(sprite==null)
            {
                sprite = this.GetComponent<UISprite>();
            }

            return sprite;
        }

     }
    
    void Start()
    {
        SkillManager._instance.OnSyncSkillComplete += this.OnSyncSkillComplete;
    }

    public void OnSyncSkillComplete()
    {
        //当从服务器端，得到技能信息后，再去修改UI
        UpdateShow();
        if (isSelected)
        {
            //默认选择第一个技能（isSelected )

            OnClick();
        }
    }
    void UpdateShow()
    {
        skill = SkillManager._instance.GetSkillByPosition(posType);
        this.skill = skill;
       
        Sprite.spriteName = skill.Icon;
        Button.normalSprite = skill.Icon;//由于将技能做成了button,因此要修改被点击后的normal属性
    }

    void OnClick()
    {
       
            transform.parent.parent.SendMessage("OnSkillClick", skill);
        
    }

    void OnDestroy()
    {
        if(SkillManager._instance !=null)
        {
            SkillManager._instance.OnSyncSkillComplete -= this.OnSyncSkillComplete;
        }
    }
}
