using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;

namespace ReverseTicTacToeGame
{
    internal class GameLogic
    {
        private static Board s_GameBoard;
        private static Player s_Player1;
        private static Player s_Player2;
        private static eGameState s_CurrentGameState;
        private static Player s_WinnerPlayer;

        internal const char k_Empty = (char)0;
        private const char k_Circle = 'O';
        private const char k_Cross = 'X';
        private const int k_MinBoardSize = 3;
        private const int k_MaxBoardSize = 9;

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

        public GameLogic(int i_BoardSize, bool i_Player1IsComputer, bool i_Player2IsComputer)
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
        public static Player WinnerPlayer
        {
            get { return s_WinnerPlayer; }
            set { s_WinnerPlayer = value; }
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
            (int row, int column) point = (0, 0);
            Player[] players = { s_Player1, s_Player2 };
            foreach(Player player in players)
            {
                UserInterface.printBoard();
                if (!player.IsComputer)
                {
                    point = UserInterface.GetValidPointFromUser(player.Sign); // the slot is inrange and free   
                }
                else
                {
                    point = getRandomPointForComputer();
                }

                s_GameBoard.SetValueOnBoard(point.row, point.column, player.Sign);
                updateStateOfGame(point, player);
                UserInterface.clearBoardBeforeNewMove();
           
                if(s_CurrentGameState != eGameState.Playing)
                {
                    break;
                }
            }
        }

        private static void updateStateOfGame((int row, int column) i_LastPointEntered, Player i_Player)
        {
             if (isQsignInPoint(i_LastPointEntered))
             {
                 updateWinnerStatistics(i_Player);
                 s_CurrentGameState = eGameState.Quit;
             }
             else if (ThereIsWin(i_LastPointEntered, i_Player.Sign))
             {
                
                 updateWinnerStatistics(i_Player);
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

        private static void updateWinnerStatistics(Player i_LoserPlayer)
        {
            if (i_LoserPlayer.Sign == s_Player1.Sign)
            {
                s_Player2.NumberOfWins++;
                s_WinnerPlayer = s_Player2;
             
            }
            else
            {
                s_Player1.NumberOfWins++;
                s_WinnerPlayer = s_Player1;
            }
        }

        private static (int, int) getRandomPointForComputer()
        {
            Random random = new Random();

            int randomIndex = random.Next(0, s_GameBoard.FreeSpotsInBoard.Count);
            (int row, int column) testedPoint = s_GameBoard.FreeSpotsInBoard.ElementAt(randomIndex);

            // while(!s_GameBoard.IsEmptySpot(testedPoint.row, testedPoint.column))
            // {
            //     ///if enter here there is a bug.
            //     randomIndex = random.Next(0, s_GameBoard.FreeSpotsInBoard.Count);
            //     testedPoint = s_GameBoard.FreeSpotsInBoard.ElementAt(randomIndex);
            // }

            return testedPoint;
        }

        internal static void CreateNewBoardForAnotherGame()
        {
            int boardSize = s_GameBoard.Size;
            s_GameBoard.GameBoard = new char[boardSize, boardSize];
        }

        internal static bool ThereIsWin((int row, int column) i_Point, char i_PlayerSign)
        {
            int numberOfSignsToWin = s_GameBoard.Size - 1;
            bool isWinInRowAndCol = checkWinInRowAndColumn(i_Point, i_PlayerSign, numberOfSignsToWin);
            bool isWinInDiagonal = checkWinInMainDiagonal(i_Point, i_PlayerSign, numberOfSignsToWin);
            bool isWinInAntiDiagonal = checkWinInAntidiagonal(i_Point, i_PlayerSign, numberOfSignsToWin);

            return isWinInDiagonal || isWinInAntiDiagonal || isWinInRowAndCol;
        }

        private static bool checkWinInAntidiagonal(
            (int row, int column) i_Point,
            char i_PlayerSign,
            int i_NumberOfSignsToWin)
        {
            int counter = 0;

                ///in squere matrix -the anti daigonal the sum of row and column equal to the matrix size+1
            
                for(int i = 1; i <= s_GameBoard.Size-1; i++)
                {
                    if(s_GameBoard.GameBoard[i , s_GameBoard.Size -i] == i_PlayerSign)
                    {
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }
            

            return counter == i_NumberOfSignsToWin;
        }

        private static bool checkWinInMainDiagonal(
            (int row, int column) i_Point,
            char i_PlayerSign,
            int i_NumberOfSignsToWin)
        {
            int counter = 0;

            if(i_Point.row == i_Point.column) ///in the main daigonal all the point the i equal to j 
            {
                for(int i = 1; i < s_GameBoard.Size; i++)
                {
                    if(s_GameBoard.GameBoard[i, i] == i_PlayerSign)
                    {
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return counter == i_NumberOfSignsToWin;
        }

        private static bool checkWinInRowAndColumn(
            (int row, int column) i_Point,
            char i_PlayerSign,
            int i_NumberOfSignsToWin)
        {
            int counterRow = 0;
            int counterColumn = 0;

            for(int i = 1; i < s_GameBoard.Size; i++)
            {
                if(s_GameBoard.GameBoard[i_Point.row, i] == i_PlayerSign)
                {
                    counterRow++;
                }

                if(s_GameBoard.GameBoard[i, i_Point.column] == i_PlayerSign)
                {
                    counterColumn++;
                }
            }

            return (counterRow == i_NumberOfSignsToWin) || (counterColumn == i_NumberOfSignsToWin);
        }

        /// <summary>
        /// Tie happend when the board is full and a win didnt couse
        /// </summary>
        /// <param name="i_Point"></param>
        /// <returns></returns>
        internal static bool thereIsTie((int row, int column) i_Point)
        {
            return GameLogic.GameBoard.IsFull();
        }

        /// <summary>
        /// we decided that point (-1,-1) will be the sign og Q from the UI ,
        /// Offcourse that (-1,-1) is not valid at any board game
        /// </summary>
        /// <param name="i_Spot"></param>
        /// <returns></returns>
        internal static bool isQsignInPoint((int row, int column) i_Spot)
        {
            bool isQsignInPoint = (i_Spot.row == -1) && (i_Spot.column == -1);

            return isQsignInPoint;
        }

        internal static bool IsEmptySpot(int i_Row, int i_Column)
        {
            bool isEmptySpot = s_GameBoard.IsEmptySpot(i_Row, i_Column);
        
            return isEmptySpot;
        }

        public static bool isInBoradRangeSize(int i_Number)
        {
            bool isInRangeOfBoard = i_Number >= 1 && i_Number < s_GameBoard.Size;

            return isInRangeOfBoard;
        }

        internal static bool IsValidSpot(int i_Row, int i_Col)
        {
            bool isValidSpot = IsEmptySpot(i_Row, i_Col) ;

            return isValidSpot;
        }
        
        public static bool isValidSizeBoard(int i_Number)
        {
            bool isValidSizeBoard = k_MinBoardSize <= i_Number & k_MaxBoardSize >= i_Number;

            return isValidSizeBoard;
        }

        internal static void WantAnotherGame()
        {
            CurrentGameState = eGameState.Playing;
            CreateNewBoardForAnotherGame();
        }
    }
}