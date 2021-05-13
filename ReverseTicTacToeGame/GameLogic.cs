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
            (int row, int colomn) point;
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

                setPointOnBoard(point, player.Sign);
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

        private static void setPointOnBoard((int, int) i_Point, char i_Sign)
        {
            throw new NotImplementedException();
        }

        private static (int, int) findAnEmptySpotInBoard()
        {
            /// TODO
            /// this is the move of the computer
            throw new NotImplementedException();
        }

        internal static bool ThereIsWin((int, int) i_Point)
        {
            ///TODO
            /// check if the last move couse win in the board and if so update the winner filed
            throw new NotImplementedException();
        }
        
        private static bool thereIsTie((int, int) i_Point)
        {
            throw new NotImplementedException();
        }

        private static void setPointOnBoard(char i_PlayerSign, (int, int) i_Point)
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

        internal static bool isThisEmptySpot((int, int) i_Spot)
        {
            ///Todo
            /// check if the spot at the spesific board is empty
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
        //         ConsoleUi.PrintTieMessege();
        //     }
        //     else if(thisMoveCouseWin || hasTheUserEnterQ)
        //     {
        //         if(currentPlaying == eTurnOf.Player1)
        //         {
        //             ///we need to add to the other plyer
        //             s_Player2.NumberOfWins++;
        //             ConsoleUi.PrintWinMessege("Player 2");
        //         }
        //
        //         if(currentPlaying == eTurnOf.Player2)
        //         {
        //             ///we need to add to the other plyer
        //             s_Player1.NumberOfWins++;
        //             ConsoleUi.PrintWinMessege("Player 1");
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