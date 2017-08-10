using UnityEngine;
using System.Collections;

/// <summary>
/// 控制层：使得道具旋转
/// </summary>
public class MakeObjRotation : Command {

    public float rotateSpeed = 1f;

    void Update()
    {
        this.gameObject.transform.Rotate(Vector3.up, rotateSpeed);
    }
}
