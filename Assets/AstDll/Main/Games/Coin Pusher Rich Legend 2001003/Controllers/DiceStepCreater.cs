using System.Collections.Generic;
using UnityEngine;
using System;

namespace CoinPusherRichLegend2001003
{
    public class DiceStepCreater
    {
        const int endIndex1 = 0;
        const int endIndex2 = 11;

        const int totalMapCount = 22;
        static int[] coins = new int[] { 10, 20, 30, 40, 50, 60, 80 };


        static List<int> _CreatDefaultMap()
        {
            List<int> mapRewards = new List<int>();

            for (int i = 0; i < totalMapCount; i++)
            {
                mapRewards.Add(-1);
            }
            mapRewards[endIndex1] = DiceGameInfo.NONE;
            mapRewards[endIndex2] = DiceGameInfo.NONE;
            return mapRewards;
        }


        static public DiceGameInfo CreatSteps(List<int> result)
        {
            bool isFinish = false;
            bool isError = true;

            List<int> mapRewards = null;

            List<int> diceTimes = null;

            float lastTimeS = Time.unscaledTime + 2f;
            while (!isFinish && Time.unscaledTime < lastTimeS)
            {
                mapRewards = _CreatDefaultMap();
                diceTimes = new List<int>();
                isError = true;

                int lastMapIndex = 0;
                int idx = 0;

                while (true && Time.unscaledTime < lastTimeS)
                {
                    int time = UnityEngine.Random.Range(1, 7);
                    int curMapIndex = lastMapIndex + time;

                    if (curMapIndex >= totalMapCount)
                        curMapIndex -= totalMapCount;

                    if (mapRewards[curMapIndex] == -1 || mapRewards[curMapIndex] == result[idx])
                    {
                        mapRewards[curMapIndex] = result[idx];
                        diceTimes.Add(time);

                        lastMapIndex = curMapIndex;

                        idx++;

                        if (idx >= result.Count)
                        {
                            isError = false;
                            break;
                        }
                    }
                }

                if (isError)
                    continue;

                if (lastMapIndex > totalMapCount / 2)
                {
                    int step = totalMapCount - lastMapIndex;
                    if (step <= 6)
                    {
                        diceTimes.Add(step);
                        isFinish = true;
                    }
                }
                else
                {
                    int step = totalMapCount / 2 - lastMapIndex;
                    if (step <= 6)
                    {
                        diceTimes.Add(step);
                        isFinish = true;
                    }
                }

            }

            if (Time.unscaledTime >= lastTimeS)
                throw (new Exception("创建游戏数据时，死循环"));


            for (int i = 0; i < mapRewards.Count; i++)
            {
                if (mapRewards[i] == -1)
                {
                    mapRewards[i] = coins[UnityEngine.Random.Range(0, coins.Length)];
                }
            }


            List<DiceStepInfo> stepInfos = new List<DiceStepInfo>();
            int curIndex = 0;
            for (int i = 0; i < diceTimes.Count; i++)
            {
                curIndex += diceTimes[i];
                if (curIndex >= totalMapCount)
                    curIndex = 0;

                DiceStepInfo item = new DiceStepInfo()
                {
                    diceData = diceTimes[i],
                    reward = i== diceTimes.Count-1? DiceGameInfo.NONE : result[i],
                    mapIndex = curIndex
                };
                stepInfos.Add(item);
            }




            DiceGameInfo info = new DiceGameInfo()
            {
                stepInfos = stepInfos,
                mapRewards = mapRewards,
            };

            return info;
        }

    }



    public class DiceGameInfo
    {
        public const int NONE = -99999;
        public List<DiceStepInfo> stepInfos;
        public List<int> mapRewards;
    }

    public class DiceStepInfo
    {
        public int diceData;
        public int reward;
        public int mapIndex;
    }
}