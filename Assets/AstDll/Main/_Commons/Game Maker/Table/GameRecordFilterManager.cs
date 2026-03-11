using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;


public class GameRecordFilterManager : Singleton<GameRecordFilterManager>
{


    public void GetAllFilterEnumValues(Action<GameFilterEnumValues> onFinish)
    {

        GameFilterEnumValues filterValues = new GameFilterEnumValues();

        int count = 6;
        Action callback = () =>
        {
            if (--count ==0)
            {
                onFinish?.Invoke(filterValues);
            }
        };

        GetDistinctLongValues("game_id", (val) =>
       {
           filterValues.gameIds = val;
           callback?.Invoke();
       });
        GetDistinctStringValues("game_type", (val) =>
        {
            filterValues.gameTypes = val;
            callback?.Invoke();
        });
        GetDistinctStringValues("turn_type", (val) =>
        {
            filterValues.turnTypes = val;
            callback?.Invoke();
        });
        GetDistinctStringValues("hit_jackpot_type", (val) =>
       {
           filterValues.hitJackpotTypes = val;
           callback?.Invoke();
       });
        GetDistinctStringValues("hit_bonus_type", (val) =>
        {
            filterValues.hitBonusTypes = val;
            callback?.Invoke();
        });

        GetDistinctFullDates((val) =>
        {
            filterValues.fullDates = val;
            callback?.Invoke();
        });
    }





    /// <summary>
    /// 通用方法：查询指定字符串字段的所有唯一值（过滤NULL和空字符串）
    /// </summary>
    private void GetDistinctStringValues(string fieldName, Action<List<string> >onFinishCallback)
    {
        List<string> values = new List<string>();
        // 去重 + 过滤NULL/空字符串 + 排序
        string sql = $"SELECT DISTINCT {fieldName} FROM {ConsoleTableName.TABLE_GAME_RECORD} WHERE {fieldName} IS NOT NULL AND {fieldName} != '' ORDER BY {fieldName}";

        SQLiteHelper.Instance.ExecuteQueryAfterOpenDB(sql, (SqliteDataReader sdr) =>
        {
            while (sdr.Read())
            {
                string val = sdr.GetString(0).Trim(); // 去除首尾空格，避免无效重复
                if (!string.IsNullOrEmpty(val))
                {
                    values.Insert(0,val);
                }
            }
            onFinishCallback?.Invoke(values);
        });

        /*
        using (IDbCommand cmd = conn.CreateCommand())
        {
            cmd.CommandText = sql;
            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string val = reader.GetString(0).Trim(); // 去除首尾空格，避免无效重复
                    if (!string.IsNullOrEmpty(val))
                    {
                        values.Add(val);
                    }
                }
            }
        }
        return values;
        */
    }



    private void  GetDistinctLongValues(string fieldName, Action<List<long>> onFinishCallback)
    {
        List<long> values = new List<long>();
        // 去重 + 过滤NULL + 排序
        string sql = $"SELECT DISTINCT {fieldName} FROM  {ConsoleTableName.TABLE_GAME_RECORD}  WHERE {fieldName} IS NOT NULL ORDER BY {fieldName}";

        SQLiteHelper.Instance.ExecuteQueryAfterOpenDB(sql, (SqliteDataReader sdr) =>
        {
            while (sdr.Read())
            {
                values.Insert(0, sdr.GetInt64(0));
            }

            onFinishCallback?.Invoke(values);
        });

    }

    const string FORMAT_DATE_DAY = "yyyy-MM-dd";
    private void GetDistinctFullDates(Action<List<string>> onFinishCallback)
    {
        List<string> fullDates = new List<string>();

        // 查询所有非空的 created_at 时间戳
        string sql = $"SELECT DISTINCT created_at FROM  {ConsoleTableName.TABLE_GAME_RECORD}  WHERE created_at IS NOT NULL ORDER BY created_at";


        SQLiteHelper.Instance.ExecuteQueryAfterOpenDB(sql, (SqliteDataReader sdr) =>
        {
            while (sdr.Read())
            {
                // 1. 转换时间戳（毫秒级）为本地时间
                long timestamp = sdr.GetInt64(0);

                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
                //DateTime utcDateTime = dateTimeOffset.UtcDateTime;
                //string time = utcDateTime.ToString(FORMAT_DATE_DAY);

                DateTime localDateTime = dateTimeOffset.LocalDateTime;
                string fullDate = localDateTime.ToString(FORMAT_DATE_DAY);

                // 3. 去重添加（避免重复日期）
                if (!fullDates.Contains(fullDate))
                {
                    fullDates.Add(fullDate);
                }
            }

            onFinishCallback?.Invoke(fullDates);
        });

    }


}





public class GameFilterEnumValues
{
    // 核心字段枚举值
    public List<long> gameIds { get; set; } = new List<long>();          // game_id 所有可能值
    public List<string> gameTypes { get; set; } = new List<string>();   // game_type 所有可能值
    public List<string> turnTypes { get; set; } = new List<string>();   // turn_type 所有可能值
    public List<string> hitJackpotTypes { get; set; } = new List<string>(); // hit_jackpot_type 所有可能值
    public List<string> hitBonusTypes { get; set; } = new List<string>();  // hit_bonus_type 所有可能值（修正笔误）

    // 日期维度：仅保留 yyyy-MM-dd 格式的完整日期列表
    public List<string> fullDates { get; set; } = new List<string>();   // 所有出现过的完整日期（yyyy-MM-dd）
}