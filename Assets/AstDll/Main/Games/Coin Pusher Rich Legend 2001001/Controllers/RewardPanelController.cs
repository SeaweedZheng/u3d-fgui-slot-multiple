using FairyGUI;
using SlotMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMaker;

namespace CoinPusherRichLegend2001001
{
    public class RewardPanelController
    {
        // Start is called before the first frame update
        public void Enable()
        {
            EventCenter.Instance.AddEventListener<EventData>(SlotMachineEvent.ON_CONTENT_EVENT, OnContentEvent);
            MachineDataManagerG2001001.Instance.RequestCoinHit(0, (res) =>
            {
                winCoins = 0;
                int[] result = res as int[];
                SetCoinHits(result);
            });
            Timers.inst.Add(0.8f, 0, RepeatCheckCoinHit);
        }

        // Update is called once per frame
        public void Disable()
        {
            EventCenter.Instance.RemoveEventListener<EventData>(SlotMachineEvent.ON_CONTENT_EVENT, OnContentEvent);
            Timers.inst.Remove(RepeatCheckCoinHit);
        }





        GComponent goOwner;

        List<GComponent> items = new List<GComponent>();
        public void InitParam(GComponent ui)
        {
            goOwner = ui;


            //$"ui://MVSymbolAppear{}";

            GList glst = goOwner.GetChild("symbols").asList;
            for (int i = 0; i < 8; i++)
            {
                GComponent go = glst.GetChildAt(i).asCom;
                items.Add(go);
                go.GetChild("icon").asLoader.url = $"ui://RichLegend2001001/MVSymbolAppear{i + 1}";
                go.GetChild("coins").asCom.GetChild("num").asTextField.text = curCoinNumLst[i].ToString();
            }

        }


        List<int> toCoinNumLst = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };
        List<int> curCoinNumLst = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };


        void OnContentEvent(EventData res)
        {
            if (res.name == SlotMachineEvent.BeginTurn)
            {
                DoTaskToCoinNumEndImmediately();
            }
        }

  
        public void SetCoinHits(int[] coinHits)
        {
            toCoinNumLst.Clear();
            toCoinNumLst.AddRange(coinHits);
            curCoinNumLst.Clear();
            curCoinNumLst.AddRange(coinHits);

            if(items.Count > 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    items[i].GetChild("coins").asCom.GetChild("num").asTextField.text = curCoinNumLst[i].ToString();
                }
            }
        }


        public void DoTaskToCoinNum(int[] coinHits)
        {
            for (int i=0; i<8; i++)
            {
                if(toCoinNumLst[i] != coinHits[i])
                {
                    DebugUtils.Log($"index: {i}  toCoin: {toCoinNumLst[i]}");
                    toCoinNumLst[i] = coinHits[i];
                    AddEffect(i);

                    if (!Timers.inst.Exists(TaskToCoinNum))
                    {
                        Timers.inst.Add(0.3f, 0, TaskToCoinNum);
                    }
                    Timers.inst.Remove(TaskToCoinNumEnd);
                    Timers.inst.Add(2f, 1, TaskToCoinNumEnd);
                }
            }
        }

        void TaskToCoinNum(object param)
        {
            for (int i = 0; i < toCoinNumLst.Count; i++)
            {
                if(curCoinNumLst[i] < toCoinNumLst[i])
                {
                    curCoinNumLst[i]++;
                    items[i].GetChild("coins").asCom.GetChild("num").asTextField.text = curCoinNumLst[i].ToString();
                }
            }
        }

        void TaskToCoinNumEnd(object param)
        {

            Timers.inst.Remove(TaskToCoinNum);

            foreach (GComponent itm in items)
            {
                itm.GetTransition("hit").Stop();
                itm.GetChild("border").visible = false;
            }

            for (int i = 0; i < toCoinNumLst.Count; i++)
            {
                if (toCoinNumLst[i] > 0)
                {
                    curCoinNumLst[i] = toCoinNumLst[i];
                    items[i].GetChild("coins").asCom.GetChild("num").asTextField.text = curCoinNumLst[i].ToString();
                }
            }
        }

        void DoTaskToCoinNumEndImmediately()
        {
            Timers.inst.Remove(TaskToCoinNumEnd);
            TaskToCoinNumEnd(null);
        }


        void AddEffect(int index)
        {
            if (!items[index].GetChild("border").visible)
            {
                items[index].GetTransition("hit").Play();
                items[index].GetChild("border").visible = true;
            }
        }


        public int winCoins = 0;
        void RepeatCheckCoinHit(object param)
        {
            MachineDataManagerG2001001.Instance.RequestCoinHit(winCoins, (res) =>
            {
                winCoins = 0;
                int[] result = res as int[];
                DoTaskToCoinNum(result);
            });
        }


    }
}