using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using static ReverseTicTacToeGame.Enums;


namespace ReverseTicTacToeGame
{
    internal class Board
    {
        private readonly int r_Size; 
        private static eSignsOfPlayers[,] s_Board;
     
        HashSet<(int, int)> m_FreeSpots;
        private const int k_MinBoardSize = 3;
        private const int k_MaxBoardSize = 9;
        internal const char k_Empty = (char)0;
        private const char k_Circle = 'O';
        private const char k_Cross = 'X';
        private const int k_QSign = -1;
  


        public Board(int i_Size)
        {
            r_Size = i_Size;
            s_Board = new eSignsOfPlayers[i_Size, i_Size];
            m_FreeSpots=new HashSet<(int, int)>();
            for(int i = 1; i < i_Size; i++)
            {
                for(int j = 1; j < i_Size; j++)
                {
                    m_FreeSpots.Add((i, j));
                    s_Board[i, j] = eSignsOfPlayers.Empty;
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
        public eSignsOfPlayers[,] GameBoard
        {
            get { return s_Board; }
            set { s_Board = value; }
        }
     

        internal void SetValueOnBoard(int i_Row, int i_Column, eSignsOfPlayers i_Symbol)
        {
            if(i_Row != k_QSign && i_Column != k_QSign)
            {
                s_Board[i_Row, i_Column] = i_Symbol;
                m_FreeSpots.Remove((i_Row, i_Column));

            }
        }

        internal bool IsEmptySpot(int i_Row, int i_Column)
        {
            bool isEmptySpot = s_Board[i_Row, i_Column] == eSignsOfPlayers.Empty;
            return isEmptySpot;
        }


        internal bool IsFull()
        {
            return this.m_FreeSpots.Count == 0;
        }

    }
}
