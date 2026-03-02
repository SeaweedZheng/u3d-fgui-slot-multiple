using SlotMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if false
/// <summary>
/// 弃用
/// </summary>
public class PayLineUtils 
{
    
    public static string GetHashCode(List<Cell> lineCells)
    {

        int rowHash = 0;
        int corHash = 0;

        foreach (Cell cell in lineCells)
        {
            rowHash += cell.rowIndex;
            corHash += cell.columnIndex;
        }
        return $"{rowHash}{corHash}{lineCells.Count}";
    }
}
#endif