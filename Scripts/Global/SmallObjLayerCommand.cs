using UnityEngine;
using System.Collections;
/// <summary>
/// 公共层：层消隐技术
/// 描述：小物件近距离显示，远距离消隐
/// </summary>
public class SmallObjLayerCommand : MonoBehaviour {

    public int iDisappearDistance = 10;                  //消隐距离；
    private float[] distanceArray = new float[32];

    void Start()
    {
        distanceArray[8] = iDisappearDistance;
        Camera.main.layerCullDistances = distanceArray;
    }
}
