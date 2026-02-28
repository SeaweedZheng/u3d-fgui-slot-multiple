using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using GameMaker;
using SlotMaker;


namespace SlotMaker
{


    public class WinTipController  
    {
        GLabel labOwnerWinTip;
        SlotMachineConfig smConfig;
        public WinTipController()
        {
            Init();
        }

        public void Init()
        {
            //Dispose();
            EventCenter.Instance.AddEventListener<EventData>(SlotMachineEvent.ON_WIN_EVENT, onWinEvent);
        }


        public void InitParam(GObject goWinTip, SlotMachineConfig cf)
        {
            labOwnerWinTip = goWinTip.asLabel;
            labOwnerWinTip.visible = false;

            smConfig = cf;
        }

        /// <summary>
        /// 销毁时调用
        /// </summary>
        public void Dispose()
        {
            EventCenter.Instance.RemoveEventListener<EventData>(SlotMachineEvent.ON_WIN_EVENT, onWinEvent);
        }


        void onWinEvent(EventData res)
        {
            switch (res.name)
            {
                case SlotMachineEvent.SkipWinLine:
                    OnSkipWin(res);
                    break;
                case SlotMachineEvent.TotalWinLine:
                case SlotMachineEvent.SingleWinLine:
                    OnSingleWin(res);
                    break;
            }
        }

        void OnSkipWin(EventData receivedEvent = null)
        {
            labOwnerWinTip.visible = false;
        }

        void OnSingleWin(EventData receivedEvent = null)
        {
            if (!smConfig.IsWECredit)
                return;

            SymbolWin sw = receivedEvent.value as SymbolWin;

            labOwnerWinTip.visible = true;
            labOwnerWinTip.title = $"{sw.earnCredit}";
            Transition transition = labOwnerWinTip.GetTransition("animBigger");
            transition.Play();
        }
    }
}