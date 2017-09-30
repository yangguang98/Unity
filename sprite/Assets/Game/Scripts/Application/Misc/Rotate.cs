using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public float speed = 360;//(度/秒)
    public Transform transf;

    //void Awake()
    //{
    //    transf = transform.parent.parent;
    //}
	void Update()
    {
        Debug.Log(transform.parent.forward);
        transform.Rotate(transform .parent .forward,Time.deltaTime * speed,Space.World);//旋转 
    }
}
