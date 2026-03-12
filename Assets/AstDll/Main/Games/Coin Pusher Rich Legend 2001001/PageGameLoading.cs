using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameMaker;

namespace CoinPusherRichLegend2001001
{
    public class PageGameLoading : MachinePageBase
    {
        public const string pkgName = "RichLegend2001001";
        public const string resName = "PageGameLoading";

        public static void OnBeforeCreat(Action onFinishCallback)
        {
            // 添加模块
            //ModuleDownloadManager.Instance.AddModeToRuning("RL2001001");

            onFinishCallback?.Invoke();
        }
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

            callback();
        }

        public override void OnOpen(PageName name, InParamsBase data)
        {
            base.OnOpen(name, data);

            // 添加事件监听
            InitParam();
        }

        GProgressBar ProgressBar;
        GTextField Load, version;

        GTweener tweener2 = null;
        public override void InitParam()
        {

            if (!isInit) return;

            if (!isOpen) return;

            GComponent gPage = this.contentPane.GetChild("page").asCom;
            version = gPage.GetChild("ver").asTextField;
            version.text = GlobalModel.hotfixVersion;
            ProgressBar = gPage.GetChild("progress").asProgress;


            StartLoadingAnimation2();
        }



        private void StartLoadingAnimation2()
        {
            // 预加载
            PageManager.Instance.PreloadPage(PageName.RichLegend2001001PageGameMain, null);


            if (tweener2 != null) tweener2.Kill();
            tweener2 = GTween.To(0, 1, 5.5f)
                .SetEase(EaseType.Linear) // 线性过渡，匀速增长
                .OnUpdate((tween) =>
                {
                    // 获取当前进度值（四舍五入为整数）
                    double progress = tween.value.x;
                    ProgressBar.value = progress;
                })
                .OnComplete(() =>
                {
                    CloseSelf(null);

                    PageManager.Instance.OpenPage(PageName.RichLegend2001001PageGameMain);
                });
        }
    }
}