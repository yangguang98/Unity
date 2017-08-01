using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System;
/// <summary>
/// 核心层：“XML对话数据解析管理层"
/// 描述：
/// 
/// 　　功能：
/// 　　　　　对于对话XML，做数据解析
/// </summary>
public class DialogDataAnalyze : MonoBehaviour {

    public static DialogDataAnalyze _instance;

    private List<DialogDataFormat> dialogDataList;

   
    private string xmlPath;    //路径
    private string xmlRootName;  //根节点

    private const float TIME_DELAY = 0.2f;
    private const string XML_ATTRIBUTE_1 = "DialogSecNum";
    private const string XML_ATTRIBUTE_2 = "DialogSecName";
    private const string XML_ATTRIBUTE_3 = "SectionIndex";
    private const string XML_ATTRIBUTE_4 = "DialogSide";
    private const string XML_ATTRIBUTE_5 = "DialogPerson";
    private const string XML_ATTRIBUTE_6 = "DialogContent";

   

    void Awake()
    {
        
        dialogDataList = new List<DialogDataFormat>();
        DontDestroyOnLoad(this);
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f); //等待xml路径与根节点赋值
        if (!string.IsNullOrEmpty(xmlPath) && !string.IsNullOrEmpty(xmlRootName))
        {

            StartCoroutine("ReadXmlConfigByWWW");
        }
        else
        {
            Debug.LogError(GetType() + "/Start(),xmlPath or xmlRootName is Null ");
        }
    }

    //得到实例
    public static DialogDataAnalyze GetInstance()
    {
        if (_instance == null)
        {
            //_instance = new DialogDataAnalyze();
            _instance = new GameObject("DialogDataAnalyze").AddComponent<DialogDataAnalyze>(); //该脚本继承自MonoBehaviour，要挂载在游戏物体上才能够运行。
        }
        return _instance;
    }

    //设置xml路径与根节点的名称
    public void SetXmlPathAndRootNodeName(string xmlPath1,string xmlRootName1)
    {
        Debug.Log("123");
        if(!string.IsNullOrEmpty (xmlPath1)&&!string.IsNullOrEmpty (xmlRootName1))
        {
            xmlPath = xmlPath1;
            xmlRootName = xmlRootName1;
        }
        //StartCoroutine("ReadXmlConfigByWWW");
    }

    //得到脚本数据集合
    public List<DialogDataFormat> GetAllXmlDataArray()
    {
        if (dialogDataList != null && dialogDataList.Count >= 1)
        {
            return dialogDataList;
        }
        else
        {
            return null;
        }
    }

    //读取XML配置文件
    IEnumerator ReadXmlConfigByWWW()
    {
        WWW www = new WWW(xmlPath);
        while(!www.isDone )
        {
            yield return www;
        }
        InitXmlConfig(www, xmlRootName);
        yield return new WaitForSeconds(0.3f);
    }

    //初始化XML
    private void InitXmlConfig(WWW www,string xmlrootName)
    {
        if(dialogDataList ==null||string.IsNullOrEmpty (www.text))
        {
            Debug.LogError(GetType() + "/InitXmlConfig(),www or xmlRootName is Null ");
            return;
        }

        //解析Xml
        XmlDocument xmlDoc = new XmlDocument();
        //xmlDoc.LoadXml(www.text);// 这种方式发布到android终端，不能正确的输出中文 用下面的四句话去代替

        System.IO.StringReader stringReader = new StringReader(www.text);
        stringReader.Read();
        System.Xml.XmlReader reader = System.Xml.XmlReader.Create(stringReader);
        xmlDoc.LoadXml(stringReader.ReadToEnd());

        XmlNodeList nodes = xmlDoc.SelectSingleNode(xmlRootName).ChildNodes;//直接选取所有的节点
        foreach(XmlElement item in nodes)
        {
            //实例化dialogDataFormat
            DialogDataFormat data = new DialogDataFormat();
            data.DialogSecNum = Convert.ToInt32(item.GetAttribute(XML_ATTRIBUTE_1));
            data.DialogSecName = Convert.ToString(item.GetAttribute(XML_ATTRIBUTE_2));
            data.SectionIndex = Convert.ToInt32(item.GetAttribute(XML_ATTRIBUTE_3));
            data.DialogSide = item.GetAttribute(XML_ATTRIBUTE_4);
            data.DialogPerson = item.GetAttribute(XML_ATTRIBUTE_5);
            data.DialogContent = item.GetAttribute(XML_ATTRIBUTE_6);
            dialogDataList.Add(data);
        }
    }//end_InitXmlConfig
}
