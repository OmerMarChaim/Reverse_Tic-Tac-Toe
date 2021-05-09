using System;


namespace ReverseTicTacToeGame
{
    internal class GameLogic
    {
        private Board m_GameBoard;
        private Player m_Player1;
        private Player m_Player2;
        internal const char k_Empty = (char)0;
        internal const char k_Circle = 'O';
        internal const char k_Cross = 'X';
        public GameLogic(byte i_BoardSize, bool i_Player1IsComputer, bool i_Player2IsComputer)
        {
      
            m_GameBoard = new Board(i_BoardSize);
            m_Player1 = new Player(k_Cross, i_Player1IsComputer);
            m_Player2 = new Player(k_Circle, i_Player2IsComputer);
            StartGame();
        }

        private static void StartGame()
        {
            ///TODO
            /// loop1 until user doesnt want another game
            ///     loop2 until we get Q or somone is won
            ///            is computer turn? yes-
            ///                 dont need to ask for play - do it autmticly
            ///                no -
            ///                 ask for a valid (in range) play from ConsoleUi
            ///                 check if the cell is empty 
            ///  
            bool wantToPlayFlag = true;
        
            while (wantToPlayFlag==true)
            {
                bool thisMoveCouseWin = false;
                bool hasTheUserEnterQ = false;
                while(!thisMoveCouseWin || !hasTheUserEnterQ)
                {
                    
                }
                ///TODO
                /// print stistics
                wantToPlayFlag = ConsoleUI.isUserWantAnotherGame();
            }
        }
    }
}
