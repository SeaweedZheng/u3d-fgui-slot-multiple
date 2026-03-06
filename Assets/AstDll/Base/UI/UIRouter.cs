using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIRouter
{
    private static UIRouter _instance;
    public static UIRouter Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIRouter();
            }
            return _instance;
        }
    }


    public Dictionary<PageName, object[]> pathDict;

    public UIRouter()
    {

        pathDict = new Dictionary<PageName, object[]>()
        {
            // 通用
            [PageName.CommonPopupSystemTip] = new object[] { "Assets/AstBundle/_Commons/Common 99000000/FGUIs", "Common99000000.PopupSystemTip" },  //Common 99000000
            [PageName.CommonPopupSystemLoading] = new object[] { "Assets/AstBundle/_Commons/Common 99000000/FGUIs", "Common99000000.PopupSystemLoading" },  //Common 99000000
            [PageName.CommonPopupSystemToast] = new object[] { "Assets/AstBundle/_Commons/Common 99000000/FGUIs", "Common99000000.PopupSystemToast" },  //Common 99000000
            [PageName.CommonPopupSystemCommon] = new object[] { "Assets/AstBundle/_Commons/Common 99000000/FGUIs", "Common99000000.PopupSystemCommon" },  //Common 99000000
            [PageName.CommonPopupSystemMask] = new object[] { "Assets/AstBundle/_Commons/Common 99000000/FGUIs", "Common99000000.PopupSystemMask" },  //Common 99000000



            // [Console Slot]拉霸机后台
            [PageName.ConsoleSlot98000000PageConsoleMain] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleMain" },
            [PageName.ConsoleSlot98000000PageConsoleMachineSettings] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleMachineSettings" },
            [PageName.ConsoleSlot98000000PopupI18nTest] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PopupI18nTest" },
            [PageName.ConsoleSlot98000000PageConsoleBusinessRecord] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleBusinessRecord" },
            [PageName.ConsoleSlot98000000PageConsoleGameInformation] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleGameInformation" },
            [PageName.ConsoleSlot98000000PopupConsoleKeyboard001] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PopupConsoleKeyboard001" },
            [PageName.ConsoleSlot98000000PopupConsoleKeyboard002] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PopupConsoleKeyboard002" },
            [PageName.ConsoleSlot98000000PopupConsoleTip] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PopupConsoleTip" },
            [PageName.ConsoleSlot98000000PopupConsoleCommon] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PopupConsoleCommon" },
            [PageName.ConsoleSlot98000000PopupConsoleSetParameter002] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PopupConsoleSetParameter002" },
            [PageName.ConsoleSlot98000000PopupConsoleSetParameter001] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PopupConsoleSetParameter001" },
            [PageName.ConsoleSlot98000000PopupConsoleCoder] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PopupConsoleCoder" },
            [PageName.ConsoleSlot98000000PopupConsoleMask] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PopupConsoleMask" },
            [PageName.ConsoleSlot98000000PopupConsoleSlideSetting] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PopupConsoleSlideSetting" },
            [PageName.ConsoleSlot98000000PageDrawLine] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleDrawLine" },
            [PageName.ConsoleSlot98000000PopupConsoleCalendar] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PopupConsoleCalendar" },
            [PageName.ConsoleSlot98000000PopupConsoleSound] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PopupConsoleSound" },
            [PageName.ConsoleSlot98000000PopupConsoleChoose001] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PopupConsoleChoose001" },
            [PageName.ConsoleSlot98000000PageConsoleLogRecord] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleLogRecord" },
            [PageName.ConsoleSlot98000000PageConsoleHardwareTest] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleHardwareTest" },
            [PageName.ConsoleSlot98000000PageConsoleCheckScreenColor] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleCheckScreenColor" },
            [PageName.ConsoleSlot98000000PageConsoleAdmin] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleAdmin" },
            [PageName.ConsoleSlot98000000PopupConsoleNotice] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PopupConsoleNotice" },
            [PageName.ConsoleSlot98000000PageConsoleGameHistory] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleGameHistory" },
            [PageName.ConsoleSlot98000000PageConsoleSettingsMenu] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleSettingsMenu" },
            [PageName.ConsoleSlot98000000PageConsoleSettingsMachine] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleSettingsMachine" },
            [PageName.ConsoleSlot98000000PageConsoleSettingsInOut] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleSettingsInOut" },
            [PageName.ConsoleSlot98000000PageConsoleSettingsRecord] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleSettingsRecord" },
            [PageName.ConsoleSlot98000000PageConsoleSettingsPrinter] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleSettingsPrinter" },
            [PageName.ConsoleSlot98000000PageConsoleSettingsBillValidator] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleSettingsBillValidator" },
            [PageName.ConsoleSlot98000000PageConsoleSettingsRemoteControl] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleSettingsRemoteControl" },
            [PageName.ConsoleSlot98000000PageConsoleSettingsScreen] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleSettingsScreen" },
            [PageName.ConsoleSlot98000000PageConsoleSettingsIOT] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleSettingsIOT" },
            [PageName.ConsoleSlot98000000PageConsoleSettingsGames] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PageConsoleSettingsGames" },
            [PageName.ConsoleSlot98000000PopupConsoleCommon002] = new object[] { "Assets/AstBundle/Consoles/Console Slot 98000000/FGUIs", "ConsoleSlot98000000.PopupConsoleCommon002" },


            // [Console Coin Pusher 1080x1920]推币机新后台
            [PageName.ConsolePusher97000000PageConsoleAdmin] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97000000/FGUIs", "ConsoleCoinPusher97000000.PageConsoleAdmin" },
            [PageName.ConsolePusher97000000PageConsoleMain] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97000000/FGUIs", "ConsoleCoinPusher97000000.PageConsoleMain" },
            [PageName.ConsolePusher97000000PageConsoleCheckHardware] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97000000/FGUIs", "ConsoleCoinPusher97000000.PageConsoleCheckHardware" },
            [PageName.ConsolePusher97000000PageConsoleCoder] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97000000/FGUIs", "ConsoleCoinPusher97000000.PageConsoleCoder" },
            [PageName.ConsolePusher97000000PageConsoleSettings] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97000000/FGUIs", "ConsoleCoinPusher97000000.PageConsoleSettings" },
            [PageName.ConsolePusher97000000PageConsoleTestCoinPush]=new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97000000/FGUIs", "ConsoleCoinPusher97000000.PageConsoleTestCoinPush" },
            [PageName.ConsolePusher97000000PageConsoleSetParameter002] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97000000/FGUIs", "ConsoleCoinPusher97000000.PageConsoleSetParameter002" },
            [PageName.ConsolePusher97000000PageConsoleSetParameter001] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97000000/FGUIs", "ConsoleCoinPusher97000000.PageConsoleSetParameter001" },
            [PageName.ConsolePusher97000000PageConsoleBusinessRecord] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97000000/FGUIs", "ConsoleCoinPusher97000000.PageConsoleBusinessRecord" },
            [PageName.ConsolePusher97000000PageConsoleCheckHardware02] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97000000/FGUIs", "ConsoleCoinPusher97000000.PageConsoleCheckHardware02" },
            [PageName.ConsolePusher97000000PageConsoleRecordChoose] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97000000/FGUIs", "ConsoleCoinPusher97000000.PageConsoleRecordChoose" },
            [PageName.ConsolePusher97000000PageConsoleEventRecord] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97000000/FGUIs", "ConsoleCoinPusher97000000.PageConsoleEventRecord" },
            [PageName.ConsolePusher97000000PageConsoleErrorRecord] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97000000/FGUIs", "ConsoleCoinPusher97000000.PageConsoleErrorRecord" },
            [PageName.ConsolePusher97000000PopupConsoleRecord] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97000000/FGUIs", "ConsoleCoinPusher97000000.PopupConsoleRecord" },
            [PageName.ConsolePusher97000000PageConsoleJpRecord] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97000000/FGUIs", "ConsoleCoinPusher97000000.PageConsoleJpRecord" },
            [PageName.ConsolePusher97000000PageConsoleCoinRecord] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97000000/FGUIs", "ConsoleCoinPusher97000000.PageConsoleCoinRecord" },



            // [Console Coin Pusher 1024x768]推币机新后台
            [PageName.ConsolePusher97001000PageConsoleAdmin] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97001000/FGUIs", "ConsoleCoinPusher97001000.PageConsoleAdmin" },
            [PageName.ConsolePusher97001000PageConsoleMain] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97001000/FGUIs", "ConsoleCoinPusher97001000.PageConsoleMain" },
            [PageName.ConsolePusher97001000PageConsoleCheckHardware] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97001000/FGUIs", "ConsoleCoinPusher97001000.PageConsoleCheckHardware" },
            [PageName.ConsolePusher97001000PageConsoleCoder] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97001000/FGUIs", "ConsoleCoinPusher97001000.PageConsoleCoder" },
            [PageName.ConsolePusher97001000PageConsoleSettings] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97001000/FGUIs", "ConsoleCoinPusher97001000.PageConsoleSettings" },
            [PageName.ConsolePusher97001000PageConsoleTestCoinPush] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97001000/FGUIs", "ConsoleCoinPusher97001000.PageConsoleTestCoinPush" },
            [PageName.ConsolePusher97001000PageConsoleSetParameter002] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97001000/FGUIs", "ConsoleCoinPusher97001000.PageConsoleSetParameter002" },
            [PageName.ConsolePusher97001000PageConsoleSetParameter001] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97001000/FGUIs", "ConsoleCoinPusher97001000.PageConsoleSetParameter001" },
            [PageName.ConsolePusher97001000PageConsoleBusinessRecord] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97001000/FGUIs", "ConsoleCoinPusher97001000.PageConsoleBusinessRecord" },
            [PageName.ConsolePusher97001000PageConsoleCheckHardware02] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97001000/FGUIs", "ConsoleCoinPusher97001000.PageConsoleCheckHardware02" },
            [PageName.ConsolePusher97001000PageConsoleRecordChoose] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97001000/FGUIs", "ConsoleCoinPusher97001000.PageConsoleRecordChoose" },
            [PageName.ConsolePusher97001000PageConsoleEventRecord] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97001000/FGUIs", "ConsoleCoinPusher97001000.PageConsoleEventRecord" },
            [PageName.ConsolePusher97001000PageConsoleErrorRecord] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97001000/FGUIs", "ConsoleCoinPusher97001000.PageConsoleErrorRecord" },
            [PageName.ConsolePusher97001000PopupConsoleRecord] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97001000/FGUIs", "ConsoleCoinPusher97001000.PopupConsoleRecord" },
            [PageName.ConsolePusher97001000PageConsoleJpRecord] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97001000/FGUIs", "ConsoleCoinPusher97001000.PageConsoleJpRecord" },
            [PageName.ConsolePusher97001000PageConsoleCoinRecord] = new object[] { "Assets/AstBundle/Consoles/Console Coin Pusher 97001000/FGUIs", "ConsoleCoinPusher97001000.PageConsoleCoinRecord" },



            // 大厅
            [PageName.Lobby89000000PageLobbyMain] = new object[] { "Assets/AstBundle/Lobbys/Lobby 89000000/FGUIs", "Lobby89000000.PageLobbyMain" },




            // [Coin Pusher]帝国之辉
            [PageName.PusherEmperorsReinPopupGameLoading] = new object[] { "Assets/AstBundle/Games/Coin Pusher Emperors Rein 200/FGUIs", "PusherEmperorsRein.PopupGameLoading" },
            [PageName.PusherEmperorsReinPageGameMain] = new object[] { "Assets/AstBundle/Games/Coin Pusher Emperors Rein 200/FGUIs", "PusherEmperorsRein.PageGameMain" },
            [PageName.PusherEmperorsReinPopupBigWin] = new object[] { "Assets/AstBundle/Games/Coin Pusher Emperors Rein 200/FGUIs", "PusherEmperorsRein.PopupBigWin" },
            [PageName.PusherEmperorsReinPopupFreeSpinTrigger] = new object[] { "Assets/AstBundle/Games/Coin Pusher Emperors Rein 200/FGUIs", "PusherEmperorsRein.PopupFreeSpinTrigger" },
            [PageName.PusherEmperorsReinPopupJackpotGame] = new object[] { "Assets/AstBundle/Games/Coin Pusher Emperors Rein 200/FGUIs", "PusherEmperorsRein.PopupJackpotGame" },

            [PageName.PusherEmperorsReinPopupJackpotOnline] = new object[] { "Assets/AstBundle/Games/Coin Pusher Emperors Rein 200/FGUIs", "PusherEmperorsRein.PopupJackpotOnline" },
            [PageName.PusherEmperorsReinPopupFreeSpinResult] = new object[] { "Assets/AstBundle/Games/Coin Pusher Emperors Rein 200/FGUIs", "PusherEmperorsRein.PopupFreeSpinResult" },
            [PageName.PusherEmperorsReinPageFreeBonusGame2] = new object[] { "Assets/AstBundle/Games/BonusGame2/FGUIs", "PusherEmperorsRein.PageFreeBonusGame2" },

            // [Slot]帝国之辉
            [PageName.SlotEmperorsReinPageERGameMain] = new object[] { "Assets/AstBundle/Games/Coin Pusher Emperors Rein 200/FGUIs", "SlotEmperorsRein.PageGameMainSlot" },




            // 富翁传奇 2001000
            [PageName.RichLegend2001000PageGameMain] = new object[] { "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001000/FGUIs", "CoinPusherRichLegend2001000.PageGameMain" },
            [PageName.RichLegend2001000PageGameLoading] = new object[] { "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001000/FGUIs", "CoinPusherRichLegend2001000.PageGameLoading" },
            [PageName.RichLegend2001000PopupJackpotGame] = new object[] { "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001000/FGUIs", "CoinPusherRichLegend2001000.PopupJackpotGame" },
            [PageName.RichLegend2001000PopupBigWin] = new object[] { "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001000/FGUIs", "CoinPusherRichLegend2001000.PopupBigWin" },

            // 富翁传奇 2001001
            [PageName.RichLegend2001001PageGameMain] = new object[] { "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001001/FGUIs", "CoinPusherRichLegend2001001.PageGameMain" },
            [PageName.RichLegend2001001PageGameLoading] = new object[] { "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001001/FGUIs", "CoinPusherRichLegend2001001.PageGameLoading" },
            [PageName.RichLegend2001001PopupJackpotGame] = new object[] { "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001001/FGUIs", "CoinPusherRichLegend2001001.PopupJackpotGame" },
            [PageName.RichLegend2001001PageGameBonus1] = new object[] { "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001001/FGUIs", "CoinPusherRichLegend2001001.PageGameBonus1" },
            [PageName.RichLegend2001001PageGameBonus2] = new object[] { "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001001/FGUIs", "CoinPusherRichLegend2001001.PageGameBonus2" },
            [PageName.RichLegend2001001PopupBigWin] = new object[] { "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001001/FGUIs", "CoinPusherRichLegend2001001.PopupBigWin" },



            // 富翁传奇 2001003
            [PageName.RichLegend2001003PageGameMain] = new object[] { "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001003/FGUIs", "CoinPusherRichLegend2001003.PageGameMain" },
            [PageName.RichLegend2001003PageGameLoading] = new object[] { "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001003/FGUIs", "CoinPusherRichLegend2001003.PageGameLoading" },
            [PageName.RichLegend2001003PopupJackpotGame] = new object[] { "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001003/FGUIs", "CoinPusherRichLegend2001003.PopupJackpotGame" },
            [PageName.RichLegend2001003PageGameBonus1] = new object[] { "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001003/FGUIs", "CoinPusherRichLegend2001003.PageGameBonus1" },
            [PageName.RichLegend2001003PageGameBonus2] = new object[] { "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001003/FGUIs", "CoinPusherRichLegend2001003.PageGameBonus2" },
            [PageName.RichLegend2001003PopupBigWin] = new object[] { "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001003/FGUIs", "CoinPusherRichLegend2001003.PopupBigWin" },
        };
    }
}


public enum PageName
{

    // 通用
    CommonPopupSystemTip,
    CommonPopupSystemLoading,
    CommonPopupSystemToast,
    CommonPopupSystemCommon,
    CommonPopupSystemMask,

    // 拉霸机-管理后天
    ConsoleSlot98000000PageConsoleMain,
    ConsoleSlot98000000PageConsoleMachineSettings,
    ConsoleSlot98000000PopupI18nTest,
    ConsoleSlot98000000PageConsoleBusinessRecord,
    ConsoleSlot98000000PageConsoleGameInformation,
    ConsoleSlot98000000PopupConsoleKeyboard001,
    ConsoleSlot98000000PopupConsoleKeyboard002,
    ConsoleSlot98000000PopupConsoleTip,
    ConsoleSlot98000000PopupConsoleCommon,
    ConsoleSlot98000000PopupConsoleSetParameter002,
    ConsoleSlot98000000PopupConsoleSetParameter001,
    ConsoleSlot98000000PopupConsoleCoder,
    ConsoleSlot98000000PopupConsoleMask,
    ConsoleSlot98000000PopupConsoleSlideSetting,
    ConsoleSlot98000000PageDrawLine,
    ConsoleSlot98000000PopupConsoleCalendar,
    ConsoleSlot98000000PopupConsoleSound,
    ConsoleSlot98000000PopupConsoleChoose001,
    ConsoleSlot98000000PageConsoleLogRecord,
    ConsoleSlot98000000PageConsoleHardwareTest,
    ConsoleSlot98000000PageConsoleCheckScreenColor,
    ConsoleSlot98000000PageConsoleAdmin,
    ConsoleSlot98000000PopupConsoleNotice,
    ConsoleSlot98000000PageConsoleGameHistory,
    ConsoleSlot98000000PageConsoleSettingsMenu,
    ConsoleSlot98000000PageConsoleSettingsMachine,
    ConsoleSlot98000000PageConsoleSettingsInOut,
    ConsoleSlot98000000PageConsoleSettingsRecord,
    ConsoleSlot98000000PageConsoleSettingsPrinter,
    ConsoleSlot98000000PageConsoleSettingsBillValidator,
    ConsoleSlot98000000PageConsoleSettingsRemoteControl,
    ConsoleSlot98000000PageConsoleSettingsScreen,
    ConsoleSlot98000000PageConsoleSettingsIOT,
    ConsoleSlot98000000PageConsoleSettingsGames,
    ConsoleSlot98000000PopupConsoleCommon002,

    // 推币机新后台 （1080 x 1920）
    ConsolePusher97000000PageConsoleMain,
    ConsolePusher97000000PageConsoleCheckHardware,
    ConsolePusher97000000PageConsoleCoder,
    ConsolePusher97000000PageConsoleSettings,
    ConsolePusher97000000PageConsoleTestCoinPush,
    ConsolePusher97000000PageConsoleSetParameter001,
    ConsolePusher97000000PageConsoleSetParameter002,
    ConsolePusher97000000PageConsoleBusinessRecord,
    ConsolePusher97000000PageConsoleAdmin,
    ConsolePusher97000000PageConsoleCheckHardware02,
    ConsolePusher97000000PageConsoleRecordChoose,
    ConsolePusher97000000PageConsoleEventRecord,
    ConsolePusher97000000PageConsoleErrorRecord,
    ConsolePusher97000000PopupConsoleRecord,
    ConsolePusher97000000PageConsoleJpRecord,
    ConsolePusher97000000PageConsoleCoinRecord,



    // 推币机新后台（1024 x 768）
    ConsolePusher97001000PageConsoleMain,
    ConsolePusher97001000PageConsoleCheckHardware,
    ConsolePusher97001000PageConsoleCoder,
    ConsolePusher97001000PageConsoleSettings,
    ConsolePusher97001000PageConsoleTestCoinPush,
    ConsolePusher97001000PageConsoleSetParameter001,
    ConsolePusher97001000PageConsoleSetParameter002,
    ConsolePusher97001000PageConsoleBusinessRecord,
    ConsolePusher97001000PageConsoleAdmin,
    ConsolePusher97001000PageConsoleCheckHardware02,
    ConsolePusher97001000PageConsoleRecordChoose,
    ConsolePusher97001000PageConsoleEventRecord,
    ConsolePusher97001000PageConsoleErrorRecord,
    ConsolePusher97001000PopupConsoleRecord,
    ConsolePusher97001000PageConsoleJpRecord,
    ConsolePusher97001000PageConsoleCoinRecord,


    // 推币机-帝国之辉
    PusherEmperorsReinPageGameMain,
    PusherEmperorsReinPopupGameLoading,
    PusherEmperorsReinPopupBigWin,
    PusherEmperorsReinPopupFreeSpinTrigger,
    PusherEmperorsReinPopupFreeSpinResult,
    PusherEmperorsReinPopupJackpotGame,

    PusherEmperorsReinPopupJackpotOnline,
    PusherEmperorsReinPageFreeBonusGame2,
    
    // 拉霸机-帝国之辉
    SlotEmperorsReinPageERGameMain,


    //【拉霸机】
    SlotEmperorsReinPageFreeBonusGame1,




    Lobby89000000PageLobbyMain,


    // 富翁传奇2001000
    RichLegend2001000PageGameMain,
    RichLegend2001000PageGameLoading,
    RichLegend2001000PopupJackpotGame,
    RichLegend2001000PopupBigWin,


    // 富翁传奇2001001
    RichLegend2001001PageGameMain,
    RichLegend2001001PageGameLoading,
    RichLegend2001001PopupJackpotGame,
    RichLegend2001001PageGameBonus1,
    RichLegend2001001PageGameBonus2,
    RichLegend2001001PopupBigWin,


    // 富翁传奇2001003
    RichLegend2001003PageGameMain,
    RichLegend2001003PageGameLoading,
    RichLegend2001003PopupJackpotGame,
    RichLegend2001003PageGameBonus1,
    RichLegend2001003PageGameBonus2,
    RichLegend2001003PopupBigWin,
}


