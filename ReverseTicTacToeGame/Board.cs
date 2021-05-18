using System.Collections.Generic;

namespace ReverseTicTacToeGame
{
    internal class Board
    {
        private readonly int r_Size;
        private ePlayersMark[,] m_Board;

        readonly HashSet<(int, int)> m_FreeSpots;
        private const int k_QSign = -1;

        public Board(int i_Size)
        {
            r_Size = i_Size;
            m_Board = new ePlayersMark[i_Size, i_Size];
            m_FreeSpots = new HashSet<(int, int)>();
            for(int i = 1; i < i_Size; i++)
            {
                for(int j = 1; j < i_Size; j++)
                {
                    m_FreeSpots.Add((i, j));
                    m_Board[i, j] = ePlayersMark.Empty;
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
        public ePlayersMark[,] GameBoard
        {
            get { return m_Board; }
            set { m_Board = value; }
        }

        internal void SetValueOnBoard(int i_Row, int i_Column, ePlayersMark i_Symbol)
        {
            if(i_Row != k_QSign && i_Column != k_QSign)
            {
                m_Board[i_Row, i_Column] = i_Symbol;
                m_FreeSpots.Remove((i_Row, i_Column));
            }
        }

        internal bool IsEmptySpot(int i_Row, int i_Column)
        {
            bool isEmptySpot = m_Board[i_Row, i_Column] == ePlayersMark.Empty;

            return isEmptySpot;
        }

        internal bool IsFull()
        {
            return this.m_FreeSpots.Count == 0;
        }
    }
}