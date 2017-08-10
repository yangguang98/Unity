using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
///   接口：配置管理器
///   作用：读取系统核心XML配置信息
/// </summary>
public interface IConfigManager {

    Dictionary<string, string> AppSetting { get; }
    int GetAppSettingMaxNum();  //获得设置的数量
}
