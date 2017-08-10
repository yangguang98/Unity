using UnityEngine;
using System.Collections;
using System.Collections .Generic ;
/// <summary>
/// 公共层：枚举类型转换字符串
/// </summary>
public class ConvertEnumToString : MonoBehaviour {

    private static ConvertEnumToString _instance;
    private Dictionary<ScenesEnum, string> scenesEnumLib;//枚举场景类型集合
	private ConvertEnumToString()
    {
        scenesEnumLib = new Dictionary<ScenesEnum, string>();
        scenesEnumLib.Add(ScenesEnum.StartScenes, "1_StartScenes");
        scenesEnumLib.Add(ScenesEnum.LoginScenes , "2_LoginScenes");
        scenesEnumLib.Add(ScenesEnum.LoadingScenes , "LoadingScenes");
        scenesEnumLib.Add(ScenesEnum.LevelOne, "3_LevelOne");
        scenesEnumLib.Add(ScenesEnum.LevelTwo, "5_LevelTwo");
        scenesEnumLib.Add(ScenesEnum.TestScenes, "100_TestDialogSecenes");
        scenesEnumLib.Add(ScenesEnum.MajorCity, "4_MagiorCity");
    }


    //得到实例
    public static ConvertEnumToString  GetInstance()
    {
        if(_instance ==null)
        {
            _instance =new ConvertEnumToString ();//在构造函数当种有一些初始化的工作，所以要使用new
        }

        return _instance ;
    }

    //通过场景的枚举类型获得场景对应的字符串类型
    public string GetStrByEnumScenes(ScenesEnum sceneEnum)
    {
        if(scenesEnumLib !=null&&scenesEnumLib .Count >=1)
        {
            return scenesEnumLib[sceneEnum];
        }
        else
        {
            Debug.LogWarning(GetType() + "/GetStrByEnumScenes()/scenesEnumLib.Count<=0!,please check!");
            return null;
        }
    }
}
