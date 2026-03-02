using FairyGUI;
using SlotMaker;
using GameMaker;


public class UIMyCreditController  
{

    public void Enable()
    {
        EventCenter.Instance.AddEventListener<EventData>(MetaUIEvent.ON_CREDIT_EVENT,OnCreditEvent);

    }
    public void Disable()
    {
        EventCenter.Instance.AddEventListener<EventData>(MetaUIEvent.ON_CREDIT_EVENT,OnCreditEvent);
    }

    GTextField gtxtCredit;
    public void InitParam(GTextField v)
    {
        gtxtCredit = v;
        gtxtCredit.text = SBoxModel.Instance.myCredit.ToString();
    }

    void OnCreditEvent(EventData res)
    {
        if (res.name == MetaUIEvent.UpdateNaviCredit)
        {
            UpdateNaviCredit data = res.value as UpdateNaviCredit;

            if (data.isAnim)
            {
                DoAddToCredit(data.toCredit, data.fromCredit);
            }
            else {
                DoSetCredit(data.toCredit);
            }

        }
    }



    enum StepAddCredit
    {
        Init = 0,
        Adding = 1,
    }

    long fromCredit;
    long toCredit;
    long curCredit;



    StepAddCredit stepAddCredit;
    void TaskAddToCredit(object pam)
    {
        switch (stepAddCredit)
        {
            case StepAddCredit.Init:
                {
                    if(fromCredit != UpdateNaviCredit.NONE)
                        curCredit = fromCredit;
                    stepAddCredit = StepAddCredit.Adding;

                    TaskAddToCredit(null);

                    Timers.inst.Add(2f, 1, TaskToFinishCredti); // 加钱动画最多2秒
                }
                break;
            case StepAddCredit.Adding:
                {
                    curCredit += 5;
                    if (curCredit >= toCredit)
                    {
                        curCredit = toCredit;
                        Timers.inst.Remove(TaskToFinishCredti); // 加钱动画最多2秒 
                    }
                    else
                    {
                        Timers.inst.Add(0.1f, 1, TaskAddToCredit);
                    }
                    gtxtCredit.text = $"{curCredit}";
                }
                break;
        }
    }

    void TaskToFinishCredti(object pam)
    {
        Timers.inst.Remove(TaskAddToCredit);
        this.fromCredit = UpdateNaviCredit.NONE;
        this.curCredit = toCredit;
        gtxtCredit.text = $"{curCredit}";
    }


    void DoAddToCredit(long toCredit, long fromCredit)
    {
        Timers.inst.Remove(TaskAddToCredit);
        this.fromCredit = fromCredit;
        this.toCredit = toCredit;
        stepAddCredit = StepAddCredit.Init;
        TaskAddToCredit(null);
    }

    void DoSetCredit(long toCredit)
    {
        Timers.inst.Remove(TaskToFinishCredti);
        Timers.inst.Remove(TaskAddToCredit);
        this.fromCredit = UpdateNaviCredit.NONE;
        this.toCredit = toCredit;
        this.curCredit = toCredit;
        gtxtCredit.text = $"{curCredit}";
    }

}
