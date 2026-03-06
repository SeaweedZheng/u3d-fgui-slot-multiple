using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TableGameRecordItem : MonoBehaviour
{
    public long id;

    /// <summary> 玩家id </summary>
    public string user_id = "";

    /// <summary> 游戏id </summary>
    public long game_id;

    /// <summary> 游戏类型 </summary>
    public string game_type = "slot";  //  "slot", "coin_pusher"  "fish_machine"

    /// <summary> 游戏奖励类型 </summary>
    public string turn_type = "";  //【新版本用】 "spin", "free_spin" "free_spin_trigger" "bonus_minigame"

    /// <summary> 算法卡原始数据 </summary>
    public string respond;

    /// <summary> 本剧游戏guid </summary>
    public string game_uid;

    /// <summary> 自定义场景数据（json） </summary>
    public string scene;

    /// <summary> 通用模板 </summary>
    public string template_name;

    /// <summary> 通用模板数据（json） </summary>
    public string template_data;

    /// <summary> 自定义数据 </summary>
    public string custom_data = "{}";

    /// <summary> 创建时间 </summary>
    public long created_at;
}