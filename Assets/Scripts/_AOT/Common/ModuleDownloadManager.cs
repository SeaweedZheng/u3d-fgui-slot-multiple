using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public partial class ModuleDownloadManager : MonoBehaviour
{

    static ModuleDownloadManager instance;
    public static ModuleDownloadManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(ModuleDownloadManager)) as ModuleDownloadManager;
            }
            return instance;
        }
    }




    /*
    public void DownLoadWebMod(string moduleName,Action<string> onSuccess, Action<string> onError)
    {
        StartCoroutine(CoDownLoadWebMod(moduleName, onSuccess, onError));
    }

    public IEnumerator CoDownLoadWebMod(string moduleName,
    //Action<string> onDownloadStart, Action<string> onDownloadEnd, Action<string> onProgress,
    Action<string> onSuccess, Action<string> onError)
    {

        //下载远程modeVer 文件到内存
        string modVversionFileWebUrl = PathHelper.GetModuleVersionWEBURL(moduleName);

        //JObject webModVerNode = null;

        // 非cdn加载
        UnityWebRequest reqModVerFile = UnityWebRequest.Get(modVversionFileWebUrl + $"?t={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}");
        yield return reqModVerFile.SendWebRequest();
        if (reqModVerFile.result == UnityWebRequest.Result.Success)
        {
            string tvStr = reqModVerFile.downloadHandler.text;
            //webModVerNode = JObject.Parse(tvStr);
            onSuccess?.Invoke(tvStr);
        }
        else
        {
            onError?.Invoke($"download mod ver file fail. url:{modVversionFileWebUrl}");
            yield break;
        }
    }
    */





    /// <summary>
    /// 
    /// </summary>
    /// <param name="moduleName"></param>
    /// <param name="needHash"></param>
    /// <param name="onDownloadStart"> 只记录最父级模块的下载开始，不记录子模块的下载开始</param>
    /// <param name="onDownloadEnd"> 只记录最父级模块的下载结束，不记录子模块的下载结束</param>
    /// <param name="onProgress">记载父级模块和子级模块下载过程</param>
    /// <param name="onFinish">该模块检测完成，可能热更完成，也可能不需要热更</param>
    /// <param name="onError"></param>
    /// <returns></returns>
    /// <remarks>
    /// *本地已存在版本是否已是远端版本？
    /// *下载远程modeVer 文件到内存
    /// *对比本地和内存中远程版本文件hash
    /// *下载依赖（不进行拷贝，等所有模块都下载完成，才一同拷贝覆盖资源）
    /// *下载dll
    /// *下载ab
    /// *下载backup
    /// *保存内存版本文件到临时目录
    /// *[X] 拷贝版本文件
    /// ==================
    /// *不进行拷贝，等所有该下载的文件下载完成，才进行拷贝
    /// (因为拷贝失败，也是要求能进入机台继续玩。不同于手机网络游戏)
    /// </remarks>
    public IEnumerator DownLoadNeedModToTemp(string moduleName, string needHash,
        Action<string> onDownloadStart, Action<string> onDownloadEnd, Action<string> onProgress,
        Action<string> onFinish, Action<string> onError)
    {

        bool isError = false;
        string msg = "";

        Action<string> errorFunc = (m) =>
        {
            isError = true;
            msg = m;
        };

        onDownloadStart?.Invoke($"start download mod :{moduleName}");


#if false
        // 继续上次下载
        string tempLoclModVerPth = PathHelper.GetTmpModuleVersionLOCPTH(moduleName);
        if (!string.IsNullOrEmpty(needHash) && File.Exists(tempLoclModVerPth))
        {
            string content = File.ReadAllText(tempLoclModVerPth);  
            JObject tmpLocModVerNode = JObject.Parse(content);
            if (tmpLocModVerNode["hash"].Value<string>() == needHash)
            {
                // 规定先下载依赖模块再下载父级模块，如果父级模块已经在下载临时路劲中，则代表下载完成。
                // 例如上次下一半，重新下载
                onFinish?.Invoke($"finish mod check: {moduleName}");
                yield break;
            }
        }
#endif

        //本地版，本满足要求的版本则无需下载
        string loclModVerPth = PathHelper.GetModuleVersionLOCPTH(moduleName);
        JObject locModVerNode = null;
        if (File.Exists(loclModVerPth))
        {
            locModVerNode = JObject.Parse(File.ReadAllText(loclModVerPth));
        }

        if (!string.IsNullOrEmpty(needHash)  && locModVerNode != null)
        {
            if (locModVerNode["hash"].Value<string>() == needHash)
            {
                //检查依赖
                foreach (KeyValuePair<string, JToken> kv in (JObject)locModVerNode["dependencies"])
                {
                    JObject node = kv.Value as JObject;

                    // 先下载依赖
                    yield return DownLoadNeedModToTemp(kv.Key, node["hash"].Value<string>(),
                            null, null, onProgress,
                            null,
                            errorFunc);
                }

                if (isError)
                {
                    onError?.Invoke(msg);
                    yield break;
                }


                onDownloadEnd?.Invoke($"end download mod :{moduleName}");
                onFinish?.Invoke($"finish mod check: {moduleName}");
                yield break;
            }
        }


        //下载远程modeVer 文件到内存
        string modVversionFileWebUrl = PathHelper.GetModuleVersionWEBURL(moduleName);

        JObject webModVerNode = null;

        // 非cdn加载
        UnityWebRequest reqModVerFile = UnityWebRequest.Get(modVversionFileWebUrl + $"?t={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}");
        yield return reqModVerFile.SendWebRequest();
        if (reqModVerFile.result == UnityWebRequest.Result.Success)
        {
            string tvStr = reqModVerFile.downloadHandler.text;
            webModVerNode = JObject.Parse(tvStr);
        }
        else
        {
            onError?.Invoke($"download mod ver file fail. url:{modVversionFileWebUrl}");
            yield break;
        }

        // 远端hash不匹配
        if (!string.IsNullOrEmpty(needHash) && webModVerNode["hash"].Value<string>() != needHash)
        {
            onError?.Invoke($"web mod version hash is not match: {moduleName}  need hash:{needHash}");
            yield break;
        }


        //下载依赖
        foreach (KeyValuePair<string, JToken> kv in (JObject)webModVerNode["dependencies"])
        {
            JObject node = kv.Value as JObject;

            yield return DownLoadNeedModToTemp(kv.Key, node["hash"].Value<string>(),
                    null, null, onProgress,
                    null, errorFunc);
        }

        if (isError)
        {
            onError?.Invoke(msg);
            yield break;
        }



        // 下载dll
        JObject localAssetDllNode = locModVerNode != null ? (locModVerNode["asset_dll"] as JObject) : null;
        JObject webAssetDllNode = webModVerNode["asset_dll"] as JObject;
        if (webAssetDllNode != null && webAssetDllNode.Count > 0)
        {
            foreach (KeyValuePair<string, JToken> kv in webAssetDllNode)
            {
                string targetHash = (kv.Value["hash"]).Value<string>();
                if (localAssetDllNode != null
                    && localAssetDllNode.ContainsKey(kv.Key)
                    && localAssetDllNode[kv.Key]["hash"].Value<string>() == targetHash
                    )
                {
                    continue;
                }

                string url = PathHelper.GetDllWEBURL(kv.Key);
                onProgress?.Invoke($"download mod {moduleName}/asset_dll: {url}");

                yield return DownloadAssetOnce(
                    url,
                    targetHash,
                    PathHelper.GetTempDllLOCPTH(kv.Key),
                    (str) => { }, errorFunc);
            }
        }

        if (isError)
        {
            onError?.Invoke(msg);
            yield break;
        }

        // 下载ab
        JObject locAssetbundleNode = locModVerNode != null ?
            (locModVerNode["asset_bundle"]["bundles"] as JObject) : null;
        JObject webAssetbundleNode = webModVerNode["asset_bundle"]["bundles"] as JObject;
        if (webAssetbundleNode != null && webAssetbundleNode.Count > 0)
        {
            foreach (KeyValuePair<string, JToken> kv in webAssetbundleNode)
            {

                string targetHash = (kv.Value["hash"]).Value<string>();
                if (locAssetbundleNode != null
                    && locAssetbundleNode.ContainsKey(kv.Key)
                    && locAssetbundleNode[kv.Key]["hash"].Value<string>() == targetHash
                    )
                {
                    continue;
                }

                string url = PathHelper.GetAssetBundleWEBPTH(kv.Key);
                onProgress?.Invoke($"download mod {moduleName}/asset_bundle: {url}");

                yield return DownloadAssetOnce(
                    url,
                    targetHash,
                    PathHelper.GetTempAssetBundleLOCPTH(kv.Key),
                    (str) => { }, errorFunc);
            }
        }

        if (isError)
        {
            onError?.Invoke(msg);
            yield break;
        }

        // 下载backup
        JObject locAssetbackupNode = locModVerNode != null ?
            (locModVerNode["asset_backup"] as JObject) : null;
        JObject webAssetbackupNode = webModVerNode["asset_backup"] as JObject;
        if (webAssetbackupNode != null && webAssetbackupNode.Count > 0)
        {
            foreach (KeyValuePair<string, JToken> kv in webAssetbackupNode)
            {

                string targetHash = (kv.Value["hash"]).Value<string>();
                if (locAssetbackupNode != null
                    && locAssetbackupNode.ContainsKey(kv.Key)
                    && locAssetbackupNode[kv.Key]["hash"].Value<string>() == targetHash
                    )
                {
                    continue;
                }

                string url = PathHelper.GetAssetBackupWEBURL(kv.Key);
                onProgress?.Invoke($"download mod {moduleName}/asset_bundle: {url}");

                yield return DownloadAssetOnce(
                    url,
                    targetHash,
                    PathHelper.GetTempAssetBackupLOCPTH(kv.Key),
                    (str) => { }, errorFunc);
            }
        }

        if (isError)
        {
            onError?.Invoke(msg);
            yield break;
        }

        // “模块版本”文件写入临时缓存路劲中
        FileUtils.WriteAllText(PathHelper.GetTmpModuleVersionLOCPTH(moduleName), webModVerNode.ToString());


        onDownloadEnd?.Invoke($"end download mod :{moduleName}");
        onFinish?.Invoke($"finish download mod: {moduleName}");
    }





    /// <summary>
    /// 查看是否有新热更完整的资源缓存
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// * 每次最后，都有删除临时缓存
    /// </remarks>
    public IEnumerator CopyTempWebHotfixFileToTargetDir(Action<string> onProgress = null)
    {
        // 是否有热更新文件需要拷贝
        if (PlayerPrefs.HasKey(HotfixState.HOTFIX_STATE) && PlayerPrefs.GetString(HotfixState.HOTFIX_STATE) == HotfixState.HotfixCopying)
        {
            //开始拷贝
            yield return FileUtils.CopyDirectoryAsync(PathHelper.tmpHotfixDirLOCPTH, PathHelper.hotfixDirLOCPTH, onProgress);

            PlayerPrefs.SetString(HotfixState.HOTFIX_STATE, HotfixState.HotfixCompleted);
        }

        // 删除缓存
        if (Directory.Exists(PathHelper.tmpHotfixDirLOCPTH))
        {
            yield return FileUtils.DeleteDirectoryAsync(PathHelper.tmpHotfixDirLOCPTH);
        }
    }



    string throwErrMsg = "";

    private IEnumerator DownloadAssetOnce(string url, string needMd5, string savePth, Action<string> onDownloadProgress, Action<string> onError)
    {

        onDownloadProgress?.Invoke($"download asset: {url}");

        /*
        if (File.Exists(savePth))
        {
            if (needMd5 == FileUtils.CalculateFileMD5(savePth))
            {
                yield break;
            }
        }
        */

        // cdn 加载
        UnityWebRequest req = UnityWebRequest.Get(url); //ApplicationSettings.Instance.hotfixUrl
        yield return req.SendWebRequest();
        if (req.result == UnityWebRequest.Result.Success)
        {
            string md5 = FileUtils.CalculateMD5(req.downloadHandler.data);

            if (md5 == needMd5)
            {
                FileUtils.WriteAllBytes(savePth, req.downloadHandler.data);
                yield break;
            }
        }

        // 非cdn加载
        UnityWebRequest req01 = UnityWebRequest.Get(url + $"?t={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}");
        yield return req01.SendWebRequest();
        if (req01.result == UnityWebRequest.Result.Success)
        {
            string md5 = FileUtils.CalculateMD5(req01.downloadHandler.data);

            if (md5 == needMd5)
            {
                FileUtils.WriteAllBytes(savePth, req01.downloadHandler.data);

                //【这块先隐藏】 s_assetDatas[dallame] = req01.downloadHandler.data;
                yield break;
            }
        }

        //

        throwErrMsg = $"download asset '{url}' is fail";
        //throw new Exception(throwErrMsg);

        PlayerPrefs.SetString(HotfixState.HOTFIX_STATE, HotfixState.HotfixDownloadFail);
        onError?.Invoke(throwErrMsg);
    }










    // 热更AB资源
    public IEnumerator DownloadManifestToTemp(Action<string> onSuccess, Action<string> onError)
    {

        using (UnityWebRequest req = UnityWebRequest.Get(PathHelper.mainfestWEBURL + $"?t={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}"))
        {
            yield return req.SendWebRequest();
            if (req.isNetworkError || req.isHttpError)
            {
                Debug.LogError(req.error);
                onError?.Invoke(req.error);
                yield break;
            }
            else
            {
                FileUtils.WriteAllBytes(PathHelper.tmpMainfestLOCPTH, req.downloadHandler.data);

                onSuccess?.Invoke(null);
            }
        }
    }


}


