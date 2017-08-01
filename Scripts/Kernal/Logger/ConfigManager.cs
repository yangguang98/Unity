using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;

///
///   接口：配置管理器
///   作用：读取系统核心XML配置信息
public class ConfigManager : IConfigManager {

    private static Dictionary<string, string> appSetting;

    public ConfigManager(string logPath,string xmlRootNodeName)
    {
        appSetting = new Dictionary<string, string>();
        IniteAndAnalysisXML(logPath,xmlRootNodeName);
    }

    //解析XML
    private void IniteAndAnalysisXML(string logPath,string xmlRootNodeName)
    {
        //参数检查
        if(string.IsNullOrEmpty (logPath)||string.IsNullOrEmpty (xmlRootNodeName ))
        {
            return;
        }
        XDocument xmlDoc;
        XmlReader xmlReader;

        try
        {
            xmlDoc = XDocument.Load(logPath);   //加载日志路径
            xmlReader = XmlReader.Create(new StringReader(xmlDoc.ToString()));  //创建读写器
        }
        catch
        {
            //TODO
            throw new XmlException(GetType() + "/IniteAndAnalysisXML()/XML Analysis Exception Check");
        }

        while(xmlReader.Read ())  //首先到达根节点
        {
            //读写指定根节点下的内容
            if(xmlReader.IsStartElement ()&&xmlReader.LocalName ==xmlRootNodeName )
            {
                using (XmlReader xmlReaderItem = xmlReader.ReadSubtree())//重新创建了一个reader ,用于读取当前节点及其所有的子节点，范围为其所有的子节点，由于这里的起点为<SystemConfigInfo>，，则读取为<SystemConfigInfo>下的所有的子节点
                {
                    while(xmlReaderItem .Read())
                    {
                        //节点元素  LogPath 
                        if(xmlReaderItem.NodeType ==XmlNodeType.Element )
                        {
                            string node = xmlReaderItem.Name;
                            xmlReaderItem.Read();
                            //内容 LogPath的内容
                            if(xmlReaderItem .NodeType ==XmlNodeType.Text )
                            {
                                appSetting[node] = xmlReaderItem.Value;
                            }
                        }
                    }
                }
            }
        }
    }

    public Dictionary<string, string> AppSetting
    {
        get 
        {
            return appSetting;
        }
    }

    public int GetAppSettingMaxNum()
    {
        if(appSetting!=null&&appSetting.Count>=1)
        {
            return appSetting.Count;
        }
        else
        {
            return 0;
        }
    }
}
