using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SlotMaker
{
    [Serializable]
    public class Cell
    {
        /// <summary> 列索引 </summary>
        public int columnIndex;
        /// <summary> 行索引 </summary>
        public int rowIndex;
        public Cell()
        {
            columnIndex = 0;
            rowIndex = 0;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnIndex">列索引</param>
        /// <param name="rowIndex">行索引</param>
        public Cell(int columnIndex, int rowIndex)
        {
            this.columnIndex = columnIndex;
            this.rowIndex = rowIndex;
        }

        public override int GetHashCode() { return GetHashCode(columnIndex, rowIndex); }

        public static int GetHashCode(int columnIndex, int rowIndex)
        {
            return columnIndex * 10000 + rowIndex;
        }
    }
}