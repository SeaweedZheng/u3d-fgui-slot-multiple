using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;


public class FilterInfo
{
    public string name;
    public string content = "";
    public bool isFiltered = true;
}
public static class DebugFilter
{
    static bool isFilter = true;

    /*
    static List<string> targetToFilter = new List<string>()
    {
        "RpcNameIsCodingActive", // 激活
        "SBOX_IDEA_INFO", // 过滤20001
        "ON_WIN_EVENT", // 赢线循环显示
        "【OrderReship】", //订单补发

    };
    */

    static List<FilterInfo> targetToFilter2 = new List<FilterInfo>()
    {
        new FilterInfo()
        {
            name = "激活信息",
            content = "RpcNameIsCodingActive",
        },
        new FilterInfo()
        {
            name = "20001信息",
            content = "SBOX_IDEA_INFO",
        },
        new FilterInfo()
        {
            name = "赢线信息",
            content = "ON_WIN_EVENT",
        },
        new FilterInfo()
        {
            name = "订单补发信息",
            content = "【OrderReship】",
        },
        new FilterInfo()
        {
            name = "后台信息",
            content = "【UDP-WS】",
        },
        new FilterInfo()
        {
            name = "后台UDP信息",
            content = "【UDP-WS】UDP/C2S :",
        },
    };




    static List<FilterInfo> _filterSettings = null;
    public static List<FilterInfo> filterSettings
    {
        get
        {
            if (_filterSettings == null)
            {
                string str = PlayerPrefs.GetString(FILTER_SETTINGS, "[]");
                JArray cacheNodes = JArray.Parse(str);

                for (int i = 0; i < targetToFilter2.Count; i++)
                {
                    for (int j = 0; j < cacheNodes.Count; j++)
                    {
                        if (cacheNodes[j]["content"].Value<string>() == targetToFilter2[i].content)
                        {
                            targetToFilter2[i].isFiltered = cacheNodes[j]["isFiltered"].Value<bool>();
                            break;
                        }
                    }
                }

                PlayerPrefs.SetString(FILTER_SETTINGS, JsonConvert.SerializeObject(targetToFilter2));
                _filterSettings = targetToFilter2;
            }
            return _filterSettings;
        }
        set
        {

        }
    }

    const string FILTER_SETTINGS = nameof(FILTER_SETTINGS);

    /*
    public static bool CheckFilter(object obj)
    {
        if(obj is string content)
        {
            foreach (string item in targetToFilter)
            {
                if (content.Contains(item))
                    return true;
            }
        }
        return false;
    }*/

    public static void SaveDebugFilterSettings()
    {
        PlayerPrefs.SetString(FILTER_SETTINGS, JsonConvert.SerializeObject(filterSettings));
    }

    public static bool CheckFilter(object obj)
    {
        if (obj is string content)//popupDebugFilter
        {
            foreach (FilterInfo item in filterSettings)
            {
                if (content.Contains(item.content))
                    return item.isFiltered;
            }
        }
        return false;
    }
}
