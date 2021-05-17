using System;
using System.Linq;

namespace ReverseTicTacToeGame
{
    internal class GameLogic
    {
        private static Board s_GameBoard;
        private static Player s_Player1;
        private static Player s_Player2;
        private static eGameState s_CurrentGameState;
        private static Player s_WinnerPlayer;
        private const int k_MinBoardSize = 3;
        private const int k_MaxBoardSize = 9;

        // internal const char k_Empty = (char)0; *** WE DONT USE THOSE? *** $O$
        // internal const char k_Circle = 'O';
        // internal const char k_Cross = 'X';

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
            s_Player1 = new Player(ePlayersMark.Player1, i_Player1IsComputer);
            s_Player2 = new Player(ePlayersMark.Player2, i_Player2IsComputer);
            s_CurrentGameState = eGameState.Playing;
        }

        public static eGameState CurrentGameState
        {
            get { return s_CurrentGameState; }
            private set { s_CurrentGameState = value; }
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

        public static void OneRoundInGame()
        {
            Player[] players = { s_Player1, s_Player2 };
            foreach(Player player in players)
            {
                (int row, int column) point;
                // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                if(!player.IsComputer)
                {
                    point = UserInterface.GetValidPointFromUser(player.Sign); // the slot is in range and free   
                }
                else
                {
                    point = getRandomPointForComputer();
                }

                s_GameBoard.SetValueOnBoard(point.row, point.column, player.Sign);
                updateStateOfGame(point, player);
                UserInterface.ClearBoardBeforeNewMove();
                UserInterface.PrintBoard();

                if(s_CurrentGameState != eGameState.Playing)
                {
                    break;
                }
            }
        }

        private static void updateStateOfGame((int row, int column) i_LastPointEntered, Player i_Player)
        {
            if(isQuitPoint(i_LastPointEntered))
            {
                updateWinnerStatistics(i_Player);
                s_CurrentGameState = eGameState.Quit;
            }
            else if(thereIsWin(i_LastPointEntered, i_Player.Sign))
            {
                updateWinnerStatistics(i_Player);
                s_CurrentGameState = eGameState.Win;
            }
            else if(thereIsTie())
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
            if(i_LoserPlayer.Sign == s_Player1.Sign)
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

            return testedPoint;
        }

        private static void createNewBoardForAnotherGame()
        {
            int boardSize = s_GameBoard.Size;
            s_GameBoard = new Board(boardSize);
            UserInterface.ClearBoardBeforeNewMove();
            UserInterface.PrintBoard();
        }

        private static bool thereIsWin((int row, int column) i_Point, ePlayersMark i_PlayerMarkSign)
        {
            int numberOfSignsToWin = s_GameBoard.Size - 1;
            bool isWinInRowAndCol = checkWinInRowAndColumn(i_Point, i_PlayerMarkSign, numberOfSignsToWin);
            bool isWinInDiagonal = checkWinInMainDiagonal(i_PlayerMarkSign, numberOfSignsToWin);
            bool isWinInAntiDiagonal = checkWinInAntiDiagonal(i_PlayerMarkSign, numberOfSignsToWin);

            return isWinInDiagonal || isWinInAntiDiagonal || isWinInRowAndCol;
        }

        private static bool checkWinInAntiDiagonal(ePlayersMark i_PlayerMarkSign, int i_NumberOfSignsToWin)
        {
            int counter = 0;

            // In square matrix - the anti diagonal the sum of row and column equal to the matrix size+1

            for(int i = 1; i <= s_GameBoard.Size - 1; i++)
            {
                if(s_GameBoard.GameBoard[i, s_GameBoard.Size - i] == i_PlayerMarkSign)
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
            ePlayersMark i_PlayerMarkSign,
            int i_NumberOfSignsToWin)
        {
            int counter = 0;

            for(int i = 1; i < s_GameBoard.Size; i++)
            {
                if(s_GameBoard.GameBoard[i, i] == i_PlayerMarkSign)
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

        private static bool checkWinInRowAndColumn(
            (int row, int column) i_Point,
            ePlayersMark i_PlayerMarkSign,
            int i_NumberOfSignsToWin)
        {
            int counterRow = 0;
            int counterColumn = 0;

            for(int i = 1; i < s_GameBoard.Size; i++)
            {
                if(s_GameBoard.GameBoard[i_Point.row, i] == i_PlayerMarkSign)
                {
                    counterRow++;
                }

                if(s_GameBoard.GameBoard[i, i_Point.column] == i_PlayerMarkSign)
                {
                    counterColumn++;
                }
            }

            return (counterRow == i_NumberOfSignsToWin) || (counterColumn == i_NumberOfSignsToWin);
        }
        
        /* Tie happened when the board is full and a win didnt cause */
        private static bool thereIsTie()
        {
            return GameLogic.GameBoard.IsFull();
        }
        
        /* we NOTE (-1,-1) as a sign of Q is appeared from the UI, (-1,-1) is not valid in ANY board game */
        private static bool isQuitPoint((int row, int column) i_Spot)
        {
            bool isQuitPoint = (i_Spot.row == -1) && (i_Spot.column == -1);

            return isQuitPoint;
        }

        internal static bool IsEmptySpot(int i_Row, int i_Column)
        {
            bool isEmptySpot = s_GameBoard.IsEmptySpot(i_Row, i_Column);

            return isEmptySpot;
        }

        public static bool IsInBoardRangeSize(int i_Number)
        {
            bool isInRangeOfBoard = i_Number >= 1 && i_Number < s_GameBoard.Size;

            return isInRangeOfBoard;
        }

        internal static bool IsValidSpot(int i_Row, int i_Col)
        {
            bool isValidSpot = IsEmptySpot(i_Row, i_Col);

            return isValidSpot;
        }

        public static bool IsValidSizeBoard(int i_Number)
        {
            bool isValidSizeBoard = k_MinBoardSize <= i_Number & k_MaxBoardSize >= i_Number;

            return isValidSizeBoard;
        }

        internal static void PreparingForAnotherRound()
        {
            CurrentGameState = eGameState.Playing;
            createNewBoardForAnotherGame();
        }
    }
}