using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMaker;

namespace Common99000000
{
    public class InParamPopupSystemMask: InParamsBase
    {
        public string message;
    }
    public class PopupSystemMask : PageBase
    {
        public const string pkgName = "Common99000000";
        public const string resName = "PopupSystemMask";
        public override PageType pageType => PageType.Overlay;
        protected override void OnInit()
        {

            base.OnInit();
        }


        public override void OnOpen(PageName name, InParamsBase data)
        {
            base.OnOpen(name, data);
            InitParam();
        }

        public override void InitParam()
        {
        }
    }
}