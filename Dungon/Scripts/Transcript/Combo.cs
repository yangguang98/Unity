using UnityEngine;
using System.Collections;

public class Combo : MonoBehaviour {

    public static Combo _instance;
    public float comboTime = 2;//连击时间（在这个时间内连击数增加）
    private int comboCount = 0;//连击数
    private float timer = 0;
    private UILabel numberLabel;
    void Awake()
    {
        _instance = this;
        this.gameObject.SetActive(false);
        numberLabel = transform.Find("NumberLabel").GetComponent<UILabel>();
    }


    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<=0)
        {
            this.gameObject.SetActive(false);
            comboCount = 0;
        }
    }
    public void ComboPlus()
    {
        //增加连击

        this.gameObject.SetActive(true);
        timer = comboTime;
        comboCount++;
        numberLabel.text = comboCount.ToString();
        transform.localScale = Vector3.one;//Shorthand for writing Vector3(1, 1, 1).
        iTween.ScaleTo(this.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.1f);//放大到1.5倍
        iTween.ShakePosition(this.gameObject, new Vector3(0.2f, 0.2f, 0.2f), 0.2f);//在指定的范围内震动
    }
}
