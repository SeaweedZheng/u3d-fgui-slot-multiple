using PusherEmperorsRein;
using SBoxApi;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CoinPusherRichLegend2001001
{
    public partial class MachineDataManagerG2001001 : ProxyHelper<MachineDataManagerG2001001>
    {

        bool isMock => ApplicationSettings.Instance.isMock;



        protected void Awake()
        {
            receiveOvertimeS = 1.5f;
            isDebugLog = true;
            prefix = "【SBox】";
        }



        protected void Start()
        {


            // ===推币机游戏数据
            //EventCenter.Instance.AddEventListener<string>(SBoxEventHandle.SBOX_COIN_PUSH_SPIN, OnResponseCoinPushSpin);
            EventCenter.Instance.AddEventListener<string>(SBoxEventHandle.SBOX_COIN_PUSH_SPIN_END, OnResponseCoinPushSpinEnd);
            //EventCenter.Instance.AddEventListener<int>(SBoxEventHandle.SBOX_COIN_PUSH_BEGIN_TURN, OnResponseBeginTurn);
            //EventCenter.Instance.AddEventListener<string>(SBoxEventHandle.SBOX_COIN_PUSH_GET_JP_CONTRIBUTION, OnResponseGetJpMajorGrandContribution);
            //EventCenter.Instance.AddEventListener<int>(SBoxEventHandle.SBOX_COIN_PUSH_RETURN_JP_CONTRIBUTION, OnResponseReturnMajorGrandContribution);
            //EventCenter.Instance.AddEventListener<int>(SBoxEventHandle.SBOX_COIN_PUSH_SET_MAJOR_GRAND_WIN, OnResponseSetMajorGrandWin);
        }


        protected override void OnDestroy()
        {

            // ===推币机游戏数据
            //EventCenter.Instance.RemoveEventListener<string>(SBoxEventHandle.SBOX_COIN_PUSH_SPIN, OnResponseCoinPushSpin);
            EventCenter.Instance.RemoveEventListener<string>(SBoxEventHandle.SBOX_COIN_PUSH_SPIN_END, OnResponseCoinPushSpinEnd);
            //EventCenter.Instance.RemoveEventListener<int>(SBoxEventHandle.SBOX_COIN_PUSH_BEGIN_TURN, OnResponseBeginTurn);
            //EventCenter.Instance.RemoveEventListener<string>(SBoxEventHandle.SBOX_COIN_PUSH_GET_JP_CONTRIBUTION, OnResponseGetJpMajorGrandContribution);
            //EventCenter.Instance.RemoveEventListener<int>(SBoxEventHandle.SBOX_COIN_PUSH_RETURN_JP_CONTRIBUTION, OnResponseReturnMajorGrandContribution);
            //EventCenter.Instance.RemoveEventListener<int>(SBoxEventHandle.SBOX_COIN_PUSH_SET_MAJOR_GRAND_WIN, OnResponseSetMajorGrandWin);

            base.OnDestroy();

        }




        ///////////////////////////游戏数据
        ///<summary> 获取游戏彩金 </summary>
        public int RequestCoinPushSpin(Action<object> finishCallback, string mark = null)
        {
            int seqId = OnRequestBefore(SBoxEventHandle.SBOX_COIN_PUSH_SPIN, null, finishCallback, null, mark);

            if (isMock)
            {
                //OnMockActive(null);
                DebugUtils.Log("待实现");
            }
            else
            {
                DebugUtils.Log("待实现");
                //SBoxIdea.CoinPushGetSpinResult(0);
            }
            return seqId;
        }
        void OnResponseCoinPushSpin(string res) => OnResponse(SBoxEventHandle.SBOX_COIN_PUSH_SPIN, res);




        public int RequestCoinPushSpinEnd(Action<object> finishCallback, string mark = null)
        {
            int seqId = OnRequestBefore(SBoxEventHandle.SBOX_COIN_PUSH_SPIN_END, null, finishCallback, null, mark);

            if (isMock)
            {
                OnMockCoinPushSpinEnd(null);
            }
            else
            {
                // 滚轮停止
                //SBoxIdea.CoinPushGetSpinEnd(0);
            }
            return seqId;
        }
        void OnResponseCoinPushSpinEnd(string res) => OnResponse(SBoxEventHandle.SBOX_COIN_PUSH_SPIN_END, res);




        public int RequestCoinCountDown(int winCredit, Action<object> finishCallback, string mark = null)
        {
            int seqId = OnRequestBefore(RpcNameCoinCountDown, winCredit, finishCallback, null, mark);

            if (isMock)
            {
                OnMockCoinCountDown(winCredit);
            }
            else
            {
                OnResponseCoinCountDown(SBoxIdea.Jackpot());
            }
            return seqId;
        }
        void OnResponseCoinCountDown(int res) => OnResponse(RpcNameCoinCountDown, res);
        const string RpcNameCoinCountDown = nameof(RpcNameCoinCountDown); //"RpcNameCoinCountDown";



        public int RequestCoinHit(int winCoins, Action<object> finishCallback, string mark = null)
        {
            int seqId = OnRequestBefore(RpcNameCoinHit, winCoins, finishCallback, null, mark);

            if (isMock)
            {
                OnMockCoinHit(winCoins);
            }
            else
            {
                //OnResponseCoinCountDown(SBoxIdea.Jackpot());
            }
            return seqId;
        }
        void OnResponseCoinHit(int[] res) => OnResponse(RpcNameCoinHit, res);
        const string RpcNameCoinHit = nameof(RpcNameCoinHit); //"RpcNameCoinCountDown";









        public int RequestGetMyCredit(int totalWiinCoin, Action<object> finishCallback, string mark = null)
        {
            int seqId = OnRequestBefore(RpcNameGetMyCredit, null, finishCallback, null, mark);

            if (isMock)
            {
                OnMockGetMyCredit(totalWiinCoin);
            }
            else
            {
                /*
                int myCredit = 0;

                foreach (SBoxPlayerScoreInfo item in SBoxIdea.sBoxInfo.PlayerScoreInfoList)
                {
                    if (item.PlayerId == 1)
                    {
                        myCredit = item.Score;
                    }
                }
                OnResponseGetMyCredit(myCredit);
                */
            }
            return seqId;
        }
        void OnResponseGetMyCredit(int res) => OnResponse(RpcNameGetMyCredit, res);
        const string RpcNameGetMyCredit = nameof(RpcNameGetMyCredit);// "RpcNameGetMyCredit";
    }
}