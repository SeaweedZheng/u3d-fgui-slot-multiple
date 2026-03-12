using GameMaker;
using Common99000000;
public class TipPopupHelper01 : ITipPopupHandel
{
    PopupSystemToast popup;
    public void OpenPopup(string msg)
    {

        int index = PageManager.Instance.IndexOf(PageName.CommonPopupSystemToast);
        if (index  == -1)
        {
            PageManager.Instance.OpenPage(PageName.CommonPopupSystemToast,
                //new EventData<string>("Null", msg),
                new InParamsPopupSystemToast() {  message = msg },
                (PageBase win) => {
                    popup = win as PopupSystemToast;
                }
            );
            return;
        }
        if(index != 0)
            popup.BringToFront();

        popup.ShowTip(new InParamsPopupSystemToast()
        {
            message = msg
        });
    }
    public void OpenPopupOnce(string msg)
    {
        int index = PageManager.Instance.IndexOf(PageName.CommonPopupSystemToast);
        if (index == -1)
        {
            PageManager.Instance.OpenPage(PageName.CommonPopupSystemToast,
                //new EventData<string>("Null", msg),
                new InParamsPopupSystemToast() { message = msg },
                (PageBase win) => {
                    popup = win as PopupSystemToast;
                }
            );
            return;
        }

        if (index != 0)
            popup.BringToFront();

        if (!popup.Contains(msg))
            popup.ShowTip(new InParamsPopupSystemToast() { message = msg });
    }
    public void ClosePopup()
    {
        PageManager.Instance.ClosePage(PageName.CommonPopupSystemToast);
    }
}
