using UnityEngine;
using System.Collections;
/// <summary>
/// 控制层：箭
/// 功能：配合敌人进行攻击
/// </summary>
public class Arrow : Command {

    public float arrowSpeed = 1f;
    public int arrowATK = 40;
    private HeroPropertyCommand heroProComand;
	
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag(Tags.player);
        if(go)
        {
            heroProComand = go.GetComponent<HeroPropertyCommand>();
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject .tag ==Tags.player )
        {
            heroProComand.DecreaseHealthValue(arrowATK);

            //销毁
            Destroy(this.gameObject, 0.5f);
        }
    }

    void Update()
    {
        if(Time.frameCount %2==0)
        {
            this.gameObject.transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
        }
    }

}
