using System;
using System.Linq;

namespace ReverseTicTacToeGame
{
    internal class GameLogic
    {
        private Board m_GameBoard;
        private Player m_Player1;
        private Player m_Player2;
        private eGameState m_CurrentGameState;
        private Player m_WinnerPlayer;
        private const int k_MinBoardSize = 3;
        private const int k_MaxBoardSize = 9;

        internal enum eGameState
        {
            Playing,
            Win,
            Tie,
            Quit
        }

        public GameLogic(int i_BoardSize, bool i_Player1IsComputer, bool i_Player2IsComputer)
        {
            m_GameBoard = new Board(i_BoardSize);
            m_Player1 = new Player(ePlayersMark.Player1, i_Player1IsComputer);
            m_Player2 = new Player(ePlayersMark.Player2, i_Player2IsComputer);
            m_CurrentGameState = eGameState.Playing;
        }

        public eGameState CurrentGameState
        {
            get { return this.m_CurrentGameState; }
            private set { this.m_CurrentGameState = value; }
        }
        public Player WinnerPlayer
        {
            get { return m_WinnerPlayer; }
            set { m_WinnerPlayer = value; }
        }
        public Player Player1
        {
            get { return m_Player1; }
            set { m_Player1 = value; }
        }

        public Player Player2
        {
            get { return m_Player2; }
            set { m_Player2 = value; }
        }

        public Board GameBoard
        {
            get { return m_GameBoard; }
        }

        public void OneRoundInGame(UserInterface i_GameUi)
        {
            Player[] players = { m_Player1, m_Player2 };
            foreach(Player player in players)
            {
                (int row, int column) point;
                // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                if(!player.IsComputer)
                {
                    point = i_GameUi.GetValidPointFromUser(player.Sign); // the slot is in range and free   
                }
                else
                {
                    point = getRandomPointForComputer();
                }

                this.GameBoard.SetValueOnBoard(point.row, point.column, player.Sign);
                updateStateOfGame(point, player);
                i_GameUi.CleanAndShowBeforeNewTurn();

                if(m_CurrentGameState != eGameState.Playing)
                {
                    break;
                }
            }
        }

        private void updateStateOfGame((int row, int column) i_LastPointEntered, Player i_Player)
        {
            if(isQuitPoint(i_LastPointEntered))
            {
                updateWinnerStatistics(i_Player);
                m_CurrentGameState = eGameState.Quit;
            }
            else if(thereIsWin(i_LastPointEntered, i_Player.Sign))
            {
                updateWinnerStatistics(i_Player);
                m_CurrentGameState = eGameState.Win;
            }
            else if(thereIsTie())
            {
                m_CurrentGameState = eGameState.Tie;
            }

            else
            {
                m_CurrentGameState = eGameState.Playing;
            }
        }

        private void updateWinnerStatistics(Player i_LoserPlayer)
        {
            if(i_LoserPlayer.Sign == m_Player1.Sign)
            {
                m_Player2.NumberOfWins++;
                m_WinnerPlayer = m_Player2;
            }
            else
            {
                m_Player1.NumberOfWins++;
                m_WinnerPlayer = m_Player1;
            }
        }

        private (int, int) getRandomPointForComputer()
        {
            Random random = new Random();

            int randomIndex = random.Next(0, this.GameBoard.FreeSpotsInBoard.Count);
            (int row, int column) testedPoint = this.GameBoard.FreeSpotsInBoard.ElementAt(randomIndex);

            return testedPoint;
        }

        private void createNewBoardForAnotherGame()
        {
            int boardSize = this.GameBoard.Size;
            this.m_GameBoard = new Board(boardSize);
        }

        private bool thereIsWin((int row, int column) i_Point, ePlayersMark i_PlayerMarkSign)
        {
            int numberOfSignsToWin = this.GameBoard.Size - 1;
            bool isWinInRowAndCol = checkWinInRowAndColumn(i_Point, i_PlayerMarkSign, numberOfSignsToWin);
            bool isWinInDiagonal = checkWinInMainDiagonal(i_PlayerMarkSign, numberOfSignsToWin);
            bool isWinInAntiDiagonal = checkWinInAntiDiagonal(i_PlayerMarkSign, numberOfSignsToWin);

            return isWinInDiagonal || isWinInAntiDiagonal || isWinInRowAndCol;
        }

        private bool checkWinInAntiDiagonal(ePlayersMark i_PlayerMarkSign, int i_NumberOfSignsToWin)
        {
            int counter = 0;

            // In square matrix - the anti diagonal the sum of row and column equal to the matrix size+1

            for(int i = 1; i <= this.GameBoard.Size - 1; i++)
            {
                if(m_GameBoard.GameBoard[i, m_GameBoard.Size - i] == i_PlayerMarkSign)
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

        private bool checkWinInMainDiagonal(ePlayersMark i_PlayerMark, int i_NumberOfSignsToWin)
        {
            int counter = 0;

            for(int i = 1; i < this.GameBoard.Size; i++)
            {
                if(this.GameBoard.GameBoard[i, i] == i_PlayerMark)
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

        private bool checkWinInRowAndColumn((int row, int column) i_Point, ePlayersMark i_PlayerMark, int i_NumberOfSignsToWin)
        {
            int counterRow = 0;
            int counterColumn = 0;

            for(int i = 1; i < this.GameBoard.Size; i++)
            {
                if(m_GameBoard.GameBoard[i_Point.row, i] == i_PlayerMark)
                {
                    counterRow++;
                }

                if(m_GameBoard.GameBoard[i, i_Point.column] == i_PlayerMark)
                {
                    counterColumn++;
                }
            }

            return (counterRow == i_NumberOfSignsToWin) || (counterColumn == i_NumberOfSignsToWin);
        }

        /* Tie happened when the board is full and a win didnt cause */
        private bool thereIsTie()
        {
            return this.GameBoard.IsFull();
        }

        /* we NOTE (-1,-1) as a sign of Q is appeared from the UI, (-1,-1) is not valid in ANY board game */
        private static bool isQuitPoint((int row, int column) i_Spot)
        {
            bool isQuitPoint = (i_Spot.row == -1) && (i_Spot.column == -1);

            return isQuitPoint;
        }

        internal bool IsEmptySpot(int i_Row, int i_Column)
        {
            bool isEmptySpot = this.GameBoard.IsEmptySpot(i_Row, i_Column);

            return isEmptySpot;
        }

        public bool IsInBoardRangeSize(int i_Number)
        {
            const int k_MinValuePossibleEnterToBoard = 1;

            bool isInRangeOfBoard = i_Number >= k_MinValuePossibleEnterToBoard && i_Number < this.GameBoard.Size;

            return isInRangeOfBoard;
        }

        internal bool IsValidSpot(int i_Row, int i_Col)
        {
            bool isValidSpot = this.IsEmptySpot(i_Row, i_Col);

            return isValidSpot;
        }

        public static bool IsValidSizeBoard(int i_Number)
        {
            bool isValidSizeBoard = k_MinBoardSize <= i_Number & k_MaxBoardSize >= i_Number;

            return isValidSizeBoard;
        }

        internal void PreparingForAnotherGame()
        {
            CurrentGameState = eGameState.Playing;
            createNewBoardForAnotherGame();
        }
    }
}