//using UnityEngine;
//using System.Collections;
///// <summary>
///// 控制层：敌人（属性）
///// </summary>
//public class EnemyCommand : Command  {

//    private bool isAlive = true;  //是否生存
//    public int maxHp = 20;        //最大生命值
//    public float currentHp = 0;   //当前生命值
//    public int heroExp = 5;

//    public bool IsAlive
//    {
//        get 
//        { 
//            return isAlive; 
//        }
//    }

//    void Start()
//    {
//        currentHp = maxHp;
//        //判断是否存活
//        StartCoroutine("CheckLifeContinue");
//    } 

//    void update ()
//    {

//    }


//    //协程需要循环执行
//    IEnumerator CheckLifeContinue()
//    {
//        while(true)
//        {
//            yield return new WaitForSeconds(GlobleParameter.INTERVER_TIME_0DOT1);
//            if (currentHp <= maxHp * 0.01)
//            {
//                isAlive = false;
//                //关于英雄增加经验值
//                HeroPropertyCommand.Instance.AddExp(heroExp);
//                //增加杀敌数量
//                HeroPropertyCommand.Instance.AddKillNum();

//                Destroy(this.gameObject);//销毁游戏物体
//            }
//        }
//    }

//    public void OnHurt(int hurtValue)
//    {
//        print("收到了攻击");
//        int hurtValues = 0;
//        hurtValues = Mathf.Abs(hurtValue);//保证大于零
//        if(hurtValues >0&&isAlive)
//        {
//            currentHp -= hurtValues;
//        }
//    }
//}
