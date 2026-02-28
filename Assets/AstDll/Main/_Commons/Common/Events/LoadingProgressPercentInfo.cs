using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingProgressPercentInfo {

    public string msg;
    public int totalStepCount; 
    public int curStepIndex;
    public int curStepSubCount = 0;

    /// <summary>
    /// 
    /// </summary>
    /// <returns> 0 到 1</returns>
    public float GetProgressValue()
    {
        int subCount = curStepSubCount;

        float subProgress = 0;

        while (--subCount >= 0)
        {
            subProgress += ((1f / (float)totalStepCount) - subProgress) * 0.2f; // 添加剩余的百分之20%
        }
        return (float)curStepIndex * (1f / (float)totalStepCount) + subProgress;
    }

}
