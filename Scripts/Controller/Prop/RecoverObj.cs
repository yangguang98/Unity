using UnityEngine;
using System.Collections;

/// <summary>
/// 控制层：使用“对象缓冲池”，按照指定的时间回收指定的对象
/// </summary>
public class RecoverObj : Command {

    public float recoverTime = 3f;

    void OnEnable()
    {
        StartCoroutine("RecoveredObj");
    }

    void OnDisable()
    {
        StopCoroutine("RecoveredObj");
    }

    void Start()
    {
        
    }

    IEnumerator RecoveredObj()
    {
        yield return new WaitForSeconds(recoverTime);
        this.GetComponent<NcAttachSound_B>().enabled = true;
        PoolManager.PoolsArray["ParticalSys"].RecoverGameObject(this.gameObject);
    }
}
