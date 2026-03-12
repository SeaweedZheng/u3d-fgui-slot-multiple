using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameMaker;

namespace Common99000000
{
    public class InParamsPopupSystemLoading : InParamsBase
    {
        public string title;
        public string url;
    }

    public class PopupSystemLoading : PageBase
    {
        public const string pkgName = "Common99000000";
        public const string resName = "PopupSystemLoading";

        public override PageType pageType => PageType.Overlay;

        GComponent goOwnerPage;

        GLoader lodBG, lodLogo;

        GProgressBar pbLoading;

        GTextField txtMsg, txtTitle;


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
            callback();
        }


        public override void OnOpen(PageName name, InParamsBase data)
        {
            base.OnOpen(name, data);
            EventCenter.Instance.AddEventListener<EventData>(GlobalEvent.ON_SYSTEM_UI_EVENT, OnSystemUIEvent);

            //LoadingProgressPercentInfo
            InitParam();
        }

        public override void OnClose(OutParamsBase data = null)
        {

            // 删除事件监听
            EventCenter.Instance.RemoveEventListener<EventData>(GlobalEvent.ON_SYSTEM_UI_EVENT, OnSystemUIEvent);

            base.OnClose(data);
        }

        void OnSystemUIEvent(EventData res)
        {
            if (res.name == GlobalEvent.LoadingProgress)
            {
                LoadingProgressInfo data = res.value as LoadingProgressInfo;
                txtMsg.text = data.msg + $"({data.progress * 100}%)";
                pbLoading.value = data.progress;

            }else if (res.name == GlobalEvent.LoadingProgressPercent)
            {
                LoadingProgressPercentInfo data = res.value as LoadingProgressPercentInfo;
                float pg = data.GetProgressValue();
                txtMsg.text = data.msg + $"({pg * 100}%)";
                pbLoading.value = pg;
            }
        }


        public override void InitParam()
        {
            if (!isInit) return;


            if (!isOpen) return;

            goOwnerPage = this.contentPane;

            pbLoading = goOwnerPage.GetChild("progress").asProgress;
            txtMsg = goOwnerPage.GetChild("msg").asTextField;
            lodBG = goOwnerPage.GetChild("bg").asLoader;

            //无法直接加载http图片
            //lodBG.url = "http://chresouce.oss-cn-guangzhou.aliyuncs.com/SlotMultiple01/debug/android/1/AstBackup/Games/Coin Pusher Rich Legend 2001000/Images/pop_loading_bg.png";
            //lodBG.url = "https://gips2.baidu.com/it/u=4160611580,2154032802&fm=3028&app=3028&f=JPEG&fmt=auto?w=720&h=1280";

            if (!true)
            {
                string url0 = "http://chresouce.oss-cn-guangzhou.aliyuncs.com/SlotMultiple01/debug/android/1/AstBackup/Games/Coin Pusher Rich Legend 2001000/Images/pop_loading_bg.png";
                FileLoaderManager.Instance.LoadImageAsTexture(url0, (Texture2D texture) =>
                {
                    NTexture nTexture = new NTexture(texture);

                    lodBG.texture = nTexture;

                    //lodBG.fill = FillType.Scale;                                  
                    //lodBG.fill = FillType.ScaleFree;      // 等比缩放，可能留白                
                });
            }
            else
            {
                lodBG.url = "";
                lodBG.texture = null;
            }



            //Debug.LogError($"@@ lodBG.url = {lodBG.url}");
            //lodBG.url = ApplicationSettings.Instance.logoUrl;

            txtTitle = goOwnerPage.GetChild("title").asTextField;
            txtTitle.text = "";

            pbLoading.value = 0;
            txtMsg.text = "";

            //Timers.inst.Remove(Update);
            //Timers.inst.Add(0.1f, 0, Update);

   
            if (inParams != null)
            {
                var inp = inParams as InParamsPopupSystemLoading;
                txtTitle.text = inp.title;
                if (!string.IsNullOrEmpty(inp.url))
                {
                    FileLoaderManager.Instance.LoadImageAsTexture(inp.url, (Texture2D texture) =>
                    {
                        NTexture nTexture = new NTexture(texture);

                        lodBG.texture = nTexture;

                        //lodBG.fill = FillType.Scale;                                  
                        //lodBG.fill = FillType.ScaleFree;      // 等比缩放，可能留白                
                    });
                }

                /*
                Dictionary<string, object> argDic = null;
                argDic = (Dictionary<string, object>)inParams.value;
                if (argDic.ContainsKey("title"))
                {
                    txtTitle.text = (string)argDic["title"];
                }
                if (argDic.ContainsKey("url"))
                {
                    string url = (string)argDic["url"];

                    if (!string.IsNullOrEmpty(url))
                    {
                        FileLoaderManager.Instance.LoadImageAsTexture(url, (Texture2D texture) =>
                        {
                            NTexture nTexture = new NTexture(texture);

                            lodBG.texture = nTexture;

                            //lodBG.fill = FillType.Scale;                                  
                            //lodBG.fill = FillType.ScaleFree;      // 等比缩放，可能留白                
                        });

                        ///Debug.LogError($"url = {url}");
                        //lodBG.url = url;  //unity3d 无法直接加载http图片
                    }
                }
                */
            }
        }
    }
}