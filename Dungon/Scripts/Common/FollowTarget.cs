using UnityEngine;
using System.Collections;
//
public class FollowTarget : MonoBehaviour {

    public Vector3 offset;
    public float smoothing = 1;
    private Transform playerBip;

    void Start()
    {
        //不同的场景使用的是相同的跟随脚本，但是不同的场景有区别，所以加相应的条件判断，执行不同的代码
        if(TranscriptManager ._instance !=null)
        {
            //第三个场景的跟随
            playerBip = TranscriptManager._instance.player.transform.Find("Bip01");//通过标签寻找物体
        }
        else
        {
            //第二个场景的跟随
            playerBip = GameObject.FindGameObjectWithTag("Player").transform.Find("Bip01");
        }
        


    }

    void FixedUpdate()
    {
        //transform.position = playerBip.position + offset;
        Vector3 targetPos = playerBip.position + offset;

        transform.position =Vector3.Lerp(transform .position ,targetPos,smoothing *Time .deltaTime );//Linearly interpolates between two vectors.
    }
}
