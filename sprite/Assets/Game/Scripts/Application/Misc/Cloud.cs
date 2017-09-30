using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour 
{
    public float OffsetX = 1000;//X方向偏移量
    public float Duration = 1f;
	void Start()
    {
        iTween.MoveBy(this.gameObject, iTween.Hash(
            "x", OffsetX,
            "easeType", iTween.EaseType.linear,
            "loopType", iTween.LoopType.loop,
            "time", Duration));
    }
}
