using System;

namespace ReverseTicTacToeGame
{
    internal class Board
    {
        private readonly byte r_Size; // enum ?? 
        private char[,] m_Board;
        private char[,] m_FreeSpots; // tuple ?? 
    

        public Board(byte i_Size)
        {
            r_Size = i_Size;
            m_Board = new char[i_Size, i_Size];
            m_FreeSpots = new char[i_Size, i_Size];
        }

        public int Size
        {
            get { return r_Size; }
        }

        public char[,] GameBoard
        {
            get { return m_Board; }
        }

        internal static void SetValueOnBoard((int,int) i_pointToSet, char i_Symbol)
        {
           
            
            if(this.IsEmptySpot(i_pointToSet(0),i_pointToSet[1]))
            {
                m_Board[i_Row, i_Row] = i_Symbol;
            }

        }

      

        internal void ClearBoardForAnotherGame()
        {
            // TODO
        }

        internal bool IsEmptySpot(byte i_Row, byte i_Column)
        {
            //TODO
            return false;
        }

        internal bool IsInRange(byte i_Row, byte i_Column)
        {
            // TODO
            return false;
        }

        internal bool IsFull()
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
