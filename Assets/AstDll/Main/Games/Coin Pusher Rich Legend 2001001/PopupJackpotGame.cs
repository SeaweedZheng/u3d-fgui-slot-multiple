using FairyGUI;
using GameMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace CoinPusherRichLegend2001001
{
    public class PopupJackpotGame : MachinePageBase
    {
        public const string pkgName = "RichLegend2001001";
        public const string resName = "PopupJackpotGame";

        protected override void OnInit()
        {

            base.OnInit();

            int count = 4;

            Action callback = () =>
            {
                if (--count == 0)
                {
                    isInit = true;
                    InitParam();
                }
            };

            ResourceManager02.Instance.LoadAsset<GameObject>(
                "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001001/Prefabs/Popup Jackpot Game/Pop FG JP 1.prefab",
                (clone) => {
                    goFgJP1Clone = clone;
                    //goFgJP1 = GameObject.Instantiate(goFgJP1Clone);
                    callback();
                });

            ResourceManager02.Instance.LoadAsset<GameObject>(
                "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001001/Prefabs/Popup Jackpot Game/Pop FG JP 2.prefab",
                (clone) => {
                    goFgJP2Clone = clone;
                    //goFgJP2 = GameObject.Instantiate(goFgJP2Clone);
                    callback();
                });

            ResourceManager02.Instance.LoadAsset<GameObject>(
                "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001001/Prefabs/Popup Jackpot Game/Pop FG JP 3.prefab",
                (clone) => {
                    goFgJP3Clone = clone;
                    //goFgJP3 = GameObject.Instantiate(goFgJP3Clone);
                    callback();
                });

            ResourceManager02.Instance.LoadAsset<GameObject>(
                "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001001/Prefabs/Popup Jackpot Game/Pop FG JP Mega.prefab",
                (clone) => {
                    goFgJPMegaClone = clone;
                    //goFgJPMega = GameObject.Instantiate(goFgJPMegaClone);
                    callback();
                });
            // 异步加载资源

            callback();

        }


        GameObject goFgJP1Clone, goFgJP2Clone, goFgJP3Clone, goFgJPMegaClone;

        GameObject goFgJP1, goFgJP2, goFgJP3, goFgJPMega;



        GTextField gtxtNumber;
        GComponent goAnchorJackpot;


        GComponent curAnchorJackpot;
        public override void OnOpen(PageName name, EventData data)
        {
            base.OnOpen(name, data);

            // 添加事件监听

            InitParam();
        }


        Action onJPPoolSubCreditCallback;

        GComponent goBG;

        GComponent gOwner;
        public override void InitParam()
        {

            if (!isInit) return;

            if (!isOpen) return;

            gOwner = this.contentPane.GetChild("page").asCom;

            gtxtNumber = gOwner.GetChild("fg").asCom.GetChild("coin").asTextField;
            goAnchorJackpot = gOwner.GetChild("fg").asCom.GetChild("anchorJackpot").asCom;

            goBG = gOwner.GetChild("bg").asCom;
            goBG.onClick.Clear();
            goBG.onClick.Add(OnClickBG);


            // 解析数据
            if (inParams != null && inParams?.value is Dictionary<string, object> argDic)
            {
                // 解析分数
                if (argDic.TryGetValue("totalEarnCredit", out var scoreVal) && scoreVal is float score)
                {
                    //DebugUtils.LogError($"【免费游戏结算弹窗】 总币数 longScore = {score}");
                    CloseAllTask();

                    DoTaskToNumber((long)score);
                    DoTaskShowNumberEnd();
                }

                if (argDic.TryGetValue("jackpotId", out var jackpotIdVal) && jackpotIdVal is int jackpotId)
                {
                    if (curAnchorJackpot != null)  
                        GameCommon.FguiUtils.DeleteWrapper(curAnchorJackpot);
                    
                    curAnchorJackpot =  gOwner.GetChild("fg").asCom.GetChild("anchorJackpot").asCom;
                    switch (jackpotId)
                    {
                        case 0:
                            GameCommon.FguiUtils.AddWrapper(curAnchorJackpot, GameObject.Instantiate(goFgJP1Clone));
                            // ameCommon.FguiUtils.AddWrapper(curAnchorJackpot, goFgJP1);  // 有bug
                            break;
                        case 1:
                            GameCommon.FguiUtils.AddWrapper(curAnchorJackpot, GameObject.Instantiate(goFgJP2Clone));
                            break;
                        case 2:
                            GameCommon.FguiUtils.AddWrapper(curAnchorJackpot, GameObject.Instantiate(goFgJP3Clone));
                            break;
                        case 3:
                            GameCommon.FguiUtils.AddWrapper(curAnchorJackpot, GameObject.Instantiate(goFgJPMegaClone));
                            break;
                    }
                }



                if (argDic.TryGetValue("onJPPoolSubCredit", out var onJPPoolSubCreditVal) && onJPPoolSubCreditVal is Action onJPPoolSubCredit)
                {
                    onJPPoolSubCreditCallback = onJPPoolSubCredit;
                }
            }

        }


        enum Step
        {
            /// <summary> 循环添加到目标值 </summary>
            AddToNumber = 0,

            /// <summary> 直接设置到目标值-停留一会 （变大？）</summary>
            ShowBigger = 1,

            /// <summary> 直接设置到目标值-停留一会 （变大？）</summary>
            ShowBiggerEnd = 2,

            /// <summary> 延时3秒自动结束 </summary>
            DelayAutoClose = 3,
        }


        long toNumber = 100;
        long curNumber = 0;
        Step step = Step.AddToNumber;

        void TaskToNumber(object param)
        {
            switch (step)
            {
                case Step.AddToNumber:

                    curNumber += 1;
                    if (curNumber <= toNumber)
                    {
                        gtxtNumber.text = curNumber.ToString();

                        Timers.inst.Add(0.05f, 1, TaskToNumber);
                    }
                    else
                    {
                        gtxtNumber.text = toNumber.ToString();

                        step = Step.ShowBigger;
                        TaskToNumber(null);
                    }
                    break;
                case Step.ShowBigger:
                    {
                        gtxtNumber.text = toNumber.ToString();
                        //GameSoundHelper.Instance.StopSound(SoundKey.PopupWinBgNumberAdd);
                        // 变大动画

                        gOwner.GetTransition("bigger").Play(() => {

                            step = Step.ShowBiggerEnd;
                            TaskToNumber(null);
                        });
                    }
                    break;
                case Step.ShowBiggerEnd:
                    {
                        // 是否延时3秒
                        if (!PlayerPrefsUtils.isPauseAtPopupFreeSpinResult)
                        {
                            step = Step.DelayAutoClose;
                            Timers.inst.Add(3f, 1, TaskToNumber);
                        }
                    }
                    break;
                case Step.DelayAutoClose:
                    {
                        ClosePopup();
                    }
                    break;
            }
        }



        void DoTaskToNumber(long longScore)
        {
            toNumber = longScore;
            curNumber = 0;
            step = Step.AddToNumber;
            TaskToNumber(null);
        }

        void DoTaskShowNumberEnd()
        {
            Timers.inst.Add(3, 1, TaskShowNumberEnd);
        }


        void TaskShowNumberEnd(object param)
        {
            if (step == Step.AddToNumber)
            {
                Timers.inst.Remove(TaskToNumber);

                step = Step.ShowBigger;
                TaskToNumber(null);
            }
        }

        void CloseAllTask()
        {
            gOwner.GetTransition("bigger").Stop();

            Timers.inst.Remove(TaskToNumber);
            Timers.inst.Remove(TaskShowNumberEnd);
        }

        void ClosePopup(object onj = null)
        {
            onJPPoolSubCreditCallback?.Invoke();
            onJPPoolSubCreditCallback = null;


            CloseAllTask();
            //GameSoundHelper.Instance.StopSound(SoundKey.PopupWinBgNumberAdd);
            CloseSelf(null);
        }



        /// <summary>
        /// 点击背景进行关闭
        /// </summary>
        void OnClickBG() => OnClickSpinButton();

        /// <summary>
        /// 点击Spin按钮进行关闭
        /// </summary>
        void OnClickSpinButton()
        {
            if (step == Step.AddToNumber)
            {
                CloseAllTask();

                step = Step.ShowBigger;
                TaskToNumber(null);
            }
            else
            {
                ClosePopup();
            }
        }
    }
}