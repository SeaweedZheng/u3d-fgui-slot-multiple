using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameMaker;

namespace CoinPusherRichLegend2001001
{
    public class InParamsPageGameBonus1 : InParamsBase
    {
        public long totalEarnCoins;
    }
    public class PageGameBonus1 : MachinePageBase
    {
        public const string pkgName = "RichLegend2001001";
        public const string resName = "PageGameBonus1";


        protected override void OnInit()
        {

            base.OnInit();

            int count = 1;

            Action callback = () =>
            {
                if (--count == 0)
                {
                    isInit = true;
                    InitParam();
                }
            };

            // 异步加载资源

            ResourceManager02.Instance.LoadAsset<GameObject>(
            "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001001/Prefabs/Page Game Bonus 1/Train.prefab",
            (GameObject clone) =>
            {
                goTrainClone = clone;
                callback();
            });

        }

        GameObject goTrainClone;

        public override void OnOpen(PageName name, InParamsBase data)
        {
            base.OnOpen(name, data);

            // 添加事件监听

            InitParam();
        }




        // public override void OnTop() { DebugUtils.Log($"i am top {this.name}"); }

        GButton btnClose;



        GComponent goOwner;

        GComponent curAnchorBonus1Effect , goBG;


        Animator atorBonus1Effect;
        public override void InitParam()
        {
            if (!isInit) return;

            if (!isOpen) return;

            /*
            // btnClose =  this.contentPane.GetChild("btnExit").asButton;
            btnClose = this.contentPane.GetChild("navBottom").asCom.GetChild("btnExit").asButton;
            btnClose.onClick.Clear();
            btnClose.onClick.Add(() => {
                //DebugUtils.Log("i am here 123");
                CloseSelf(null);
                //  CloseSelf(new EventData("Exit"));
            });
            */
            goOwner = this.contentPane.GetChild("page").asCom;

            GComponent nextAnchorBonus1Effect = goOwner.GetChild("anchorBonus1Effect").asCom;

            if (curAnchorBonus1Effect != nextAnchorBonus1Effect)
            {
                if(curAnchorBonus1Effect != null)
                    GameCommon.FguiUtils.DeleteWrapper(curAnchorBonus1Effect);

                curAnchorBonus1Effect = nextAnchorBonus1Effect;
                GameObject go = GameObject.Instantiate(goTrainClone);
                GameCommon.FguiUtils.AddWrapper(curAnchorBonus1Effect, go);
                atorBonus1Effect = go.transform.Find("Anchor").GetChild(0).GetComponent<Animator>();
            }


            goBG = goOwner.GetChild("bg").asCom;
            goBG.onClick.Clear();
            goBG.onClick.Add(OnClickSpin);
        }


        void OnClickSpin()
        {
            atorBonus1Effect.Play("End");
            Timers.inst.Add(2f, 1, (pam) =>
            {
                CloseSelf(null);
            });
        }
    }
}
