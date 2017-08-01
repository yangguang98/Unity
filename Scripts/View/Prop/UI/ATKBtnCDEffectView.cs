using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 视图层：CD冷却
/// </summary>
public class ATKBtnCDEffectView : MonoBehaviour {

    public Text TxtCountNumber;           //数字倒计时控件
    public Image imgCircle;
    public GameObject goWhiteAndBlack;
    public float cdTime = 2f;
    private float timer = 0f;
    private bool isStartTimer = false;    //是否开始计时
    private Button btnSelf;               //本Button
    private bool enable = false;          //是否启用本控件

    void Start()
    {
        TxtCountNumber.enabled = false;        //倒计时不显示
        btnSelf = this.GetComponent<Button>();
        EnableSelf();            //启用本控件
    }


    void Update()
    {
        if(enable)//是否启用本控件
        {
            if (isStartTimer)
            {
                goWhiteAndBlack.SetActive(true);
                timer += Time.deltaTime;

                //控件倒计时显示
                TxtCountNumber.text = Mathf.RoundToInt(cdTime - timer).ToString ();

                imgCircle.fillAmount = timer / cdTime;     //所占比例
                btnSelf.interactable = false;
                if (timer >= cdTime)
                {
                    TxtCountNumber.enabled = false;        //倒计时不显示
                    btnSelf.interactable = true;           //启用按钮  
                    isStartTimer = false;
                    imgCircle.fillAmount = 1;
                    goWhiteAndBlack.SetActive(false);
                    timer = 0f;
                }
            }
        }
        
    }//Update


    //响应用户点击
    public void ResponseBtnClick()
    {
        isStartTimer = true;
        btnSelf.interactable = false;  //按钮禁用
        TxtCountNumber.enabled =true;  //显示控件
    }

    //启用自己
    public void EnableSelf()
    {
        enable = true;
        goWhiteAndBlack.SetActive(false);
        btnSelf.interactable = true;
    }

    //禁用自己
    public void DisableSelf()
    {
        enable = false;
        goWhiteAndBlack.SetActive(true);
        btnSelf.interactable = false;
    }
}
