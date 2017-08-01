using UnityEngine;
using System.Collections;
/// <summary>
/// 控制层：英雄的展示
/// </summary>
public class DisplayHeroCommand : Command {

    //public AnimationClip idle;
    //public AnimationClip run;
    //public AnimationClip attack;

    private Animation currentAnimation;
    private float intervalTimes = 3f;
    private int randomNum = 0;
    void Start()
    {
        currentAnimation = this.GetComponent<Animation>();
    }
    

    void Update()
    {
        intervalTimes -= Time.deltaTime;
        if(intervalTimes <=0)
        {
            intervalTimes = 3f;
            randomNum = Random.Range(1, 4);
            Display(randomNum);
        }
    }

    //展示动作
    internal void Display(int num)
    {
        switch (num)
        {
            case 1:
                DisplayIdle();
                break;
            case 2:
                DisplayRunning();
                break;
            case 3:
                DisplayAttack();
                break;
            default :
                break;
        }
    }

    //展现休闲
    internal void DisplayIdle()
    {
        if(currentAnimation !=null)
        {
            //currentAnimation.CrossFade(idle.name);
            currentAnimation.CrossFade("Idle");
        }
    }

    //展现攻击
    internal void DisplayRunning()
    {
        if(currentAnimation !=null)
        {
            //currentAnimation.CrossFade(run.name);
            currentAnimation.CrossFade("Run");
        }
    }

    //展现攻击
    internal void DisplayAttack()
    {
        if(currentAnimation !=null)
        {
            //currentAnimation.CrossFade(attack.name);
            currentAnimation.CrossFade("Attack1");
        }
    }

}
