using System;


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
                bool thisMoveCouseTie = false;
                while(!thisMoveCouseWin && !hasTheUserEnterQ && !thisMoveCouseTie)
                {
                    switch(currentPlaying)
                    {
                        case eTurnOf.Player1:
                            {
                               point= ConsoleUI.GetValidSpotInBoard();
                               if(isQsignInPoint(point))
                               {
                                    ///TODO
                                    /// need to be sure we quit by Q and not by won
                                    hasTheUserEnterQ = true;
                                    break;
                               }
                               isEmptySpot = isThisEmptySpot(point);
                               while(!isEmptySpot)
                               {
                                   point=ConsoleUI.GetNewValidSpotInBoard(point);
                                   isEmptySpot = isThisEmptySpot(point);
                               }

                               setPointatboard(m_Player1.Sign,point);
                               thisMoveCouseWin = thereIsWin(point);
                               if(thisMoveCouseWin)
                                 
                                  break;
                              //there is win
                              else if(thereIsTie(point))
                               {
                                   thisMoveCouseTie = true;
                                   break;
                               }
                              {
                                  currentPlaying = m_Player2.IsComputer ? eTurnOf.Computer : eTurnOf.Player2;
                              }
                              break;
                            }
                        case eTurnOf.Player2:
                            {
                                break;
                            }
                        case eTurnOf.Computer:
                            /// there is a problem - need to know who is the player that play right now
                            {
                                point = findAnEmptySpotInBoard();
                              ///  setPointatboard();
                                break;
                            }
                            if(currentPlaying == eTurnOf.Player1)
                            {
                                m_Player1.NumberOfWins++;
                            }
                            if (currentPlaying == eTurnOf.Player1)

                    }
                }
                ///TODO
                /// print stistics
                wantToPlayFlag = ConsoleUI.isUserWantAnotherGame();
            }
        }

        private static bool thereIsTie((int, int) point)
        {
            throw new NotImplementedException();
        }

        private static void setPointatboard((int, int) point, char sign)
        {
            throw new NotImplementedException();
        }

        private static (int, int) findAnEmptySpotInBoard()
        {
            /// TODO
            /// this is the move of the computer
            throw new NotImplementedException();
        }

        private static bool thereIsWin((int, int) i_Point)
        {
            ///TODO
            /// check if the last move couse win in the board
            throw new NotImplementedException();
        }

        private static void setPointatboard(char i_PlayerSign, (int,int) i_point)
        {
            ///TODO
            /// drew this plyersign in the spesific spot in board
            throw new NotImplementedException();
        }

        private static bool isQsignInPoint((int, int) i_Spot)
        {
            ///TODO
            /// Check if Q has showed - for now we will define it at (-1,-1) point
            throw new NotImplementedException();
        }

        private static bool isThisEmptySpot((int, int) i_Spot)
        {
            ///Todo
            /// check if the spot at the spesific board is empty
            throw new NotImplementedException();
        }
    }
}
