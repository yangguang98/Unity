using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 核心层：对话数据管理器（类）
/// 描述：
///      作用：根据“对话数据格式”定义，输入
///      “段落编号”，输出给定的对话内容（对话的双方，人名）
/// </summary>
public class DialogDataMgr  {

    private static DialogDataMgr _instance;   //本类实例
    private static List<DialogDataFormat> allDialogDataList;//所有的对话数据

    private static List<DialogDataFormat> currentDialogDataBuffer; //当前对话缓存
    private static int indexByDialogSection;    //对话序号
    private int originalDiaSectionNum = 1;

    private DialogDataMgr ()
    {
        allDialogDataList = new List<DialogDataFormat>();
        currentDialogDataBuffer = new List<DialogDataFormat>();
        indexByDialogSection = 0;
    }

    public static DialogDataMgr GetInstance()
    {
        if(_instance ==null)
        {
            _instance = new DialogDataMgr();
        }
        return _instance;
    }


    //加载外部数据集合
    public bool LoadAllDialogData(List <DialogDataFormat > dialogDataList)
    {
        if(dialogDataList==null||dialogDataList .Count ==0)
        {
            return false;
        }

        if(allDialogDataList !=null&&allDialogDataList .Count==0)
        {
            //保证只加载一次
            for(int i=0;i<dialogDataList .Count ;i++)
            {
                allDialogDataList.Add(dialogDataList[i]);//加载数据!!!!!!!!!!!!!!!!!
            }
            return true;
        }
        else
        {
            return false;
        }
    }


    //
    /// <summary>
    /// 得到下一条记录
    /// </summary>
    /// <param name="dialogSecNum">输入：段落编号</param>
    /// <param name="side">输出：对话方</param>
    /// <param name="person">输出：人名</param>
    /// <param name="dialogCotent">输出：对话内容</param>
    /// <returns>
    /// true:输出合法对话数据
    /// false:没有输出对话数据  表示对话都接受了
    /// </returns>
    public bool GetNextDialogInfoRecoder(int dialogSecNum,out DialogSide side,out string person,out string dialogCotent)//out的使用方法
    {
        side=DialogSide.None ;
        person ="";
        dialogCotent ="";
        //输入参数检查
        if (dialogSecNum < 0)
            return false;

        //进入下一段对话了 add Function
        if(dialogSecNum !=originalDiaSectionNum )
        {
            //重置“内部”编号
            indexByDialogSection = 0;
            //清空对话缓存
            currentDialogDataBuffer.Clear();
            //把当前的“段落编号”记录下来
            originalDiaSectionNum = dialogSecNum;
        }


        if(currentDialogDataBuffer !=null&&currentDialogDataBuffer .Count >=1)
        {
            if(indexByDialogSection <currentDialogDataBuffer.Count )
            {
                ++indexByDialogSection;
            }
            else
            {
                return false;
            }
        }
        else
        {
            //当前缓存为空
            ++indexByDialogSection;
        }
        GetDialogInfoRecoder(dialogSecNum, out side, out person, out  dialogCotent);
        return true;
    }

    //得到对话信息
    private  bool GetDialogInfoRecoder(int dialogSecNum,out DialogSide side,out string person,out string dialogCotent)
    {
        side = DialogSide.None;
        person = "";
        dialogCotent = "";
        string dialogSide = "";
        if(dialogSecNum <=0)
        {
            return false;
        }

        //找到数据
        if (currentDialogDataBuffer != null && currentDialogDataBuffer.Count >= 1)
        {
            for(int i=0;i<currentDialogDataBuffer .Count ;i++)
            {
                //段落编号相同
                if(currentDialogDataBuffer [i].DialogSecNum ==dialogSecNum )
                {
                    //段内序号相同
                    if(currentDialogDataBuffer [i].SectionIndex ==indexByDialogSection)
                    {
                        //找到数据，提取数据
                        dialogSide = currentDialogDataBuffer[i].DialogSide;
                        if (dialogSide.Trim().Equals("Hero"))
                        {
                            side = DialogSide.Hero;
                        }else if(dialogSide.Trim().Equals("NPC"))
                        {
                            side = DialogSide.NPCSide ;
                        }
                        person = currentDialogDataBuffer[i].DialogPerson;

                        dialogCotent = currentDialogDataBuffer[i].DialogContent;
                        return true;
                    }
                }
            }
        }

        //没有找到数据，，则从所有数据集合中获取数据，并且将数据加入到缓存中
        if (allDialogDataList != null && allDialogDataList.Count >= 1)
        {
            //1.获取数据
            for (int i = 0; i < allDialogDataList.Count; i++)
            {
                //段落编号相同
                if (allDialogDataList[i].DialogSecNum == dialogSecNum)
                {
                    //段内序号相同
                    if (allDialogDataList[i].SectionIndex == indexByDialogSection)
                    {
                        //找到数据，提取数据
                        dialogSide = allDialogDataList[i].DialogSide;
                        if (dialogSide.Trim().Equals("Hero"))
                        {
                            side = DialogSide.Hero;
                        }
                        else if (dialogSide.Trim().Equals("NPC"))
                        {
                            side = DialogSide.NPCSide;
                        }
                        person = allDialogDataList[i].DialogPerson;

                        dialogCotent = allDialogDataList[i].DialogContent;
                        //2.将当前段落号中的数据加入到缓存中
                        LoadDataToBuffer(dialogSecNum);
                        return true;
                    }
                }
            }
        }
        //无法查询到结果
        return false;
    }

    //将当前段落编号中的数据，写入“当前段落缓存集合中”
    private bool LoadDataToBuffer(int num)
    {
        //输入参数检测
        if(num<=0)
        {
            return false;
        }
        if(allDialogDataList !=null&&allDialogDataList .Count >=1)
        {
            currentDialogDataBuffer.Clear();//清空以前的数据
            for(int i=0;i<allDialogDataList .Count ;i++)
            {
                if(allDialogDataList [i].DialogSecNum ==num)
                {
                    currentDialogDataBuffer.Add(allDialogDataList[i]);
                }
            }
            return true;
        }
        return false;
    }
}

//对话双方
public enum DialogSide
{
    Hero,
    None,
    NPCSide
}
