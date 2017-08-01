using UnityEngine;
using System.Collections;
/// <summary>
/// 视图层：Ui攻击虚拟按键按压事件处理
/// 描述： 当点击normal按钮不放的时候，其会连续调用 OnStateUpate函数，
/// </summary>
public class ATKNormalPressedView : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //这个函数执行的次数太多，，控制执行的次数
        if(UnityHelper.GetInstance().GetSmallTime(0.8f))
        {
//#if UNITY_ANDROID||UNITY_IPHONE
            HeroAttackByETCommand.Instance.ResponseATKByNormal();
            Debug.Log("攻击");
//#endif
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
