using UnityEngine;
using System.Collections;
using UnityEngine.UI ;

public class FadeInAndOut : MonoBehaviour {

    public static FadeInAndOut Instance;
    public float FloColorChangeSpeed=1;
    public GameObject goRawImage;
    private RawImage rawImage;

    private bool boolScenesToClear = true;//屏幕逐渐清晰
    private bool boolScenesToBlack = false;//屏幕逐渐暗淡

    void Awake()
    {
        Instance = this;
        if(goRawImage )
        {
            rawImage = goRawImage.GetComponent<RawImage>();
        }
    }

    void Update()
    {
        //屏幕淡入
        if(boolScenesToClear)
        {
            ScenesToClear();
        }else if(boolScenesToBlack)//屏幕淡出
        {
            ScenesToBlack();
        }
    }

    //淡入
    private void FadeToClear()
    {
        rawImage .color=Color.Lerp (rawImage .color ,Color.clear ,FloColorChangeSpeed *Time.deltaTime );
    }

    //淡出
    private void FadeToBlack()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.black, FloColorChangeSpeed * Time.deltaTime);
    }

    //屏幕淡入
    private void ScenesToClear()
    {
        //调用淡入淡出方法
        FadeToClear();
        
        //对终止条件做一些判定，修改一些bool值
        if(rawImage.color.a<=0.05)
        {
            rawImage.color = Color.clear;
            rawImage.enabled = false;
            boolScenesToClear = false;
        }
    }


    //屏幕淡出
    private void ScenesToBlack()
    {
        rawImage.enabled = true;
        FadeToBlack();

        //在update中终止调用的条件
        if(rawImage .color .a>=0.95)
        {
            rawImage.color = Color.black;
            boolScenesToBlack = true;
        }
    }
    
    //设置场景的淡入
    public void SetScenesToClear()
    {
        boolScenesToClear = false;
        boolScenesToBlack = true;
    }

    //设置场景的淡出
    public void SetScenesToBlack()
    {
        boolScenesToBlack = true;
        boolScenesToClear = false; 
    }
}
