using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//一些需要共享的数据  ，，，常驻内存，这些数据被初始化后，就常驻内存，，，直到游戏结束（关闭游戏）
public class GameModel : Model
{
    

    #region 常量
    #endregion

    #region 事件
   

    #endregion

    #region 字段

    List<Level> m_Levels = new List<Level>();//所有的关卡信息


    int m_PlayLevelIndex = -1;//当前玩的游戏关卡索引

    


    int m_GameProgress = -1;//当前游戏进度，要存档的，

    
    int gold = 0;//游戏当前分数


    bool m_isPlaying = false ;//游戏是否在进行

    
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.M_GameModel; }
    }

    
    public List<Level> AllLevels
    {
        //返回所有关卡信息
        get 
        {
            return m_Levels;
        }
    }


    
    public Level PlayerLevel
    {
        //返回正在玩的关卡
        get
        {
            if(m_PlayLevelIndex <0||m_PlayLevelIndex >m_Levels .Count -1)
            {
                throw new IndexOutOfRangeException("关卡不存在");
            }
            return m_Levels[m_PlayLevelIndex];
        }
    }

    public int Gold
    {
        get { return gold; }
        set { gold = value; }
    }

    public bool IsPlaying
    {
        get { return m_isPlaying; }
        set { m_isPlaying = value; }
    }

    public int GameProgress
    {
        get { return m_GameProgress; }
    }

    public int PlayLevelIndex
    {
        get { return m_PlayLevelIndex; }
    }
    
    public int LevelCount
    {
        //关卡总数量
        get
        {
            return m_Levels.Count;
        }
    }


    public bool IsGamePassed
    {
        //是否通关
        get
        {
            return m_GameProgress >=LevelCount - 1;
        }
    }
    #endregion

    #region 方法


    
    public void Initialize()
    {
        //初始化    模型里面的数据一般需要初始化

        //构建Level集合
        List<FileInfo> files = Tools.GetLevelFiles();
        List<Level> levels = new List<Level>();
        for (int i = 0; i < files.Count; i++)
        {
            Level level = new Level();
            Tools.FillLevel(files[i].FullName, ref level);//这里要添加ref不然会报错，FileInfo里面包含的信息很多，
            levels.Add(level);
        }
        m_Levels = levels;//存储

        //读取游戏进度 ，，，读取
        m_GameProgress = Saver.GetProgress();

    }

    public void StartLevel(int levelIndex)
    {
        //游戏开始
        m_PlayLevelIndex =levelIndex;
        m_isPlaying = true;
    }

    public void StopLevel(bool isSuccess)
    {
        //游戏结束
        if(isSuccess &&PlayLevelIndex >GameProgress )
        {
            m_GameProgress = PlayLevelIndex;//修改内存值
            Saver.SetProgress(PlayLevelIndex);//修改本地值
        }
        m_isPlaying = false;
    }

    public void ClearProgress()
    {
        //清档
        m_PlayLevelIndex = -1;
        m_isPlaying = false;
        m_GameProgress = -1;

        Saver.SetProgress(-1);//进度需要存档
    }

    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    #endregion

    #region 帮助方法
    #endregion


}
