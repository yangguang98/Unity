using UnityEngine;
using System.Collections;

public class PlayerEffect : MonoBehaviour {

    private Renderer[] renderArray;
    private NcCurveAnimation[] curveAnimationArray;
    private GameObject effectOffset;
    void Start()
    {
        renderArray = this.GetComponentsInChildren<Renderer>();//在子物体上去获得组件
        curveAnimationArray = this.GetComponentsInChildren<NcCurveAnimation>();
        if(transform .Find ("EffectOffset")!=null)
        {
            effectOffset = transform.Find("EffectOffset").gameObject;
        }
        
    }


    void Update()
    {
        //if(Input.GetMouseButtonDown (0))
        //{
        //    Show();
        //}
    }
    public void Show()
    {
        if(effectOffset!=null)
        {
            //IceStrikeEffect特效的播放

            effectOffset.SetActive(false);
            effectOffset.SetActive(true);
        }
        else
        {
            foreach (Renderer renderer in renderArray)
            {
                renderer.enabled = true;//组件的启用
            }

            foreach (NcCurveAnimation anim in curveAnimationArray)
            {
                anim.ResetAnimation();//这个方法的作用？？？？？？？？？？？？？
            }
        }
       
    }

   
}
