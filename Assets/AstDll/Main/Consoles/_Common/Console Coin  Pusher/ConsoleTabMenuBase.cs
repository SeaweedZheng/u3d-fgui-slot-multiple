
using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;


public class ConsoleTabMenuBase  // : MonoBehaviour
{
    public GComponent goOwnerMenu;

    protected Action onClickExitCallback, onClickNext, onClickPrev;
    protected Dictionary<int, string> menuMap;

    protected int curIndexMenuItem = 0;


    //GObject goPrevItem = null, goNextItem = null;


    /// <summary>
    /// 设置上一页，下一页按钮
    /// </summary>
    /// <param name="isStartTab"></param>
    /// <param name="isEndTab"></param>
    public void SetPrevNextButton(bool isStartTab, bool isEndTab)
    {
        if (isStartTab && isEndTab)
        {
            GObject goItem = goOwnerMenu.GetChild("prev");
            if (goItem != null)
            {
                //goPrevItem = goItem;
                goOwnerMenu.RemoveChild(goItem);
            }

            goItem = goOwnerMenu.GetChild("next");
            if (goItem != null)
            {
                //goNextItem = goItem;
                goOwnerMenu.RemoveChild(goItem);
            }
        }
        else if (isStartTab && !isEndTab)
        {
            GObject goItem = goOwnerMenu.GetChild("prev");
            if (goItem != null)
            {
                //goPrevItem = goItem;
                goOwnerMenu.RemoveChild(goItem);
            }
        }
        else if (!isStartTab && isEndTab)
        {
            GObject goItem = goOwnerMenu.GetChild("next");
            if (goItem != null)
            {
                //goNextItem = goItem;
                goOwnerMenu.RemoveChild(goItem);
            }
        }
    }

    public virtual void Init() //调用一次
    {
        // 事件监听，资源加载
    }

    public virtual void Dispose()//调用一次
    {
        // 取消事件监听，资源卸载
    }

    //频繁调用-多语言
    public virtual void InitParam(GComponent comp, Action onClickPrev, Action onClickNext, Action onClickExitCallback, bool isStartTab, bool isEndTab)
    {
        this.goOwnerMenu = comp;
        this.onClickPrev = onClickPrev;
        this.onClickNext = onClickNext;
        this.onClickExitCallback = onClickExitCallback;

        SetPrevNextButton(isStartTab, isEndTab);
    }


    protected void AddClickEvent()
    {
        for (int i = 0; i < goOwnerMenu.numChildren; i++)
        {
            int index = i;

            // 没有内容，则点击抬头==点击确认按钮
            goOwnerMenu.GetChildAt(index).asCom.GetChild("title").onClick.Clear();
            goOwnerMenu.GetChildAt(index).asCom.GetChild("title").onClick.Add(() =>
            {
                curIndexMenuItem = index;
                SetAllow();
                if (goOwnerMenu.GetChildAt(curIndexMenuItem).asCom.GetChild("value").asRichTextField.text == "")
                {
                    OnClickConfirm();
                }
            });


            // 有内容，点击内容==点击确认按钮
            goOwnerMenu.GetChildAt(index).asCom.GetChild("value").onClick.Clear();
            goOwnerMenu.GetChildAt(index).asCom.GetChild("value").onClick.Add(() =>
            {
                curIndexMenuItem = index;
                SetAllow();
                if (goOwnerMenu.GetChildAt(curIndexMenuItem).asCom.GetChild("value").asRichTextField.text != "")
                {
                    OnClickConfirm();
                }
            });
        }
    }

    /// <summary>
    /// 显示箭头
    /// </summary>
    protected void SetAllow()
    {
        for (int i = 0; i < goOwnerMenu.numChildren; i++)
        {
            goOwnerMenu.GetChildAt(i).asCom.GetChild("icon").visible = i == curIndexMenuItem;
        }
    }
    public void OnClickNext()
    {
        if (++curIndexMenuItem >= goOwnerMenu.numChildren)
            curIndexMenuItem = 0;
        SetAllow();
    }

    public void OnClickPrev()
    {
        if (--curIndexMenuItem < 0)
            curIndexMenuItem = goOwnerMenu.numChildren - 1;
        SetAllow();
    }

    public virtual void OnClickConfirm()
    {
        if (menuMap.ContainsKey(curIndexMenuItem))
        {

            switch (menuMap[curIndexMenuItem])
            {
                case "prev":
                    {
                        onClickPrev?.Invoke();
                    }
                    return;
                case "next":
                    {
                        onClickNext?.Invoke();
                    }
                    return;
                case "exit":
                    {
                        onClickExitCallback?.Invoke();
                    }
                    return;
            }
        }
    }

    public void SetItemTouchable(string name, bool isTouchable)
    {
        goOwnerMenu.GetChild(name).asCom.GetChild("title").touchable = isTouchable;
        goOwnerMenu.GetChild(name).asCom.GetChild("value").touchable = isTouchable;


        if (isTouchable)
        {
            goOwnerMenu.GetChild(name).asCom.GetChild("value").asRichTextField.color = Color.white;
        }
        else
        {

#if !false
            Color grayColor;
            // 注意：十六进制字符串必须带 # 前缀
            //if (ColorUtility.TryParseHtmlString("#555555", out grayColor))
            if (ColorUtility.TryParseHtmlString("#FFFF99", out grayColor))
            {
                // 解析成功，grayColor 即为目标颜色（A=1.0）
                goOwnerMenu.GetChild(name).asCom.GetChild("value").asRichTextField.color = grayColor;
            }
#else

            goOwnerMenu.GetChild(name).asCom.GetChild("value").asRichTextField.color = Color.gray;
#endif
        }

    }


    public void ResetItem(bool isClearAllArrow)
    {
        menuMap = new Dictionary<int, string>();
        curIndexMenuItem = 0;
        for (int i = 0; i < goOwnerMenu.numChildren; i++)
        {
            GComponent goItem = goOwnerMenu.GetChildAt(i).asCom;
            if (isClearAllArrow)
                goItem.GetChild("icon").visible = false;
            else
                goItem.GetChild("icon").visible = i == curIndexMenuItem;

            menuMap.Add(i, goItem.name);
        }
    }
}
