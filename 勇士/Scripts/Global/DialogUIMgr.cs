using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 视图层：通用对话界面管理层
/// </summary>
public class DialogUIMgr : MonoBehaviour
{

    public  static DialogUIMgr _instance;

    //UI对象
    public GameObject goHero;
    public GameObject goNPC_Left;
    public GameObject goNPC_Right;
    public GameObject goSingleDialogArea;      //单人对话信息区域
    public GameObject goDoubleDialogArea;      //双人对话信息区域

    //对话显示控件
    public Text personName;
    public Text doubleDialogContent;           //单人对话内容
    public Text singleDialogContent;           //双人对话内容

    //Sprint资源
    public Sprite[] sprHero;
    public Sprite[] sprNPC_Left;
    public Sprite[] sprNPC_Right;

    void Awake()
    {
        _instance = this;//单例模式
    }

    ////得到单例模式
    //public DialogUIMgr GetInstance()
    //{
    //    if (_instance == null)
    //    {
    //        _instance = this;
    //    }
    //    return _instance;
    //}

    //显示下一条对话信息  True表示 对话都结束了，false 表示对话还有下一条对话
    public bool DisplayNextDialog(DialogType diaType, int dialogSectionNum)
    {
        bool isEnd = false;                                //对话是否结束 
        DialogSide diaSide = DialogSide.None;
        string diaPerson = "";
        string diaContent = "";
        //切换对话类型
        ChangeDialogType(diaType);

        //得到对话信息
        bool flag = DialogDataMgr.GetInstance().GetNextDialogInfoRecoder(dialogSectionNum, out diaSide, out diaPerson, out diaContent);

        if (flag)
        {
            //显示对话信息
            DisplayDialogInfo(diaType, diaSide, diaPerson, diaContent);
        }
        else
        {
            isEnd = true;
        }
        return isEnd;
    }

    //切换对话类型
    private void ChangeDialogType(DialogType type)
    {
        switch (type)
        {
            case DialogType.None:
                goHero.SetActive(false);
                goNPC_Left.SetActive(false);
                goNPC_Right.SetActive(false);
                goSingleDialogArea.SetActive (false );    //单人对话信息区域
                goDoubleDialogArea.SetActive (false );    //双人对话信息区域
                break;
            case DialogType.Double:
                goHero.SetActive(true);
                goNPC_Left.SetActive(false);
                goNPC_Right.SetActive(true);
                goSingleDialogArea.SetActive (false);    //单人对话信息区域
                goDoubleDialogArea.SetActive (true);    //双人对话信息区域
                break;
            case DialogType.Single:
                goHero.SetActive(false);
                goNPC_Left.SetActive(true);
                goNPC_Right.SetActive(false);
                goSingleDialogArea.SetActive (true);      //单人对话信息区域
                goDoubleDialogArea.SetActive (false);    //双人对话信息区域
                break;
            default:
                goHero.SetActive(false);
                goNPC_Left.SetActive(false);
                goNPC_Right.SetActive(false);
                goSingleDialogArea.SetActive (false );    //单人对话信息区域
                goDoubleDialogArea.SetActive (false );    //双人对话信息区域
                break;
        }
    }

    //显示对话信息
    /// <summary>
    /// 
    /// </summary>
    /// <param name="diaType">对话类型</param>
    /// <param name="diaSide">那边在说  用于显示图片的亮暗</param>
    /// <param name="diaPerson">对话人名字</param>
    /// <param name="diaContent">对话内容</param>
    private void DisplayDialogInfo(DialogType diaType, DialogSide diaSide, string diaPerson, string diaContent)
    {
        switch (diaType )
        {
            case DialogType.None:
                break;
            case DialogType.Double:
                //显示人名，对话信息
                if (!string.IsNullOrEmpty(diaPerson) && !string.IsNullOrEmpty(diaContent))
                {
                    //Modify 2016 
                    if (diaSide ==DialogSide.Hero)
                    {
                        personName.text = GlobalParameterMgr.playerName;   
                    }
                    else
                    {
                        personName.text = diaPerson;
                    }
                    
                    doubleDialogContent.text = diaContent;
                }
                //显示精灵
                switch (diaSide)
	            {
                    case DialogSide.Hero:
                        goHero.GetComponent<Image>().overrideSprite = sprHero[0];//显示为彩色!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        goNPC_Right.GetComponent<Image>().overrideSprite = sprNPC_Right[1];//显示为灰色
                        break;
                    case DialogSide.None:
                        break;
                    case DialogSide.NPCSide:
                        goHero.GetComponent<Image>().overrideSprite = sprHero[1];//显示为彩色
                        goNPC_Right.GetComponent<Image>().overrideSprite = sprNPC_Right[0];//显示为灰色
                        break;
                    default:
                        break;
	            }
                break;
            case DialogType.Single:
                singleDialogContent.text = diaContent;
                break;
            default:
                break;
        }
         
    }
}

//对话类型
public enum DialogType
{
    None,
    Double,
    Single
}
