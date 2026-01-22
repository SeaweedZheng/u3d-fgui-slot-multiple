using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameMaker
{
    /// <summary> 滚轮彩金信息 </summary>
    [Serializable]
    public class UIJackpotInfo
    {
        public string name;

        public int id;
        /// <summary> ui当前显示的彩金值（动画彩金值） </summary>
        public float nowCredit;
        /// <summary> 当前真正的彩金值 </summary>
        public float curCredit;
        public float maxCredit;
        public float minCredit;


    }


    /// <summary> 彩金中奖信息 </summary>
    [System.Serializable]
    public class JackpotWinInfo
    {
        public string name;
        public int id;
        /// <summary> 中奖时，得到的彩金值 </summary>
        public float winCredit;
        /// <summary> 中奖时真正的彩金值 </summary>
        public float whenCredit;
        /// <summary> 当前真正的彩金值 </summary>
        public float curCredit;

        public long creditBefore = -1;

        public long creditAfter = -1;

        public static string GetJPDefaultName(int id)
        {
            switch (id)
            {
                case 0:
                    return "grand"; //jp1
                //case 0:  return "mega";  
                case 1:
                    return "major";//jp2
                case 2:
                    return "minor";//jp3
                case 3:
                    return "mini";//jp4
            }
            return null;
        }
    }

    /// <summary> 本轮彩金信息 </summary>
    public class JackpotRes
    {

        public float curJackpotGrand;
        public float curJackpotMega;
        public float curJackpotMajor;
        public float curJackpotMinior;
        public float curJackpotMini;

        public List<JackpotWinInfo> jpWinLst = new List<JackpotWinInfo>();
    }

    public class JackpotRes02
    {
        public Dictionary<int, float> curJackpots = null;

        public Dictionary<int, JackpotWinInfo> jpWinDic = new Dictionary<int, JackpotWinInfo>();
        public float GetCurJackpot(int jpId)
        {
            if(curJackpots.ContainsKey(jpId))
                return curJackpots[jpId];
            return 0;
        }

        public JackpotWinInfo GetJackpotWinInfo(int jpId)
        {
            if (jpWinDic.ContainsKey(jpId))
                return jpWinDic[jpId];
            return null;
        }
    }


    public class WinJackpotInfo
    {
        public int macId;
        public int seat;

        /// <summary> 中多少个币 </summary>
        public int win;

        public int jackpotId;
        public long orderId;

        /// <summary> 毫秒时间戳 </summary>
        public long time;
    }
}
