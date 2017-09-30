using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

[CustomEditor (typeof(Map))]
public class MapEditor:Editor 
{
    [HideInInspector]
    public Map Map = null;

    List<FileInfo> m_files = new List<FileInfo>();//关卡列表
    int m_selectIndex = -1;//当前编辑的关卡索引号

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(Application .isPlaying )
        {
            //游戏运行的状态，开才允许你去绘制

            Map = target as Map;//关联的Mono脚本组件？？？？？？？？？？？？？？？？？？？？？

            EditorGUILayout.BeginHorizontal();

            int currentIndex = EditorGUILayout.Popup(1, GetNames(m_files));//第一个为默认选择值得索引，第二个为可以提供选择的值，返回值为当前选择值的索引
            if (currentIndex != m_selectIndex)
            {
                m_selectIndex = currentIndex;
                Debug.Log(m_selectIndex);
                LoadLevel();//加载索引
            }

            if (GUILayout.Button("读取列表"))
            {
                //读取关卡列表，，默认显示第一个关卡
                LoadLevelFiles();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("清除塔点"))
            {
                Map.ClearHolder();
            }
            if (GUILayout.Button("清除路径"))
            {
                Map.ClearRoad();
            }

            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("保存数据"))
            {
                SaveLevel();
            }

        }

        if(GUI.changed)
        {
            //ui界面的字段的值发生了变化，，要存入到磁盘中
            EditorUtility.SetDirty(target);
        }
        
    }


    string[] GetNames(List<FileInfo > files)
    {
        //获取文件的名字
        List<string> names = new List<string>();
        foreach (FileInfo file in files)
        {
            names.Add(file.Name);

        }
        return names.ToArray();
    }

    void LoadLevelFiles()
    {
        //加载关卡文件
        Clear();//清除状态
        m_files = Tools.GetLevelFiles();//加载关卡信息列表

        if (m_files.Count > 0)
        {
            m_selectIndex = 0;
            LoadLevel();
        }

    }

    void SaveLevel()
    {
        Level level = Map.Level;//获取当前加载的关卡

        List<Point> list = null;

        //收集方塔点
        list = new List<Point>();
        for(int i=0;i<Map.Grid.Count;i++)
        {
            Tile tile = Map.Grid[i];
            if(tile.CanHold )
            {
                Point p = new Point(tile.X, tile.Y);
                list.Add(p);
            }
        }

        level.Holder = list;



        //收集寻路点
        list = new List<Point>();
        for(int i=0;i<Map.Road .Count ;i++)
        {
            Tile tile = Map.Road[i];
            Point p = new Point(tile.X, tile.Y);
            list.Add(p);
        }
        level.Path = list;


        //保存关卡
        string fileName = m_files[m_selectIndex].FullName;//路径
        Tools.SaveLevel(fileName, level);//写入到文件中


        //弹框提示
        EditorUtility.DisplayDialog("保存关卡数据", "保存成功", "确定");
    }

    void LoadLevel()
    {
        //编辑器中的加载列表

        FileInfo file = m_files[m_selectIndex];
        Level level = new Level();
        Tools.FillLevel(file.FullName, ref level);//将文件转化为Level对象
        Map.LoadLevel(level);//加载数据存储到本地，，改变本地显示
    }

    void Clear()
    {
        //清除本类中的字段,,,由于每次加载过程中，关联的Map组件都不会发生变化，因此不要将其清空
        m_files.Clear();
        m_selectIndex = -1;
    }
}
