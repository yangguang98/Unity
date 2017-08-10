using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
/// <summary>
/// 视图层：学习背包系统的拖拽基本原理
/// </summary>
/// 
///RectTransform.position表示的为世界坐标，只是其z轴为固定的值
///对于UI上的物体，RectTransform.position和transform.posotion的数值是一样的，并且都是世界坐标
///
public class TestPackageDrag : MonoBehaviour,IBeginDragHandler ,IDragHandler,IEndDragHandler {

    public CanvasGroup canvasGroup; //用于贴图的穿透处理
    public Vector3 originalPos;  //原始方位

    public RectTransform myReTransform;  //二维方位
    
    void Start()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();

        //myReTransform = this.transform as RectTransform; //两种表达都可以
        myReTransform = this.GetComponent<RectTransform>();

        originalPos = myReTransform.position;
    }

    //拖拽前
    public void OnBeginDrag(PointerEventData eventData)
    {
        //忽略自身，（可以穿透）
        canvasGroup.blocksRaycasts = false;//当其设置为false时，那么就无法拖动了，无法捕捉射线了 ,如果设置为true，那么就挡住了射线，不能拖拽到指定的位置
        this.gameObject.transform.SetAsLastSibling();//保证当前贴图可见，不被覆盖
    }

    //拖拽中
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        //屏幕坐标，转二维矩阵坐标
        if(RectTransformUtility.ScreenPointToWorldPointInRectangle(myReTransform,eventData.position ,eventData.pressEventCamera,out globalMousePos))
        {
            myReTransform.position = globalMousePos;//myReTransform.position是一个三维坐标，只是Z轴是固定的
            print("myReTransform.position"+myReTransform.position);
            print("this.transform .position" + this.transform.position);
        }
    }

    //拖拽后
    public void OnEndDrag(PointerEventData eventData)
    {
        //当钱鼠标经过的“格子名称”
        GameObject cur = eventData.pointerEnter;
        if(cur!=null)
        {
            if (cur.name.Equals("ImgDes"))
            {
                myReTransform.position = cur.transform.position;
                originalPos = myReTransform.position;
            }
            else
            {
                //没有遇到目标物体就撤回
                myReTransform.position = originalPos;
                canvasGroup.blocksRaycasts = true;
            }
        }
    }
}
