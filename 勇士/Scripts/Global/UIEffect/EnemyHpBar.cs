using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 公共层：敌人血条
/// 描述：查看敌人当前生命值
/// </summary>
public class EnemyHpBar : MonoBehaviour {

    private GameObject targetEnemy;             //目标对象
    private Camera wordCamera;                  //世界坐标系
    private Camera guiCamera;                   //UI坐标系

    private Slider uiSlider;    //显示控件

    public float floHpPrefabLength = 2f;      //血条长度
    public float floHpPerfabHeight = 1f;      //血条宽度

    private float currentHp;
    private float maxHp;

    void Start()
    {
        //得到UISlider控件
        uiSlider = this.GetComponent<Slider>();


        //世界摄像机
        wordCamera = Camera.main.GetComponent<Camera>();
        //UI摄像机
        guiCamera = GameObject.FindGameObjectWithTag("Tag_UICamera").GetComponent<Camera>();

        if(targetEnemy ==null)
        {
            Debug.LogError(GetType() + "/Start()/targetEnemyObj==null!");
            return;
        }

    }

    void Update()
    {
        try
        {
            //当前与最大的生命数值
            currentHp = targetEnemy.GetComponent<BaseEnemyProCommand>().CurrentHp;
            maxHp = targetEnemy.GetComponent<BaseEnemyProCommand>().MaxHp;

            //计算血量
            uiSlider.value = currentHp / maxHp;

            //控件尺寸
            this.transform.localScale = new Vector3(floHpPrefabLength, floHpPerfabHeight);

            //销毁
            if(currentHp <=maxHp *0.05)
            {
                Destroy(this.gameObject);
            }
        }
        catch(System .Exception )
        {

        }
    }

    //血条三维坐标系与UI坐标系的转换
    void LateUpdate()
    {
        //获取目标物体屏幕坐标
        Vector3 pos = wordCamera.WorldToScreenPoint(targetEnemy.transform.position);
        //屏幕坐标转换为UI的世界坐标
        pos = guiCamera.ScreenToWorldPoint(pos);
        //确定UI的最终位置
        pos.z = 0;
        transform.position = new Vector3(pos.x, pos.y+3f, pos.z);
    }

    //设置血条目标
    public void SetTargetEnemy(GameObject go)
    {
        targetEnemy = go;
    }


}
