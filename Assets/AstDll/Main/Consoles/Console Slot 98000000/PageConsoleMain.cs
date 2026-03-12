using FairyGUI;
using GameMaker;
using System.Collections.Generic;
using SBoxApi;
using System;


namespace ConsoleSlot98000000
{
    public class PageConsoleMain : PageBase
    {
        public const string pkgName = "ConsoleSlot98000000";
        public const string resName = "PageConsoleMain";
        public override PageType pageType => PageType.Overlay;

        public static void OnBeforeCreat(Action onFinishCallback)
        {
            /**/
            int count = 3;
            Action xmlFinishCB = () => {

                if (--count == 0)
                {
                    DebugUtils.LogWarning($"{resName} : before creat");
                    onFinishCallback?.Invoke();
                }
            };

            Action jsonFinishCB = () =>
            {

                FguiI18nManager.Instance.Add(I18nLang.cn,
                    "Assets/AstBundle/Consoles/Console Slot 98000000/ABs/I18ns/language_cn_console_01.xml",
                    pkgName,
                    xmlFinishCB);
                FguiI18nManager.Instance.Add(I18nLang.tw,
                    "Assets/AstBundle/Consoles/Console Slot 98000000/ABs/I18ns/language_tw_console_01.xml",
                    pkgName,
                    xmlFinishCB);
                FguiI18nManager.Instance.Add(I18nLang.en,
                    "Assets/AstBundle/Consoles/Console Slot 98000000/ABs/I18ns/language_en_console_01.xml",
                    pkgName,
                    xmlFinishCB);
            };
            

            I18nMgr.Add("Assets/AstBundle/Consoles/Console Slot 98000000/ABs/I18ns/i18nConsole001.json", pkgName, jsonFinishCB);

            // DebugUtils.LogWarning($"{resName} : before creat");
            // onFinishCallback?.Invoke();
        }


        protected override void OnInit()
        {
            
            //this.contentPane = UIPackage.CreateObject("ConsoleSlot98000000", "PageConsoleMain").asCom;
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
            callback();
        }


        public override void OnTop()
        {
            DebugUtils.Log($"i am top ConsoleMainPage {this.name}");
        }

        public override void OnOpen(PageName name, InParamsBase data)
        {
            base.OnOpen(name, data);

            CommonPopupHandler.Instance.ClosePopup();

            InitParam();
            OnChenkUser();
        }


        GList glstMenu;

        GButton btnGameInfo, btnBusinessRecord, btnGameHistory, btnLogRecord,
                btnSettings, btnVolumeSetting, btnHardwareTest, btnTouchCallbrate,
                btnTimeAndDate, btnLanguage, btnAdmin, btnExit;

        GObject goMaskDontClick;

        int permissions = -1; //1：普通密码权限，2：管理员密码权限，3：超级管理员密码权限


        //GButton btnSettingsCache, btnAdminCache;





        

        public override void InitParam()
        {

            if (!isInit) return;

            if (!isOpen) return;

            goMaskDontClick = this.contentPane.GetChild("mask");

            glstMenu = this.contentPane.GetChild("menu").asList;



            GButton _btnSettings = glstMenu.GetChild("settings")?.asButton ?? null;
            if (_btnSettings != null)
            {
                glstMenu.RemoveChild(_btnSettings);

                if (btnSettings != null && _btnSettings != btnSettings)
                {
                    btnSettings.Dispose();
                }
                btnSettings = _btnSettings;
                btnSettings.onClick.Clear();
                btnSettings.onClick.Add(OnClickSettings);
            }


            GButton _btnAdmin = glstMenu.GetChild("admin")?.asButton ?? null;
            if (_btnAdmin != null)
            {
                glstMenu.RemoveChild(_btnAdmin);

                if (btnAdmin != null && _btnAdmin != btnAdmin)
                {
                    btnAdmin.Dispose();
                }
                btnAdmin = _btnAdmin;
                btnAdmin.onClick.Set(OnClickAdmin);
            }



            btnGameInfo = glstMenu.GetChild("gameInfo").asButton;
            btnGameInfo.onClick.Clear();
            btnGameInfo.onClick.Add(OnClickGameInfo);

            btnBusinessRecord = glstMenu.GetChild("businessRecord").asButton;
            btnBusinessRecord.onClick.Clear();
            btnBusinessRecord.onClick.Add(OnClickBusinessRecord);

            btnGameHistory = glstMenu.GetChild("gameHistory").asButton;
            btnGameHistory.onClick.Clear();
            btnGameHistory.onClick.Add(OnClickGameHistory);


            btnLogRecord = glstMenu.GetChild("logRecord").asButton;
            btnLogRecord.onClick.Clear();
            btnLogRecord.onClick.Add(OnClickLogRecord);

            btnTimeAndDate = glstMenu.GetChild("date").asButton;
            btnTimeAndDate.onClick.Clear();
            btnTimeAndDate.onClick.Add(OnClickTimeDate);

            btnVolumeSetting = glstMenu.GetChild("sound").asButton;
            btnVolumeSetting.onClick.Clear();
            btnVolumeSetting.onClick.Add(OnClickSound);


            btnLanguage = glstMenu.GetChild("language").asButton;
            btnLanguage.onClick.Clear();
            btnLanguage.onClick.Add(OnClickLanguage);
            SetLanguageIcon();



            btnHardwareTest = glstMenu.GetChild("hardware").asButton;
            btnHardwareTest.onClick.Clear();
            btnHardwareTest.onClick.Add(OnClickHardwareTest);



            btnTouchCallbrate = glstMenu.GetChild("touch").asButton;
            btnTouchCallbrate.onClick.Clear();
            btnTouchCallbrate.onClick.Add(OnClickTouchCallbrate);







            btnExit = glstMenu.GetChild("exit").asButton;
            btnExit.onClick.Clear();
            btnExit.onClick.Add(OnClickExit);
        }


        async void OnChenkUser()
        {
            goMaskDontClick.visible = true;

            OutParamsBase res = await PageManager.Instance.OpenPageAsync(PageName.ConsoleSlot98000000PopupConsoleKeyboard001,
                /*new EventData<Dictionary<string, object>>("",
                    new Dictionary<string, object>()
                    {
                        ["title"] = I18nMgr.T("Enter Password"),
                        ["isPlaintext"] = false,
                    })*/
                new InParamsPopupConsoleKeyboard001()
                {
                    title = I18nMgr.T("Enter Password"),
                    isPlaintext = false,
                }
             );

            permissions = -1;

            if (res != null && res.code == 0)
            {
                var result = res as OutParamsPopupConsoleKeyboard001;
                string pwdStr = result.value;
                DebugUtils.Log($"键盘输入结果 ：{pwdStr}");

                try
                {
                    int pwd = int.Parse(pwdStr); //这里有可能失败

                    MachineDataManager02.Instance.RequestCheckPassword(pwd,
                    (res) =>
                    {

                        SBoxPermissionsData data = res as SBoxPermissionsData;
                        if (data.result == 0 && data.permissions > 0)
                        {
                            goMaskDontClick.visible = false;

                            permissions = data.permissions;//1：普通密码权限，2：管理员密码权限，3：超级管理员密码权限

                            //btnSettings.visible = permissions >= 2;
                            //btnAdmin.visible =  permissions == 3;

                            /*
                            if (permissions >= 2)
                            {
                                glstMenu.AddChildAt(btnSettings, glstMenu.numChildren - 1);
                            }
                            if (permissions == 3)
                            {
                                glstMenu.AddChildAt(btnAdmin, glstMenu.numChildren - 1);
                            }*/

                            //glstMenu.RefreshVirtualList();  这有问题



                            SBoxModel.Instance.curPermissions = permissions;

                            CheckPermissions();

                            if (SBoxModel.Instance.isCurPermissionsAdmin)
                                SBoxModel.Instance.passwordAdmin = pwd;


                            /*
                                            case UserType.Admin:
                                SBoxModel.Instance.passwordAdmin = pwd;
                                return;
                            case UserType.Manager:
                                SBoxModel.Instance.passwordManager = pwd;
                                return;
                            case UserType.Shift:
                                SBoxModel.Instance.passwordShift = pwd;

                            */


                            DebugUtils.Log($"当前用户权限{SBoxModel.Instance.curPermissions}; 密码: {pwd}");
                        }
                        else
                        {
                            OnCheckUserError();
                        }

                    }, (error) =>
                    {
                        OnCheckUserError();
                    });
                }
                catch
                {
                    OnCheckUserError();
                }
            }
            else
            {
                OnClickExit();
            }
        }



        void SetLanguageIcon()
        {
            string url = "ui://Console/icon_lang_cn";
            switch (SBoxModel.Instance.language)
            {
                case "en":
                    url = "ui://Console/icon_lang_en";
                    break;
                case "cn":
                case "tw":
                    url = "ui://Console/icon_lang_cn";
                    break;
            }
            btnLanguage.GetChild("icon2").asLoader.url = url;
        }

        void CheckPermissions()
        {
            if (SBoxModel.Instance.curPermissions >= 2)
            {
                glstMenu.AddChildAt(btnSettings, glstMenu.numChildren - 1);
            }
            if (SBoxModel.Instance.curPermissions == 3)
            {
                glstMenu.AddChildAt(btnAdmin, glstMenu.numChildren - 1);
            }
        }

        void OnCheckUserError()
        {
            OnChenkUser();
            CommonPopupHandler.Instance.OpenPopupSingle(
            new CommonPopupInfo()
            {
                isUseXButton = false,
                buttonAutoClose1 = true,
                buttonAutoClose2 = true,
                type = CommonPopupType.YesNo,
                text = I18nMgr.T("Error Password"),
                buttonText1 = I18nMgr.T("Cancel"),
                callback1 = () =>
                {
                    //DebugUtils.LogError("i am callback1");
                },
                buttonText2 = I18nMgr.T("Confirm"),
                callback2 = () =>
                {
                    //DebugUtils.LogError("i am callback2");
                }
            });
        }

        void OnClickGameInfo() => PageManager.Instance.OpenPage(PageName.ConsoleSlot98000000PageConsoleGameInformation);

        void OnClickBusinessRecord() => PageManager.Instance.OpenPage(PageName.ConsoleSlot98000000PageConsoleBusinessRecord);

        //void OnClickSettings() => PageManager.Instance.OpenPage(PageName.ConsolePageConsoleMachineSettings);
        void OnClickSettings() => PageManager.Instance.OpenPage(PageName.ConsoleSlot98000000PageConsoleSettingsMenu); 
        void OnClickHardwareTest()=> PageManager.Instance.OpenPage(PageName.ConsoleSlot98000000PageConsoleHardwareTest);

        void OnClickTouchCallbrate() => PageManager.Instance.OpenPage(PageName.ConsoleSlot98000000PageDrawLine);

        void OnClickAdmin() => PageManager.Instance.OpenPage(PageName.ConsoleSlot98000000PageConsoleAdmin);
        void OnClickExit()
        {
            SBoxModel.Instance.curPermissions = -1;
            PageManager.Instance.ClosePage(this);
        }

        async void OnClickTimeDate()
        {
            OutParamsBase res = await PageManager.Instance.OpenPageAsync(PageName.ConsoleSlot98000000PopupConsoleCalendar);

            if (res != null && res.code == 0)
            {
                var result = res as OutParamsPopupConsoleCalendar;
                try
                {
                    long timestamp = result.timestamp;
                    string date = result.date;
                    DebugUtils.LogError($"获得时间戳： {timestamp}  对应日期：{date}");
                }
                catch (Exception ex)
                {
                }
            }
        }


        void OnClickSound()=> PageManager.Instance.OpenPage(PageName.ConsoleSlot98000000PopupConsoleSound);

        void OnClickLogRecord() => PageManager.Instance.OpenPage(PageName.ConsoleSlot98000000PageConsoleLogRecord); 

        void OnClickGameHistory() => PageManager.Instance.OpenPage(PageName.ConsoleSlot98000000PageConsoleGameHistory); 


        async void OnClickLanguage()
        {

            Dictionary<string, string> selectLst = new Dictionary<string, string>();
            foreach(TableSupportLanguageItem item in SBoxModel.Instance.supportLanguage)
            {
                selectLst.Add(item.number, item.name);
            }

            Func<string, string> getSelectedDes = (number) =>
                    {
                        if (selectLst.ContainsKey(number))
                            return string.Format(I18nMgr.T("Selected language: {0}"), I18nMgr.T(selectLst[number]));  
                        return number;
                    };

            OutParamsBase res = await PageManager.Instance.OpenPageAsync(PageName.ConsoleSlot98000000PopupConsoleChoose001,
               /* new EventData<Dictionary<string,object>>("",
                new Dictionary<string, object>()
                {
                    ["title"] = I18nMgr.T("Choose Language"),
                    ["selectLst"] = selectLst,
                    ["selectNumber"] = SBoxModel.Instance.language,
                    ["getSelectedDes"] = getSelectedDes,
                })*/
                new InParamsPopupConsoleChoose001()
                {
                    title = I18nMgr.T("Choose Language"),
                    selectLst = selectLst,
                    selectNumber = SBoxModel.Instance.language,
                    getSelectedDes = getSelectedDes,
                }
            );

            if (res != null && res.code == 0)
            {
                var result = res as OutParamsPopupConsoleChoose001;
                try
                {
                    string selectNumber = result.number;

                    if (SBoxModel.Instance.language == selectNumber)
                        return;

                    //关闭所有弹窗
                    CommonPopupHandler.Instance.CloseAllPopup();

                    SBoxModel.Instance.language = selectNumber; 
                    MachineDeviceCommonBiz.Instance.CheckLanguage();


                    MaskPopupHandler.Instance.OpenPopup();
                    Timers.inst.Add(2, 1, (data) =>
                    {

                        CheckPermissions();
                        SetLanguageIcon();
                        goMaskDontClick.visible = false;

                        MaskPopupHandler.Instance.ClosePopup();
                    });

                }
                catch (Exception ex)
                {
                }
            }
            
        }

    }
}