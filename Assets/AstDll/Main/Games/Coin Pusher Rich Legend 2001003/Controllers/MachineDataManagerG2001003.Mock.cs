using SBoxApi;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MachineDataManagerG2001003 
{

    float timeForCoinCountDown = 0;
    int remainCoinCountDown = -1;
    int coinToCountDown = 0;

    void OnMockCoinCountDown(object req)
    {
        if (remainCoinCountDown < 0)
        {
            remainCoinCountDown = (int)req;
            coinToCountDown = remainCoinCountDown * 10 / 100;
            if (coinToCountDown <= 0)
                coinToCountDown = 1;
        }

        OnResponseCoinCountDown(remainCoinCountDown);

        if (remainCoinCountDown == 0)
        {
            remainCoinCountDown = -1;
        }
        else if (timeForCoinCountDown < Time.unscaledTime)
        {
            timeForCoinCountDown = Time.unscaledTime + UnityEngine.Random.Range(0.02f, 0.5f);
            remainCoinCountDown -= coinToCountDown;

            if (remainCoinCountDown < 0)
                remainCoinCountDown = 0;
        }
    }


    int remainCoinHit = 0;
    int coinToHit = 0;
    float timeForCoinHit = 0;
    List<int> coinHits = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };
    string lastCoinHitsStr = "";
    void OnMockCoinHit(object req)
    {
        if (remainCoinHit <= 0)
        {
            remainCoinHit = (int)req;
            coinToHit = remainCoinHit * 10 / 100;
            if (coinToHit <= 0)
                coinToHit = 1;
        }

        int coinHit = 0;
        if (remainCoinHit > 0 && timeForCoinHit < Time.unscaledTime)
        {
            timeForCoinHit = Time.unscaledTime + UnityEngine.Random.Range(0.02f, 0.5f);

            coinHit = remainCoinHit > coinToHit ? coinToHit : remainCoinHit;

            remainCoinHit -= coinHit;
            if (remainCoinHit < 0)
                remainCoinHit = 0;
        }

        int index = UnityEngine.Random.Range(0,20);
        if (coinHit != 0 && index <= 7)
        {
            coinHits[index] += coinHit;
            if (coinHits[index] > 150)
                coinHits[index] = 0;

            OnResponseCoinHit(coinHits.ToArray());
        }
        else
        {
            OnResponseCoinHit(coinHits.ToArray());
        }
        //DebugUtils.Log($"remainCoinHit: {remainCoinHit} index: {index} coinHit:{coinHit} ");
    }





    void OnMockCoinPushSpinEnd(object req)
    {
        // 滚轮停止
        JSONNode res = new JSONObject();
        res["code"] = 0;
        //res["credit"] = myCreditMock;
        //res["playerCredit"] = myCreditMock;
        EventCenter.Instance.EventTrigger(SBoxEventHandle.SBOX_COIN_PUSH_SPIN_END, res.ToString());
    }







    public int myCreditMock
    {
        get => MachineDataManager02.Instance.myCreditMock;
        set => MachineDataManager02.Instance.myCreditMock = value;
    }

    void OnMockGetMyCredit(int totalWinCoin) //(object req)
    {
        int coinInNum = 0;
        if(totalWinCoin != 0)
        {
            int isReward = UnityEngine.Random.Range(0,5);
            if (isReward == 1)
            {
                coinInNum = UnityEngine.Random.Range(1, 10);
                if(coinInNum > totalWinCoin)
                    coinInNum = totalWinCoin;  
                myCreditMock += coinInNum * SBoxModel.Instance.CoinInScale;
            }
        }
        OnResponseGetMyCredit(myCreditMock);
    }

}

