using GameMaker;
using ConsoleSlot98000000;
using Common99000000;

public class MaskPopupHelper01 : IMaskPopupHandel
{

    PageBase popup;

    public void OpenPopup(string info)
    {
        PageManager.Instance.OpenPage(PageName.CommonPopupSystemMask,    //PageName. ConsoleSlot98000000PopupConsoleMask,
            //new EventData<string>("Null", info),
            new InParamPopupSystemMask()
            {
                message = info,
            },
            (PageBase win) => {
                popup = win as PageBase;
            }
        );
    }
    public void SetContent(string info)
    {
    }
    public void ClosePopup()
    {
        if (popup != null)
        {
            PageManager.Instance.ClosePage(popup, null);
        }
        popup = null;
    }
}
