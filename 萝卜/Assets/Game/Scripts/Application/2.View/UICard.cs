using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

//用来管理每张card的UI显示
//加载level的时候，一个level对应一个card,并且对应一个UICard
public class UICard : MonoBehaviour ,IPointerDownHandler {

    public event Action<Card> OnClick;//点击事件

    public Image imgCard;
    public Image imgLock;

    Card m_Card = null;//卡牌属性

    bool m_isTransparent = false;  //通过是否被锁定，来判定是否是透明的


    public bool IsTransparent
    {
        //设置半透明状态
        get
        {
            return m_isTransparent;
        }
        set
        {
            m_isTransparent = value;

            Image[] images = new Image[] { imgCard, imgLock };//这种语法结构!!!!!!!!!!!!
            foreach (Image img in images)
            {
                Color c = img.color;
                c.a=value ? 0.5f:1f;  
                img.color =c ;
            }
        }
    }

    public void DataBind(Card card)
    {
        //加载level的时候，一个level对应一个card,并且对应一个UICard
        m_Card = card;
        string cardFile = "file://" + Consts.CardDir +"\\"+ m_Card.cardImage;//设置绝对路径。。如果要使用www来加载对应的资源，则前面应该加上“file://”,,这个斜杠是有正方向的，也有反方向的，是什么原因？？？？？？？？？？？？？？？

        StartCoroutine(Tools.LoadImage(cardFile, imgCard));//异步加载图片
        imgLock.gameObject.SetActive(card.isLocked);//是否锁定
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        //对于这个游戏物体，如果有鼠标的点击事件发生，那么就会触发该方法 
        if(OnClick !=null)
        {
            OnClick(m_Card);
        }
    }

    void OnDestroy()
    {
        //这个函数何时调用？？？？？？？？？？？？？？？？？？？？？？？？？？？
        while(OnClick !=null)
        {
            OnClick -= OnClick;
        }
    }

    
}
