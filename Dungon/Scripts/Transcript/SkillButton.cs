using UnityEngine;
using System.Collections;

public class SkillButton : MonoBehaviour {


    public PosType posType = PosType.Basic;
    private PlayerAnimation playerAnimation=null;
    public float coldTime = 4;//总的冷却时间
    private float coldTimer = 0;//表示剩余的冷却时间
    private UISprite  maskSprite;
    private UIButton btn;
	// Use this for initialization



	void Start()
    {
        playerAnimation = TranscriptManager._instance .player.GetComponent<PlayerAnimation>();
        if(transform .Find ("Mask"))
        {
            //basic中也会调用该方法，防止出现空指针

            maskSprite = transform.Find("Mask").GetComponent<UISprite>();
        }

        btn = this.GetComponent<UIButton>();
        
    }

    void Update()
    {
        if (maskSprite == null)
            return;
        if(coldTimer>0)
        {
            coldTimer -= Time.deltaTime;
            maskSprite.fillAmount = coldTimer / coldTime;//得到比例
            if(coldTimer<=0)
            {
                Enable();
            }
        }

        else
        {
            maskSprite.fillAmount = 0;

        }
    }
    void OnPress(bool isPress)
    {
        //按下和抬起都会调用这个函数，，，按下isPress就为true，，抬起就为false
        //点击之后需要调用PlayerAnimation中的方法
        playerAnimation.OnAttackButtonClick(isPress, posType);
        Debug.Log("isPress:"+isPress);
            if (isPress && maskSprite != null)//基础攻击按钮不需要冷却
            {
                //开始执行冷却效果

                coldTimer = coldTime;
                Disable();
            }
        
    }

    void Disable()
    {
        this.GetComponent<Collider>().enabled = false;
        btn.SetState(UIButtonColor.State.Normal,true);
    }

    void Enable()
    {
        this.GetComponent<Collider>().enabled = true;
        btn.SetState(UIButton.State.Normal, true);
    }
}
