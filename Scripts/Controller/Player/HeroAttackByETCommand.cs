using UnityEngine;
using System.Collections;
/// <summary>
/// 主角攻击控制：通过EasyTouch
/// </summary>
public class HeroAttackByETCommand : Command
{
//#if UNITY_ANDROID||UNITY_IPHONE
    public static HeroAttackByETCommand Instance;

    public static event PlayerCtrByStrDelegate playerControlEvent;

    void Awake()
    {
        Instance = this;
    }
    public void ResponseATKByNormal()
    {
        if(playerControlEvent !=null)
        {
            playerControlEvent(GlobleParameter.INPUT_MGR_ATTACK_NORMAL);
        }
    }

    public void ResponseATKByA()
    {
        if (playerControlEvent != null)
        {
            playerControlEvent(GlobleParameter.INPUT_MGR_ATTACK_MAGICTRICK_A);
        }
    }

    public void ResponseATKByB()
    {
        if (playerControlEvent != null)
        {
            playerControlEvent(GlobleParameter.INPUT_MGR_ATTACK_MAGICTRICK_B);
        }
    }

    public void ResponseATKByC()
    {
        if (playerControlEvent != null)
        {
            playerControlEvent(GlobleParameter.INPUT_MGR_ATTACK_MAGICTRICK_C);
        }
    }

    public void ResponseATKByD()
    {
        if (playerControlEvent != null)
        {
            playerControlEvent(GlobleParameter.INPUT_MGR_ATTACK_MAGICTRICK_D);
        }
    }
//#endif
}
