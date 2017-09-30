using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class Singleton<T>:MonoBehaviour where T:MonoBehaviour 
{
    private static T m_instance = null;

    public static T Instance
    {
        get
        {
            return m_instance;
        }
    }

    protected virtual void Awake()
    {
        //Awake默认是私有的，要让子类继承该方法 ，那么就要改成非私有的方法
        m_instance = this as T;

    }
}