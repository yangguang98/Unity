using UnityEngine;
using System.Collections;
/// <summary>
/// 主角移动控制：通过键盘
/// </summary>
public class HeroMoveByKeyCommand : Command
{
#if UNITY_STANDALONE_WIN||UNITY_EDITOR
    //private const string JOYSTICK_NAME = "Herojoystick";//摇杆名称

    private CharacterController cc;                     //色控制器
    public float moveSpeed = 5f;                        //英雄移动速度
    public float gravity = 1f;                          //模拟重力



    void Start()
    {
        cc = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        ControlMoving();
    }

    //控制主角移动  
    void ControlMoving()
    {

        ////获取摇杆中心偏移的坐标  
        //float joyPositionX = move.joystickAxis.x;
        //float joyPositionY = move.joystickAxis.y;
        float floMovingXPos = Input.GetAxis("Horizontal");
        float floMovingYPos = Input.GetAxis("Vertical");

        if (floMovingXPos != 0 || floMovingYPos != 0)
        {
            //设置角色的朝向（朝向当前坐标+摇杆偏移量）
            if (HeroAnimationCommand.Instance.CurrentActionState == HeroActionState.Idle || HeroAnimationCommand.Instance.CurrentActionState == HeroActionState.Runing)
            {
                transform.LookAt(new Vector3(transform.position.x - floMovingXPos, transform.position.y, transform.position.z - floMovingYPos));
            }
            
            //移动玩家的位置（按朝向位置移动）
            Vector3 movement = transform.forward * Time.deltaTime * moveSpeed;
            movement.y = movement.y - gravity;
            //在播放动画的时候就不能够移动其位置了
            if (HeroAnimationCommand.Instance.CurrentActionState ==HeroActionState.Idle||HeroAnimationCommand .Instance .CurrentActionState ==HeroActionState .Runing )
            {
                
                cc.Move(movement);
                //播放奔跑动画  
                //GetComponent<Animation>().CrossFade("Run");
                if (UnityHelper.GetInstance().GetSmallTime(0.3f))
                {
                    HeroAnimationCommand.Instance.SetActionState(HeroActionState.Runing);
                }
            }
        }
        else
        {
            ////没有键盘输入的时候，这个语句会被不断的调用，影响动画的播放，所以让其隔一段时间在执行
            //if (HeroAnimationCommand.Instance.CurrentActionState == HeroActionState.Idle || HeroAnimationCommand.Instance.CurrentActionState == HeroActionState.Runing)
            //{
            //    if (UnityHelper.GetInstance().GetSmallTime(0.3f))
            //    {
            //        HeroAnimationCommand.Instance.SetActionState(HeroActionState.Idle);
            //    }
            //}
            
        }
    }
#endif
}
