using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ReverseTicTacToeGame
{
    internal class Board
    {
        private readonly int r_Size; 
        private static char[,] m_Board;
     
        HashSet<(int, int)> m_FreeSpots;
        private const int k_MinBoardSize = 3;
        private const int k_MaxBoardSize = 9;
        internal const char k_Empty = (char)0;
        private const char k_Circle = 'O';
        private const char k_Cross = 'X';
        private const int Q_sign = -1;

        public Board(int i_Size)
        {
            r_Size = i_Size;
            m_Board = new char[i_Size, i_Size];
            m_FreeSpots=new HashSet<(int, int)>();
            for(int i = 1; i < i_Size; i++)
            {
                for(int j = 1; j < i_Size; j++)
                {
                    m_FreeSpots.Add((i, j));
                }
            }
        }

        public int Size
        {
            get { return r_Size; }
        }
        public HashSet<(int, int)> FreeSpotsInBoard
        {
            get { return m_FreeSpots; }
        }
        public char[,] GameBoard
        {
            get { return m_Board; }
            set { m_Board = value; }
        }
        public string BoardToSting
        {
            get { return ToStringBoard(); }
        }

        internal void SetValueOnBoard(int i_Row, int i_Column, char i_Symbol)
        {
            if(i_Row != Q_sign && i_Column != Q_sign)
            {
                m_Board[i_Row, i_Column] = i_Symbol;
                m_FreeSpots.Remove((i_Row, i_Column));

            }
        }

        internal bool IsEmptySpot(int i_Row, int i_Column)
        {
            bool isEmptySpot = m_Board[i_Row, i_Column] == (char)0;
            return isEmptySpot;
        }

        internal string ToStringBoard()
        {

            StringBuilder resultedString = new StringBuilder();
            for (int i = 1; i < this.r_Size ; i++)
            {
                resultedString.Append($"  {i}  ");
            }

            resultedString.AppendLine();
            for (int row = 1; row < r_Size ; row++)
            {
                resultedString.Append($"{row}|");
                for (int col = 1; col < r_Size ; col++)
                {
                    char current = m_Board[row, col];
                    if(current == k_Empty)
                    {
                        resultedString.Append($"  {current} |");    
                    }
                    else
                    {
                        resultedString.Append($" {current} |");
                    }
                    
                }

                resultedString.AppendLine("");
                resultedString.Append(" "); 
                for (int col = 1; col < r_Size; col++)
                {
                    resultedString.Append($"=====");
                }

                resultedString.AppendLine();
            }

            return resultedString.ToString();
        }

        internal bool IsFull()
        {
            return this.m_FreeSpots.Count == 0;
        }

    }
}
