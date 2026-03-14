using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary> 打码页面 </summary>
public class InParamsConsoleCoderBase : InParamsBase
{
    public string A;
    public string B;
    public string C;
    public string D;
    public string E;
    public string day;
    public string hour;
    public string minute;
}
public class OutParamsConsoleCoderBase : OutParamsBase
{
    public string password;
}



/// <summary> 下拉选择项 </summary>
public class InParamItemSelectOption
{
    /// <summary> 选项类型 </summary>
    public string selectType;
    /// <summary> 选项名称</summary>
    public string selectName;

    /// <summary> 当前选择</summary>
    public string selectKey;
    /// <summary>
    /// 选项列表
    /// </summary>
    /// <remarks>
    /// * selectContentKey - slelectContentName
    /// </remarks>
    public Dictionary<string, string> selectContent = new Dictionary<string, string>();
}