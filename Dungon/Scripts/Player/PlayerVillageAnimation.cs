using UnityEngine;
using System.Collections;

public class PlayerVillageAnimation : MonoBehaviour {

    private Animator anim;
    private NavMeshAgent agent;
    void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(this.GetComponent <Rigidbody >().velocity.magnitude >0.5f||agent.velocity.magnitude >0.5f)//看看11111111(这里加一个速度不可以吗？？？)跑动的是后他们不是相等的吗？？
        {
            anim.SetBool("Move", true);
            
        }
        else
        {
            anim.SetBool("Move", false);
        }
    }
}
