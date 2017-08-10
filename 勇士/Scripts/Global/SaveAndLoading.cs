using UnityEngine;
using System.Collections;




using kernel;
/// <summary>
/// 核心层：对象持久化
/// 描述：将模型层核心数据持久化，
/// </summary>
public class SaveAndLoading : MonoBehaviour
{

    //void Start()
    //{
    //    SaveGameProgress();
    //}
    public static SaveAndLoading _instance;

    /*数据持久化路径*/

    //全局参数对象路径
    private static string fileByGlobalData = Application.persistentDataPath + "GlobalParaData.xml";
    //玩家核心数据对象路径
    private static string fileByKernalData = Application.persistentDataPath + "KernalParaData.xml";
    //玩家扩展数据对象路径
    private static string fileByExtendData = Application.persistentDataPath + "ExtendParaData.xml";
    //玩家背包数据对象路径
    private static string fileByPackageData = Application.persistentDataPath + "PackageParaData.xml";


    //模型层代理类
    private PlayerKernalDataProxy playerKernalDataProxy;
    private PlayerExternalDataProxy playerExternalDataProxy;
    private PlayerPackageDataProxy playerPackageDataProxy;

    public static SaveAndLoading GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameObject("SaveAndLoading").AddComponent<SaveAndLoading>();
            return _instance;
        }
        return _instance;
    }


    #region 存储游戏进度
    public void SaveGameProgress()
    {
        playerKernalDataProxy = PlayerKernalDataProxy.GetInsance();
        playerExternalDataProxy = PlayerExternalDataProxy.GetInsance();
        playerPackageDataProxy = PlayerPackageDataProxy.GetInstance();

        //存储游戏全局参数
        StoreToXml_GlobalData();
        //存储玩家核心数据
        StoreToXml_KernalData();
        //存储玩家扩展数据
        StoreToXml_ExtendData();
        //存储玩家背包数据
        StoreToXml_PackageData();
    }

    //存储游戏全局参数
    private void StoreToXml_GlobalData()
    {
        string playerName = GlobalParameterMgr.playerName;
        ScenesEnum scenesName = GlobalParameterMgr.nextScenesName;

        GlobalParameterData gpd = new GlobalParameterData(scenesName, playerName);

        //序列化对象
        string s = XmlOperation.GetInstance().SerializeObject(gpd, typeof(GlobalParameterData));

        //创建xml,写入
        if (!string.IsNullOrEmpty(fileByGlobalData))
        {
            XmlOperation.GetInstance().CreateXML(fileByGlobalData, s);
        }
        Debug.Log(GetType() + "/StoreToXml_GlobalData()/xml Path=" + fileByGlobalData);

    }

    //存储玩家核心数据
    private void StoreToXml_KernalData()
    {
        //数据准备
        float health = playerKernalDataProxy.Health;
        float magic = playerKernalDataProxy.Magic;
        float attack = playerKernalDataProxy.Attack;
        float def = playerKernalDataProxy.Defence;
        float dex = playerKernalDataProxy.Dexterity;

        float maxHealth = playerKernalDataProxy.MaxHealth;
        float maxMagic = playerKernalDataProxy.MaxMagic;
        float maxAttack = playerKernalDataProxy.MaxAttack;
        float maxDEF = playerKernalDataProxy.MaxDefence;
        float maxDEX = playerKernalDataProxy.MaxDexterity;

        float attackByPro = playerKernalDataProxy.AttackByPro;
        float defenceByPro = playerKernalDataProxy.DefenceByPro;
        float dexterityByPro = playerKernalDataProxy.DexterityByPro;

        //实例化“类”
        PlayerKernalData pKD = new PlayerKernalData(health, magic, attack, def, dex, maxHealth, maxMagic, maxAttack, maxDEF, maxDEX, attackByPro, defenceByPro, dexterityByPro);

        //对象序列化
        string s = XmlOperation.GetInstance().SerializeObject(pKD, typeof(PlayerKernalData));

        //创建XML对象，写入文件
        if (!string.IsNullOrEmpty(fileByKernalData))
        {
            XmlOperation.GetInstance().CreateXML(fileByKernalData, s);
        }
    }

    //存储玩家扩展数据
    private void StoreToXml_ExtendData()
    {
        //数据准备
        float exp = playerExternalDataProxy.Experience;
        float killNum = playerExternalDataProxy.KillNum;
        float level = playerExternalDataProxy.Level;
        float gold = playerExternalDataProxy.Gold;
        float diamond = playerExternalDataProxy.Diamond;


        //实例化“类”
        PlayerExternalData pED = new PlayerExternalData(exp, killNum, level, gold, diamond);

        //对象序列化
        string s = XmlOperation.GetInstance().SerializeObject(pED, typeof(PlayerExternalData));

        //创建XML对象，写入文件

        if (!string.IsNullOrEmpty(fileByExtendData))
        {
            XmlOperation.GetInstance().CreateXML(fileByExtendData, s);
        }
    }

    //存储玩家背包数据
    private void StoreToXml_PackageData()
    {
        //数据准备
        int bloodBottleNum = playerPackageDataProxy.BooldBottleNum;
        int magicBottleNum = playerPackageDataProxy.MagicBottleNum;
        int proATKNum = playerPackageDataProxy.PropATKNum;
        int proDEFNum = playerPackageDataProxy.PropDEFNum;
        int proDEXNum = playerPackageDataProxy.PropDEXNum;

        //实例化“类”
        PlayerPackageData pPD = new PlayerPackageData(bloodBottleNum, magicBottleNum, proDEFNum, proDEXNum, proATKNum);

        //序列化对象
        string s = XmlOperation.GetInstance().SerializeObject(pPD, typeof(PlayerPackageData));

        //创建XML对象，写入文件
        if (!string.IsNullOrEmpty(fileByPackageData))
        {
            XmlOperation.GetInstance().CreateXML(fileByPackageData, s);
        }
    }
    #endregion


    #region 提取游戏进度

    /// <summary>
    /// 提取全局数据
    /// </summary>
    /// <returns></returns>
    public bool LoadGlobalParameter()
    {
        ReadFromXml_GlobalData();
        return true;
    }

    /// <summary>
    /// 提取玩家数据
    /// </summary>
    /// <returns></returns>
    public bool LoadPlayerData()
    {

        ////核心数据
        ReadFromXml_KernalData();
        ////扩展数据
        ReadFromXml_ExtendData();
        ////背包数据
        ReadFromXml_PackageData();
        return true;
    }

    //读取游戏的全局参数
    private void ReadFromXml_GlobalData()
    {
        GlobalParameterData gpd;

        //参数检查
        if (string.IsNullOrEmpty(fileByGlobalData))
        {
            Debug.LogError(GetType() + "/ReadFromXml_GlobalData()/fileByGlobalData is Empty");
            return;
        }

        try
        {
            //读取xml数据
            string strTemp = XmlOperation.GetInstance().LoadXML(fileByGlobalData);

            //反序列化
            gpd = XmlOperation.GetInstance().DeserializeObject(strTemp, typeof(GlobalParameterData)) as GlobalParameterData;

            //赋值
            GlobalParameterMgr.playerName = gpd.PlayerName;
            GlobalParameterMgr.nextScenesName = gpd.NextScenesName;
            GlobalParameterMgr.curGameType = CurrentGameType.Continue;
        }
        catch
        {
            Debug.LogError(GetType() + "/ReadFromXml_GlobalData()/读取游戏的全局参数，不成功，请检查");
        }


    }

    ////核心数据
    private void ReadFromXml_KernalData()
    {
        PlayerKernalData pKD;

        //参数检查
        if (string.IsNullOrEmpty(fileByKernalData))
        {
            Debug.LogError(GetType() + "/ReadFromXml_KernalData()/fileByKernalData is Empty");
            return;
        }

        try
        {
            //读取xml数据
            string strTemp = XmlOperation.GetInstance().LoadXML(fileByKernalData);

            //反序列化
            pKD = XmlOperation.GetInstance().DeserializeObject(strTemp, typeof(PlayerKernalData)) as PlayerKernalData;

            //赋值
            PlayerKernalDataProxy._instance.Health = pKD.Health;
            PlayerKernalDataProxy._instance.Magic = pKD.Magic;
            PlayerKernalDataProxy._instance.Attack = pKD.Attack;
            PlayerKernalDataProxy._instance.Defence = pKD.Defence;
            PlayerKernalDataProxy._instance.Dexterity = pKD.Dexterity;

            PlayerKernalDataProxy._instance.MaxHealth = pKD.MaxHealth;
            PlayerKernalDataProxy._instance.MaxMagic = pKD.MaxMagic;
            PlayerKernalDataProxy._instance.MaxAttack = pKD.MaxAttack;
            PlayerKernalDataProxy._instance.MaxDefence = pKD.MaxDefence;
            PlayerKernalDataProxy._instance.MaxDexterity = pKD.MaxDexterity;

            PlayerKernalDataProxy._instance.AttackByPro = pKD.AttackByPro;
            PlayerKernalDataProxy._instance.DefenceByPro = pKD.DefenceByPro;
            PlayerKernalDataProxy._instance.DexterityByPro = pKD.DexterityByPro;
        }
        catch
        {
            Debug.LogError(GetType() + "/ReadFromXml_KernalData()/读取游戏的核心参数，不成功，请检查");
        }
    }
    ////扩展数据
    private void ReadFromXml_ExtendData()
    {
        PlayerExternalData pED;

        //参数检查
        if (string.IsNullOrEmpty(fileByExtendData))
        {
            Debug.LogError(GetType() + "/ReadFromXml_ExtendData()/fileByExternalData is Empty");
            return;
        }

        try
        {
            //读取xml数据
            string strTemp = XmlOperation.GetInstance().LoadXML(fileByExtendData);

            //反序列化
            pED = XmlOperation.GetInstance().DeserializeObject(strTemp, typeof(PlayerExternalData)) as PlayerExternalData;

            //赋值
            PlayerExternalDataProxy._instance.Experience = pED.Experience;
            PlayerExternalDataProxy._instance.KillNum = pED.KillNum;
            PlayerExternalDataProxy._instance.Level = pED.Level;
            PlayerExternalDataProxy._instance.Gold = pED.Gold;
            PlayerExternalDataProxy._instance.Diamond = pED.Diamond;
        }
        catch
        {
            Debug.LogError(GetType() + "/ReadFromXml_ExtendData()/读取游戏的扩展参数，不成功，请检查");
        }
    }
    ////背包数据
    private void ReadFromXml_PackageData()
    {

        PlayerPackageData pPD;

        //参数检查
        if (string.IsNullOrEmpty(fileByPackageData))
        {
            Debug.LogError(GetType() + "/ReadFromXml_PackageData()/fileByPackageData is Empty");
            return;
        }

        try
        {
            //读取xml数据
            string strTemp = XmlOperation.GetInstance().LoadXML(fileByPackageData);

            //反序列化
            pPD = XmlOperation.GetInstance().DeserializeObject(strTemp, typeof(PlayerPackageData)) as PlayerPackageData;

            //赋值
            PlayerPackageDataProxy.GetInstance().BooldBottleNum = pPD.BooldBottleNum;
            PlayerPackageDataProxy.GetInstance().MagicBottleNum = pPD.MagicBottleNum;
            PlayerPackageDataProxy.GetInstance().PropATKNum = pPD.PropATKNum;
            PlayerPackageDataProxy.GetInstance().PropDEFNum = pPD.PropDEFNum;
            PlayerPackageDataProxy.GetInstance().PropDEXNum = pPD.PropDEXNum;
        }
        catch
        {
            Debug.LogError(GetType() + "/ReadFromXml_PackageData()/读取游戏的背包参数，不成功，请检查");
        }
    #endregion
    }
}