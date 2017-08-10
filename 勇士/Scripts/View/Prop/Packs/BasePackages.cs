using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
/// <summary>
/// 视图层：背包系统
///        定义父类
///  描述：定义装备系统的公有属性
///  特别注意：必须给每一个道具，添加同样的标签
/// </summary>
public class BasePackages : MonoBehaviour{

    protected string strTargetName;  //目的格子名称
    private CanvasGroup canvasGroup; //用于贴图的穿透处理
    private Vector3 originalPos;  //原始方位
    private Transform myTransform;  //本对象transform
    private RectTransform myReTransform;  //二维方位


    //运行本类实例，通过子类调用
    protected void RunInstanceByChild()
    {
        BaseStart();
    }

    //父类实例化
    public void BaseStart()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();

        //myReTransform = this.transform as RectTransform; //两种表达都可以
        myReTransform = this.GetComponent<RectTransform>();
        myTransform = this.transform;
        originalPos = myReTransform.position;
    }

    //拖拽前
    public void BaseOnBeginDrag(PointerEventData eventData)
    {
        //忽略自身，（可以穿透）
        canvasGroup.blocksRaycasts = false;//当其设置为false时，那么就无法拖动了，无法捕捉射线了 ,如果设置为true，那么就挡住了射线，不能拖拽到指定的位置
        this.gameObject.transform.SetAsLastSibling();//保证当前贴图可见，不被覆盖
        //获取原始方位
        originalPos = myTransform.position;
    }

    //拖拽中
    public void BaseOnDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        //屏幕坐标，转二维矩阵坐标 让鼠标点击的物体更随鼠标移动
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(myReTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            myReTransform.position = globalMousePos;//myReTransform.position是一个三维坐标，只是Z轴是固定的
            print("myReTransform.position" + myReTransform.position);
            print("this.transform .position" + this.transform.position);
        }
    }

    //拖拽后
    public void BaseOnEndDrag(PointerEventData eventData)
    {
        //当钱鼠标经过的“格子名称”
        GameObject cur = eventData.pointerEnter;
        if (cur != null)
        {
            //遇到目标位置
            if (cur.name.Equals(strTargetName))
            {
                myTransform.position = cur.transform.position;
                originalPos = myTransform.position;

                //执行特定的装备方法
                InvokeMethodByEndDrag();
            }
            else
            {
                //移动到背包系统的其他有效位置上
 
                //如果是“同类背包道具”，则交换位置
                if ((cur.tag == eventData.pointerDrag.tag) && (cur.name != eventData.pointerDrag.name))//eventData.pointerDrag 被拖拽的物体
                {
                    //两个贴图位置互换算法
                    Vector3 targetPos = cur.transform.position;
                    cur.transform.position = originalPos;
                    myTransform.position = targetPos;
                    //新的位置确定为原始位置
                    originalPos = myTransform.position;
                }else{
                    //拖拽到背包界面的其他对象上
                    myTransform.position = originalPos;
                }

                //阻止穿透可以再次移动
                canvasGroup.blocksRaycasts = true;
            }
        }
        else
        {
            myTransform.position = originalPos;//返回
            canvasGroup.blocksRaycasts = true;
        }
    }

    protected virtual void InvokeMethodByEndDrag()
    {
        print(GetType() + "InvokeMethodByEndDrag");
    }
}
