using System;
using System.Runtime.CompilerServices;

namespace ReverseTicTacToeGame
{
    internal class Board
    {
        private readonly byte r_Size; 
        private static char[,] m_Board;
        private char[,] m_FreeSpots; // tuple ?? 
        private const byte k_MinBoardSize = 3;
        private const byte k_MaxBoardSize = 9;

        public Board(byte i_Size)
        {
            r_Size = i_Size;
            m_Board = new char[i_Size, i_Size];
            m_FreeSpots = new char[i_Size, i_Size];
        }

        public byte Size
        {
            get { return r_Size; }
        }

        public char[,] GameBoard
        {
            get { return m_Board; }
            set { m_Board = value; }
        }


        internal static void SetValueOnBoard(byte i_Row, byte i_Column, char i_Symbol)
        {

            if (IsEmptySpot(i_Row, i_Column)&& IsPointIsInRange(i_Row,i_Column))
            {
                m_Board[i_Row, i_Column] = i_Symbol;

            }

        }


       

        internal static bool IsEmptySpot(byte i_Row, byte i_Column)
        {
            return m_Board[i_Row,i_Column] == null ;
        }

        internal static bool IsPointIsInRange(byte i_Row, byte i_Column)
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
