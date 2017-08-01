using UnityEngine;
using System.Collections;
//核心层：  核心层的参数
public class KernalParameter
{

    //暂时不用
//#if UNITY_STANDALONE_WIN
//    //系统配置信息_日志路径
//    internal static readonly string SystemLogPath="file://"+Application.dataPath+"/StreamingAssets/SystemConfigInfo.xml";
//    internal static readonly string SystemLogRootName="SystemConfigInfo";

//    internal static readonly string SystemDialogPath = "file://" + Application.dataPath + "/StreamingAssets/SystemDialogsInfo.xml"; //对话系统XMl路径
//    internal static readonly string SystemDialogRootNamePath = "Dialogs_CN";//对话系统XMl根节点名称
//#endif

//#if UNITY_ANDROID
//    //系统配置信息_日志路径
//    internal static readonly string SystemLogPath=Application.dataPath+"!/Assets/SystemConfigInfo.xml";
//    internal static readonly string SystemLogRootName="SystemConfigInfo";

//     internal static readonly string SystemDialogPath = Application.dataPath + "!/Assets/SystemDialogsInfo.xml"; //对话系统XMl路径
//    internal static readonly string SystemDialogRootNamePath = "Dialogs_CN";//对话系统XMl根节点名称

//#endif

//#if UNITY_IPHONE
//    //系统配置信息_日志路径
//    internal static readonly string SystemLogPath=Application.dataPath+"/Raw/SystemConfigInfo.xml";
//    internal static readonly string SystemLogRootName="SystemConfigInfo";


//    internal static readonly string SystemDialogPath = Application.dataPath + "/Raw/SystemDialogsInfo.xml"; //对话系统XMl路径
//    internal static readonly string SystemDialogRootNamePath = "Dialogs_CN";//对话系统XMl根节点名称


//#endif

    //得到日志路径
    public static string GetLogPath()
    {
        string logPath = null;

        //Android或者iphone环境
        if(Application.platform ==RuntimePlatform.Android ||Application.platform ==RuntimePlatform.IPhonePlayer )
        {
            logPath = Application.streamingAssetsPath + "/SystemConfigInfo.xml";
        }
        else
        {
            //Win环境
            logPath = "file://" + Application.streamingAssetsPath + "/SystemConfigInfo.xml";
        }
        return logPath;
    }

    //得到日志根节点名称
    public static string GetLogRootNodeName()
    {
        string rootNodeName = null;
        rootNodeName = "SystemConfigInfo";
        return rootNodeName;
    }

    //得到对话系统XML路径
    public static string GetDialogXMLPath()
    {
        string XMLPath = null;

        //Android或者iphone环境
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            XMLPath = Application.streamingAssetsPath + "/SystemDialogsInfo.xml";
        }
        else
        {
            //Win环境
            XMLPath = "file://" + Application.streamingAssetsPath + "/SystemDialogsInfo.xml";
        }
        return XMLPath;
    }

    //得到对话系统XML根节点
    public static string GetDialogXMLRootNode()
    {
        string xmlRootName = null;
        xmlRootName = "Dialogs_CN";
        return xmlRootName;
    }
}
