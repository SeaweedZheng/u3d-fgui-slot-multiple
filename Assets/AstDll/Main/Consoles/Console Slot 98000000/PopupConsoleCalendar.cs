using FairyGUI;
using GameMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SpringGUI;


namespace ConsoleSlot98000000
{
    public  class OutParamsPopupConsoleCalendar: OutParamsBase
    {
        public string date;
        public long timestamp;
    }
    public class PopupConsoleCalendar : PageBase
    {
        public const string pkgName = "ConsoleSlot98000000";
        public const string resName = "PopupConsoleCalendar";
        public override PageType pageType => PageType.Overlay;
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
            callback();
            /*ResourceManager02.Instance.LoadAsset<GameObject>(
            "Assets/AstBundle/Games/Coin Pusher Emperors Rein 200/Prefabs/Game Controller/Push Game Main Controller.prefab",
            (GameObject clone) =>
            {
                callback();
            });*/

        }

        public override void OnOpen(PageName name, InParamsBase data)
        {
            base.OnOpen(name, data);
            InitParam();
        }

        // public override void OnTop() { DebugUtils.Log($"i am top {this.name}"); }

        GButton btnClose, btnSave, btnCancel;



        IVCalendar calendarView = IViewManager.Instance.GetIV<IVCalendar>();
        //IVCalendar calendarView = new CalendarView();
        CalendarController calendarCtrl = new CalendarController();
        public override void InitParam()
        {


            if (!isInit) return;

            if (!isOpen) return;

            btnClose = this.contentPane.GetChild("btnExit").asButton;
            btnClose.onClick.Clear();
            btnClose.onClick.Add(() =>
            {
                //CloseSelf(new EventData("Exit"));
                CloseSelf(null);
            });

            btnCancel = this.contentPane.GetChild("button1").asButton;
            btnCancel.onClick.Clear();
            btnCancel.onClick.Add(() =>
            {
                //CloseSelf(new EventData("Exit"));
                CloseSelf(null);
            });


            btnSave = this.contentPane.GetChild("button2").asButton;
            btnSave.onClick.Clear();
            btnSave.onClick.Add(() =>
            {
                /*
                Dictionary<string, object> result = new Dictionary<string, object>()
                {
                    ["date"] = calendarCtrl.GetLastSelectDate(),
                    ["timestamp"] = calendarCtrl.GetLastSelectTimestamp(),
                };
                CloseSelf(new EventData<Dictionary<string, object>>("Result", result));
                */

                CloseSelf(new OutParamsPopupConsoleCalendar()
                {
                    date = calendarCtrl.GetLastSelectDate(),
                    timestamp = calendarCtrl.GetLastSelectTimestamp(),
                });

            });

            //calendarView.InitParam(this.contentPane); // 注入ui的实例
            //calendarCtrl.InitParam(calendarView);  // 注入iv的实例

            calendarView.InitParam(this.contentPane);
            calendarCtrl.InitParam(calendarView);
            
        }
    }
}


