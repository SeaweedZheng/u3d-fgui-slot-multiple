using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageUtils  
{
    public static void GoBackToLobby()
    {
        int index = PageManager.Instance.IndexOf(PageName.Lobby01PageLobbyMain);
        if (index != -1)
        {
            while (true)
            {
                PageBase pg = PageManager.Instance.GetTopPage();

                if (pg.pageName == PageName.Lobby01PageLobbyMain)
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
}
