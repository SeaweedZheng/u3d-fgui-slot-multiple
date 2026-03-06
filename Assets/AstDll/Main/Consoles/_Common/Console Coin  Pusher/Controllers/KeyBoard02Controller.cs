using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace CommonConsoleCoinPusher
{
    public class KeyBoard02Controller : IKeyboard
    {
        public static string signMI = "<img src='ui://ConsoleCoinPusher97000000/icon-asterisk' />";
        public KeyBoard02Controller() { Init(); }
        void Init() { }

        const int NUM = 10;

        public void ClickNext()
        {
            if (!isCanOnClick)
                return;

            if (++curIndexKeyboard >= glstCurKeyboard.numChildren)
                curIndexKeyboard = 0;

            for (int i = 0; i < glstCurKeyboard.numChildren; i++)
            {
                glstCurKeyboard.GetChildAt(i).asCom.GetChild("icon").asImage.visible = i == curIndexKeyboard;
            }

        }
        public void ClickPrev()
        {
            if (!isCanOnClick)
                return;

            if (--curIndexKeyboard < 0)
                curIndexKeyboard = glstCurKeyboard.numChildren - 1;

            for (int i = 0; i < glstCurKeyboard.numChildren; i++)
            {
                glstCurKeyboard.GetChildAt(i).asCom.GetChild("icon").asImage.visible = i == curIndexKeyboard;
            }

        }


        public void ClickDown()
        {

            if (!isCanOnClick)
                return;

            curIndexKeyboard += NUM;
            if (curIndexKeyboard >= glstCurKeyboard.numChildren)
                curIndexKeyboard -= NUM;

            SetCursorVisiable(curIndexKeyboard);

        }

        void SetAllCursorInvisible()
        {
            for (int i = 0; i < glstCurKeyboard.numChildren; i++)
            {
                glstCurKeyboard.GetChildAt(i).asCom.GetChild("icon").asImage.visible = false;
            }
        }
        void SetCursorVisiable(int index)
        {
            curIndexKeyboard = index;
            for (int i = 0; i < glstCurKeyboard.numChildren; i++)
            {
                glstCurKeyboard.GetChildAt(i).asCom.GetChild("icon").asImage.visible = i == curIndexKeyboard;
            }
        }

        public void SetZeroCursorVisiable()
        {
            /*
            GList keyboard = new GList();
            switch (UseWhatKeyboard)
            {
                case 0:
                    keyboard = glstkeyboard;
                    break;
                case 1:
                    keyboard = glstkeyboardZiFu;
                    break;
                case 2:
                    keyboard = glstkeyboardDaXie;
                    break;
                case 3:
                    keyboard = glstkeyboardXiaoXie;
                    break;
            }
            glstCurKeyboard = keyboard;
            */

            curIndexKeyboard = 0;
            for (int i = 0; i < glstCurKeyboard.numChildren; i++)
            {
                if (i == 0)
                {
                    glstCurKeyboard.GetChildAt(i).asCom.GetChild("icon").asImage.visible = true;
                }
                else
                {
                    glstCurKeyboard.GetChildAt(i).asCom.GetChild("icon").asImage.visible = false;
                }

            }
        }


        public void ClickUp()
        {

            if (!isCanOnClick)
                return;

            curIndexKeyboard -= NUM;
            if (curIndexKeyboard < 0)
                curIndexKeyboard += NUM;

            SetCursorVisiable(curIndexKeyboard);
        }


        bool IsKBReturn(string data) => data == "Return" || data == "返回";
        bool IsKBConfirm(string data) => data == "Confirm" || data == "确认";
        bool IsKBClear(string data) => data == "Clear" || data == "清空";
        bool IsKBDelete(string data) => data == "Delete" || data == "删除";




        public void ClickConfirm()
        {
            if (!isCanOnClick)
                return;

            GList keyboard = new GList();
            switch (UseWhatKeyboard)
            {
                case 0:
                    keyboard = glstkeyboard;
                    break;
                case 1:
                    keyboard = glstkeyboardZiFu;
                    break;
                case 2:
                    keyboard = glstkeyboardDaXie;
                    break;
                case 3:
                    keyboard = glstkeyboardXiaoXie;
                    break;
            }

            string data = keyboard.GetChildAt(curIndexKeyboard).asCom.GetChild("title").text;

            //if (curIndexKeyboard == keyboard.numChildren - 1) // exit
            if (IsKBReturn(data))
            {
                onClickExitCallback?.Invoke();
            }
            else if (IsKBConfirm(data)) //else if (curIndexKeyboard == keyboard.numChildren - 2) // ok
            {
                onClickOKCallback?.Invoke(inputResult);
            }
            else if (IsKBDelete(data)) //else if (curIndexKeyboard == keyboard.numChildren - 3) //delete
            {
                if (curIndexInput >= 0)
                {
                    if (--curIndexInput < 0)
                    {
                        curIndexInput = 0;
                        return;
                    }
                    else
                    {
                        inputResult = inputResult.Substring(0, inputResult.Length - 1);
                        glstInput.GetChildAt(curIndexInput).text = "";
                    }

                }
            }
            else if (IsKBClear(data)) // else if (curIndexKeyboard == keyboard.numChildren - 4) //clear
            {
                ClearKeyboard(false);
            }
            else
            {

                if (curIndexInput == glstInput.numChildren)
                {
                    return;
                }
                else
                {
                    string data0 = keyboard.GetChildAt(curIndexKeyboard).asCom.GetChild("title").text;
                    switch (data0)
                    {
                        case "Space":
                        case "空格":
                            data0 = " ";
                            break;
                        case "#+=":
                        case "字符":
                            OpenWhatKeyborad(1);
                            return;
                        case "abc":
                        case "字母":
                        case "小写":
                            OpenWhatKeyborad(3);
                            return;
                        case "123":
                        case "数字":
                            OpenWhatKeyborad(0);
                            return;
                        case "ABC":
                        case "大写":
                            OpenWhatKeyborad(2);
                            return;
                    }
                    glstInput.GetChildAt(curIndexInput).text = isPlaintext ? data : signMI;
                    inputResult += data0;
                    curIndexInput++;
                }
            }

        }


        Action<string> onClickOKCallback;
        Action onClickExitCallback;


        public GComponent goOwnerKeyboard;
        GList glstInput, glstkeyboard, glstkeyboardZiFu, glstkeyboardDaXie, glstkeyboardXiaoXie, glstCurKeyboard;
        GLabel labTip;


        int curIndexKeyboard = 0;
        int curIndexInput = 0;

        int UseWhatKeyboard = 0;

        bool isPlaintext = true;
        public bool isCanOnClick;
        string inputResult = "";

        public void ClearKeyboard(bool isClearAllArrow)
        {
            curIndexKeyboard = 0;
            curIndexInput = 0;
            inputResult = "";

            if (isClearAllArrow)
            {
                SetAllCursorInvisible();
            }
            else
            {
                SetCursorVisiable(curIndexKeyboard);
            }

            for (int i = 0; i < glstInput.numChildren; i++)
            {
                glstInput.GetChildAt(i).asLabel.title = "";
            }
        }
        public void InitParam(GComponent gKB, bool isPlaintext, Action<string> onClickOKCallback, Action onClickExitCallback)
        {
            if (gKB == null) return;

            goOwnerKeyboard = gKB;
            this.isPlaintext = isPlaintext;
            this.onClickOKCallback = onClickOKCallback;
            this.onClickExitCallback = onClickExitCallback;

            GObject[] childs = goOwnerKeyboard.GetChildren();
            foreach (GObject child in childs)
            {
                DebugUtils.Log("--" + child.name);
            }

            glstkeyboard = goOwnerKeyboard.GetChild("buttons").asList;
            glstkeyboardZiFu = goOwnerKeyboard.GetChild("buttonsZiFu").asList;
            glstkeyboardDaXie = goOwnerKeyboard.GetChild("buttonsDaXie").asList;
            glstkeyboardXiaoXie = goOwnerKeyboard.GetChild("buttonsXiaoXie").asList;
            glstInput = goOwnerKeyboard.GetChild("input").asList;
            GComponent compTip = goOwnerKeyboard.GetChild("tip").asCom;
            GRichTextField rtxtTip = compTip.GetChild("title").asRichTextField;
            rtxtTip.text = "";
            labTip = compTip.asLabel;
            labTip.title = "";

            InitKeyboardButttonClickEventAll();



            isCanOnClick = false;
            OpenWhatKeyborad(0);
            ClearKeyboard(true);
            /*
            ClearKeyboard(true);
            UseWhatKeyboard = 0;
            SetZeroCursorVisiable(0);
            glstkeyboard.visible = true;
            glstkeyboardZiFu.visible = false;
            glstkeyboardDaXie.visible = false;
            glstkeyboardXiaoXie.visible = false;
            isCanOnClick = false;
            */
        }



        public void EnableKeyboard()
        {
            ClearKeyboard(false);
            isCanOnClick = true;
        }

        public void DisableKeyboard()
        {
            ClearKeyboard(true);
            isCanOnClick = false;
        }



        public void InitKeyboardButtonClickEvent(int n)
        {
            GList keyboard = new GList();
            switch (n)
            {
                case 0:
                    keyboard = glstkeyboard;
                    break;
                case 1:
                    keyboard = glstkeyboardZiFu;
                    break;
                case 2:
                    keyboard = glstkeyboardDaXie;
                    break;
                case 3:
                    keyboard = glstkeyboardXiaoXie;
                    break;
            }
            glstCurKeyboard = keyboard;

            for (int i = 0; i < glstCurKeyboard.numChildren; i++)
            {
                int index = i;

                string data = glstCurKeyboard.GetChildAt(index).asCom.GetChild("title").text;

                if (IsKBReturn(data))
                {
                    glstCurKeyboard.GetChildAt(index).onClick.Clear();
                    glstCurKeyboard.GetChildAt(index).onClick.Add(() =>
                    {
                        if (!isCanOnClick)
                            return;

                        ClearKeyboard(true);
                        isCanOnClick = false;
                        onClickExitCallback?.Invoke();
                    });
                }
                else if (IsKBClear(data))
                {
                    glstCurKeyboard.GetChildAt(index).onClick.Clear();
                    glstCurKeyboard.GetChildAt(index).onClick.Add(() =>
                    {
                        if (!isCanOnClick)
                            return;
                        ClearKeyboard(false);
                    });
                }
                else if (IsKBConfirm(data))
                {
                    glstCurKeyboard.GetChildAt(index).onClick.Clear();
                    glstCurKeyboard.GetChildAt(index).onClick.Add(() =>
                    {
                        if (!isCanOnClick)
                            return;

                        string res = inputResult;
                        DisableKeyboard();
                        onClickOKCallback?.Invoke(res);
                    });

                }
                else if (IsKBDelete(data))
                {
                    glstCurKeyboard.GetChildAt(index).onClick.Clear();
                    glstCurKeyboard.GetChildAt(index).onClick.Add(() =>
                    {
                        if (!isCanOnClick)
                            return;
                        if (curIndexInput >= 0)
                        {
                            if (--curIndexInput < 0)
                            {
                                curIndexInput = 0;
                                return;
                            }
                            else
                            {
                                inputResult = inputResult.Substring(0, inputResult.Length - 1);
                                glstInput.GetChildAt(curIndexInput).text = "";

                            }
                        }

                        //curIndexKeyboard = index;
                        SetCursorVisiable(index);
                    });
                }
                else
                {
                    glstCurKeyboard.GetChildAt(index).onClick.Clear();
                    glstCurKeyboard.GetChildAt(index).onClick.Add(() =>
                    {
                        if (!isCanOnClick)
                            return;
                        curIndexKeyboard = index;
                        if (curIndexInput == glstInput.numChildren)
                        {
                            return;
                        }
                        else
                        {
                            string data0 = glstCurKeyboard.GetChildAt(curIndexKeyboard).asCom.GetChild("title").text;
                            SetCursorVisiable(curIndexKeyboard);
                            switch (data0)
                            {
                                case "Space":
                                case "空格":
                                    data0 = " ";
                                    break;
                                case "#+=":
                                case "字符":
                                    OpenWhatKeyborad(1);
                                    return;
                                case "abc":
                                case "字母":
                                case "小写":
                                    OpenWhatKeyborad(3);
                                    return;
                                case "123":
                                case "数字":
                                    OpenWhatKeyborad(0);
                                    return;
                                case "ABC":
                                case "大写":
                                    OpenWhatKeyborad(2);
                                    return;
                            }
                            glstInput.GetChildAt(curIndexInput).text = isPlaintext ? data0 : signMI;
                            inputResult += data0;
                            curIndexInput++;

                        }
                    });
                }

            }
        }




        void InitKeyboardButttonClickEventAll()
        {
            InitKeyboardButtonClickEvent(0);
            InitKeyboardButtonClickEvent(1);
            InitKeyboardButtonClickEvent(2);
            InitKeyboardButtonClickEvent(3);
        }


        void OpenWhatKeyborad(int n)
        {
            glstkeyboard.visible = n == 0 ? true : false;
            glstkeyboardZiFu.visible = n == 1 ? true : false;
            glstkeyboardDaXie.visible = n == 2 ? true : false;
            glstkeyboardXiaoXie.visible = n == 3 ? true : false;

            UseWhatKeyboard = n;

            switch (UseWhatKeyboard)
            {
                case 0:
                    glstCurKeyboard = glstkeyboard;
                    break;
                case 1:
                    glstCurKeyboard = glstkeyboardZiFu;
                    break;
                case 2:
                    glstCurKeyboard = glstkeyboardDaXie;
                    break;
                case 3:
                    glstCurKeyboard = glstkeyboardXiaoXie;
                    break;
            }

            SetZeroCursorVisiable();
        }

    }

}
