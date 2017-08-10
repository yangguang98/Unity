using UnityEngine;
using System.Collections;
/// <summary>
/// 控制层：敌人战士AI系统
/// 
/// 描述：
///     1.思考过程
///     2.敌人的移动处理
/// </summary>
public class WarriorAICommand : Command
{
    float rotateSpeed = 1f;         //旋转速度
    public float moveSpeed = 5f;    //移动速度
    public float attackDistance = 2f;   //攻击距离
    public float cordonDistance = 5f;   //境界距离
    public float thinkInterval = 1f;    //思考的间隔时间

    private GameObject goHero;      //主角 
    private Transform myTransform;  //当前方位
    private BaseEnemyProCommand proCommand;
    private CharacterController cc;
    
    void OnEnable()
    {
        //开启思考协程
        StartCoroutine(ThinkProcess());

        //开启移动协程
        StartCoroutine(MovingProcess());
    }

    void OnDisable()
    {
        //开启思考协程
        StopCoroutine(ThinkProcess());

        //开启移动协程
        StopCoroutine(MovingProcess());
    }

    void Start()
    {
        goHero = GameObject.FindGameObjectWithTag(Tags.player);
        myTransform = this.GetComponent<Transform>();
        proCommand = this.GetComponent<BaseEnemyProCommand>();
        cc = this.GetComponent<CharacterController>();

        //确定个体差异性参数
        moveSpeed = UnityHelper.GetInstance().GetRandomNum(1, 2);
        attackDistance = UnityHelper.GetInstance().GetRandomNum(1, 3);
        cordonDistance = UnityHelper.GetInstance().GetRandomNum(4, 15);
        thinkInterval = UnityHelper.GetInstance().GetRandomNum(1, 3);

        ////开启思考协程
        //StartCoroutine(ThinkProcess());

        ////开启移动协程
        //StartCoroutine(MovingProcess());
    }

    //思考,,,,,主要用来的状态
    IEnumerator ThinkProcess()
    {
        yield return new WaitForSeconds(GlobleParameter .INTERVER_TIME_1);
        while (true && proCommand.CurrentState != EnemyState.death)
        {
            yield return new WaitForSeconds(thinkInterval);
            //主角当前位置
            Vector3 pos = goHero.transform.position;

            //得到主角与当前敌人的距离
            float dis = Vector3.Distance(myTransform.position, pos);

            //距离判断
            if (dis < attackDistance)
            {
                //攻击状态
                proCommand.CurrentState = EnemyState.attack;
            }
            else if (dis < cordonDistance)
            {
                //追击（警戒）
                proCommand.CurrentState = EnemyState.walking;
            }
            else
            {
                proCommand.CurrentState = EnemyState.idle; 
            }
        }
    }

    //移动   通过状态来判断，脚本移动移动
    IEnumerator MovingProcess()
    {
        yield return new WaitForSeconds(GlobleParameter .INTERVER_TIME_1);
        while (true && proCommand.CurrentState != EnemyState.death)
        {
            yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT02 );


            //面向主角
            UnityHelper.GetInstance().FaceToGoal(this.gameObject.transform, goHero.transform , rotateSpeed);
            //移动
            switch (proCommand.CurrentState)
            {
                    //正常走动
                case EnemyState.walking:
                    Vector3 v = Vector3.ClampMagnitude((goHero.transform.position - myTransform.position), moveSpeed * Time.deltaTime);//另外一种方法设置移动速度
                    cc.Move(v);
                    break;
                    //受伤后退
                case EnemyState.hurt:
                    Vector3 vec = -transform.forward * moveSpeed/2 * Time.deltaTime;
                    cc.Move(vec);
                    break;
                default :
                    break;
            }
        }
    }
}
