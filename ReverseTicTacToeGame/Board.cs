using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ReverseTicTacToeGame
{
    internal class Board
    {
        private readonly int r_Size; 
        private static char[,] m_Board;
     
        HashSet<(int, int)> m_FreeSpots;
        private const int k_MinBoardSize = 3;
        private const int k_MaxBoardSize = 9;

        public Board(int i_Size)
        {
            r_Size = i_Size;
            m_Board = new char[i_Size, i_Size];

            for(int i = 1; i < i_Size; i++)
            {
                for(int j = 0; j < i_Size; j++)
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


        internal void SetValueOnBoard(int i_Row, int i_Column, char i_Symbol)
        {

            if (IsEmptySpot(i_Row, i_Column)==true && IsPointIsInRange(i_Row,i_Column)==true)
            {
                m_Board[i_Row, i_Column] = i_Symbol;

            }

            m_FreeSpots.Remove((i_Row, i_Column));


        }


       

        internal bool IsEmptySpot(int i_Row, int i_Column)
        {
            return m_Board[i_Row,i_Column] == null ;
        }

        internal bool IsPointIsInRange(int i_Row, int i_Column)
        {
            
            return (i_Row>=k_MinBoardSize && i_Row<=k_MaxBoardSize) && (i_Column >= k_MinBoardSize && i_Column <= k_MaxBoardSize);
        }

        internal static bool IsFull()
        {
            // TODO
            // check if the m_FreeSpots is empty
            return false;
        }
        
        
        
        //
        //constructor of the object board ,include :
        // 1. matrix in between 3-9 cells - V
        // 2. array with empty places - V
        // 3. size of the metrix - V
        // 4. print board - 
        //
        //method to write X or O to the place we need on the board?
        //
        //  clear the board for new game
        // internal | public | private - V 
        // readonly - V




    }
}

//// gameLogic :
/// 1. board
/// 2. validation
/// 3. game logic and stats
///
///
/// ui:
/// 1. gamemanger
/// 2.
///

/// 
