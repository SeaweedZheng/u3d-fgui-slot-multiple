using GameMaker;
using SlotMaker;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using System;
using Newtonsoft.Json;

public partial class SlotMachinePresenter 
{

    public void SkipWinLine(bool isIncludeTag)
    {

        // 打开基础图标
        for (int cIdx = 0; cIdx < smConfig.Column; cIdx++)
        {
            for (int rIdx = 0; rIdx <= smConfig.Row; rIdx++)
            {
                view.RemoveSymbolTwinkleEffect(cIdx, rIdx);
                view.RemoveSymbolBiggerEffect(cIdx, rIdx);
                view.RemoveBorderEffect(cIdx, rIdx);
                view.HideBaseSymbolIcon(cIdx, rIdx, false);
            }
        }

        // 去除层级功能
        view.ReturnSortingOrder();
        //FguiSortingOrderManager.Instance.ReturnSortingOrder(maskSortOrder);
        //FguiSortingOrderManager.Instance.ReturnAllSortingOrder();

        string[] exclude = isIncludeTag ? new string[] { } : new string[] { };
        for (int cIdx = 0; cIdx < smConfig.Column; cIdx++)
        {
            for (int rIdx = 0; rIdx <= smConfig.Row; rIdx++)
            {
                view.ReturnSymbolEffectToPool(cIdx, rIdx, exclude);
            }
        }

        view.CloseAllPlayLines();

        OnWinEvent(SlotMachineEvent.ON_WIN_EVENT, new EventData(SlotMachineEvent.SkipWinLine));
    }

    public IEnumerator ShowSymbolWinBySetting(SymbolWin symbolWin, bool isUseMySelfSymbolNumber, SpinWinEvent eventType)
    {

        //停止特效显示
        SkipWinLine(false);


        // 立马停止时，不播放赢分环节？
        if (isStopImmediately && smConfig.IsWESkipAtStopImmediately)
            yield break;

        //显示遮罩
        SetSlotCover(smConfig.IsWEShowCover);

        Dictionary<string, string> symbolHitEffect = smConfig.GetSymbolHitEffect();

        foreach (Cell cel in symbolWin.cells)
        {

            int symbolNumberSelf = view.GetVisibleSymbolNumberFromDeck(cel.columnIndex, cel.rowIndex);

            int symbolNumber = isUseMySelfSymbolNumber ? symbolNumberSelf : symbolWin.symbolNumber;

            string symbolName = symbolHitEffect[$"{symbolNumber}"];  // wild  or symbol;


            /*
            // 【替换】图标动画  
            GComponent goSymbolHit = fguiPoolHelper.GetObject(TagPoolObject.SymbolHit, symbolName).asCom;
            GComponent goSymbol = GetVisibleSymbolFromDeck(cel.column, cel.row);
            AddSymbolEffect(goSymbol, goSymbolHit, IsWESymbolAnim);

            int rowIndex = cel.row;
            // 设置层级
            FguiSortingOrderManager.Instance.ChangeSortingOrder(goSymbol, goExpectation, maskSortOrder, null,
                (self) => rowIndex + DeckUpStartIndex);

            */

            // 图标动画
            view.ShowSymbolHitEffect(cel.columnIndex, cel.rowIndex, symbolName, smConfig.IsWESymbolAnim);



            // 边框
            if (smConfig.IsWEFrame)
            {
                view.ShowBorderEffect(cel.columnIndex, cel.rowIndex);
            }

            // 整体变大特效
            if (smConfig.IsWETwinkle)
                view.ShowTwinkleEffect(cel.columnIndex, cel.rowIndex);
            else if (smConfig.IsWEBigger)
                view.ShowBiggerEffect(cel.columnIndex, cel.rowIndex);

        }


        // 是否显示线
        if (smConfig.IsWEShowLine)
        {
            view.ShowPayLines(symbolWin);
        }


        // 事件
        if (eventType == SpinWinEvent.TotalWinLine)
        {
            OnWinEvent(SlotMachineEvent.ON_WIN_EVENT, new EventData<SymbolWin>(SlotMachineEvent.TotalWinLine, symbolWin));
        }
        else if (eventType == SpinWinEvent.SingleWinLine)
        {
            OnWinEvent(SlotMachineEvent.ON_WIN_EVENT, new EventData<SymbolWin>(SlotMachineEvent.SingleWinLine, symbolWin));
        }

        yield return SlotWaitForSeconds(smConfig.WETimeS);
    }



    public IEnumerator ShowWinListBySetting(List<SymbolWin> winList)
    {

        // 立马停止时，不播放赢分环节？
        if (isStopImmediately && smConfig.IsWESkipAtStopImmediately)
            yield break;

        if (smConfig.IsWETotalWinLine)
        {
            yield return ShowSymbolWinBySetting(GetTotalSymbolWin(winList), true, SpinWinEvent.TotalWinLine);
        }
        else
        {
            int idx = 0;
            while (idx < winList.Count)
            {
                yield return ShowSymbolWinBySetting(winList[idx], true, SpinWinEvent.SingleWinLine);

                ++idx;

                // 立马停止时，不播放赢分环节？
                if (isStopImmediately && smConfig.IsWESkipAtStopImmediately)
                    break;
            }
        }

        //关闭遮罩
        CloseSlotCover();

        //停止特效显示
        SkipWinLine(false);
    }



    /// <summary>
    /// 显示单条或多条赢线
    /// </summary>
    /// <param name="symbolWin"></param>
    /// <param name="isUseMySelfSymbolNumber"></param>
    public virtual void ShowSymbolWinDeck(SymbolWin symbolWin, bool isUseMySelfSymbolNumber)
    {
        //停止特效显示
        SkipWinLine(false);

        //显示遮罩
        SetSlotCover(smConfig.IsWEShowCover);

        Dictionary<string, string> symbolHitEffect = smConfig.GetSymbolHitEffect();

        foreach (Cell cel in symbolWin.cells)
        {
            int symbolNumberSelf = view.GetVisibleSymbolNumberFromDeck(cel.columnIndex, cel.rowIndex);

            int symbolNumber = isUseMySelfSymbolNumber ? symbolNumberSelf : symbolWin.symbolNumber;

            string symbolName = symbolHitEffect[$"{symbolNumber}"];  // wild  or symbol;

            view.ShowSymbolHitEffect(cel.columnIndex, cel.rowIndex, symbolName, smConfig.IsWESymbolAnim);


            // 边框
            if (smConfig.IsWEFrame)
            {
                //GComponent goBorderEffect = fguiPoolHelper.GetObject(TagPoolObject.SymbolBorder, BorderEffect).asCom;
                //AddBorderEffect(goSymbol, goBorderEffect);
                view.ShowBorderEffect(cel.columnIndex, cel.rowIndex);
            }

            // 整体变大特效
            if (smConfig.IsWETwinkle)
                view.ShowTwinkleEffect(cel.columnIndex, cel.rowIndex);
            else if (smConfig.IsWEBigger)
                view.ShowBiggerEffect(cel.columnIndex, cel.rowIndex);
        }

        // 是否显示线
        if (smConfig.IsWEShowLine)
        {
            if (symbolWin is TotalSymbolWin)
            {
                TotalSymbolWin totalSymbolWin = symbolWin as TotalSymbolWin;
                view.ShowPayLines(totalSymbolWin.lineNumbers);
            }
            else
            {
                view.ShowPayLines(new List<int> { symbolWin.lineNumber });
            }
        }
    }


    /// <summary>
    /// 多个独立图标赢
    /// </summary>
    /// <param name="bonusWin"></param>
    /// <param name="isUseMySelfSymbolNumber"></param>
    public void ShowSymbolWinDeck(List<BonusWin> bonusWin, bool isUseMySelfSymbolNumber)
    {
        //停止特效显示
        SkipWinLine(false);

        //显示遮罩
        SetSlotCover(smConfig.IsWEShowCover);

        Dictionary<string, string> symbolHitEffect = smConfig.GetSymbolHitEffect();

        foreach (BonusWin item in bonusWin)
        {
            Cell cel = item.cell;

            int symbolNumberSelf = view.GetVisibleSymbolNumberFromDeck(cel.columnIndex, cel.rowIndex);

            int symbolNumber = isUseMySelfSymbolNumber ? symbolNumberSelf : item.symbolNumber;

            string symbolName = symbolHitEffect[$"{symbolNumber}"];  // wild  or symbol;

            view.ShowSymbolHitEffect(cel.columnIndex, cel.rowIndex, symbolName, smConfig.IsWESymbolAnim);

            /*
            // 图标动画  
            GComponent goSymbolHit = fguiPoolHelper.GetObject(TagPoolObject.SymbolHit, symbolName).asCom;
            GComponent goSymbol = GetVisibleSymbolFromDeck(cel.column, cel.row);

            AddSymbolEffect(goSymbol, goSymbolHit, IsWESymbolAnim);


            int rowIndex = cel.row;
            // 设置层级
            FguiSortingOrderManager.Instance.ChangeSortingOrder(goSymbol, goExpectation, maskSortOrder, null,
                (self) => rowIndex + DeckUpStartIndex);
            */

            // 边框
            if (smConfig.IsWEFrame)
            {
                //GComponent goBorderEffect = fguiPoolHelper.GetObject(TagPoolObject.SymbolBorder, BorderEffect).asCom;
                //AddBorderEffect(goSymbol, goBorderEffect);
                view.ShowBorderEffect(cel.columnIndex, cel.rowIndex);
            }

            // 整体变大特效
            if (smConfig.IsWETwinkle)
                view.ShowTwinkleEffect(cel.columnIndex, cel.rowIndex);
            else if (smConfig.IsWEBigger)
                view.ShowBiggerEffect(cel.columnIndex, cel.rowIndex);

        }
    }





    /// <summary>
    /// 空闲模式下-轮流显示线
    /// </summary>
    /// <param name="winList"></param>
    /// <returns></returns>
    public IEnumerator ShowWinListAwayDuringIdle(List<SymbolWin> winList)
    {
        while (winList.Count > 0) //while (idx < winList.Count)
        {
            yield return ShowWinListBySetting(winList);
        }
    }



    public void ShowSpecailSymbolEffectBySetting(List<int> symbolNumbers, SymbolEffectType effectType, int? toSymbolNumber = null)
    {
        List<Cell> cells = view.GetSymbolCells(symbolNumbers);
        ShowSpecailSymbolEffectBySetting(cells, effectType, toSymbolNumber);
    }

    public void ShowSpecailSymbolEffectBySetting(List<Cell> cells, SymbolEffectType effectType, int? toSymbolNumber = null)
    {

        // 图标特效
        view.ShowSymbolEffects(cells, effectType, smConfig.IsWESymbolAnim, toSymbolNumber);

        // 边框
        if (smConfig.IsWEFrame)
        {
            view.ShowBorderEffects(cells);
        }

        // 整体变大特效
        if (smConfig.IsWETwinkle)
            view.ShowTwinkleEffects(cells);
        else if (smConfig.IsWEBigger)
            view.ShowBiggerEffects(cells);
    }

    public void ShowSpecailSymbolWinBySetting(SymbolWin winList, SymbolEffectType effectType, int? toSymbolNumber = null)
    {
        try
        {
            ShowSpecailSymbolEffectBySetting(winList.cells, effectType, toSymbolNumber) ;
        }
        catch (Exception ex)
        {
            Debug.LogError($"winList = {JsonConvert.SerializeObject(winList)}");
        }


        // 显示中线
        if (smConfig.IsWEShowLine) 
            view.ShowPayLines(new List<int> { winList.lineNumber });


        //显示遮罩
        SetSlotCover(smConfig.IsWEShowCover);
    }
}
