using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 核心层：触发游戏对象显示与隐藏
/// 描述：即手工版的“遮挡剔除”
/// 由两个触发器组成，靠近A标签场景的触发器显示A标签场景，靠近B标签场景的触发器显示B标签场景
/// </summary>
public class TriggerDiaplayAndHide : MonoBehaviour {

    public string tagByHero = "Player";                  //标签：英雄
    public string tagByDisObj = "TagDisplayName";        //标签：需要显示的对象
    public string tagByHideObj = "TagHideName";          //标签：需要隐藏的对象
    private GameObject[] goDisplayObjArray;                //需要显示的对象集合
    private GameObject[] goHideObjArray;                   //需要隐藏的对象集合 

    void Start()
    {
        //得到需要显示的游戏对象
        goDisplayObjArray = GameObject.FindGameObjectsWithTag(tagByDisObj);

        //得到需要隐藏的游戏对象
        goHideObjArray = GameObject.FindGameObjectsWithTag(tagByHideObj);
    }

    //进入触发区域
    void OnTriggerEnter(Collider col)
    {
        
        if(col.gameObject .tag ==tagByHero)
        {
            foreach (GameObject go in goDisplayObjArray )
            {
                go.SetActive(true);
            }
        }
    }

    //离开触发检测
    void OnTriggerExit(Collider col)
    {
        if(col.gameObject .tag ==tagByHero)
        {
            foreach (GameObject go in goHideObjArray)
            {
                go.SetActive(false);
            }
        }
    }
}
