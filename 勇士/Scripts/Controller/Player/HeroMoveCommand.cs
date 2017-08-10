using UnityEngine;
using System.Collections;
/// <summary>
/// 控制层：主角移动控制脚本（通过EasyTouch）
/// </summary> 
public class HeroMoveCommand : Command
{
//#if UNITY_ANDROID||UNITY_IPHONE
    //private const string JOYSTICK_NAME = "Herojoystick";//摇杆名称
    
    private CharacterController cc;                     //色控制器
    public float moveSpeed = 5f;                        //英雄移动速度
    public float attackMoveSpeed = 10f;                 //攻击移动速度
    public float gravity = 1f;                          //模拟重力

    #region 事件注册

    //游戏对象销毁
    public void OnDestroy()
    {

    }

    //active为false
    public void OnDisable()
    {

    }

    //游戏对象启用
    public void OnEnabel()
    {

    }

    #endregion

    void Start()
    {
        cc = this.GetComponent<CharacterController>(); 

        //攻击移动
        StartCoroutine("AttackByMove");
    }

    IEnumerator AttackByMove()
    {
        yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT5 );
        while(true)
        {
            yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT5);
            if(HeroAnimationCommand.Instance.CurrentActionState == HeroActionState.NormalAttack)
            {
                Vector3 vec = transform.forward * attackMoveSpeed / 2 * Time.deltaTime;
                cc.Move(vec);
            }
        }
    }
    void OnEnable()
    {
        EasyJoystick.On_JoystickMove += OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
    }

    //移动摇杆结束  
    void OnJoystickMoveEnd(MovingJoystick move)
    {
        //停止时，角色恢复idle  
        if (move.joystickName == GlobleParameter.JOYSTICK_NAME)
        {
            //GetComponent<Animation>().CrossFade("Idle");
            HeroAnimationCommand.Instance.SetActionState(HeroActionState.Idle );
        }
    }


    //移动摇杆中  
    void OnJoystickMove(MovingJoystick move)
    {
        if (move.joystickName != GlobleParameter.JOYSTICK_NAME)
        {
            return;
        }

        //获取摇杆中心偏移的坐标  
        float joyPositionX = move.joystickAxis.x;
        float joyPositionY = move.joystickAxis.y;

        if (joyPositionY != 0 || joyPositionX != 0)
        {
            //设置角色的朝向（朝向当前坐标+摇杆偏移量）
            if (HeroAnimationCommand.Instance.CurrentActionState == HeroActionState.Idle || HeroAnimationCommand.Instance.CurrentActionState == HeroActionState.Runing)
            {
                transform.LookAt(new Vector3(transform.position.x - joyPositionX, transform.position.y, transform.position.z - joyPositionY));
            }
            

            //移动玩家的位置（按朝向位置移动）  
            
            Vector3 movement = transform.forward * Time.deltaTime * moveSpeed;
            movement.y = movement.y - gravity;

            //在播放移动动画的时候才移动其位置
            if (HeroAnimationCommand.Instance.CurrentActionState == HeroActionState.Runing || HeroAnimationCommand.Instance.CurrentActionState == HeroActionState.Idle)
            {
                
                cc.Move(movement);

                //播放奔跑动画  
                //GetComponent<Animation>().CrossFade("Run");
                if (UnityHelper.GetInstance().GetSmallTime(0.1f))
                {
                    HeroAnimationCommand.Instance.SetActionState(HeroActionState.Runing);
                }
            
            }
            

            
        }
    }
//#endif
}
