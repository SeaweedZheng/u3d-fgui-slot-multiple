using FairyGUI;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ConsoleSlot98000000
{
    public class MultipleGameRecordView : IVMultipleGameRecord
    {
        GComponent ui;

        GComponent goSearch;

        GLoader gldGameRecord;

        GRichTextField txtSearchValue;

        public void InitParam(GComponent u)
        {
            ui = u;

            goSearch = ui.GetChild("search").asCom;
            txtSearchValue = goSearch.GetChild("value").asRichTextField;
            txtSearchValue.onClick.Clear();
            txtSearchValue.onClick.Add(OnShearchGameRecorf);
            txtSearchValue.text = $"#{I18nMgr.T("All")}";

            GComponent goDelete = goSearch.GetChild("delete").asCom;
            goDelete.onClick.Clear();
            goDelete.onClick.Add((context) =>
            {
                context.StopPropagation(); // 停止事件冒泡(不起作用)
                OnClickButtonDelete();
            });


            gldGameRecord = ui.GetChild("gameRecordTemplate").asLoader;
        }

        void OnClickButtonDelete()
        {
            DebugUtils.LogError($"【Test】:清除");
            SelectGameRecordFilterInfo newFilterInfo = new SelectGameRecordFilterInfo();

            if (JsonConvert.SerializeObject(newFilterInfo) !=
                JsonConvert.SerializeObject(curFilterInfo))
            {
                txtSearchValue.text = $"#{I18nMgr.T("All")}";
                curFilterInfo = newFilterInfo;
                SelectGameRecordPageInfo pageInfo = new SelectGameRecordPageInfo()
                {
                    totalCountPerPage = totalCountPerPage,
                    selectNumberPage = 1,
                };

                onSelectGameRecord(curFilterInfo, pageInfo);
            }
        }


        async void OnShearchGameRecorf()
        {
            DebugUtils.LogError($"【Test】:点击查找");
            List<InParamItemSelectOption> options = new List<InParamItemSelectOption>();
            InParamItemSelectOption op;

            op = new InParamItemSelectOption();
            op.selectType = nameof(totalGameFilterOptions.gameTypes);
            op.selectName = I18nMgr.T("Game Type:");
            op.selectKey = curFilterInfo.selectedIndexGameType.ToString();
            op.selectContent.Add("-1", I18nMgr.T("All"));
            for (int i = 0; i < totalGameFilterOptions.gameTypes.Count; i++)
            {
                op.selectContent.Add(i.ToString(), I18nMgr.T(totalGameFilterOptions.gameTypes[i])); //"id:200"
            }
            options.Add(op);


            op = new InParamItemSelectOption();
            op.selectType = nameof(totalGameFilterOptions.gameIds);
            op.selectName = I18nMgr.T("Game ID:");
            op.selectKey = curFilterInfo.selectedIndexGameId.ToString();
            op.selectContent.Add("-1", I18nMgr.T("All"));
            for (int i=0;i< totalGameFilterOptions.gameIds.Count; i++)
            {
                op.selectContent.Add(i.ToString(), I18nMgr.T($"{totalGameFilterOptions.gameIds[i]}") ); //"id:200"
            }
            options.Add(op);

            op = new InParamItemSelectOption();
            op.selectType = nameof(totalGameFilterOptions.turnTypes);
            op.selectName = I18nMgr.T("Turn Type:");
            op.selectKey = curFilterInfo.selectedIndexTurnType.ToString();
            op.selectContent.Add("-1", I18nMgr.T("All"));
            for (int i = 0; i < totalGameFilterOptions.turnTypes.Count; i++)
            {
                op.selectContent.Add(i.ToString(), I18nMgr.T(totalGameFilterOptions.turnTypes[i])); //"id:200"
            }
            options.Add(op);

            op = new InParamItemSelectOption();
            op.selectType = nameof(totalGameFilterOptions.hitJackpotTypes);
            op.selectName = I18nMgr.T("Hit Jackpot Types:");
            op.selectKey = curFilterInfo.selectedIndexHitJackpotTypes.ToString();
            op.selectContent.Add("-1", I18nMgr.T("All"));
            for (int i = 0; i < totalGameFilterOptions.hitJackpotTypes.Count; i++)
            {
                op.selectContent.Add(i.ToString(), I18nMgr.T(totalGameFilterOptions.hitJackpotTypes[i])); //"id:200"
            }
            options.Add(op);

            op = new InParamItemSelectOption();
            op.selectType = nameof(totalGameFilterOptions.hitBonusTypes);
            op.selectName = I18nMgr.T("Hit Bonus Types:");
            op.selectKey = curFilterInfo.selectedIndexHitBonusTypes.ToString();
            op.selectContent.Add("-1", I18nMgr.T("All"));
            for (int i = 0; i < totalGameFilterOptions.hitBonusTypes.Count; i++)
            {
                op.selectContent.Add(i.ToString(), I18nMgr.T(totalGameFilterOptions.hitBonusTypes[i])); //"id:200"
            }
            options.Add(op);

            op = new InParamItemSelectOption();
            op.selectType = nameof(totalGameFilterOptions.fullDates);
            op.selectName = I18nMgr.T("Date:");
            op.selectKey = curFilterInfo.selectedIndexDate.ToString();
            op.selectContent.Add("-1", I18nMgr.T("All"));
            for (int i = 0; i < totalGameFilterOptions.fullDates.Count; i++)
            {
                op.selectContent.Add(i.ToString(), I18nMgr.T(totalGameFilterOptions.fullDates[i])); //"id:200"
            }


            OutParamsBase res = await PageManager.Instance.OpenPageAsync(PageName.ConsoleSlot98000000PopupConsoleSearch,
                new InParamsPopupConsoleSearch()
                {
                    title = I18nMgr.T("Search Record"),
                    options = options,
                });
            if (res != null && res.code == 0)
            {
                string showFilterName = "";

                var result = res as OutParamsPopupConsoleSearch;

                SelectGameRecordFilterInfo filterInfo = new SelectGameRecordFilterInfo();

                foreach(var item in result.selectResult)
                {
                    switch (item.Key)
                    {
                        case nameof(totalGameFilterOptions.gameTypes):
                            {
                                int selIndex = int.Parse(item.Value);
                                filterInfo.selectedIndexGameType = selIndex;
                                if (selIndex != -1)
                                {
                                    showFilterName += I18nMgr.T(totalGameFilterOptions.gameTypes[selIndex]); 
                                }
                            }
                            break;
                        case nameof(totalGameFilterOptions.gameIds):
                            {
                                int selIndex = int.Parse(item.Value);
                                filterInfo.selectedIndexGameId =  selIndex;
                                if (selIndex != -1)
                                {
                                    showFilterName += "/";
                                    showFilterName += totalGameFilterOptions.gameIds[selIndex].ToString();
                                }
                            }
                            break;
                        case nameof(totalGameFilterOptions.turnTypes):
                            {
                                int selIndex = int.Parse(item.Value);
                                filterInfo.selectedIndexTurnType = selIndex;
                                if (selIndex != -1)
                                {
                                    showFilterName += "/";
                                    showFilterName += I18nMgr.T(totalGameFilterOptions.turnTypes[selIndex]);
                                }
                            }
                            break;
                        case nameof(totalGameFilterOptions.hitJackpotTypes):
                            {
                                int selIndex = int.Parse(item.Value);
                                filterInfo.selectedIndexHitJackpotTypes = selIndex;
                                if (selIndex != -1)
                                {
                                    showFilterName += "/";
                                    showFilterName += I18nMgr.T(totalGameFilterOptions.hitJackpotTypes[selIndex]);
                                }
                            }
 
                            break;
                        case nameof(totalGameFilterOptions.hitBonusTypes):
                            {
                                int selIndex = int.Parse(item.Value);
                                filterInfo.selectedIndexHitBonusTypes = selIndex;
                                if (selIndex != -1)
                                {
                                    showFilterName += "/";
                                    showFilterName += I18nMgr.T(totalGameFilterOptions.hitBonusTypes[selIndex]);
                                }
                            }

                            break;
                        case nameof(totalGameFilterOptions.fullDates):
                            {
                                int selIndex = int.Parse(item.Value);
                                filterInfo.selectedIndexDate = selIndex;
                                if (selIndex != -1)
                                {
                                    showFilterName += "/";
                                    showFilterName += totalGameFilterOptions.fullDates[selIndex];
                                }
                            }
                            break;
                    }
                }
                curFilterInfo = filterInfo;

                txtSearchValue.text = string.IsNullOrEmpty(showFilterName)? $"#{I18nMgr.T("All")}" : "#"+ showFilterName;

                SelectGameRecordPageInfo pageInfo = new SelectGameRecordPageInfo()
                {
                    totalCountPerPage = totalCountPerPage,
                    selectNumberPage = 1,
                };
                onSelectGameRecord(curFilterInfo, pageInfo);
            }

        }


        public event Action<SelectGameRecordFilterInfo, SelectGameRecordPageInfo> onSelectGameRecord;

        public event Action onClickNext;
        public event Action onClickPrev;

        public void ClearAll()
        {
            selectNumberPage = 1;
            totalGameFilterOptions = null;
        }





        SelectGameRecordFilterInfo curFilterInfo;
        TotalGameFilterOptions totalGameFilterOptions;
        public const int totalCountPerPage = 1;
        public int selectNumberPage = 1;
        public SelectGameRecordPageInfo SetDefaultSelect()
        {
            return new SelectGameRecordPageInfo()
            {
                totalCountPerPage = totalCountPerPage,
                selectNumberPage = selectNumberPage,
            };
        }


        public void SetTotalGameFilterOptions(TotalGameFilterOptions Filter)
        {
            totalGameFilterOptions = Filter;
        }

        public void SetContent(SelectGameRecordPageResult content)
        {

        }
    }


}