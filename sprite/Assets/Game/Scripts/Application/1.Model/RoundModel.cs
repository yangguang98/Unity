using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//每一个level里都有很多的round,,,一个round 相当于一波
//这里的RoundModel为一个Level 中的所有Round 信息，存储在m_Rounds中
public class RoundModel : Model  {


    

    #region 常量
    public const float ROUND_INTERVAL = 3f;//round之间的间隔
    public const float SPWAN_INTERVAL = 1f;//怪之间的间隔
    #endregion

    #region 事件
    #endregion

    #region 字段
    List<Round> m_Rounds = new List<Round> ();
    int m_roundIndex = -1;//当前在第几波


    bool m_allRoundsComplete = false;//是否所有怪物都是放出来了
    Coroutine m_Coroutine;

    
     
    #endregion

    #region 属性

    public int RoundIndex
    {
        get { return m_roundIndex; }
    }

    public int RoundTotal
    {
        //多少回合数
        get { return m_Rounds.Count; }
    }

    public bool AllRoundsComplete
    {
        get { return m_allRoundsComplete; }
        set { m_allRoundsComplete=value;}
    }

    public override string Name
    {
        get { return Consts.M_RoundModel; }
    }
    #endregion

    #region 方法

    public void LoadLevel(Level level)
    {
        //加载关卡
        m_roundIndex = -1;
        AllRoundsComplete = false;
        m_Rounds = level.Rounds;
    }
    public void StartRound()
    {
        //在这个Level中 中开始出怪
        Game.Instance.StartCoroutine(RunRound ());//继承自MonoBehaviour的类才可以使用StartCoroutine这个方法
    }

    public void StopRound()
    {
        //停止协程
        Game.Instance.StopCoroutine (RunRound ());
    }

    IEnumerator RunRound()
    {
        //开始level 中的所有round
        for(int i=0;i<m_Rounds .Count ;i++)
        {
            //回合(一波)开始事件
            StartRoundArgs e = new StartRoundArgs();
            e.roundTotal = m_Rounds.Count;
            e.roundIndex = i;
            SendEvent(Consts.E_StartRound, e);//发送开始出怪的事件

            Round round = m_Rounds[i];

            for(int k=0;i<round.Count ;k++)
            {
                //出怪间隙
                yield return new WaitForSeconds(SPWAN_INTERVAL);

                //出怪事件
                SpwanMonsterArgs e1 = new SpwanMonsterArgs();
                e1.MonsterType =round.Monster ;
                SendEvent(Consts.E_SpwanMonster, e1);
            }

            //增加回合数
            m_roundIndex++;

            //回合时间间隙
            if (i < m_Rounds.Count-1)
                //最后一个回合不需要等待
                yield return new WaitForSeconds(ROUND_INTERVAL);
        }

        //出怪完成
        AllRoundsComplete = true;

        //回合结束事件
    }

    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    #endregion

    #region 帮助方法
    #endregion
}
