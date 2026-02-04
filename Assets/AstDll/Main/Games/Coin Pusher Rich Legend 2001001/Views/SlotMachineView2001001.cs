using FairyGUI;
using SlotMaker;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CoinPusherRichLegend2001001 {
    public class SlotMachineView2001001 : SlotMachineViewBase
    {

        //FguiGObjectPoolHelper gobjPool = new FguiGObjectPoolHelper();

        const string MARK_POOL_GOBJ_2001001 = nameof(MARK_POOL_GOBJ_2001001);

        const string MARK_POOL_GAMEOBJ_2001001 = nameof(MARK_POOL_GAMEOBJ_2001001);

        public override void ShowSymbolHitEffect(int colIndex, int rowIndex, string symbolName, bool isAmin = true)
        {

            GComponent gSymbolHit = FguiGObjectPoolHelper.Instance.GetObject(symbolName, TagPoolGObject.SymbolHit, MARK_POOL_GOBJ_2001001).asCom;
            gSymbolHit.visible = true;

            GComponent goSymbol = GetVisibleSymbolFromDeck(colIndex, rowIndex);

            GMovieClip clip =  gSymbolHit.GetChild("clip").asMovieClip;
            if (isAmin)
            {
                clip.frame = 0;  
                clip.playing = true;

            }
            else
            {
                clip.frame = 0; //如果动画处于停止状态，可以设置停止在第几帧
                clip.playing = false;
            }


            GComponent goAnimator = goSymbol.GetChild("animator").asCom;
            goAnimator.AddChild(gSymbolHit);

            // 是否隐藏原有图标
            if (smConfig.IsWEHideBaseSymbol)
            {
                HideBaseSymbolIcon(colIndex, rowIndex, true);
            }


            // 设置层级
            FguiSortingOrderManager.Instance.ChangeSortingOrder(goSymbol, goAnchorSymbolEffect, maskSortOrder, null,
                (self) => rowIndex + smConfig.DeckUpStartIndex);

        }



        public override void ReturnSymbolEffectToPool(int colIndex, int rowIndex, string[] exclude)
        {

            //Debug.Log($"返回特效：col:{colIndex} row:{rowIndex}");
            GComponent goSymbol = GetVisibleSymbolFromDeck(colIndex, rowIndex);

            FguiGObjectPoolHelper.Instance.ReturnAllToPool(goSymbol, exclude);

            // Return fgui GameObject to pool
        }



        public override void ShowBorderEffect(int colIndex, int rowIndex)
        {
            GComponent goSymbol = GetVisibleSymbolFromDeck(colIndex, rowIndex);
            GObject goBorder = goSymbol.GetChild("animator").asCom.GetChild("border");
            goBorder.visible = true;

            goBorder.parent.SetChildIndex(goBorder, goBorder.parent.numChildren - 1); //置顶
        }



        public override void RemoveBorderEffect(int colIndex, int rowIndex)
        {
            GComponent goSymbol = GetVisibleSymbolFromDeck(colIndex, rowIndex);
            GObject goBorder = goSymbol.GetChild("animator").asCom.GetChild("border");
            goBorder.visible = false;
        }
    }
}
