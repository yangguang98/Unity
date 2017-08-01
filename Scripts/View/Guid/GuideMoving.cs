using UnityEngine;
using System.Collections;

/// <summary>
/// 视图层：新手引导动画效果
/// </summary>
public class GuideMoving : MonoBehaviour
{

    public GameObject goMovingGoal;//移动的目标对象


    void Start()
    {
        iTween.MoveTo(this.gameObject,
                  iTween.Hash("position", new Vector3(78,78,0),
                              "time", 1F,
                              "looptype", iTween.LoopType.loop));
    }
}
