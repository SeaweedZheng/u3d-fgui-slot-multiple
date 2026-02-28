using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMaker;
public static class GlobalEvent_OnSystemUIEvent
{
    public const string LoadingProgress = nameof(LoadingProgressInfo); // msg flot-progress
    public const string OtehrName = nameof(OtehrName);
    public static void OnEnable()
    {
        EventCenter.Instance.AddEventListener<EventData>(nameof(GlobalEvent_OnSystemUIEvent), OnSystemUIEvent);
    }

    public static void OnSystemUIEvent(EventData res)
    {
        if (res.name == GlobalEvent_OnSystemUIEvent.LoadingProgress)
        {
            LoadingProgressInfo data = res.value as LoadingProgressInfo;
        }
    }
}


public static partial class GlobalEvent
{
    ///<summary> 系统UI事件 </summary>
    public const string ON_SYSTEM_UI_EVENT = nameof(ON_SYSTEM_UI_EVENT);
    public const string LoadingProgress = nameof(LoadingProgressInfo); // msg flot-progress
    public const string LoadingProgressPercent = nameof(LoadingProgressPercentInfo); // msg flot-progress
    public const string LoadingProgress1 = nameof(LoadingProgress1); // msg flot-progress
}