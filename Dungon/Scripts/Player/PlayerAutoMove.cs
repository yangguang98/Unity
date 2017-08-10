using UnityEngine;
using System.Collections;

public class PlayerAutoMove : MonoBehaviour {

    private NavMeshAgent agent;
    public float minDistance = 4;
    public Transform target;
    
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(agent.enabled)
        {
            if(agent.remainingDistance <minDistance&&agent.remainingDistance !=0)//寻路的剩余距离小于最小的距离
            {
                agent.Stop();//停止导航
                agent.enabled = false;
                TaskManager._instance.OnArriveDestination();
            }
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05)
        {
            //若在寻路过程中按下键盘，则停止寻路

            StopAuto();
        }

    }

    public void SetDestination(Vector3 targetPos)
    {
        //外部的接口函数，用来设定导航的目的地

        
        agent.enabled = true;
        agent.SetDestination(targetPos);

    }

    public void StopAuto()
    {
        if(agent.enabled)
        {
            agent.Stop();
            agent.enabled = false;
        }
    }
}
