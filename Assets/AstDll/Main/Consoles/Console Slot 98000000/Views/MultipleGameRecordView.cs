using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleGameRecordView : IVMultipleGameRecord
{
    GComponent ui;
    public void InitParam(GComponent u)
    {
        ui = u;
    }
    public event Action<SelectGameRecordFilterInfo, SelectGameRecordPageInfo> onSelectGameRecord; 

    public event Action onClickNext;
    public event Action onClickPrev;

    public void ClearAll()
    {
        selectNumberPage = 1;
        totalGameFilterOptions = null;
    }

    TotalGameFilterOptions totalGameFilterOptions;
    public const int totalCountPerPage = 1;
    public int selectNumberPage = 1;
    public SelectGameRecordPageInfo SetDefaultSelect()
    {
        return new SelectGameRecordPageInfo() {
            totalCountPerPage = totalCountPerPage,
            selectNumberPage = selectNumberPage,
        };
    }


    public void SetTotalGameFilterOptions(TotalGameFilterOptions Filter)
    {
        totalGameFilterOptions = Filter;
    }

    public void SetContent(SelectGameRecordPageResult content)
    {

    }
}
