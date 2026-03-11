using FairyGUI;
using GameMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



namespace ConsoleSlot98000000
{
    public class InParamsPopupConsoleSearch: InPrarmsBase
    {
        public class InParamsSelectOption
        {
            /// <summary> 选项类型 </summary>
            public string selectType;

            /// <summary> 选项名称</summary>
            public string selectName;

            /// <summary>
            /// 选项列表
            /// </summary>
            /// <remarks>
            /// * selectContentKey - slelectContentName
            /// </remarks>
            public Dictionary<string,string> selectContent = new Dictionary<string,string>();
        }

        /// <summary> 弹窗抬头 </summary>
        public string title = "";

        /// <summary> 选项集合 </summary>
        public List<InParamsSelectOption> options = new List<InParamsSelectOption>();
    }

    public class OutParamsPopupConsoleSearch: OutParamsBase
    {
        /// <summary>
        /// 选择结果
        /// </summary>
        /// <remarks>
        /// * selectType - selectContentKey
        /// </remarks>
        public Dictionary<string,string> selectResult = new Dictionary<string,string>();
    }



    public class PopupConsoleSearch : PageBase
    {
        public const string pkgName = "ConsoleSlot98000000";
        public const string resName = "PopupConsoleSearch";

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
        }

        public override void OnOpen(PageName name, EventData data)
        {
            base.OnOpen(name, data);

            // 添加事件监听

            InitParam();

            new InParamsPopupConsoleSearch()
            {
                title = "",
                options = new List<InParamsPopupConsoleSearch.InParamsSelectOption>()
                {
                    new InParamsPopupConsoleSearch.InParamsSelectOption()
                    {
                        selectType = "select",
                        selectContent = new Dictionary<string, string>()
                        {
                            //[]=
                        }
                    }
                }
            };
        }


        public override void OnClose(EventData data = null)
        {

            // 删除事件监听

            base.OnClose(data);
        }


        // public override void OnTop() { DebugUtils.Log($"i am top {this.name}"); }

        GButton btnClose;





        public override void InitParam()
        {

            if (!isInit) return;

            OnPreLoaded(); // 其他卡顿的资源实例化

            preLoadedCallback?.Invoke();
            preLoadedCallback?.RemoveAllListeners();

            if (!isOpen) return;

            // btnClose =  this.contentPane.GetChild("btnExit").asButton;
            btnClose = this.contentPane.GetChild("navBottom").asCom.GetChild("btnExit").asButton;
            btnClose.onClick.Clear();
            btnClose.onClick.Add(() =>
            {
                //DebugUtils.Log("i am here 123");
                CloseSelf(null);
                //  CloseSelf(new EventData("Exit"));
            });


            if (inParams != null)
            {   
                Dictionary<string, object> argDic = null;
                argDic = (Dictionary<string, object>)inParams.value;
                //title = (string)argDic["title"];
                //isPlaintext = (bool)argDic["isPlaintext"];
                if (argDic.ContainsKey("content"))
                {
                    //input = (string)argDic["content"];
                }
            }
         
        }

        public void OnPreLoaded()
        {

        }
    }
}