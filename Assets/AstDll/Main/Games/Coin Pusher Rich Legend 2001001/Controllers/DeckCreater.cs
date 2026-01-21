using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;
using SlotMaker;

namespace CoinPusherRichLegend2001001
{
    public class DeckCreater : Singleton<DeckCreater>
    {


        public Dictionary<int, List<int[]>> hitLinesDic = new Dictionary<int, List<int[]>>()
        {
            [1] = new List<int[]>()
        {
            new int[] { 1 },
            new int[] { 2 },
            new int[] { 3 },
            new int[] { 4 },
            new int[] { 5 },
            new int[] { 6 },
            new int[] { 7 },
            new int[] { 8 },
        },
            [2] = new List<int[]>()
        {
            new int[] { 1,7 },
            new int[] { 1,8},
            new int[] { 2,7 },
            new int[] { 2,8 },
            new int[] { 3,7 },
            new int[] { 3,8 },
            new int[] { 4,7 },
            new int[] { 4,8 },
            new int[] { 5,7 },
            new int[] { 5,8 },
            new int[] { 6,7 },
            new int[] { 6,8 },
            new int[] { 7,8 },
            new int[] { 1,4 },
            new int[] { 1,5 },
            new int[] { 1,6 },
            new int[] { 2,4 },
            new int[] { 2,5 },
            new int[] { 2,6 },
            new int[] { 3,4 },
            new int[] { 3,5 },
            new int[] { 3,6 },

            /*
            new int[] { 1,2 },
            new int[] { 1,3 },
            new int[] { 2,3 },
            new int[] { 4,5 },
            new int[] { 4,6 },
            new int[] { 5,6 },
            */
        },
            [3] = new List<int[]>()
        {
            new int[] { 1,7,8 },
            new int[] { 3,7,8 },
            new int[] { 4,7,8 },
            new int[] { 6,7,8 },

            new int[] { 1,4,8 },
            new int[] { 1,5,7 },
            new int[] { 1,5,8 },
            new int[] { 1,6,7 },
            new int[] { 2,4,7 },
            new int[] { 2,4,8 },
            new int[] { 2,5,7 },
            new int[] { 2,5,8 },
            new int[] { 2,6,7 },
            new int[] { 2,6,8 },
            new int[] { 3,4,7 },
            new int[] { 3,5,8 },
            new int[] { 3,6,8 },

            new int[] { 1,2,5 },
            new int[] { 1,3,4 },
            new int[] { 1,3,6 },
            new int[] { 1,4,6 },
            new int[] { 2,3,5 },
            new int[] { 2,4,5 },
            new int[] { 2,5,6 },
            new int[] { 3,4,6 },
        },


            [4] = new List<int[]>()
        {
            new int[] { 1,4,7,8 },
            new int[] { 1,6,7,8 },
            new int[] { 3,4,7,8 },
            new int[] { 3,6,7,8 },

            new int[] { 1,2,4,8 },
            new int[] { 1,2,6,7 },
            new int[] { 1,4,5,8 },
            new int[] { 1,5,6,7 },
            new int[] { 2,3,4,7 },
            new int[] { 2,3,6,8 },
            new int[] { 3,4,5,7 },
            new int[] { 3,5,6,8 },

            new int[] { 1,3,4,6 },
        },

            [5] = new List<int[]>()
        {
            new int[] { 1,3,5,7,8 },
            new int[] { 2,4,6,7,8 },

            new int[] { 1,2,4,5,8 },
            new int[] { 1,2,5,6,7 },
            new int[] { 2,3,4,5,7 },
            new int[] { 2,3,5,6,8 },
        },

            [6] = new List<int[]>()
        {
            new int[] { 1,2,4,6,7,8 },
            new int[] { 1,3,4,5,7,8 },
            new int[] { 1,3,5,6,7,8 },
            new int[] { 2,3,4,6,7,8 },
        },
        };


        //1,2,3
        //4,5,6
        //7,8,9

        int[] GetRowColIndex(int posNumber)
        {
            switch (posNumber)
            {
                case 1:
                    return new int[] { 0, 0 };
                case 2:
                    return new int[] { 0, 1 };
                case 3:
                    return new int[] { 0, 2 };
                case 4:
                    return new int[] { 1, 0 };
                case 5:
                    return new int[] { 1, 1 };
                case 6:
                    return new int[] { 1, 2 };
                case 7:
                    return new int[] { 2, 0 };
                case 8:
                    return new int[] { 2, 1 };
                case 9:
                    return new int[] { 2, 2 };
            }
            return null;
        }

        JSONArray nodeLines;

        const int miniNumber = 1;
        const int MaxNumber = 13;


        public void SetNodLines(string str)
        {
            nodeLines = JSONArray.Parse(str) as JSONArray;
        }




        public DeskInfo CreatNotWinDeck()
        {
            DeskInfo info = new DeskInfo();
            info.winList = null;

            Dictionary<int, int> excludeSymbols = new Dictionary<int, int>();


            List<List<int>> RowColDeck = new List<List<int>>();

            for (int r = 0; r < 3; r++)
            {
                List<int> row = new List<int>();
                for (int c = 0; c < 3; c++)
                {
                    row.Add(-1);
                }
                RowColDeck.Add(row);
            }

            for (int numb = miniNumber; numb < MaxNumber + 1; numb++)
            {
                excludeSymbols.Add(numb, 2);
            }

            List<int> middleSymbol = new List<int>();

            for (int r = 0; r < 3; r++)
            {
                int number = -1;
                do
                {
                    int nb = Random.Range(miniNumber, MaxNumber + 1);
                    if (excludeSymbols.ContainsKey(nb) && excludeSymbols[nb] > 0)
                    {
                        number = nb;
                        --excludeSymbols[nb];

                        middleSymbol.Add(nb);
                    }
                } while (number == -1);
                RowColDeck[1][r] = number;
            }


            for (int c = 0; c < 3; c++)
            {
                int number = -1;
                do
                {
                    int nb = Random.Range(miniNumber, MaxNumber + 1);
                    if (excludeSymbols.ContainsKey(nb) && excludeSymbols[nb] > 0)
                    {
                        number = nb;
                        --excludeSymbols[nb];

                        middleSymbol.Add(nb);
                    }
                } while (number == -1);
                RowColDeck[c][1] = number;
            }


            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (RowColDeck[r][c] == -1)
                    {
                        int number = -1;
                        do
                        {
                            int nb = Random.Range(miniNumber, MaxNumber + 1);
                            if (!middleSymbol.Contains(nb)
                                && excludeSymbols.ContainsKey(nb)
                                && excludeSymbols[nb] > 0)
                            {
                                number = nb;
                                --excludeSymbols[nb];
                            }
                        } while (number == -1);
                        RowColDeck[r][c] = number;
                    }
                }
            }

            info.deckRowCol = RowColDeck;
            return info;
        }



        public DeskInfo CreatDeck(int hitSymbolNumber, int hitLineCount)
        {
            DeskInfo info = new DeskInfo();
            List<SymbolWin> winList = new List<SymbolWin>();

            int targetIndex = Random.Range(0, hitLinesDic[hitLineCount].Count);
            int[] lines = hitLinesDic[hitLineCount][targetIndex];

            List<List<int>> RowColDeck = new List<List<int>>();

            for (int r = 0; r < 3; r++)
            {
                List<int> row = new List<int>();
                for (int c = 0; c < 3; c++)
                {
                    row.Add(-1);
                }
                RowColDeck.Add(row);
            }


            // 中线赋值
            for (int n = 0; n < lines.Length; n++)
            {
                int lineNumber = lines[n];
                int lineIndex = lineNumber - 1;
                JSONArray arr = nodeLines[lineIndex] as JSONArray;

                SymbolWin sw = new SymbolWin()
                {
                    symbolNumber = hitSymbolNumber,
                    lineNumber = lineNumber,
                };
                List<Cell> hitCells = new List<Cell>();

                foreach (JSONNode item in arr)
                {


                    int[] rc = GetRowColIndex((int)item);
                    int rowIndex = rc[0];
                    int colIndex = rc[1];

                    Cell cl = new Cell()
                    {
                        columnIndex = colIndex,
                        rowIndex = rowIndex,
                    };
                    hitCells.Add(cl);

                    if (RowColDeck[rowIndex][colIndex] == -1)
                    {
                        RowColDeck[rowIndex][colIndex] = hitSymbolNumber;
                    }
                }
                sw.cells = hitCells;

                winList.Add(sw);
            }

            // 允许的图标，每个图标最多2个
            Dictionary<int, int> excludeSymbols = new Dictionary<int, int>();

            for (int numb = miniNumber; numb < MaxNumber + 1; numb++)
            {
                if (numb == hitSymbolNumber)
                    continue;
                excludeSymbols.Add(numb, 2);
            }


            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (RowColDeck[r][c] == -1)
                    {
                        int number = -1;
                        do
                        {
                            int nb = Random.Range(miniNumber, MaxNumber + 1);
                            if (excludeSymbols.ContainsKey(nb) && excludeSymbols[nb] > 0)
                            {
                                number = nb;
                                --excludeSymbols[nb];
                            }
                        } while (number == -1);
                        RowColDeck[r][c] = number;
                    }
                }
            }

            info.deckRowCol = RowColDeck;
            info.winList = winList;

            return info;
        }
    }


    public class DeskInfo
    {
        public List<List<int>> deckRowCol;
        public List<SymbolWin> winList;
    }

}