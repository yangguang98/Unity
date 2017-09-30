using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
//任何的Ui显示，都是通过数据来控制的
public class UISelect : View
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    public Button BtnStart;
    List<Card> m_Cards = new List<Card>();//所有的Card信息
    int m_selectedIndex = -1;//默认选择的关卡
    GameModel m_gameModel = null;
    #endregion

    #region 属性

    public override string Name
    {
        get
        {
            return Consts.v_Select;
        }
    }

    #endregion
     
    #region 方法

    public void GoBack()
    {
        //返回开始界面
        Game.Instance.LoadScene(1);
    }

    public void ChooseLevel()
    {
        //选中关卡，开始游戏
        StartLevelArgs e = new StartLevelArgs() 
        {
            LevelIndex =m_selectedIndex //属性初始化
        };

        Debug.Log("123");
        SendEvent(Consts.E_StartLevel, e);//加载关卡
    }

    void LoadCards()
    {
        //获取Level集合
        List<Level> levels = m_gameModel.AllLevels;
        

        //构建card集合
        List<Card> cards = new List<Card>();
        for(int i=0;i<levels.Count ;i++)
        {
            Card card = new Card()
            {
                //这种语法结构！！！！！！！！！！！！！！！！！！！！！！！！！
                levelID =i,
                cardImage=levels[i].cardImage ,
                //isLocked = i > m_gameModel.GameProgress //i大于本底存档的进度，则锁，本底存档m_gameModel .GameProgress相当于游戏玩到第几关了
                isLocked =!(i<=m_gameModel.GameProgress+1)
            };
            cards.Add(card);
        }
        this.m_Cards = cards;//保存card信息

        //为每一张卡片设置监听的事件，，
        UICard[] uiCards = this.transform.Find("UICards").GetComponentsInChildren<UICard>();
        foreach(UICard uiCard in uiCards )
        {
            uiCard.OnClick += (card) =>//匿名委托  ，括号中为传入进来的参数
            {
                SelectCard(card.levelID);//这里的levelID刚刚好等于卡片的索引号码
            };
        }

        //默认选中第一个卡片
        SelectCard(0);//设置，卡片的索引号一次为：0 1 2 3 4 
    }

    void SelectCard(int index)
    {
        //选择卡牌 被选中的卡牌放在中间 

        if(m_selectedIndex ==index )
        {
            return;
        }

        m_selectedIndex = index;

        //计算索引号  任何情况下只有一个索引号码为零
        int left = m_selectedIndex - 1;
        int current = m_selectedIndex;
        int right = m_selectedIndex + 1;

        //绑定数据
        Transform containers = this.transform.Find("UICards");


        //总共只有三张卡片存在，只是每次让其显示的内容不同，不是通过移动位置
        //左边
        if(left <0)
        {
            //卡片最小的索引为0，没有索引小于0的卡片存在
            containers.GetChild(0).gameObject.SetActive(false);//这个还按照顺序来的
        }
        else
        {
            containers.GetChild(0).gameObject.SetActive(true);
            containers.GetChild(0).GetComponent<UICard>().IsTransparent = true;
            containers.GetChild(0).GetComponent<UICard>().DataBind(m_Cards[left]); //设置显示内容
        }

        //当前
        containers.GetChild(1).GetComponent<UICard>().IsTransparent = false;
        containers.GetChild(1).GetComponent<UICard>().DataBind(m_Cards[index]);

        //控制游戏开始游戏按钮,,当前选择关卡被锁，那么就不能显示开始按钮
        BtnStart.gameObject.SetActive(!m_Cards[current].isLocked);//只是用来显示当前关卡是否被锁了

        //右边
        if (right>=m_Cards .Count)
        {
            containers.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            containers.GetChild(2).gameObject.SetActive(true);
            containers.GetChild(2).GetComponent<UICard>().IsTransparent = true;
            containers.GetChild(2).GetComponent<UICard>().DataBind(m_Cards[right]);
        }

        
    }
    
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_EnterScene );
    }

    public override void HandlerEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_EnterScene :
                //每次进入到场景中都会触发这个事件,,,,，，，，，场景的起点
                SceneArgs e = data as SceneArgs;
                if(e.SceneIndex ==2)
                {
                    //第二个场景,,只进入到第二个场景才会触发这个事件
                    m_gameModel = GetModel<GameModel>();//视图中也可以得到Model!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    LoadCards();//初始化卡片
                }
                break;
        }
    }

    #endregion

    #region 帮助方法
    #endregion

    

    
}
