using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using System;
using GameMaker;

namespace ConsoleCoinPusher97000000
{
    public class TabAdmin0002 : ConsoleTabMenuBase
    {

        public override void Init() { }

        public override void Dispose() { }



        public override void InitParam(GComponent comp, Action onClickPrev, Action onClickNext, Action onClickExitCallback, bool isStartTab, bool isEndTab)
        {
            base.InitParam(comp, onClickPrev, onClickNext, onClickExitCallback, isStartTab, isEndTab);


            goOwnerMenu.GetChild("pauseAtPopupGameLoadingOnce").asCom.GetChild("value").asRichTextField.text = PlayerPrefsUtils.isPauseAtPopupGameLoadingOnce ? I18nMgr.T("ON") : I18nMgr.T("OFF");
            goOwnerMenu.GetChild("deletePanel").asCom.GetChild("value").asRichTextField.text = PlayerPrefsUtils.isTestDeletePanel ? I18nMgr.T("ON") : I18nMgr.T("OFF");
            goOwnerMenu.GetChild("deleteReels").asCom.GetChild("value").asRichTextField.text = PlayerPrefsUtils.isTestDeleteReels ? I18nMgr.T("ON") : I18nMgr.T("OFF");

            ResetItem(false);
            AddClickEvent();
        }

        public override void OnClickConfirm()
        {
            if (menuMap.ContainsKey(curIndexMenuItem))
            {

                switch (menuMap[curIndexMenuItem])
                {
                    case "pauseAtPopupGameLoadingOnce":
                        {
                            OnPauseAtPopupGameLoadingOnce();
                        }
                        return;
                    case "deletePanel":
                        {
                            OnDeletePanel();
                        }
                        return;
                    case "deleteReels":
                        {
                            OnDeleteReels();
                        }
                        return;
                    case "destroyPanel":
                        {
                            OnDestroyPanel();
                        }
                        return;
                    case "destroyReels":
                        {
                            OnDestroyReels();
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



        void OnPauseAtPopupGameLoadingOnce()
        {
            PlayerPrefsUtils.isPauseAtPopupGameLoadingOnce = !PlayerPrefsUtils.isPauseAtPopupGameLoadingOnce;
            goOwnerMenu.GetChild("pauseAtPopupGameLoadingOnce").asCom.GetChild("value").asRichTextField.text = PlayerPrefsUtils.isPauseAtPopupGameLoadingOnce ? I18nMgr.T("ON") : I18nMgr.T("OFF");
        }
        void OnDeletePanel()
        {
            PlayerPrefsUtils.isTestDeletePanel = !PlayerPrefsUtils.isTestDeletePanel;
            goOwnerMenu.GetChild("deletePanel").asCom.GetChild("value").asRichTextField.text = PlayerPrefsUtils.isTestDeletePanel ? I18nMgr.T("ON") : I18nMgr.T("OFF");
        }
        void OnDeleteReels()
        {
            PlayerPrefsUtils.isTestDeleteReels = !PlayerPrefsUtils.isTestDeleteReels;
            goOwnerMenu.GetChild("deleteReels").asCom.GetChild("value").asRichTextField.text = PlayerPrefsUtils.isTestDeleteReels ? I18nMgr.T("ON") : I18nMgr.T("OFF");

        }

        private void OnDestroyPanel()
        {
            EventCenter.Instance.EventTrigger<string>(GlobalEvent.ON_TEST_EVENT, GlobalEvent.DestroyPanel);
        }
        private void OnDestroyReels()
        {
            EventCenter.Instance.EventTrigger<string>(GlobalEvent.ON_TEST_EVENT, GlobalEvent.DestroyReels);
        }

    }
}