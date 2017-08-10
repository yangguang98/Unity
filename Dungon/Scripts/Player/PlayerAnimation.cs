
using UnityEngine;
using System.Collections;
/// <summary>
/// 包含动画，技能图标被点击之后就调用该方法
/// 
/// 
/// bool  是只要这个bool值为ture,,那么动画会一直去播放
/// trigger  是触发一次就播放一次
/// </summary>
public class PlayerAnimation : MonoBehaviour
{


    private Animator anim;
    private PlayerAttack playerAttack;


    private Player player;
    private BattleController battleController;

    private bool isSyncPlayerAnimation = false;//表示是否需要同步动画

    void Start()
    {
        player = this.GetComponent<Player>();
        if (GameController.Instance.battleType == BattleType.Team && player.roleID == PhotonEngine.Instance.role.ID)
        {
            battleController = GameController.Instance.GetComponent<BattleController>();
            isSyncPlayerAnimation = true;//本地角色，，当动画状态发生改变后，需要同步到其他的客户端
        }
        anim = this.GetComponent<Animator>();
        playerAttack = this.GetComponent<PlayerAttack>();
    }
    // Use this for initialization
    public void OnAttackButtonClick(bool isPress, PosType posType)
    {
        if (playerAttack.hp <= 0)
            return;
        if (posType == PosType.Basic)
        {
            if (isPress)
            {
                anim.SetTrigger("Attack");
                if (isSyncPlayerAnimation)
                {

                    //当动画的状态发生了改变，如果是本地角色，那么就要同步到其他的客户端
                    PlayerAnimationModel model = new PlayerAnimationModel() { attack = true };
                    battleController.SyncPlayerAnimation(model);
                }
            }
        }
        else
        {

            anim.SetBool("Skill" + (int)posType, true);//本底角色动画的改变

            if (isSyncPlayerAnimation)
            {
                //如果是本地角色，那么就要同步到其他的客户端


                //按下和抬起都需要同步
                if(isPress )
                {
                    //按下
                    switch ((int)posType)
                    {
                        case 1:
                            PlayerAnimationModel model = new PlayerAnimationModel() { skill1 = true };
                            battleController.SyncPlayerAnimation(model);
                            break;
                        case 2:
                            PlayerAnimationModel model2 = new PlayerAnimationModel() { skill2 = true };
                            battleController.SyncPlayerAnimation(model2);
                            break;
                        case 3:
                            PlayerAnimationModel model3 = new PlayerAnimationModel() { skill3 = true };
                            battleController.SyncPlayerAnimation(model3);
                            break;
                    }
                }
                else
                {
                    //抬起
                    PlayerAnimationModel model4 = new PlayerAnimationModel(); 
                    battleController.SyncPlayerAnimation(model4);
                }
            }
        }
    }

    public void SyncAnimation(PlayerAnimationModel model)
    {
        //这里的控制和enemy的控制，还是有区别的，，一个是，，animation，一个是Animator 
        if (model.attack)
        {
            anim.SetTrigger("Attack");
        }
        if (model.die)
        {
            anim.SetTrigger("Die");
        }
        if (model.takeDamage)
        {
            anim.SetTrigger("TakeDamage");
        }
        anim.SetBool("Skill1", model.skill1);
        anim.SetBool("Skill2", model.skill2);
        anim.SetBool("Skill3", model.skill3);
    }
}
