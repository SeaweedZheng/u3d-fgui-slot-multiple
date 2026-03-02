using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMaker;

namespace Common99000000
{
    public class PopupSystemMask : PageBase
    {
        public const string pkgName = "Common99000000";
        public const string resName = "PopupSystemMask";
        public override PageType pageType => PageType.Overlay;
        protected override void OnInit()
        {

            base.OnInit();
        }


        public override void OnOpen(PageName name, EventData data)
        {
            base.OnOpen(name, data);
            InitParam();
        }

        public override void InitParam()
        {
        }
    }
}