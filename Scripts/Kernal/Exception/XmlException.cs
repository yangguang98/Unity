using UnityEngine;
using System.Collections;
using System;
/// <summary>
/// 核心层：自定义异常   （XML解析异常）
/// 作用：   专门定位于XML解析的异常
/// </summary>
public class XmlException : SystemException  {

    public XmlException() : base() { }

    public XmlException (string exception):base(exception){}
}
