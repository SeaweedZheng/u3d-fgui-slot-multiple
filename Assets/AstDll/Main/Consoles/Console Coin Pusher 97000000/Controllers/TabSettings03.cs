using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ConsoleCoinPusher97000000
{
    public class TabSettings03 : ConsoleTabMenuBase
    {

        public override void Init()
        {

        }
        public override void InitParam(GComponent comp, Action onClickPrev, Action onClickNext, Action onClickExitCallback, bool isStartTab, bool isEndTab)
        {
            base.InitParam(comp, onClickPrev, onClickNext, onClickExitCallback, isStartTab, isEndTab);

            ResetItem(false);
            AddClickEvent();
        }


        public override void OnClickConfirm()
        {
            if (menuMap.ContainsKey(curIndexMenuItem))
            {

                switch (menuMap[curIndexMenuItem])
                {
                    case "prev":
                        {
                            onClickPrev?.Invoke();
                        }
                        return;
                    case "next":
                        {
                            onClickNext?.Invoke();
                        }
                        return;
                    case "exit":
                        {
                            onClickExitCallback?.Invoke();
                        }
                        return;
                }
            }
        }

    }
}