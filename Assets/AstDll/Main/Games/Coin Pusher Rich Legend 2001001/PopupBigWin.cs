using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameMaker;

namespace CoinPusherRichLegend2001001
{
    public class PopupBigWin : MachinePageBase
    {
        public const string pkgName = "RichLegend2001001";
        public const string resName = "PopupBigWin";
        protected override void OnInit()
        {

            base.OnInit();

            int count = 1;

            Action callback = () =>
            {
                if (--count == 0)
                {
                    isInit = true;
                    InitParam();
                }
            };

            // 异步加载资源

            callback();




            machineBtnClickHelper = new MachineButtonClickHelper()
            {

                upClickHandler = new Dictionary<MachineButtonKey, Action<MachineButtonInfo>>()
                {
                    [MachineButtonKey.BtnSpin] = (info) =>
                    {
                        OnClickSpinButton();
                    },
                },
            };
        }

        public override void OnOpen(PageName name, EventData data)
        {
            base.OnOpen(name, data);

            // 添加事件监听

            InitParam();
        }


        public override void OnClose(EventData data = null)
        {

            // 删除事件监听

            base.OnClose(data);
        }


        // public override void OnTop() { DebugUtils.Log($"i am top {this.name}"); }



        GTextField gtxtNumber;

        GComponent gOwner;
        public override void InitParam()
        {

            if (!isInit) return;
            
            preLoadedCallback?.Invoke();
            preLoadedCallback?.RemoveAllListeners();

            if (!isOpen) return;


            gOwner = this.contentPane.GetChild("page").asCom;

            gOwner.onTouchEnd.Clear();
            gOwner.onTouchEnd.Add(OnClickBG);

            gtxtNumber = gOwner.GetChild("fg").asCom.GetChild("coin").asTextField;



            int toNum  = 10000;

            if (inParams != null)
            {   
                Dictionary<string, object> argDic = null;
                argDic = (Dictionary<string, object>)inParams.value;
                toNum = (int)argDic["totalWinCpions"];
            }

            DoTaskToNumber(toNum);
            
        }










        enum Step
        {

            In,
            /// <summary> 循环添加到目标值 </summary>
            AddToNumber,

            /// <summary> 直接设置到目标值-停留一会 （变大？）</summary>
            ShowBigger,

            /// <summary> 直接设置到目标值-停留一会 （变大？）</summary>
            ShowBiggerEnd,

            /// <summary> 延时3秒自动结束 </summary>
            DelayAutoClose,
        }


        long toNumber = 100;
        long curNumber = 0;
        Step step = Step.AddToNumber;

        void TaskToNumber(object param)
        {
            switch (step)
            {
                case Step.In:
                    {
                        gtxtNumber.text = 0.ToString();
                        gOwner.GetTransition("in").Play();

                        step = Step.AddToNumber;
                        Timers.inst.Add(0.2f, 1, TaskToNumber);
                    }
                    break;
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

            Timers.inst.Remove(TaskToNumber);
            Timers.inst.Remove(TaskShowNumberEnd);

            DoTaskShowNumberEnd();

            step = Step.In;
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