using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ConsoleCoinPusher97001000
{
    public class TabAdmin01 : ConsoleTabMenuBase
    {

        public override void Init() { }

        public override void Dispose() { }



        public override void InitParam(GComponent comp, Action onClickPrev, Action onClickNext, Action onClickExitCallback, bool isStartTab, bool isEndTab)
        {
            base.InitParam(comp, onClickPrev, onClickNext, onClickExitCallback, isStartTab, isEndTab);


            goOwnerMenu.GetChild("installVer").asCom.GetChild("value").asRichTextField.text = $"{ApplicationSettings.Instance.appVersion}/{GlobalModel.installHofixVersion}";
            goOwnerMenu.GetChild("sofwareVer").asCom.GetChild("value").asRichTextField.text = $"{ApplicationSettings.Instance.appVersion}/{GlobalModel.hotfixVersion}";
            goOwnerMenu.GetChild("appKey").asCom.GetChild("value").asRichTextField.text = ApplicationSettings.Instance.appKey;
            goOwnerMenu.GetChild("hotfixKey").asCom.GetChild("value").asRichTextField.text = GlobalModel.hotfixKey;
            goOwnerMenu.GetChild("severAddress").asCom.GetChild("value").asRichTextField.text = ApplicationSettings.Instance.resourceServer;
            goOwnerMenu.GetChild("autoHofixAddress").asCom.GetChild("value").asRichTextField.text = GlobalModel.autoHotfixUrl;

            goOwnerMenu.GetChild("isDebugLog").asCom.GetChild("value").asRichTextField.text = SBoxModel.Instance.isDebugLog ? I18nMgr.T("ON") : I18nMgr.T("OFF");
            goOwnerMenu.GetChild("enableDebugTool").asCom.GetChild("value").asRichTextField.text = SBoxModel.Instance.isUseTestTool ? I18nMgr.T("ON") : I18nMgr.T("OFF");
            goOwnerMenu.GetChild("enableReporterPage").asCom.GetChild("value").asRichTextField.text = SBoxModel.Instance.isUseReporterPage ? I18nMgr.T("ON") : I18nMgr.T("OFF");


            //goOwnerMenu.GetChild("isUseAllConsolePage").asCom.GetChild("value").asRichTextField.text = PlayerPrefsUtils.isUseAllConsolePage ? I18nMgr.T("ON") : I18nMgr.T("OFF");

            goOwnerMenu.GetChild("isUseMqttDefault").asCom.GetChild("value").asRichTextField.text = SBoxModel.Instance.isUseRemoteDefault ? I18nMgr.T("ON") : I18nMgr.T("OFF");

            goOwnerMenu.GetChild("fguiInputMethod").asCom.GetChild("value").asRichTextField.text = I18nMgr.T(Stage.touchScreen ? "Touch" : "Mouse");



            SetItemTouchable("installVer", false);
            SetItemTouchable("sofwareVer", false);
            SetItemTouchable("appKey", false);
            SetItemTouchable("hotfixKey", false);
            SetItemTouchable("severAddress", false);
            SetItemTouchable("autoHofixAddress", false);



            /*#seaweed#*/
            if (ApplicationSettings.Instance.isRelease)
            {
                SetItemTouchable("isDebugLog", false);
                SetItemTouchable("enableDebugTool", false);
                SetItemTouchable("enableReporterPage", false);
                //SetItemTouchable("isUseAllConsolePage", false);
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
                    case "isDebugLog":
                        {
                            OnClickIsDebugLog();
                        }
                        return;
                    case "enableDebugTool":
                        {
                            OnClickEnableDebugTool();
                        }
                        return;
                    case "enableReporterPage":
                        {
                            OnClickEnableReporterPage();
                        }
                        return;
                   /* case "isUseAllConsolePage":
                        {
                            OnClickIsUseAllConsolePage();
                        }
                        return;*/
                    case "isUseMqttDefault":
                        {
                            OnClickIsUseMqttDefault();
                        }
                        return;
                    case "fguiInputMethod":
                        {
                            OnClickFguiInputMethod();
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

        private void OnClickFguiInputMethod()
        {
            Stage.touchScreen = !Stage.touchScreen;
            goOwnerMenu.GetChild("fguiInputMethod").asCom.GetChild("value").asRichTextField.text = I18nMgr.T(Stage.touchScreen ? "Touch" : "Mouse");
        }


        private void OnClickIsUseMqttDefault()
        {
            SBoxModel.Instance.isUseRemoteDefault = true;
            goOwnerMenu.GetChild("isUseMqttDefault").asCom.GetChild("value").asRichTextField.text = SBoxModel.Instance.isUseRemoteDefault ? I18nMgr.T("ON") : I18nMgr.T("OFF");
            MachineDeviceCommonBiz.Instance.CheckMqttRemoteButtonController();
        }
        private void OnClickIsDebugLog()
        {
            SBoxModel.Instance.isDebugLog = !SBoxModel.Instance.isDebugLog;
            goOwnerMenu.GetChild("isDebugLog").asCom.GetChild("value").asRichTextField.text = SBoxModel.Instance.isDebugLog ? I18nMgr.T("ON") : I18nMgr.T("OFF");
        }

        void OnClickEnableDebugTool()
        {

            SBoxModel.Instance.isUseTestTool = !SBoxModel.Instance.isUseTestTool;
            goOwnerMenu.GetChild("enableDebugTool").asCom.GetChild("value").asRichTextField.text = SBoxModel.Instance.isUseTestTool ? I18nMgr.T("ON") : I18nMgr.T("OFF");
            TestUtils.CheckTestManager();
        }


        void OnClickEnableReporterPage()
        {
            SBoxModel.Instance.isUseReporterPage = !SBoxModel.Instance.isUseReporterPage;
            goOwnerMenu.GetChild("enableReporterPage").asCom.GetChild("value").asRichTextField.text = SBoxModel.Instance.isUseReporterPage ? I18nMgr.T("ON") : I18nMgr.T("OFF");
            TestUtils.CheckReporter();
        }





        void OnClickIsUseAllConsolePage()
        {
            PlayerPrefsUtils.isUseAllConsolePage = !PlayerPrefsUtils.isUseAllConsolePage;
            goOwnerMenu.GetChild("isUseAllConsolePage").asCom.GetChild("value").asRichTextField.text = PlayerPrefsUtils.isUseAllConsolePage ? I18nMgr.T("ON") : I18nMgr.T("OFF");
        }



    }
}