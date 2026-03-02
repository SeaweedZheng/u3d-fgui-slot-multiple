using GameMaker;
using Common99000000;
public class CommonPopupSystemHelper01 : IComomonPopupHandler
{
    PopupSystemCommon popup;
    public void OpenPopup(CommonPopupInfo info) {
        PageManager.Instance.OpenPage(PageName.CommonPopupSystemCommon, 
            new EventData<CommonPopupInfo>("Null", info) ,
            (PageBase win) => {
                popup = win as PopupSystemCommon;
            }
        );
    }
    public void SetContent(CommonPopupInfo info) {
        if (popup == null)
        {
            DebugUtils.LogError("popup is null");
            return;
        }
        popup.SetContent(new EventData<CommonPopupInfo>("Null", info));
    }
    public void ClosePopup() {

        if(popup != null)
            PageManager.Instance.ClosePage(popup,null);
        popup = null;
    }

    public bool IsOpen()
    {
        return popup != null && popup.isShowing;
    }

    /*
    public bool isTop
    {
        get
        {
            return popup != null && popup.isTop;
        }
    }
    */
    public void SetPopupToTop()
    {
        popup.SetPopupToTop();
    }
}
