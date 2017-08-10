using UnityEngine;
using System.Collections;
/// <summary>
/// 主角攻击控制：通过键盘输入
/// </summary>
public class HeroAttackBykeyCommand : Command {

#if UNITY_STANDALONE_WIN||UNITY_EDITOR

    public static event PlayerCtrByStrDelegate playerControlEvent; 


    void Update()
    {
        if(Input.GetButtonDown(GlobleParameter.INPUT_MGR_ATTACK_NORMAL))
        {
            print("NormalAttack,您按了J键");
            if(playerControlEvent !=null)
            {
                playerControlEvent(GlobleParameter.INPUT_MGR_ATTACK_NORMAL); 
            }
        }
        else if(Input.GetButtonDown(GlobleParameter.INPUT_MGR_ATTACK_MAGICTRICK_A ))
        {
            print("MagicTrickA,您按了K键");
            if (playerControlEvent != null)
            {
                playerControlEvent(GlobleParameter.INPUT_MGR_ATTACK_MAGICTRICK_A);
            }
        }
        else if (Input.GetButtonDown(GlobleParameter.INPUT_MGR_ATTACK_MAGICTRICK_B ))
        {
            print("MagicTrickB,您按了L键");
            if (playerControlEvent != null)
            {
                playerControlEvent(GlobleParameter.INPUT_MGR_ATTACK_MAGICTRICK_B);
            }
        }
    }//Update_end 

#endif
}
