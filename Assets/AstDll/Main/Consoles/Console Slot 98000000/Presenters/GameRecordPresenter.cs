using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IVGameRecordRecord
{

    event Action<string,long,string,string,string,long,long> onSelectClassify;  //游戏类型，游戏id,本局类型，中奖类型，开始时间，结束时间

    event Action<int> onSelectDate;
    event Action onClickNext;
    event Action onClickPrev;


    /// <summary>
    /// 清除所有内容（包括：页尾、内容、日期）
    /// </summary>
    void ClearAll();
    void SetDateList(List<string> date);
    void SetSelectDateIndex(int index);
    void SetContent(TableGameRecordItem content, int curPageIndex, int pageCount);
}
public class GameRecordPresenter
{

    IVGameRecordRecord _view;

    List<string> dropdownDateLstGameRecord;
    int curPageIndex = 0;
    int pageCount = 1;
    const string FORMAT_DATE_DAY = "yyyy-MM-dd";
    TableGameRecordItem curGame;

    public void InitParam(IVGameRecordRecord view)
    {
        if (this._view != null)
        {
            this._view.onSelectDate -= OnDropdownChangedGameRecord;
            this._view.onClickPrev -= OnClickPrevPage;
            this._view.onClickNext -= OnClickNextPage;
        }

        this._view = view;
        this._view.onSelectDate += OnDropdownChangedGameRecord;
        this._view.onClickPrev += OnClickPrevPage;
        this._view.onClickNext += OnClickNextPage;


        InitGameRecordInfo();
    }

    void InitGameRecordInfo()
    {


        string sql = $"SELECT created_at FROM {ConsoleTableName.TABLE_GAME_RECORD}";

        List<long> date = new List<long>();
        dropdownDateLstGameRecord = new List<string>();
        SQLiteHelper.Instance.ExecuteQueryAfterOpenDB(sql, (SqliteDataReader sdr) =>
        {
            while (sdr.Read())
            {
                long d = sdr.GetInt64(0);
                date.Add(d);
            }
            foreach (long timestamp in date)
            {
                //DebugUtil.Log($"时间搓：{timestamp}");
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
                //DateTime utcDateTime = dateTimeOffset.UtcDateTime;
                //string time = utcDateTime.ToString(FORMAT_DATE_DAY);

                DateTime localDateTime = dateTimeOffset.LocalDateTime;
                string time = localDateTime.ToString(FORMAT_DATE_DAY);

                if (!dropdownDateLstGameRecord.Contains(time))
                {
                    //dropdownDateLstGameRecord.Add(time);
                    dropdownDateLstGameRecord.Insert(0, time);
                    //DebugUtil.Log($"时间搓：{timestamp} 时间 ：{time}");
                }
            }
            dropdownDateLstGameRecord.Insert(0, I18nMgr.T("All"));

            _view.ClearAll();
            if (dropdownDateLstGameRecord.Count > 0)
            {
                _view.SetDateList(dropdownDateLstGameRecord);
                OnDropdownChangedGameRecord(0);
            }
        });

    }


    static readonly string[] GAME_TYPE_LIST = new string[] { "ALL", "slot", "coin_pusher"}; // new string[]{ "ALL", "slot", "coin_pusher", "fish_machine" };

    // 选类型，选

    void GetData(string gameType = null, long? gameId = null, string turnType = null, string hitJackpotType = null, string hitBonusType = null, long? startTime = null, long? endTime = null)
    {
        bool isAll = gameType==null && gameId==null && turnType==null && hitJackpotType==null && hitBonusType == null &&  startTime == null && endTime==null;

        GetTotalFilterCondition(gameType, gameId, turnType, hitJackpotType, hitBonusType, startTime, endTime);

        string sql = $"SELECT created_at FROM {ConsoleTableName.TABLE_GAME_RECORD}";

        if (!isAll && !string.IsNullOrEmpty(totalFilterCondition))
        {
            sql = sql + $" WHERE {totalFilterCondition}"; // WHERE type
        }


        List<long> date = new List<long>();
        dropdownDateLstGameRecord = new List<string>();
        SQLiteHelper.Instance.ExecuteQueryAfterOpenDB(sql, (SqliteDataReader sdr) =>
        {
            while (sdr.Read())
            {
                long d = sdr.GetInt64(0);
                date.Add(d);
            }
            foreach (long timestamp in date)
            {
                //DebugUtil.Log($"时间搓：{timestamp}");
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
                //DateTime utcDateTime = dateTimeOffset.UtcDateTime;
                //string time = utcDateTime.ToString(FORMAT_DATE_DAY);

                DateTime localDateTime = dateTimeOffset.LocalDateTime;
                string time = localDateTime.ToString(FORMAT_DATE_DAY);

                if (!dropdownDateLstGameRecord.Contains(time))
                {
                    //dropdownDateLstGameRecord.Add(time);
                    dropdownDateLstGameRecord.Insert(0, time);
                    //DebugUtil.Log($"时间搓：{timestamp} 时间 ：{time}");
                }
            }
            dropdownDateLstGameRecord.Insert(0, I18nMgr.T("All"));

            _view.ClearAll();
            if (dropdownDateLstGameRecord.Count > 0)
            {
                _view.SetDateList(dropdownDateLstGameRecord);
                OnDropdownChangedGameRecord(0);
            }
        });

    }




    List<TableGameRecordItem> resGameRecord;


    int idxDate = 0;


    /// <summary>
    /// 修改日期时
    /// </summary>
    /// <param name="index"></param>
    void OnDropdownChangedGameRecord(int index)
    {
        idxDate = index;

        if (index == 0)  // ALL
        {
            SQLiteHelper.Instance.OpenDB(ConsoleTableName.DB_NAME, (connect) =>
            {
                string sql = $"SELECT COUNT(*) FROM sqlite_master WHERE type ='table' and name='{ConsoleTableName.TABLE_GAME_RECORD}';";
                //创建命令
                var command = connect.CreateCommand();
                command.CommandText = sql;
                int count = Convert.ToInt32(command.ExecuteScalar());

                //如果结果为1则表示存在该表格
                bool isExists = count == 1;

                if (isExists)
                {
                    sql = $"SELECT COUNT(*) FROM {ConsoleTableName.TABLE_GAME_RECORD}";
                    DebugUtils.Log(sql);
                    using (var command1 = new SqliteCommand(sql, connect))
                    {
                        pageCount = (int)command1.ExecuteScalar(); // 每页一条数据，总页数，就是总条数。
                        curPageIndex = 0;

                        if (pageCount > 0)
                            GetGameData(0);
                    }
                }
                else
                {
                    DebugUtils.Log($"不存在表 {ConsoleTableName.TABLE_GAME_RECORD} ");
                }
            });
        }
        else  // 日期
        {

            string yyyy_MM_dd = dropdownDateLstGameRecord[(int)index];

            string sql2 = $"SELECT * FROM {ConsoleTableName.TABLE_GAME_RECORD} WHERE DATE(DATETIME(created_at / 1000, 'unixepoch', 'localtime')) = '{yyyy_MM_dd}'"; //可以用

            List<string> result = new List<string>();
            SQLiteHelper.Instance.ExecuteQueryAfterOpenDB(sql2, (SqliteDataReader sdr) =>
            {
                resGameRecord = new List<TableGameRecordItem>();
                while (sdr.Read())
                {
                    resGameRecord.Insert(0,
                    new TableGameRecordItem()
                    {
                        user_id = sdr.GetString(sdr.GetOrdinal("user_id")),
                        game_id = sdr.GetInt32(sdr.GetOrdinal("game_id")),
                        game_uid = sdr.GetString(sdr.GetOrdinal("game_uid")),

                        game_type = sdr.GetString(sdr.GetOrdinal("game_type")),
                        turn_type = sdr.GetString(sdr.GetOrdinal("turn_type")),
                        hit_jackpot_type = sdr.GetString(sdr.GetOrdinal("hit_jackpot_type")),
                        hit_bonus_type = sdr.GetString(sdr.GetOrdinal("hit_bonus_type")),
                        //respond = sdr.GetString(sdr.GetOrdinal("respond")),
                        //scene = sdr.GetString(sdr.GetOrdinal("scene")),
                        template_name = sdr.GetString(sdr.GetOrdinal("template_name")),
                        template_data = sdr.GetString(sdr.GetOrdinal("template_data")),
                        //custom_data = sdr.GetString(sdr.GetOrdinal("custom_data")),
                        created_at = sdr.GetInt64(sdr.GetOrdinal("created_at")),
                    });
                }


                curPageIndex = 0;
                pageCount = resGameRecord.Count;

                GetGameRecordWhen(0);
            });

        }
    }

    void GetGameRecordWhen(int index)
    {
        _view.SetContent(resGameRecord[index], curPageIndex, pageCount);
    }



    void GetGameData(long index)
    {
        string sql = $"SELECT * FROM {ConsoleTableName.TABLE_GAME_RECORD} LIMIT 1 OFFSET {pageCount - 1 - index}";

        SQLiteHelper.Instance.ExecuteQueryAfterOpenDB(sql, (SqliteDataReader sdr) =>
        {

            while (sdr.Read())
            {
                curGame = new TableGameRecordItem()
                {
                    user_id = sdr.GetString(sdr.GetOrdinal("user_id")),
                    game_id = sdr.GetInt32(sdr.GetOrdinal("game_id")),
                    game_uid = sdr.GetString(sdr.GetOrdinal("game_uid")),

                    game_type = sdr.GetString(sdr.GetOrdinal("game_type")),
                    turn_type = sdr.GetString(sdr.GetOrdinal("turn_type")),
                    hit_jackpot_type = sdr.GetString(sdr.GetOrdinal("hit_jackpot_type")),
                    hit_bonus_type = sdr.GetString(sdr.GetOrdinal("hit_bonus_type")),
                    //respond = sdr.GetString(sdr.GetOrdinal("respond")),
                    //scene = sdr.GetString(sdr.GetOrdinal("scene")),
                    template_name = sdr.GetString(sdr.GetOrdinal("template_name")),
                    template_data = sdr.GetString(sdr.GetOrdinal("template_data")),
                    //custom_data = sdr.GetString(sdr.GetOrdinal("custom_data")),
                    created_at = sdr.GetInt64(sdr.GetOrdinal("created_at")),
                };
            }

            _view.SetContent(curGame, curPageIndex, pageCount);
        });
    }


    string totalFilterCondition = "1=1";

    // event Action<string, string, string, string, long, long> onSelectClassify;  //游戏类型，游戏id,本局类型，中奖类型，开始时间，结束时间
    private void OnSelectClassify(string gameType, long? gameId, string turnType, string hitJackpotType, string hitBonusType, long? startTime, long? endTime) {
        // 1. 拼接 SQL 基础语句和动态条件
        //string baseSql = $"SELECT * FROM  {ConsoleTableName.TABLE_GAME_RECORD} WHERE 1=1";

        //string baseSql = $"SELECT * FROM  {ConsoleTableName.TABLE_GAME_RECORD} WHERE ";

    }

    string GetTotalFilterCondition(string gameType, long? gameId, string turnType, string hitJackpotType, string hitBonusType, long? startTime, long? endTime) { 


        totalFilterCondition = "";
        // 游戏类型（字符串替换方式）
        if (!string.IsNullOrEmpty(gameType))
        {
            totalFilterCondition += " AND game_type = @GameType";
            totalFilterCondition = totalFilterCondition.Replace("@GameType", EscapeSqlString(gameType));
        }

        // 游戏ID（字符串替换方式）
        if (gameId.HasValue)
        {
            totalFilterCondition += " AND game_id = @GameId";
            totalFilterCondition = totalFilterCondition.Replace("@GameId", gameId.Value.ToString());
        }

        // 回合类型（字符串替换方式）
        if (!string.IsNullOrEmpty(turnType))
        {
            totalFilterCondition += " AND turn_type = @TurnType";
            totalFilterCondition = totalFilterCondition.Replace("@TurnType", EscapeSqlString(turnType));
        }

        // 中奖类型（字符串替换方式）
        if (!string.IsNullOrEmpty(hitJackpotType))
        {
            totalFilterCondition += " AND hit_jackpot_type = @HitJackpotType";
            totalFilterCondition = totalFilterCondition.Replace("@HitJackpotType", EscapeSqlString(hitJackpotType));
        }

        // 奖励类型（字符串替换方式）
        if (!string.IsNullOrEmpty(hitBonusType))
        {
            totalFilterCondition += " AND hit_bonus_type = @HitBonusType";
            totalFilterCondition = totalFilterCondition.Replace("@HitBonusType", EscapeSqlString(hitBonusType));
        }

        // 开始时间（字符串替换方式）
        if (startTime.HasValue)
        {
            totalFilterCondition += " AND created_at >= @StartTime";
            totalFilterCondition = totalFilterCondition.Replace("@StartTime", startTime.Value.ToString());
        }

        // 结束时间（字符串替换方式）
        if (endTime.HasValue)
        {
            totalFilterCondition += " AND created_at <= @EndTime";
            totalFilterCondition = totalFilterCondition.Replace("@EndTime", endTime.Value.ToString());
        }


        if (string.IsNullOrEmpty(totalFilterCondition))
        {
            totalFilterCondition = $" 1=1 {totalFilterCondition}";
        }

        return totalFilterCondition;
    }

    private string EscapeSqlString(string input)
    {
        if (string.IsNullOrEmpty(input))
            return "''";
        // SQLite中字符串用单引号包裹，需要转义输入中的单引号（两个单引号表示一个）
        return "'" + input.Replace("'", "''") + "'";
    }

    private void OnClickNextPage()
    {
        if (curPageIndex + 1 >= pageCount)
            return;

        curPageIndex++;


        if (idxDate == 0)
            GetGameData(curPageIndex); //全部
        else
            GetGameRecordWhen(curPageIndex); //日期
    }
    private void OnClickPrevPage()
    {
        if (curPageIndex <= 0)
            return;

        curPageIndex--;

        if (idxDate == 0)
            GetGameData(curPageIndex); //全部
        else
            GetGameRecordWhen(curPageIndex); //日期
    }
}





