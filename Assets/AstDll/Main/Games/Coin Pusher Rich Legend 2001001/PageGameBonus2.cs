using FairyGUI;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameMaker;
using SimpleJSON;

namespace CoinPusherRichLegend2001001
{
    public class PageGameBonus2 : MachinePageBase
    {
        public const string pkgName = "RichLegend2001001";
        public const string resName = "PageGameBonus2";


        protected override void OnInit()
        {

            base.OnInit();

            int count = 2;

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
            "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001001/Prefabs/Page Game Bonus 2/ButtonHand.prefab",
            (GameObject clone) =>
            {
                cloneButtonHand = clone;
                callback();
            });

            ResourceManager02.Instance.LoadAsset<GameObject>(
            "Assets/AstBundle/Games/Coin Pusher Rich Legend 2001001/Prefabs/Page Game Bonus 2/RichMainJump.prefab",
            (GameObject clone) =>
            {
                cloneRichMainJump = clone;
                callback();
            });



            machineBtnClickHelper = new MachineButtonClickHelper()
            {

                upClickHandler = new Dictionary<MachineButtonKey, Action<MachineButtonInfo>>()
                {
                    [MachineButtonKey.BtnSpin] = (info) =>
                    {
                        OnClickSpin();
                    },
                },
            };

        }

        GameObject cloneButtonHand, cloneRichMainJump;

        public override void OnOpen(PageName name, EventData data)
        {
            base.OnOpen(name, data);


            // 添加事件监听
            creditCtr.Enable();


            InitParam();
        }


        public override void OnClose(EventData data = null)
        {


            creditCtr.Disable();

            base.OnClose(data);
        }



        // public override void OnTop() { DebugUtils.Log($"i am top {this.name}"); }

        GButton btnClose;



        GComponent goOwner;

        GComponent goBG;









        GTextField gtxtClock, gtxtDiceGameScore;

        UIMyCreditController creditCtr = new UIMyCreditController();

        GComponent goMap , goDice , goFlyCoin;

        GComponent curAnchorButtonHand, curAnchorRichMainJump;

        Animator atorButtonHand, atorRichMainJump;


        DiceGameInfo diceGameInfo;


        int totalRewardCoins = 0;

        public override void InitParam()
        {
            if (!isInit) return;

            if (!isOpen) return;


            GComponent goPage = this.contentPane.GetChild("page").asCom;

            creditCtr.InitParam(goPage.GetChild("myCredit").asCom.GetChild("credit").asTextField);



            // 10, 20, 30, 40, 50, 60, 80
            //diceGameInfo = DiceStepCreater.CreatSteps(new List<int>() {40, 20, 30 , 20 ,80 ,10, 50 , 60 , 20, 20 });
            diceGameInfo = DiceStepCreater.CreatSteps(new List<int>() { 80, 80 });


            if (inParams != null)
            {
                Dictionary<string, object> argDic = null;
                argDic = (Dictionary<string, object>)inParams.value;
                JSONNode node = (JSONNode)argDic["bonusResults"];

                List<int> rewardCoins = new List<int>();
                foreach (JSONNode item in (node["rewardCoins"] as JSONArray))
                {
                    rewardCoins.Add((int)item);
                }

                diceGameInfo = DiceStepCreater.CreatSteps(rewardCoins);
            }




            goMap = goPage.GetChild("map").asCom;
            for (int i = 0; i <= 21; i++)
            {
                GComponent item = goMap.GetChild($"goldItem{i}").asCom;
                item.GetController("GoldNum").SetSelectedPage(GetControllerName(diceGameInfo.mapRewards[i]));
                item.GetController("GoldState").SetSelectedPage("star");
            }



            GComponent nextAnchorRichMainJump = goMap.GetChild("anchorRichManJump").asCom;
            if (curAnchorRichMainJump != nextAnchorRichMainJump)
            {
                if (curAnchorRichMainJump != null)
                    GameCommon.FguiUtils.DeleteWrapper(curAnchorRichMainJump);

                curAnchorRichMainJump = nextAnchorRichMainJump;
                GameObject go = GameObject.Instantiate(cloneRichMainJump);
                GameCommon.FguiUtils.AddWrapper(curAnchorRichMainJump, go);
                atorRichMainJump = go.transform.Find("Anchor").GetChild(0).GetComponent<Animator>();
            }


            GComponent nextAnchorButtonHand = goMap.GetChild("anchorButtonHand").asCom;
            if (curAnchorButtonHand != nextAnchorButtonHand)
            {
                if (curAnchorButtonHand != null)
                    GameCommon.FguiUtils.DeleteWrapper(curAnchorButtonHand);

                curAnchorButtonHand = nextAnchorButtonHand;
                GameObject go = GameObject.Instantiate(cloneButtonHand);
                GameCommon.FguiUtils.AddWrapper(curAnchorButtonHand, go);
                atorButtonHand = go.transform.Find("Anchor").GetChild(0).GetComponent<Animator>();
            }




            gtxtClock = goMap.GetChild("CountDown").asTextField;
            gtxtClock.text = 0.ToString();

            goDice = goMap.GetChild("diceAni").asCom;


            gtxtDiceGameScore = goMap.GetChild("diceGameScore").asTextField;

            goFlyCoin = goPage.GetChild("flyCoin").asCom;

            /*
            Timers.inst.Add(4f, 1, (parm) =>
            {
                atorRichMainJump.Play($"Jump2");
            });
            */

            DoTaskGame();
        }


        string GetControllerName(int coin)
        {
            switch (coin)
            {
                case 10:
                    return "ten";
                case 20:
                    return "twenty";
                case 30:
                    return "thirty";
                case 40:
                    return "forty";
                case 50:
                    return "fifty";
                case 60:
                    return "sixty";
                case 80:
                    return "eighty";
                default:
                    return "Zero";
            }
        }


        string GetDiceControllerPageName(int number)
        {
            switch (number)
            {
                case 1:
                    return "One";
                case 2:
                    return "Two";
                case 3:
                    return "Three";
                case 4:
                    return "Four";
                case 5:
                    return "Five";
                case 6:
                    return "Six";
                default:
                    return "None";
            }
        }

        /// <summary>
        /// 按下开始丢筛子
        /// </summary>
        void OnClickSpin()
        {
            if (stepGame == StepGame.CutDowm)
            {
                Timers.inst.Remove(TaskGame);

                stepGame = StepGame.UseDice;
                TaskGame(null);
            }
        }
        


        void DoTaskGame()
        {
            stepGame = StepGame.Init;
            TaskGame(null);
        }




        enum StepGame
        {
            Init = 0,
            StartCutDown,
            CutDowm,
            UseDice,
            StartJumpNext,
            JumpNext,
            CoinShow,
            CoinFly,
            GameOver,
        }





        StepGame stepGame;

        int cutDown = 0;
        const int CUT_DOWN = 5;

        GTweener tweenerRichMainJump, terrnerFlyCoin;
        List<DiceStepInfo> diceStep = null;


        DiceStepInfo curDiceStepInfo;
        int curMapIndex;



        void TaskGame(object pam)
        {
            switch (stepGame)
            {
                case StepGame.Init:
                    {
                        // Npc复位
                        Vector2 toPos = goMap.GetChild("goldItem0").xy;
                        curAnchorRichMainJump.xy = new Vector2(toPos.x, toPos.y);

                        // 筛子复位
                        goDice.GetController("DiceState").SetSelectedPage("None");  //None

                        // 闹钟复位
                        curAnchorButtonHand.visible = false;

                        totalRewardCoins = 0;
                        curMapIndex = 0;

                        stepGame = StepGame.StartCutDown;
                        Timers.inst.Add(3f, 1, TaskGame); //TaskGame(null);
                    }
                    break;
                case StepGame.StartCutDown:
                    {

                        cutDown = CUT_DOWN;
                        gtxtClock.text = $"{cutDown}";
                        curAnchorButtonHand.visible = true;

                        stepGame = StepGame.CutDowm;
                        TaskGame(null);
                    }
                    break;
                case StepGame.CutDowm:
                    {
                        if (--cutDown < 0)
                        {
                            stepGame = StepGame.UseDice;
                            TaskGame(null);
                        }
                        else
                        {
                            gtxtClock.text = $"{cutDown}";
                            Timers.inst.Add(1f, 1, TaskGame);
                        }
                    }
                    break;
                case StepGame.UseDice:
                    {
                        curAnchorButtonHand.visible = false;

                        curDiceStepInfo = diceGameInfo.stepInfos[0];
                        diceGameInfo.stepInfos.RemoveAt(0);


                        // 筛子
                        goDice.GetController("DiceState").SetSelectedPage("None");  //None

                        goDice.GetController("DiceState")
                            .SetSelectedPage(GetDiceControllerPageName(curDiceStepInfo.diceData));

                        stepGame = StepGame.StartJumpNext;
                        Timers.inst.Add(2.5f, 1, TaskGame);

                    }
                    break;
                case StepGame.StartJumpNext:
                    {
   
                        //atorRichMainJump.Play($"Jump{curDiceStepInfo.diceData}");
                        //atorRichMainJump.Play($"Jump");

                        atorRichMainJump.Play($"Jump{curDiceStepInfo.diceData}");

                        stepGame = StepGame.JumpNext;
                        Timers.inst.Add(1.5f, 1, TaskGame);

                    }
                    break;
                case StepGame.JumpNext:
                    {

                        curDiceStepInfo.diceData--;

                        if(++curMapIndex >= 22)
                            curMapIndex = 0;
                        
                        Vector2 toPos = goMap.GetChild($"goldItem{curMapIndex}").xy;


                        tweenerRichMainJump = GTween.To(curAnchorRichMainJump.xy, toPos, 0.5f)
                        //.SetEase(EaseType.ExpoOut)  // 设置缓动函数
                        .SetEase(EaseType.Linear)  // 设置缓动函数
                        .OnUpdate((GTweener tweener) =>
                        {
                            // 每次更新时调用
                            curAnchorRichMainJump.xy = new Vector2(tweener.value.x, tweener.value.y);
                        })
                        .OnComplete(() =>
                        {
                            if (curDiceStepInfo.diceData > 0)
                            {
                                stepGame = StepGame.JumpNext;
                                TaskGame(null);
                            }
                            else
                            {

                                if (diceGameInfo.stepInfos.Count == 0)
                                {
                                    stepGame = StepGame.GameOver;
                                    Timers.inst.Add(3f, 1, TaskGame);
                                }
                                else
                                {
                                    stepGame = StepGame.CoinShow;
                                    TaskGame(null);
                                }
                            }

                        });
                    }
                    break;
                case StepGame.CoinShow:
                    {
                        goMap.GetChild($"goldItem{curMapIndex}").asCom.GetController("GoldState")
                            .SetSelectedPage("idle");

                        stepGame = StepGame.CoinFly;
                        Timers.inst.Add(2.5f, 1, TaskGame);
                    }
                    break;
                case StepGame.CoinFly:
                    {
                        goMap.GetChild($"goldItem{curMapIndex}").asCom.GetController("GoldState")
                            .SetSelectedPage("over");


                            Vector2 worldPos = goMap.LocalToGlobal(goMap.GetChild($"goldItem{curMapIndex}").xy);
                            Vector2 fromPosLoc = goFlyCoin.parent.GlobalToLocal(worldPos);

                            worldPos = goMap.LocalToGlobal(gtxtDiceGameScore.xy);
                            Vector2 toPosLoc = goFlyCoin.parent.GlobalToLocal(worldPos);

                            goFlyCoin.xy = fromPosLoc;
                            goFlyCoin.visible = true;

                            goFlyCoin.GetController("GoldNum")
                                .SetSelectedPage(GetControllerName(curDiceStepInfo.reward));
                            goFlyCoin.GetController("GoldState")
                                .SetSelectedPage("star");

                            float distance = Vector2.Distance(fromPosLoc, toPosLoc);


                            terrnerFlyCoin = GTween.To(fromPosLoc, toPosLoc, distance * 0.0015f)
                            .SetEase(EaseType.Linear)  // 设置缓动函数
                            .OnUpdate((GTweener tweener) =>
                            {
                                // 每次更新时调用
                                goFlyCoin.xy = new Vector2(tweener.value.x, tweener.value.y);
                            })
                            .OnComplete(() =>
                            {
                                totalRewardCoins += curDiceStepInfo.reward;
                                goFlyCoin.visible = false;
                                gtxtDiceGameScore.text = totalRewardCoins.ToString();


                                stepGame = StepGame.StartCutDown;
                                Timers.inst.Add(1f, 1, TaskGame);
                            });
                        

                    }
                    break;
                case StepGame.GameOver:
                    {
                        CloseSelf(null);
                    }
                    break;
            }
        }

    }
}
