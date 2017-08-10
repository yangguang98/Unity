using UnityEngine;
using System.Collections;

public class ShopUI : MonoBehaviour {

    public static ShopUI Instance;
    private TweenPosition tweenPos;
    private GameObject coinScrollView;
    private GameObject diamondScrollView;
    void Awake()
    {
        Instance = this;
        tweenPos = this.GetComponent<TweenPosition>();
        coinScrollView = transform.Find("CoinScrollView").gameObject;
        diamondScrollView = transform.Find("DiamondScrollView").gameObject;
        diamondScrollView.SetActive(false);
    }

    public void Show()
    {
        tweenPos.PlayForward();
    }

    public void Hide()
    {
        tweenPos.PlayReverse();
    }

    public void OnBuyCoinButtonClick()
    {
        coinScrollView.SetActive(true);
        diamondScrollView.SetActive(false);
    }

    public void OnBuyDiamondButtonClick()
    {
        diamondScrollView.SetActive(true);
        coinScrollView.SetActive(false);
    }

    public void OnBuy(int coinCount,int diamondCount)
    {
        //coinCount和diamondCount一个为正数，一个为负数，一个为需要消耗的数目，一个为可以得到的数目
        bool isSuccess=PlayerInfo._instance.Exchange(coinCount, diamondCount);//看能否兑换成功
        if(isSuccess)
        {

        }
        else
        {
            if(coinCount <0)
            {
                MessageManager._instance.ShowMessage("金币不足");
            }
            else
            {
                MessageManager._instance.ShowMessage("钻石不足");
            }
        }
    }
}
