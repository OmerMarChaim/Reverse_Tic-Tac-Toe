using System;


namespace ReverseTicTacToeGame
{
    internal class GameLogic
    {
        private Board m_GameBoard;
        private static Player m_Player1;
        private static Player m_Player2;
        private static gameState m_currentGameState;
        internal const char k_Empty = (char)0;
        internal const char k_Circle = 'O';
        internal const char k_Cross = 'X';

        public enum eTurnOf
        {
            Player1,
            Player2
        }

        public enum gameState
        {
            Playing,
            Win,
            Tie,
            Quit
        }


        public GameLogic(byte i_BoardSize, bool i_Player1IsComputer, bool i_Player2IsComputer)
        {

            m_GameBoard = new Board(i_BoardSize);
            m_Player1 = new Player(k_Cross, i_Player1IsComputer);
            m_Player2 = new Player(k_Circle, i_Player2IsComputer);
            m_currentGameState = gameState.Playing;
        }

        public gameState CuurentGameState
        {
            get { return m_currentGameState; }
            set { m_currentGameState = value; }
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
        public static void OneMoveInGame()
        {
            
            eTurnOf currentPlaying = eTurnOf.Player1;
            (int, int) point;
           
                bool thisMoveCouseWin = false;
                bool hasTheUserEnterQ = false;
                bool thisMoveCouseTie = false;
               
                    switch(currentPlaying)
                    {
                        case eTurnOf.Player1:
                            {
                                point = m_Player1.IsComputer == true ? getComputerRandomPoint() : getHumanPointByUser();

                                if(m_currentGameState==)
                                {
                                    ///TODO
                                    /// need to cheack at Console U;
                                    hasTheUserEnterQ = true;
                                    break;
                                }
                             

                                setPointatboard(m_Player1.Sign,point);
                                if(thereIsWin(point))
                                {
                                    //there is win
                                    thisMoveCouseWin = true;
                                    break;
                                }
                              
                                else if(thereIsTie(point))
                                {
                                    //there is Tie
                                    thisMoveCouseTie = true;
                                    break;
                                }
                                {
                                    currentPlaying =  eTurnOf.Player2;
                                }
                                break;
                            }
                        case eTurnOf.Player2:
                            {
                                break;
                            }
                    }
               

                if (thisMoveCouseTie)
                {
                    ConsoleUI.PrintTieMessege();
                }
                else if (thisMoveCouseWin|| hasTheUserEnterQ)
                {
                    if (currentPlaying == eTurnOf.Player1)
                    {
                        ///we need to add to the other plyer
                        m_Player2.NumberOfWins++;
                        ConsoleUI.PrintWinMessege("Player 2");
                    }

                    if(currentPlaying == eTurnOf.Player2)
                    {
                        ///we need to add to the other plyer
                        m_Player1.NumberOfWins++;
                        ConsoleUI.PrintWinMessege("Player 1");
                    }

                }
                ///need to do method to print stistics
                wantToPlayFlag = ConsoleUI.isUserWantAnotherGame();

            
            
        }

        private static (int, int) getHumanPointByUser()
        {
            bool isEmptySpot = true;

            (int,int) validSpotInBoard= ConsoleUI.GetValidSpotInBoard();
            isEmptySpot = isThisEmptySpot(validSpotInBoard);
            // should be in function 
            while (!isEmptySpot)
            {
                validSpotInBoard = ConsoleUI.GetNewValidSpotInBoard(validSpotInBoard);
                isEmptySpot = isThisEmptySpot(validSpotInBoard);
            }

            return validSpotInBoard;
        }

        private static (int, int) getComputerRandomPoint()
        {
            throw new NotImplementedException();
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
