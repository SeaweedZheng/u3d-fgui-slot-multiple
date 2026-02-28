using FairyGUI;
using GameMaker;


namespace ConsoleSlot98000000
{
    public class PopupI18nTest : PageBase
    {
        public const string pkgName = "ConsoleSlot98000000";
        public const string resName = "PopupI18nTest";

        protected override void OnInit()
        {
            
            base.OnInit();
        }



        public override void OnOpen(PageName name, EventData data)
        {
            base.OnOpen(name, data);
            InitParam();
        }

        GButton btnConsole;
        public override void InitParam()
        {
            btnConsole = this.contentPane.GetChild("buttonConsole").asButton;
            btnConsole.onClick.Clear();
            btnConsole.onClick.Add(() =>
            {
                DebugUtils.Log("i am here");
                PageManager.Instance.OpenPage(PageName.ConsoleSlot98000000PageConsoleMain);
            });



            DebugUtils.Log("111 = " + GRoot.inst.gameObjectName);
            // displayObject.gameObject
            DebugUtils.Log("111 = " + this.contentPane.gameObjectName);
            DebugUtils.Log("111 = " + this.contentPane.displayObject.gameObject);
            // this.contentPane
        }
    }
}