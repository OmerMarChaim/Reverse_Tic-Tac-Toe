using System;
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
        private static Player s_LoserPlayer;

        internal const char k_Empty = (char)0;
        private const char k_Circle = 'O';
        private const char k_Cross = 'X';
        private static int[,] checkWinTable;
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
                UserInterface.clearBoardBeforeNewMove();
                UserInterface.printBoard();
                if(player.IsComputer == false)
                {
                    point = UserInterface.GetValidPointFromUser(); // the slot is inrange and free   
                }
                else
                {
                    point = getRandomPointForComputer();
                }

                s_GameBoard.SetValueOnBoard(point.row, point.column, player.Sign);
                updateStateOfGame(point,player);
                if(s_CurrentGameState != eGameState.Playing) 
                {
                    break;
                }

                
               
            }
        }

        private static void updateStateOfGame((int row, int column) i_LastPointEntered, Player i_Player)
        {
            if(ThereIsWin(i_LastPointEntered, i_Player.Sign))
            {
                s_LoserPlayer = i_Player;
                ///todo
                /// updates number of wins
                s_CurrentGameState = eGameState.Win;
            }
            else if(thereIsTie(i_LastPointEntered))
            {
                s_CurrentGameState = eGameState.Tie;
            }
            else if(isQsignInPoint(i_LastPointEntered))
            {
                s_LoserPlayer = i_Player;
                s_CurrentGameState = eGameState.Quit;

            }
            else
            {
                s_CurrentGameState = eGameState.Playing;
            }
        }

        private static (int, int) getRandomPointForComputer()
        {
            var random= new Random();

             int randomIndex = random.Next(0, s_GameBoard.FreeSpotsInBoard.Count);
             (int row, int column) maybePoint = s_GameBoard.FreeSpotsInBoard.ElementAt(randomIndex);
            
             while (!s_GameBoard.IsEmptySpot(maybePoint.row, maybePoint.column))
             {
                 ///if enter here there is a bug.
                randomIndex = random.Next(0, s_GameBoard.FreeSpotsInBoard.Count);
                maybePoint = s_GameBoard.FreeSpotsInBoard.ElementAt(randomIndex);
            }
             return maybePoint;

        }
         internal static void ClearBoardForAnotherGame()
        {
            int boardSize =(int)s_GameBoard.Size;
            s_GameBoard.GameBoard = new char[boardSize, boardSize];
        }
    

        internal static bool ThereIsWin((int row, int column) i_Point, char i_PlayerSign)
        {
            int numberOfSignsToWin = s_GameBoard.Size - 1;
           return checkWinInRowAndColumn(i_Point, i_PlayerSign, numberOfSignsToWin) == true
               || checkWinInMainDiagonal(i_Point, i_PlayerSign, numberOfSignsToWin) == true
               || checkWinInAntidiagonal(i_Point, i_PlayerSign, numberOfSignsToWin) == true;
         
        }

        private static bool checkWinInAntidiagonal((int row, int column) i_Point, char i_PlayerSign, int i_NumberOfSignsToWin)
        {
            int counter = 0;
          
            if(i_Point.row + i_Point.column==s_GameBoard.Size) ///in squere matrix -the anti daigonal the sum of row and column equal to the matrix size+1
            {
                for(int i = 0; i < s_GameBoard.Size; i++)
                {
                    if(s_GameBoard.GameBoard[i + 1, s_GameBoard.Size - i] == i_PlayerSign)
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

        private static bool checkWinInMainDiagonal((int row, int column) i_Point,  char i_PlayerSign, int i_NumberOfSignsToWin)
        {
            int counter = 0;
          
            if (i_Point.row == i_Point.column) ///in the main daigonal all the point the i equal to j 
            {
                for(int i = 1 ; i < s_GameBoard.Size; i++)
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
            return counter== i_NumberOfSignsToWin;

        }

        private static bool checkWinInRowAndColumn((int row, int column) i_Point, char i_PlayerSign, int i_NumberOfSignsToWin)
        {
            int counterRow = 0;
            int counterColumn = 0;

            for (int i = 1; i < s_GameBoard.Size ; i++)
            {
             
                if(s_GameBoard.GameBoard[i_Point.row, i] == i_PlayerSign)
                {
                    counterRow++;
                }
                if(s_GameBoard.GameBoard[i,i_Point.column] == i_PlayerSign)
                {
                    counterColumn++;
                }
               
            }

            return (counterRow ==  i_NumberOfSignsToWin) || (counterColumn == i_NumberOfSignsToWin);
        }

        /// <summary>
    /// Tie happend when the board is full and a win didnt couse
    /// </summary>
    /// <param name="i_Point"></param>
    /// <returns></returns>
        private static bool thereIsTie((int row, int column) i_Point)
        {
            return Board.IsFull();
        }

     
        private static bool isQsignInPoint((int row, int column) i_Spot)
        {
            return (i_Spot.row == 0) && (i_Spot.column==0) ;

        }

        public static bool isThisEmptySpot((int row, int column) i_ValidSpotInBoard)
        {
            return s_GameBoard.IsEmptySpot((int)i_ValidSpotInBoard.row, (int)i_ValidSpotInBoard.column);
        }

        public static bool isInRangeOfBoard(int i_Number)
        {
            return (i_Number >= k_MinBoardSize && i_Number <= k_MaxBoardSize) ;

        }

  
    }
}