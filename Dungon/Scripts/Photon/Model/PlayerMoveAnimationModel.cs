using UnityEngine;
using System.Collections;
using System;

public class PlayerMoveAnimationModel
{

    public bool IsMove { get; set; }
    //public DateTime Time { get; set; } 


    public string time;

    public void SetTime(DateTime dateTime)
    {
        time = dateTime.ToString("yyyyMMddHHmmssffff");
    }

    public DateTime GetTime()
    {
        DateTime dt = DateTime.ParseExact(time, "yyyyMMddHHmmssffff", System.Globalization.CultureInfo.CurrentCulture);//将字符串按照指定的格式转化为DateTime
        return dt;
    }

}

