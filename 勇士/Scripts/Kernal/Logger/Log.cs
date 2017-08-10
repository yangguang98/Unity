using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Threading;
using System.Collections.Generic;    //线程
/// <summary>
/// 核心层：日志调试系统（Log日志）
/// 描述：更加方便的对代码进行调试，调试系统程序
/// 基本原理：
///    1.把开发人员在代码转那个定义的调试语句，写入本日志的缓存
///    2.当缓存中数量超过定义的最大写入文件值，则把缓存内容调试语句一次性写入文本文件
/// </summary>
public static class Log  {
    private  static List<string> logArray;         //Log日志缓存数据
    private  static string logPath;                //日志路径
    private static State logState;                       //Log日志状态
    private static int logMaxCapacity;                   //Log日志最大容量
    private static int logBufferMaxNumber;               //log日志缓存最大容量,,大于这个值就往文件中写

    /*日志文件常量定义 */
    //XML配置文件，标签常量
    private const string XML_CONFIG_PATH = "LogPath";  //logPath
    private const string XML_CONFIG_STATE = "LogState";  //logState
    private const string XML_CONFIG_MAXCAPACITY = "LogMaxCapacity";  //logMaxCapacity
    private const string XML_CONFIG_BUFFERNUM = "LogBuffer";  //logBufferNum

    //日志状态部署模式
    private const string XML_CONFIG_DEVELOP = "Develop";
    private const string XML_CONFIG_SPECIAL = "Special";
    private const string XML_CONFIG_DEPLOY = "Deploy";
    private const string XML_CONFIG_STOP = "Stop";

    //临时变量
    private static string logPathTemp = null;
    private static string logStateTemp=null;
    private static string logMaxCapacityTemp = null;
    private static string logBufferMaxNumberTemp = null;

    static Log()
    {
        logArray = new List<string>();

        #if UNITY_STANDALONE_WIN ||UNITY_EDITOR

        IConfigManager configMgr = new ConfigManager(KernalParameter.GetLogPath(), KernalParameter.GetLogRootNodeName());
        logPathTemp = configMgr.AppSetting[XML_CONFIG_PATH];
        logStateTemp = configMgr.AppSetting[XML_CONFIG_STATE];
        logMaxCapacityTemp = configMgr.AppSetting[XML_CONFIG_MAXCAPACITY];
        logBufferMaxNumberTemp = configMgr.AppSetting[XML_CONFIG_BUFFERNUM];

        #endif
        
        if(string.IsNullOrEmpty (logPathTemp))
        {
            logPath = UnityEngine.Application.persistentDataPath + "\\DungeonFighterLog.txt";
        }
        else
        {
            logPath = logPathTemp;
        }

        
        if(!string.IsNullOrEmpty (logStateTemp ))
        {
            switch (logStateTemp)
            {
                case XML_CONFIG_DEVELOP :
                    logState = State.Develop;
                    break;
                case XML_CONFIG_SPECIAL:
                    logState =State.Special;
                    break;
                case XML_CONFIG_DEPLOY:
                    logState = State.Deploy;
                    break;
                case XML_CONFIG_STOP:
                    logState = State.Stop;
                    break;
                default:
                    logState = State.Stop;
                    break;
            }
        }
        else
        {
            logState = State.Stop;
        }

        
        if(!string.IsNullOrEmpty (logMaxCapacityTemp ))
        {
            logMaxCapacity = Convert.ToInt32(logMaxCapacityTemp);
        }
        else
        {
            logMaxCapacity = 2000;
        }
        
        if(!string.IsNullOrEmpty (logBufferMaxNumberTemp ))
        {
            logBufferMaxNumber = Convert.ToInt32(logBufferMaxNumberTemp);
        }
        else
        {
            logBufferMaxNumber = 1;
        }

        #if UNITY_STANDALONE_WIN ||UNITY_EDITOR
        //创建日志文件
        if(!File.Exists (logPath ))
        {
            File.Create(logPath);

            //关闭当前线程
            Thread.CurrentThread.Abort();//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //把日志文件中的数据同步到日志缓存中 
            SyncFileDataToLogArray();
        }
        #endif
        

    }//Log_end


    //把日志文件中的数据同步到日志缓存中 
    private static void SyncFileDataToLogArray()
    {
        if(!string.IsNullOrEmpty (logPath ))
        {
            StreamReader sr = new StreamReader(logPath);
            while(sr.Peek()>=0) //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                logArray.Add(sr.ReadLine()); 
            }
            sr.Close();
        }
    }

    //写数据到文件中，信息，重要等级
    public static void Write(string fileData,Level level)
    {
        //参数检查
        if(logState ==State.Stop )
        {
            return;
        }

        //如果日志缓存数量超过指定容量，则清空
        if(logArray .Count >=logMaxCapacity )
        {
            logArray.Clear();//清空缓存
        }

        if(!string.IsNullOrEmpty (fileData ))
        {
            //增加日期
            fileData = "Log State:" + logState.ToString() + "/" + DateTime.Now.ToString()+fileData;

            //不同日志状态，分特定的情形写入文件
            if(level==Level.High)
            {
                fileData = "@@@ Error Or Warring Or Important@@@" + fileData;
            }

            //不同的状态
            switch (logState)
            {
                case State.Develop:
                    AppendDataToFile(fileData);
                    break;
                case State.Special:
                    if(level==Level.High ||level==Level.Special)
                    {
                        AppendDataToFile(fileData);
                    }
                    break;
                case State.Deploy:
                    if(level==Level.High)
                    {
                        AppendDataToFile(fileData);
                    }
                    break;
                case State.Stop:

                    break;
                default:
                    break;
            }
        }
    }//Write_end
    public static void Write(string fileData)
    {
        Write(fileData, Level.Low);
    }//Write_end

    //追加数据到文件
    private static void AppendDataToFile(string fileData)
    {
        if(!string.IsNullOrEmpty (fileData ))
        {
            //调试信息数据追加到缓存集合中
            logArray.Add(fileData);
        }

        //缓存结婚数量超过一定指定数量（“LogBuffer”）,则将缓存中的数据写入到文件中
        if(logArray .Count >=logBufferMaxNumber )
        {
            //同步缓存中的数据信息到实体文件中
            SyncLogArrayToFile();
        }
        
    }

    


    #region 重要管理方法

    //查询日志缓存中所有数据
    public static List<string> QueryAllDataFromLogBuffer()
    {
        if(logArray ==null)
        {
            return null;
        }
        else
        {
            return logArray;
        }
    }

    //清除实体日志文件与日志缓存中所有数据
    public static void ClearLogAndBufferData()
    {
        if(logArray !=null)
        {
            logArray.Clear();
        }
        SyncLogArrayToFile();
    }

    //同步日志缓存中的信息到文件中
    public static void SyncLogArrayToFile()
    {
        if (!string.IsNullOrEmpty(logPath))
        {
            StreamWriter sw = new StreamWriter(logPath);
            foreach (string item in logArray)
            {
                sw.WriteLine(item);
            }
            sw.Close();
        }
    }

    #endregion


    #region 部署模式
    public enum State
    {
        Develop,       //开发
        Special,       //指定输出  
        Deploy,        //部署   只输出最核心的信息
        Stop           //停止

    }

    //调试信息的等级，表示其重要程度
    public enum Level
    {
        High,
        Special,
        Low
    }
#endregion
}
