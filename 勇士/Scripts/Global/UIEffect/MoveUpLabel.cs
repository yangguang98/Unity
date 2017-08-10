using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 公共层：漂字特效
/// 功能：表示特定对象，失血数值显示
/// </summary>
public class MoveUpLabel : MonoBehaviour {

    private GameObject targetEnemy;             //目标对象
    private Camera wordCamera;                  //世界坐标系
    private Camera guiCamera;                   //UI坐标系

    private Text uiText;    //显示控件

    public float floPrefabLength = 2f;      //漂字预设长度
    public float floPerfabHeight = 1f;      //漂字预设宽度

    private float currentReduceHpNumber=-999;//减少的生命数值

    private float floOffset = 2f; //漂字位置偏移量

    void Start()
    {
        //得到UISlider控件
        uiText = this.GetComponent<Text>();


        //世界摄像机
        wordCamera = Camera.main.GetComponent<Camera>();
        //UI摄像机
        guiCamera = GameObject.FindGameObjectWithTag("Tag_UICamera").GetComponent<Camera>();

        if (targetEnemy == null)
        {
            Debug.LogError(GetType() + "/Start()/targetEnemyObj==null!");
            return;
        }

        if(currentReduceHpNumber==-999)
        {
            Debug.LogError(GetType() + "/Start()/currentReduceHpNumber==null");
        }

    }

    //计算失血数值
    void Update()
    {
        //控件显示血量
        uiText.text = currentReduceHpNumber.ToString();
        //控件尺寸
        this.transform.localScale = new Vector3(floPrefabLength, floPerfabHeight, 0);

        //位置的偏移量
        floOffset += 0.01f;
        //本预设销毁，由缓冲池处理
    }

    //漂字三维坐标系与UI坐标系的转换
    void LateUpdate()
    {
        //获取目标物体屏幕坐标
        Vector3 pos = wordCamera.WorldToScreenPoint(targetEnemy.transform.position);
        //屏幕坐标转换为UI的世界坐标
        pos = guiCamera.ScreenToWorldPoint(pos);
        //确定UI的最终位置
        pos.z = 0;
        transform.position = new Vector3(pos.x, pos.y + floOffset, pos.z);
    }

    //设置血条目标
    public void SetTargetEnemy(GameObject go)
    {
        targetEnemy = go;
    }

    //设置减少生命数值
    public void SetReduceHp(int num)
    {
        currentReduceHpNumber = num;
    }


}
