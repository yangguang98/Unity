using UnityEngine;
using System.Collections;
/// <summary>
/// 核心层：资源动态加载管理器
/// 
/// 描述：
///    1.属于“脚本插件”，适用于任何项目，开发出具备“对象缓冲”功能的资源加载脚本
///    
/// </summary>
public class ResourcesMgr : MonoBehaviour {

    private static ResourcesMgr _instance;
    private Hashtable ht  = null;                                       //容器键值对集合

    private  ResourcesMgr ()
    {
        ht = new Hashtable();
    }

    public static ResourcesMgr GetInstance()
    {
        if(_instance ==null)
        {
            _instance =new GameObject ("_ResourcesMgr").AddComponent <ResourcesMgr >();
            
        }
        return _instance;
    }

    //从缓存池中取物体
    public T LoadResource<T>(string path,bool isCatch)where T:UnityEngine.Object
    { 
        if(ht.Contains (path))
        {
            return ht[path] as T;
        }

        T resources=Resources.Load<T>(path);
        if( resources==null)
        { 
            Debug.LogWarning(GetType() + "/GetInstance()/TResources 提取的资源找不到，请检查,path:"+path);
        }else if(isCatch )
        {
            ht.Add(path, resources);
        }

        return resources;
    }

    //调用资源（带对象缓冲）
    public GameObject LoadAsset(string path,bool isCatch)
    {
        GameObject go = LoadResource<GameObject>(path, isCatch);
        GameObject goClone = GameObject.Instantiate(go) as GameObject;
        if(goClone==null)
        {
            Debug.LogWarning(GetType() + "/LoadAsset()/实例化资源不成功，请检查,path:" + path);
        }
        return goClone;
    }
}
