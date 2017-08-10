using UnityEngine;
using System.Collections;
/// <summary>
/// 核心层：帮助方法
/// 目的：集成大量通用方法
/// </summary>
/// 


public class UnityHelper {

    private static UnityHelper _instance;
    private float deltaTime=0f;            //累加时间

    //构造函数必须为私有的
    private UnityHelper ()
    {

    }

    //获得单例模式
    public static UnityHelper GetInstance()
    {
        if(_instance ==null)
        {
            _instance = new UnityHelper();
            
        }
        return _instance;
    }

	//间隔指定的时间，返回布尔值
    public bool GetSmallTime(float smallIntervalTime)
    {
        deltaTime += Time.deltaTime;
        if(deltaTime >=smallIntervalTime )
        {
            deltaTime = 0;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 面向指定目标旋转
    /// </summary>
    /// <param name="self">本身</param>
    /// <param name="goal">目标</param>
    /// <param name="rotateSpeed">旋转速度</param>
    public void FaceToGoal(Transform self, Transform goal, float rotateSpeed)
    {
        self.rotation = Quaternion.Slerp(self.rotation, Quaternion.LookRotation(new Vector3(goal.position.x, 0, goal.position.z) - new Vector3(self.position.x, 0, self.position.z)), rotateSpeed);
    }

    //得到指定范围的随机数
    public int GetRandomNum(int minNum,int maxNum)
    {
        int randomNumResult = 0;
        if(minNum ==maxNum )
        {
            randomNumResult = minNum;
        }
        randomNumResult=Random.Range(minNum ,maxNum +1);
        return randomNumResult;
    }
}
