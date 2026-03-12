using FairyGUI;
using GameMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



namespace ConsoleSlot98000000
{

    public class InParamsPopupConsoleSearch: InParamsBase
    {
        /// <summary> 弹窗抬头 </summary>
        public string title = "";

        /// <summary> 选项集合 </summary>
        public List<InParamItemSelectOption> options = new List<InParamItemSelectOption>();
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

        public override void OnOpen(PageName name, InParamsBase data)
        {
            base.OnOpen(name, data);

            // 添加事件监听

            InitParam();

            new InParamsPopupConsoleSearch()
            {
                title = "",
                options = new List<InParamItemSelectOption>()
                {
                    new InParamItemSelectOption()
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


        public override void OnClose(OutParamsBase data = null)
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

                var inp = inParams as InParamsPopupConsoleSearch;


                /*
                Dictionary<string, object> argDic = null;
                argDic = (Dictionary<string, object>)inParams.value;
                //title = (string)argDic["title"];
                //isPlaintext = (bool)argDic["isPlaintext"];
                if (argDic.ContainsKey("content"))
                {
                    //input = (string)argDic["content"];
                }
                */
            }
         
        }

        public void OnPreLoaded()
        {

        }
    }
}