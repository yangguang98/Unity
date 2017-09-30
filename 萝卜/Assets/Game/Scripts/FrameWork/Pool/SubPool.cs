using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
//每一个subPool中存储的是一类游戏物体
public class SubPool
{

    GameObject m_prefab;//对象模板，，预设
    List<GameObject> m_objects = new List<GameObject>();//集合
    public string Name  
    {
        //名字标识  ，，，就是怪物的名字
        get { return m_prefab.name; }
    }

    public SubPool (GameObject prefab)
    {
        //构造
        this.m_prefab = prefab;
    }

    public GameObject Spwan()
    {
        GameObject go = null;
        foreach (GameObject obj in m_objects )
        {
            if(!obj.activeSelf )//看看！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
            {
                go = obj;
                break;
            }

            if(go==null)
            {
                go = GameObject.Instantiate(m_prefab);
                m_objects.Add(go);
            }
        } 
        go.SetActive(true);
        go.SendMessage("OnSpwan", SendMessageOptions.DontRequireReceiver);//产生了怪物以后，调用该怪物上的OnSpwan 方法，来对怪物的各种属性进行初始化
        return go; 
    }

    public void Unspwan(GameObject go)
    {
        //回收对象
        if(Contains(go))
        {
            go.SendMessage("OnUnspwan", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);//存在于池子中，，，然后将其状态设置为非激活状态
        }
    }

    public void UnspwanAll()
    {
        //回收所有对象
        foreach (GameObject item in m_objects )
        {
            if(item.activeSelf )
            {
                //处于活跃状态，则将其收回
                Unspwan(item);
            }
        }
    }

    public bool Contains(GameObject go)
    {
        //是否包含对象
        return m_objects.Contains(go);
    }

}