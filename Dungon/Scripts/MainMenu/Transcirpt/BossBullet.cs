using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossBullet : MonoBehaviour {

    public float moveSpeed = 3;
    public float Damage//通过attack03赋值
    {
        set;
        private get;//看看
    }
    private List <GameObject > playerList=new List<GameObject>() ;
    private float repeateRate =1f;
    private float force = 1000;
    void Start()
    {
        InvokeRepeating("Attack", 0, repeateRate);
        Destroy(this.gameObject, 5f);
    }
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;//The blue axis of the transform in world space.
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            //碰到主角
            if (playerList.IndexOf(col.gameObject) < 0)
            {
                playerList.Add(col.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            if (playerList.IndexOf(col.gameObject) >= 0)
            {
                //存在就移除
                playerList.Remove(col.gameObject);
            }
        }
    }

    void Attack()
    {
        foreach(GameObject player in playerList )
        {
            player.SendMessage ("TakeDamge",Damage*repeateRate ,SendMessageOptions.DontRequireReceiver );//这个函数是repeateRate执行一次，而damage为一秒钟的伤害，则每一次执行时，产生的伤害为repeateRate*damage
            player.GetComponent<Rigidbody>().AddForce(transform.forward * force);
        }
    }
}
