using System;

public interface IVMultipleGameRecord
{
    event Action<SelectGameRecordFilterInfo, SelectGameRecordPageInfo> onSelectGameRecord;  //游戏类型，游戏id,本局类型，中奖类型，开始时间，结束时间

    event Action onClickNext;
    event Action onClickPrev;

    //int totalCountPerPage { get; }
    //int selectNumberPage { get; }

    /// <summary>
    /// 清除所有内容（包括：页尾、内容、日期）
    /// </summary>
    void ClearAll();

    /// <summary>
    /// 设置为默认的游戏内容选择
    /// </summary>
    /// <returns></returns>
    SelectGameRecordPageInfo SetDefaultSelect();

    //void SetToAll
    void SetTotalGameFilterOptions(TotalGameFilterOptions Filter);
    //void SetSelectDateIndex(int index);
    void SetContent(SelectGameRecordPageResult content);
}

public class MultipleGameRecordPresenter
{

    IVMultipleGameRecord _view;


    const string FORMAT_DATE_DAY = "yyyy-MM-dd";

    //TableGameRecordItem curGame;

    TotalGameFilterOptions totalGameRecordFilter;
    SelectGameRecordFilterInfo curFilterInfo;
    SelectGameRecordPageInfo curPageInfo;
    int totalPageCont = 0;

    public void InitParam(IVMultipleGameRecord view)
    {
        if (this._view != null)
        {
            this._view.onSelectGameRecord -= OnSelectGameRecord;
            this._view.onClickPrev -= OnClickPrevPage;
            this._view.onClickNext -= OnClickNextPage;
        }

        this._view = view;
        this._view.onSelectGameRecord += OnSelectGameRecord;
        this._view.onClickPrev += OnClickPrevPage;
        this._view.onClickNext += OnClickNextPage;

        InitView();
    }


    void OnSelectGameRecord(SelectGameRecordFilterInfo select, SelectGameRecordPageInfo pageInfo)
    {
        curFilterInfo = select;
        curPageInfo = pageInfo;
        GetGameRecord(select, pageInfo);
    }

    void GetGameRecord(SelectGameRecordFilterInfo select, SelectGameRecordPageInfo pageInfo)
    {
        GameRecordFilterManager.Instance.GetGameRecord(select, pageInfo, (result) =>
        {
            totalPageCont = result.totalPageCount;
            _view.SetContent(result);
        });
    }


    void InitView()
    {
        GameRecordFilterManager.Instance.GetAllFilterOptions((res) =>
        {
            totalGameRecordFilter = res;

            _view.SetTotalGameFilterOptions(totalGameRecordFilter);
            _view.ClearAll();

            curPageInfo = _view.SetDefaultSelect();
            curFilterInfo = null;
            GetGameRecord(curFilterInfo, curPageInfo);
        });
    }


    TotalGameFilterOptions totalGameFilterOptions;


    private void OnClickNextPage()
    {
        if (curPageInfo.selectNumberPage + 1 > totalPageCont)
            return;

        curPageInfo.selectNumberPage++;


        GetGameRecord(curFilterInfo, curPageInfo);
    }

    private void OnClickPrevPage()
    {
        if (curPageInfo.selectNumberPage <= 1)
            return;

        curPageInfo.selectNumberPage--;

        GetGameRecord(curFilterInfo, curPageInfo);
    }
}

