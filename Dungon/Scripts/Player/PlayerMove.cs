using UnityEngine;
using System.Collections;
using System;
/// <summary>
/// 每个模型上挂的控制移动的代码相同，当有多个相同 的模型出现在场景中，通过判定其是否是本地的模型（玩家控制的模型）来控移动
/// </summary>

public class PlayerMove : MonoBehaviour {

    public float velocity = 5;
    private Animator anim;
    private PlayerAttack playerAttack;
    public bool isCanControl = true;
    private BattleController battleConttoller;
    private Vector3 lastPosition = Vector3.zero;
    private Vector3 lastEulerAngles = Vector3.zero;
    private bool isMove = false;
    private DateTime lastUpdateTime=DateTime.Now ;  
    void Start()
    {
        anim = this.GetComponent<Animator>();
        playerAttack = this.GetComponent<PlayerAttack>();
        if(GameController .Instance .battleType ==BattleType.Team &&isCanControl)
        {
            //由主机发起同步的请求
            battleConttoller = GameController.Instance.GetComponent<BattleController>();
            InvokeRepeating("SyncPositionAndRotation", 0, 1f / 30);//同步位置和旋转信息,,时时去检测
            InvokeRepeating("SyncMoveAnimation", 0, 1f / 30);//同步移动动画，，，时时去检测
        }
    }
    void Update()
    {
        if (isCanControl == false) return;

        if (playerAttack!=null&&playerAttack.hp <= 0)//由于有两个模型的Tag为Player,而两个模型上所挂载的脚本有不同的，为了达到控制副本中的Player,因此加上前面的条件判断
            return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 nowVel = this.GetComponent<Rigidbody>().velocity;
        if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f)
        {
            anim.SetBool("Move", true);
            //if(anim.GetAnimatorTransitionInfo (1).IsName ("Empty State"))
            //{//加了这个就不能控制主角的移动了
                //在释放技能的时候，角色不能移动

                this.GetComponent<Rigidbody>().velocity = new Vector3(velocity * h, nowVel.y, velocity * v);//注意这里乘的h,v没有加上负号

                transform.LookAt(new Vector3(h, 0, v) + transform.position);//确定朝向，看向一个具体的位置
            //}
           
        }
        else
        {
            anim.SetBool("Move", false);
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, nowVel.y, 0);
        }
     }

    public void SyncPositionAndRotation()
    {
        //同步位置和旋转信息  ,,,只会在主机上去执行，即用户登录的那个客户端，
        Vector3 position = transform.position;
        Vector3 eulerAngles = transform.eulerAngles;
        if(position .x !=lastPosition .x||position .y !=lastPosition .y||position .z!=lastPosition .z ||eulerAngles .x!=lastEulerAngles .x||eulerAngles .y!=lastEulerAngles .y||eulerAngles .z!=lastEulerAngles .z)
        {
            //进行同步
            battleConttoller.SyncPositionAndRotation(position, eulerAngles);

            lastEulerAngles = eulerAngles;//将当前的eulerAngles赋值给lastEulerAngles;
            lastPosition = position;
        }
    }

    public void SetPositionAndEulerAngles(Vector3 position,Vector3 eulerAngles)
    {
        //非主机调用，主机发来的东西，然后同步本地的游戏模型
        transform.position = position;
        transform.eulerAngles = eulerAngles;
    }

    public void SyncMoveAnimation()
    {
        //同步移动动画
        if(isMove !=anim.GetBool ("Move"))
        {
            //当前动画发生了改变，需要同步
            PlayerMoveAnimationModel model = new PlayerMoveAnimationModel() { IsMove = anim.GetBool ("Move")};//？？？？？？？？？？？？？？？？？？？？这是什么语法结构
            model.SetTime(DateTime.Now);
            //LitJson在转化为字符串时，会将时间的毫秒丢失掉，因此可能导致不同步的情况
            battleConttoller.SyncMoveAnimation(model); 
            isMove = anim.GetBool("Move");
        }
    }

    public void SetAnim(PlayerMoveAnimationModel model)
    {
        //设置动画同步的回调
        if(model.GetTime () >lastUpdateTime)
        {
            //用时间作为判断条件？？？？？？？？？？？？为什么用时间作为回调的条件
            anim.SetBool("Move", model.IsMove);
            lastUpdateTime = model.GetTime ();
        }
    }

}
