using GameMaker;
using ConsoleSlot98000000;
public class TipPopupHelper01 : ITipPopupHandel
{
    PopupConsoleTip popup;
    public void OpenPopup(string msg)
    {

        int index = PageManager.Instance.IndexOf(PageName.ConsoleSlot98000000PopupConsoleTip);
        if (index  == -1)
        {
            PageManager.Instance.OpenPage(PageName.ConsoleSlot98000000PopupConsoleTip,
                new EventData<string>("Null", msg),
                (PageBase win) => {
                    popup = win as PopupConsoleTip;
                }
            );
            return;
        }
        if(index != 0)
            popup.BringToFront();
        popup.ShowTip(new EventData<string>("Null", msg));
    }
    public void OpenPopupOnce(string msg)
    {
        int index = PageManager.Instance.IndexOf(PageName.ConsoleSlot98000000PopupConsoleTip);
        if (index == -1)
        {
            PageManager.Instance.OpenPage(PageName.ConsoleSlot98000000PopupConsoleTip,
                new EventData<string>("Null", msg),
                (PageBase win) => {
                    popup = win as PopupConsoleTip;
                }
            );
            return;
        }

        if (index != 0)
            popup.BringToFront();

        if (!popup.Contains(msg))
            popup.ShowTip(new EventData<string>("Null", msg));
    }
    public void ClosePopup()
    {
        PageManager.Instance.ClosePage(PageName.ConsoleSlot98000000PopupConsoleTip);
    }
}
