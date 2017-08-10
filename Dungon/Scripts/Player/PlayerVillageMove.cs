using UnityEngine;
using System.Collections;

public class PlayerVillageMove : MonoBehaviour {

    public float velocty = 5;
    private NavMeshAgent agent;

    void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 vel = GetComponent<Rigidbody>().velocity;
        if(Mathf.Abs(h)>0.05f||Mathf.Abs(v)>0.05)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-h * velocty, vel.y, -v * velocty);//改变物体的速度
            transform.rotation=Quaternion.LookRotation(new Vector3(-h, 0, -v));//改变任务的朝向
        }
        else
        {
            if(agent.enabled==false)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        if(agent.enabled)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity);
        }
    }

}
