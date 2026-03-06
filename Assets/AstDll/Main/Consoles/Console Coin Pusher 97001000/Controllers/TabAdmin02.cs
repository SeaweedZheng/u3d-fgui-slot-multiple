using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ConsoleCoinPusher97001000
{
    public class TabAdmin02 : ConsoleTabMenuBase
    {

        public override void Init() { }

        public override void Dispose() { }



        public override void InitParam(GComponent comp, Action onClickPrev, Action onClickNext, Action onClickExitCallback, bool isStartTab, bool isEndTab)
        {
            base.InitParam(comp, onClickPrev, onClickNext, onClickExitCallback, isStartTab, isEndTab);


            goOwnerMenu.GetChild("pauseAtPopupFreeSpinTrigger").asCom.GetChild("value").asRichTextField.text = PlayerPrefsUtils.isPauseAtPopupFreeSpinTrigger ? I18nMgr.T("ON") : I18nMgr.T("OFF");
            goOwnerMenu.GetChild("pauseAtPopupJackpotGame").asCom.GetChild("value").asRichTextField.text = PlayerPrefsUtils.isPauseAtPopupJackpotGame ? I18nMgr.T("ON") : I18nMgr.T("OFF");
            goOwnerMenu.GetChild("pauseAtPopupJackpotOnline").asCom.GetChild("value").asRichTextField.text = PlayerPrefsUtils.isPauseAtPopupJackpotOnline ? I18nMgr.T("ON") : I18nMgr.T("OFF");
            goOwnerMenu.GetChild("pauseAtPopupFreeSpinResult").asCom.GetChild("value").asRichTextField.text = PlayerPrefsUtils.isPauseAtPopupFreeSpinResult ? I18nMgr.T("ON") : I18nMgr.T("OFF");

            /*#seaweed#*/
            if (ApplicationSettings.Instance.isRelease)
            {
                SetItemTouchable("pauseAtPopupFreeSpinTrigger", false);
                SetItemTouchable("pauseAtPopupJackpotGame", false);
                SetItemTouchable("pauseAtPopupJackpotOnline", false);
                SetItemTouchable("pauseAtPopupFreeSpinResult", false);
            }


            ResetItem(false);
            AddClickEvent();


            // 初始化FGUI（基础触摸支持）
            //Stage.touchScreen = true;
        }


        public override void OnClickConfirm()
        {
            if (menuMap.ContainsKey(curIndexMenuItem))
            {

                switch (menuMap[curIndexMenuItem])
                {
                    case "pauseAtPopupFreeSpinTrigger":
                        {
                            OnClickPauseAtPopupFreeSpinTrigger();
                        }
                        return;
                    case "pauseAtPopupFreeSpinResult":
                        {
                            OnClickPauseAtPopupFreeSpinResult();
                        }
                        return;
                    case "pauseAtPopupJackpotGame":
                        {
                            OnClickPauseAtPopupJackpotGame();
                        }
                        return;
                    case "pauseAtPopupJackpotOnline":
                        {
                            OnClickPauseAtPopupJackpotOnline();
                        }
                        return;
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







        void OnClickPauseAtPopupFreeSpinTrigger()
        {
            PlayerPrefsUtils.isPauseAtPopupFreeSpinTrigger = !PlayerPrefsUtils.isPauseAtPopupFreeSpinTrigger;
            goOwnerMenu.GetChild("pauseAtPopupFreeSpinTrigger").asCom.GetChild("value").asRichTextField.text = PlayerPrefsUtils.isPauseAtPopupFreeSpinTrigger ? I18nMgr.T("ON") : I18nMgr.T("OFF");
        }

        void OnClickPauseAtPopupFreeSpinResult()
        {
            PlayerPrefsUtils.isPauseAtPopupFreeSpinResult = !PlayerPrefsUtils.isPauseAtPopupFreeSpinResult;
            goOwnerMenu.GetChild("pauseAtPopupFreeSpinResult").asCom.GetChild("value").asRichTextField.text = PlayerPrefsUtils.isPauseAtPopupFreeSpinResult ? I18nMgr.T("ON") : I18nMgr.T("OFF");

            //"pauseAtPopupFreeSpinResult"
        }

        void OnClickPauseAtPopupJackpotGame()
        {
            PlayerPrefsUtils.isPauseAtPopupJackpotGame = !PlayerPrefsUtils.isPauseAtPopupJackpotGame;
            goOwnerMenu.GetChild("pauseAtPopupJackpotGame").asCom.GetChild("value").asRichTextField.text = PlayerPrefsUtils.isPauseAtPopupJackpotGame ? I18nMgr.T("ON") : I18nMgr.T("OFF");
        }
        void OnClickPauseAtPopupJackpotOnline()
        {
            PlayerPrefsUtils.isPauseAtPopupJackpotOnline = !PlayerPrefsUtils.isPauseAtPopupJackpotOnline;
            goOwnerMenu.GetChild("pauseAtPopupJackpotOnline").asCom.GetChild("value").asRichTextField.text = PlayerPrefsUtils.isPauseAtPopupJackpotOnline ? I18nMgr.T("ON") : I18nMgr.T("OFF");
        }





    }
}