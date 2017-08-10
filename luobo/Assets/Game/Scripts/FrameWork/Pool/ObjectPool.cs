using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ObjectPool:Singleton <ObjectPool>
{
    public string ResourceDir = "";

    Dictionary<string, SubPool> m_pools = new Dictionary<string, SubPool>();


    
    public GameObject Spawn(string name)
    {
        //取对象  ,参数是对象的名字

        if(!m_pools .ContainsKey (name))
        {
            RegisterNew(name);
        }
        SubPool pool = m_pools[name];
        return pool.Spwan();//委托池子去产生一个对象
    }


    
    public void Unspwan(GameObject go)
    {
        //回收对象,,参数是GameObject 

        SubPool pool = null;
        foreach(SubPool p in m_pools.Values )//这里的m_pools是一个字典
        {
            if(p.Contains (go))
            {
                pool = p;
                break;
            }
        }
        pool.Unspwan(go);
    }

    
    public void UnSpwanAll()
    {
        //回收所有对象

        foreach(SubPool item in m_pools.Values  )
        {
            item.UnspwanAll();
        }
    }

    
    void RegisterNew(string name)
    {
        //创建新子池子

        //预设路径
        string path = "";
        if(string.IsNullOrEmpty (ResourceDir ))
        {
            path = name;
        }
        else
        {
            path = ResourceDir + "/" + name;
        }

        //加载预设
        GameObject prefab = Resources.Load<GameObject>(path);

        //创建对象池子
        SubPool pool = new SubPool(prefab);
        m_pools.Add(pool.Name, pool);//添加到大的池子
    }
}
