using CY.GameFramework;
using FairyGUI;
using GameMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConsoleSlot98000000
{
    public class InParamsPopupConsoleKeyboard001 : InParamsBase
    {
        /*
        public InParamsPopupConsoleKeyboard001(string title, bool isPlaintext=false, string content="")
        {
            this.title = title;
            this.isPlaintext = isPlaintext;
            this.content = content;
        }*/
        /// <summary> 抬头 </summary>
        public string title = "";
        /// <summary> 是否明文 </summary>
        public bool isPlaintext;
        /// <summary> 显示内容 </summary>
        public string content = "";
    }

    public class OutParamsPopupConsoleKeyboard001 : OutParamsBase
    {
        public string value;
    }
    public class PopupConsoleKeyboard001 : PageBase
    {
        public const string pkgName = "ConsoleSlot98000000";
        public const string resName = "PopupConsoleKeyboard001";
        public override PageType pageType => PageType.Overlay;
        protected override void OnInit()
        {
            
            base.OnInit();
        }




        public override void OnOpen(PageName name, InParamsBase data)
        {
            //info = data;
            base.OnOpen(name, data);
            InitParam();
        }

        // public override void OnTop() {  DebugUtils.Log($"i am top {this.name}"); }



        //EventData info;

        GButton btnClose, btnInput;

        GTextField txtTitle, txtInput;

        GComponent cmpKeyboardRoot;

        CompInputController compInputCtr = new CompInputController();
        CompKeyboardController compKBCtrl = new CompKeyboardController();

        /// <summary> 抬头 </summary>
        string title = "";
        /// <summary> 是否明文 </summary>
        bool isPlaintext = false;
        string inputText = "";
        public override void InitParam()
        {

            btnClose = this.contentPane.GetChild("btnExit").asButton;
            btnClose.onClick.Clear();
            btnClose.onClick.Add(() =>
            {
                //CloseSelf(null);
                //PageManager.Instance.ClosePage(this, new EventData("Exit"));
                CloseSelf(null);//CloseSelf(new EventData("Exit"));
            });

            txtTitle = this.contentPane.GetChild("title").asTextField;
            txtInput = this.contentPane.GetChild("input").asCom.GetChild("title").asTextField;
            btnInput = this.contentPane.GetChild("input").asButton;


            cmpKeyboardRoot = this.contentPane.GetChild("keyboard").asCom;





            if (inParams != null)
            {
                var inp = inParams as InParamsPopupConsoleKeyboard001;
                title = inp.title;
                isPlaintext = inp.isPlaintext;
                inputText = inp.content;
                /*
                Dictionary<string, object> argDic = (Dictionary<string, object>)inParams.value;
                title = (string)argDic["title"];

                if (argDic.ContainsKey("isPlaintext"))
                {
                    isPlaintext = (bool)argDic["isPlaintext"];
                }
                if (argDic.ContainsKey("content"))
                {
                    inputText = (string)argDic["content"];
                }
                */
            }

            txtTitle.text = title;

            compKBCtrl.Init(cmpKeyboardRoot);
            compInputCtr.Init(btnInput, txtInput, OnClickButtonOk, null, inputText, isPlaintext);
            compInputCtr.GetFocus();
        }


        void OnClickButtonOk(string value)
        {
            //CloseSelf(new EventData<string>("Result", value));

            CloseSelf(new OutParamsPopupConsoleKeyboard001()
            {
                value = value
            });
        }

    }
}