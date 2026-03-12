using FairyGUI;
using GameMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PusherEmperorsRein
{

    public class InParamsPopupNextWin : InParamsBase
    {
        public string title;
        public bool isPlaintext;
        public string content;
    }

    public class OutParamsPopupNextWin : OutParamsBase
    {
        public int value;
    }

    public class PopupNextWin : PageBase
    {
        public const string pkgName = "EmperorsRein";
        public const string resName = "PopupNextWin";

        protected override void OnInit()
        {
            
            base.OnInit();
        }



        public override void OnOpen(PageName name, InParamsBase data)
        {
            base.OnOpen(name, data);
            InitParam();
        }

        // public override void OnTop() { DebugUtils.Log($"i am top {this.name}"); }

        /// <summary> ̧ͷ </summary>
        string title = "";

        /// <summary> �Ƿ����� </summary>
        bool isPlaintext = false;

        string inputText = "";

        public override void InitParam()
        {
   
            if (inParams != null)
            {
                var inp = inParams as InParamsPopupNextWin;
                title = inp.title;
                isPlaintext = inp.isPlaintext;
                if(!string.IsNullOrEmpty(inp.content))
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
        }

        void End(int value)
        {
            //PageManager.Instance.ClosePage(this, new EventData<string>("Result", value));
            //CloseSelf(new EventData<int>("Result", value));

            CloseSelf(new OutParamsPopupNextWin()
            {
                value = value
            });
        }
    }
}