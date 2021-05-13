using System;

namespace ReverseTicTacToeGame
{
    internal class GameLogic
    {
        private static Board s_GameBoard;
        private static Player s_Player1;
        private static Player s_Player2;
        private static eGameState s_CurrentGameState;
        private static Player s_Winner;
        internal const char k_Empty = (char)0;
        private const char k_Circle = 'O';
        private const char k_Cross = 'X';
        private static int[,] checkWinTable;

        internal enum eTurnOf
        {
            Player1,
            Player2
        }

        internal enum eGameState
        {
            Playing,
            Win,
            Tie,
            Quit
        }

        public GameLogic(byte i_BoardSize, bool i_Player1IsComputer, bool i_Player2IsComputer)
        {
            s_GameBoard = new Board(i_BoardSize);
            s_Player1 = new Player(k_Cross, i_Player1IsComputer);
            s_Player2 = new Player(k_Circle, i_Player2IsComputer);
            s_CurrentGameState = eGameState.Playing;
            checkWinTable = new int[i_BoardSize+ 1, i_BoardSize + 1];
            ///init checkWinTable
            for(int i = 0; i < i_BoardSize + 1; i++)
            {
                checkWinTable[0, i] = int.MinValue;
                checkWinTable[i, 0] = int.MinValue;
            }
        }

        public static eGameState CurrentGameState
        {
            get { return s_CurrentGameState; }
            set { s_CurrentGameState = value; }
        }
        public static Player Winner
        {
            get { return s_Winner; }
            set { s_Winner = value; }
        }
        public static Player Player1
        {
            get { return s_Player1; }
            set { s_Player1 = value; }
        }
        
        public static Player Player2
        {
            get { return s_Player2; }
            set { s_Player2 = value; }
        }

        public static Board GameBoard
        {
            get { return s_GameBoard; }
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
            //eTurnOf currentPlaying = eTurnOf.Player1;
            (int row, int column) point;
            Player[] players = { s_Player1, s_Player2 };
            foreach(Player player in players)
            {
                if(player.IsComputer == false)
                {
                    point = UserInterface.GetValidPointFromUser(); // the slot is inrange and free   
                }
                else
                {
                    point = getRandomPointForComputer();
                }

                Board.SetValueOnBoard((byte)point.row,(byte)point.column, player.Sign);
                updateStateOfGame(point);
                if(s_CurrentGameState != eGameState.Playing) 
                {
                    break;
                }

            }
        }

        private static void updateStateOfGame((int row, int colomn) i_LastPointEntered)
        {
            if(ThereIsWin(i_LastPointEntered))
            {
                s_CurrentGameState = eGameState.Win;
            }
            else if(thereIsTie(i_LastPointEntered))
            {
                s_CurrentGameState = eGameState.Tie;
            }
            else
            {
                s_CurrentGameState = eGameState.Playing;
            }
        }

        private static (int, int) getRandomPointForComputer()
        {
            throw new NotImplementedException();
        }
         internal static void ClearBoardForAnotherGame()
        {
            byte boardSize =(byte)s_GameBoard.Size;
            s_GameBoard.GameBoard = new char[boardSize, boardSize];
        }
    

        internal static bool ThereIsWin((int row, int colomn) i_Point)
        {
            /// check if the last move couse win in the board and if so update the winner filed
            int maxValue = int.MinValue;
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    int currentValue = checkWinTable[i_Point.row + i, i_Point.colomn + j];
                    if(currentValue > maxValue)
                    {
                        maxValue = currentValue;
                    }
                }
            }

            return false;
        }
        
    /// <summary>
    /// Tie happend when the board is full and a win didnt couse
    /// </summary>
    /// <param name="i_Point"></param>
    /// <returns></returns>
        private static bool thereIsTie((int row, int colomn) i_Point)
        {
            return Board.IsFull();
        }

     
        private static bool isQsignInPoint((int row, int colomn) i_Spot)
        {
            ///TODO
            /// Check if Q has showed - for now we will define it at (-1,-1) point
            throw new NotImplementedException();
        }

        public static bool isThisEmptySpot((int row, int colomn) i_ValidSpotInBoard)
        {
           return Board.IsEmptySpot((byte)i_ValidSpotInBoard.row,(byte)i_ValidSpotInBoard.colomn)
        }

        public static bool isInRangeOfBoard(int i_Number)
        {
            throw new NotImplementedException();
        }
    }
}

        //     switch(currentPlaying)
        //     {
        //         case eTurnOf.Player1:
        //             {
        //                 point = s_Player1.IsComputer == true ? getComputerRandomPoint() : getPointFromUser();
        //
        //                 if(s_CurrentGameState ==)
        //                 {
        //                     ///TODO
        //                     /// need to cheack at Console U;
        //                     hasTheUserEnterQ = true;
        //
        //                     break;
        //                 }
        //
        //                 setPointatboard(s_Player1.Sign, point);
        //                 if(thereIsWin(point))
        //                 {
        //                     //there is win
        //                     thisMoveCouseWin = true;
        //
        //                     break;
        //                 }
        //
        //                 else if(thereIsTie(point))
        //                 {
        //                     //there is Tie
        //                     thisMoveCouseTie = true;
        //
        //                     break;
        //                 }
        //
        //                 {
        //                     currentPlaying = eTurnOf.Player2;
        //                 }
        //
        //                 break;
        //             }
        //         case eTurnOf.Player2:
        //             {
        //                 break;
        //             }
        //     }
        //
        //     if(thisMoveCouseTie)
        //     {
        //         ConsoleUi.PrintTieMessage();
        //     }
        //     else if(thisMoveCouseWin || hasTheUserEnterQ)
        //     {
        //         if(currentPlaying == eTurnOf.Player1)
        //         {
        //             ///we need to add to the other plyer
        //             s_Player2.NumberOfWins++;
        //             ConsoleUi.PrintWinMessage("Player 2");
        //         }
        //
        //         if(currentPlaying == eTurnOf.Player2)
        //         {
        //             ///we need to add to the other plyer
        //             s_Player1.NumberOfWins++;
        //             ConsoleUi.PrintWinMessage("Player 1");
        //         }
        //     }
        //
        //     ///need to do method to print stistics
        //     wantToPlayFlag = ConsoleUi.IsUserWantAnotherGame();
        // }

        // private static (int, int) getPointFromUser()
        // {
        //     bool isEmptySpot = true;
        //
        //     (int, int) validSpotInBoard = ConsoleUI.GetValidSpotInBoard();
        //     isEmptySpot = isThisEmptySpot(validSpotInBoard);
        //     // should be in function 
        //     while(!isEmptySpot)
        //     {
        //         validSpotInBoard = ConsoleUI.GetNewValidSpotInBoard(validSpotInBoard);
        //         isEmptySpot = isThisEmptySpot(validSpotInBoard);
        //     }
        //
        //     return validSpotInBoard;
        // }