using UnityEngine;
using System.Collections;

public class HpBarManager : MonoBehaviour {

    public static HpBarManager _instance;
    public GameObject hpBarPerfab;
    public GameObject hudTextPrefab;


    void Awake()
    {
        _instance = this;
    }

    public GameObject  GetHpBar(GameObject target)
    {
        //创建一个血条 跟随target

        GameObject go=NGUITools.AddChild(this.gameObject, hpBarPerfab);//添加物体达到实例化的目的
        go.GetComponent<UIFollowTarget>().target = target.transform;

        return go;
    }

    public GameObject GetHudText(GameObject  target)
    {
        GameObject go = NGUITools.AddChild(this.gameObject, hudTextPrefab);
        go.GetComponent<UIFollowTarget>().target = target.transform;
        return go;
    }
}
