using SlotMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMaker;
using FairyGUI;

public class UIWinNumController
{
    public void Enable()
    {
        EventCenter.Instance.AddEventListener<EventData>(SlotMachineEvent.ON_WIN_EVENT, OnTotalWinCredit);
        EventCenter.Instance.AddEventListener<EventData>(Observer.ON_PROPERTY_CHANGED_EVENT, OnPropertyChange);
    }
    public void Disable()
    {
        EventCenter.Instance.RemoveEventListener<EventData>(SlotMachineEvent.ON_WIN_EVENT, OnTotalWinCredit);
        EventCenter.Instance.RemoveEventListener<EventData>(Observer.ON_PROPERTY_CHANGED_EVENT, OnPropertyChange);
    }

    GTextField gtxtWin;
    public void InitParam(GTextField v)
    {
        gtxtWin = v;
        gtxtWin.text = SBoxModel.Instance.myCredit.ToString();
    }


    protected virtual void OnTotalWinCredit(EventData res)
    {
        if (res.name == SlotMachineEvent.TotalWinCredit)
        {
            long totalWinCredit = (long)res.value;

            DoSetNum(totalWinCredit);
        }
        else if (res.name == SlotMachineEvent.PrepareTotalWinCredit)
        {
            PrepareTotalWinCredit result = res.value as PrepareTotalWinCredit;

            if (result.isAddToCredit)
            {
                DoAddToNum(result.totalWinCredit);
            }
            else
            {
                DoSetNum(result.totalWinCredit);
            }
        }
    }

    long toNum;
    long curNum;


    enum StepAddWin
    {
        Init = 0,
        Adding = 1,
    }

    StepAddWin stepAddWin;

    void TaskAddToNum(object pam)
    {
        switch (stepAddWin)
        {
            case StepAddWin.Init:
                {
                    stepAddWin = StepAddWin.Adding;
                    TaskAddToNum(null);
                    Timers.inst.Add(2f, 1, TaskToFinishNum); // 加钱动画最多2秒
                }
                break;
            case StepAddWin.Adding:
                {

                    if (curNum != toNum)
                    {
                        long gap1 = toNum - curNum;
                        long gap = curNum > toNum ? -5 : 5;

                        if(gap<0 && gap < gap1) // -5 < -4
                        {
                            gap = gap1;
                        }
                        else if( gap > 0 && gap > gap1)
                        {
                            gap = gap1;
                        }

                        curNum += gap;

                        if(curNum == toNum)
                        {
                            Timers.inst.Remove(TaskToFinishNum); // 加钱动画最多2秒 
                        }
                        else
                        {
                            Timers.inst.Add(0.1f, 1, TaskAddToNum);
                        }
                    }

                    gtxtWin.text = $"{curNum}";
                }
                break;
        }
    }

    void TaskToFinishNum(object pam)
    {
        Timers.inst.Remove(TaskAddToNum);
        this.curNum = toNum;
        gtxtWin.text = $"{curNum}";
    }


    public void DoAddToNum(long toNum)
    {
        Timers.inst.Remove(TaskToFinishNum);
        Timers.inst.Remove(TaskAddToNum);
        this.toNum = toNum;
        stepAddWin = StepAddWin.Init;
        TaskAddToNum(null);
    }

    public void DoSetNum(long toNum)
    {
        Timers.inst.Remove(TaskToFinishNum);
        Timers.inst.Remove(TaskAddToNum);
        this.toNum = toNum;
        this.curNum = toNum;
        gtxtWin.text = $"{curNum}";
    }

    protected virtual void OnPropertyChange(EventData res = null)
    {
        //ContentModel
        string name = res.name;
        switch (name)
        {
            case "ContentModel/gameState":
                OnPropertyGameState(res);
                break;
        }
    }

    protected virtual void OnPropertyGameState(EventData res = null)
    {
        string gameState = (string)res?.value;

        if (gameState == GameState.Spin || gameState == GameState.FreeSpin)
        {
            DoSetNum(0);
        }
    }
}
