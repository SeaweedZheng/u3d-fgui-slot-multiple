using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageUtils  
{
    public static void GoBackToLobby()
    {
        int index = PageManager.Instance.IndexOf(PageName.Lobby89000000PageLobbyMain);
        if (index != -1)
        {
            while (true)
            {
                PageBase pg = PageManager.Instance.GetTopPage();

                if (pg.pageName == PageName.Lobby89000000PageLobbyMain)
                {
                    return;
                }
                else
                {
                    PageManager.Instance.ClosePage(pg);
                }
            }
        }
    }

    /// <summary>
    /// 是否存在大厅
    /// </summary>
    public static bool HasLobby => PageManager.Instance.IndexOf(PageName.Lobby89000000PageLobbyMain) != -1;
}
