using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 视图层：背包系统显示
/// 
/// 作用：根据背包系统模型层后台数据，显示背包系统的道具
/// </summary>
public class UIPackage : MonoBehaviour {

	//道具对象
    public GameObject goPropBloodBottle;    //血瓶
    public GameObject goPropMagicBottle;    //魔法瓶
    public GameObject goPropATK;            //攻击力道具
    public GameObject goPropDEF;            //防御力道具
    public GameObject goPropDEX;            //敏捷度道具

    //道具数量
    public Text txtProBloodBottleNum;
    public Text txtProMagicBottleNum;

    void Start()
    {

    }
    void Awake()
    {
        //事件注册 ，，都注册在一个事件上，因此要靠事件的参数去判定执行哪个事件
        PlayerPackageData.playerPackageDataEvent+=DisplayBloodBottle;
        PlayerPackageData.playerPackageDataEvent+=DisplayMagicBottle;
        PlayerPackageData.playerPackageDataEvent+=DisplayATK;
        PlayerPackageData.playerPackageDataEvent+=DisplayDEF;
        PlayerPackageData.playerPackageDataEvent+= DisplayDEX;
        print(GetType() + "/Awake()");
    }

    //显示血瓶以及数量
    public void DisplayBloodBottle(KeyValuesUpdate kv)
    {
        if(kv.Key .Equals ("IBooldBottleNum"))
        {
            if(goPropBloodBottle&&txtProBloodBottleNum)
            {
                //道具数量大于等于1，则显示结果
                
                if(System.Convert.ToInt32 (kv.Value)>=1)
                {
                    goPropBloodBottle.SetActive(true);
                    print("显示血瓶");
                    txtProBloodBottleNum.text = kv.Value.ToString();
                }
            }
        }
    }

    //显示魔法瓶及数量

    public void DisplayMagicBottle(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("IMagicBottleNum"))
        {
            if (goPropMagicBottle && txtProMagicBottleNum)
            {
                //道具数量大于等于1，则显示结果

                if (System.Convert.ToInt32(kv.Value) >= 1)
                {
                    goPropMagicBottle.SetActive(true);
                    txtProMagicBottleNum.text = kv.Value.ToString();
                }
            }
        }
    }

    //显示攻击力道具

    public void DisplayATK(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("IPropATKNum"))
        {
            if (goPropATK)
            {
                //道具数量大于等于1，则显示结果

                if (System.Convert.ToInt32(kv.Value) >= 1)
                {
                    goPropATK.SetActive(true);
                }
            }
        }
    }
    //防御力道具

    public void DisplayDEF(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("IPropDEFNum"))
        {
            if (goPropDEF)
            {
                //道具数量大于等于1，则显示结果

                if (System.Convert.ToInt32(kv.Value) >= 1)
                {
                    goPropDEF.SetActive(true);
                }
            }
        }
    }
    //敏捷度道具

    public void DisplayDEX(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("IPropDEXNum"))
        {
            if (goPropDEX)
            {
                //道具数量大于等于1，则显示结果

                if (System.Convert.ToInt32(kv.Value) >= 1)
                {
                    goPropDEX.SetActive(true);
                }
            }
        }
    }
}
