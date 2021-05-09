﻿using System;


namespace ReverseTicTacToeGame
{
    internal class GameLogic
    {
        private Board m_GameBoard;
        private static Player m_Player1;
        private static Player m_Player2;
        internal const char k_Empty = (char)0;
        internal const char k_Circle = 'O';
        internal const char k_Cross = 'X';
        public enum eTurnOf
        {
            Player1, Player2, Computer
        }

        public GameLogic(byte i_BoardSize, bool i_Player1IsComputer, bool i_Player2IsComputer)
        {
      
            m_GameBoard = new Board(i_BoardSize);
            m_Player1 = new Player(k_Cross, i_Player1IsComputer);
            m_Player2 = new Player(k_Circle, i_Player2IsComputer);
            StartGame();
        }


        ///TODO
        /// loop1 until user doesnt want another game
        ///     loop2 until we get Q or somone is won
        ///            is computer turn? yes-
        ///                 dont need to ask for play - do it autmticly
        ///                no -
        ///                 ask for a valid (in range) play from ConsoleUi
        ///                 check if the cell is empty 
        ///  
        private static void StartGame()
        {
         
            bool wantToPlayFlag = true;

            
            eTurnOf currentPlaying;
            currentPlaying = m_Player1.IsComputer ? eTurnOf.Computer : eTurnOf.Player1;
            (int, int) point;
            while (wantToPlayFlag==true)
            {
                bool thisMoveCouseWin = false;
                bool hasTheUserEnterQ = false;
                bool isEmptySpot = true;
                while(!thisMoveCouseWin || !hasTheUserEnterQ)
                {
                    switch(currentPlaying)
                    {
                        case eTurnOf.Player1:
                            {
                               point= ConsoleUI.GetValidSpotInBoard();
                               if(isQsignInSpot(point))
                               {
                                   ///TODO
                                   /// need to be sure we quit by Q and not by won
                                   break;
                               }
                               isEmptySpot = isThisEmptySpot(point);
                               while(!isEmptySpot)
                               {
                                   point=ConsoleUI.GetNewValidSpotInBoard(point);
                                   isEmptySpot = isThisEmptySpot(point);
                               }

                               setPointatboard(m_Player1.Sign);
                               thisMoveCouseWin = thereIsWin(point);
                              if(thisMoveCouseWin)
                                  break;
                              //there is win
                              else
                              {
                                  currentPlaying = m_Player2.IsComputer ? eTurnOf.Computer : eTurnOf.Player2;
                              }
                            }
                        case eTurnOf.Player2:
                            {
                                break;
                            }
                        case eTurnOf.Computer:
                            {
                                break;
                            }
                    }
                }
                ///TODO
                /// print stistics
                wantToPlayFlag = ConsoleUI.isUserWantAnotherGame();
            }
        }

        private static bool thereIsWin((int, int) i_Point)
        {
            throw new NotImplementedException();
        }

        private static void setPointatboard(char i_PlayerSign)
        {
            throw new NotImplementedException();
        }

        private static bool isQsignInSpot((int, int) i_Spot)
        {
            throw new NotImplementedException();
        }

        private static bool isThisEmptySpot((int, int) i_Spot)
        {
            throw new NotImplementedException();
        }
    }
}
