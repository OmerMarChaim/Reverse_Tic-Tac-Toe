using System;
using System.Text;
using static ReverseTicTacToeGame.GameLogic;
using static ReverseTicTacToeGame.GameLogic.eGameState;
using static ReverseTicTacToeGame.eSignsOfPlayers;

namespace ReverseTicTacToeGame
{
    internal class UserInterface
    {
        private const char k_Circle = 'O';
        private const char k_Cross = 'X';
        private const char k_Empty = ' ';
        private const char k_Player1Sign = k_Cross;
        private const char k_Player2Sign = k_Circle;
        private GameLogic m_Game;

        internal UserInterface(GameLogic i_Game)
        {
            m_Game = i_Game;
        }

        public static char Player1Sign
        {
            get { return k_Player1Sign; }
        }
        public static char Player2Sign
        {
            get { return k_Player2Sign; }
        }

        public static void InitGame() // Checked
        {
            Console.WriteLine("Hello and Welcome to our game!");

            int boardSize = getValidBoardSize() + 1;
            bool player1IsComputer = false;
            bool player2IsComputer = isPlayerIsComputer();
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            GameLogic game = new GameLogic(boardSize, player1IsComputer, player2IsComputer);
            UserInterface gameUi = new UserInterface(game);
            gameUi.printBoard();
            gameUi.startGame();
        }

        private void startGame() // Checked
        {
            bool wantAnotherGameFlag = true;

            while(wantAnotherGameFlag)
            {
                while(this.m_Game.CurrentGameState == GameLogic.eGameState.Playing)
                {
                    this.m_Game.OneRoundInGame(this);
                }

                updateTheUserInterfaceAccordingTheState();
                wantAnotherGameFlag = isUserWantAnotherGame();
            }
        }

        private bool isUserWantAnotherGame() // Checked
        {
            Console.WriteLine($"Do you want another game? please enter 'Y' for yes ,'Q' for Quit");
            bool wantAnotherGameFlag = getValidYesOrNo();
            // In case which the user want another game - we ask the logic to make for us Preparing For Another Round
            if(wantAnotherGameFlag)
            {
                this.m_Game.PreparingForAnotherGame();
                this.CleanAndShowBeforeNewTurn();
            }
            else
            {
                Console.WriteLine("Thank you for playing with us! see you in Ex03");
            }

            return wantAnotherGameFlag;
        }

        /// <summary>
        ///  TODO MOVE TO valition 
        /// </summary>
        /// <returns></returns>
        private bool getValidYesOrNo() // Checked
        {
            string userInput;
            bool yesOrNoFlag = false;
            bool isValid = false;
            while(!isValid)
            {
                userInput = Console.ReadLine();
                bool isYisAppeared = userInput == "Y" || userInput == "y";
                bool isQisAppeared = userInput == "Q" || userInput == "q";
                if(isQisAppeared || isYisAppeared)
                {
                    isValid = true;
                    if(isYisAppeared)
                    {
                        yesOrNoFlag = true;
                    }
                }
                else
                {
                    Console.WriteLine("Your input is invalid. if you want to play more enter 'Y', other 'Q'");
                }
            }

            return yesOrNoFlag;
        }

        private void updateTheUserInterfaceAccordingTheState() // Checked
        {
            eGameState currentState = this.m_Game.CurrentGameState;
            eSignsOfPlayers signOfTheWinner = this.m_Game.WinnerPlayer.Sign;

            if(currentState == Win)
            {
                printWinMessage(signOfTheWinner);
            }
            else if(currentState == Tie)
            {
                printTieMessage();
            }
            else if(currentState == Quit)
            {
                printQuitMessage(signOfTheWinner);
            }

            printScore();
        }

        private void printScore() // Checked
        {
            Console.WriteLine(
                $@"The current score is:
{this.m_Game.Player1.Sign}:{this.m_Game.Player1.NumberOfWins}
{this.m_Game.Player2.Sign}:{this.m_Game.Player2.NumberOfWins}");
        }

        private static void printTieMessage() // Checked
        {
            Console.WriteLine("No one is going to win this game, there's a tie! This game is over without winner.");
        }

        private static void printWinMessage(eSignsOfPlayers i_SignOfTheWinner)
        {
            Console.WriteLine($"Well done! The winner in this round is : {i_SignOfTheWinner}");
        }

        private static void printQuitMessage(eSignsOfPlayers i_SignOfTheWinner)
        {
            Console.WriteLine($"You Quit from the Game! The winner in this round is : {i_SignOfTheWinner}");
        }

        internal (int, int) GetValidPointFromUser(eSignsOfPlayers i_PlayerSign) // Checked
        {
            bool isValidPoint = false;
            int row = 0;
            int column = 0;
            Console.WriteLine(
                $@"{i_PlayerSign}! Its your turn now! 
Please enter one digit number as row and then column for your next move :");

            while(!isValidPoint)
            {
                Console.WriteLine($"{i_PlayerSign} : Please enter row number:");
                row = getValidNumberInBoardRangeFromUser();
                if(row == -1)
                {
                    column = -1;

                    break;
                }

                Console.WriteLine($"{i_PlayerSign} : Please enter column number:");
                column = getValidNumberInBoardRangeFromUser();
                if(column == -1)
                {
                    row = -1;

                    break;
                }

                isValidPoint = this.m_Game.IsEmptySpot(row, column);
                if(isValidPoint == false)
                {
                    Console.WriteLine(
                        $@"{i_PlayerSign} : Sorry, this spot is not empty. Please choose a new one! 
REMINDER: choose one digit number as ROW and then COLUMN in valid range size, for your next move");
                }
            }

            return (row, column);
        }

        private static void clearBoardBeforeNewMove() // Checked
        {
            // Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("Clear!");
        }

        private void printBoard() // Checked
        {
            Console.WriteLine(this.toStringBoard());
        }

        private string toStringBoard() // Checked
        {
            char space = ' ';
            StringBuilder resultedString = new StringBuilder($"{space}{space}");

            for(int col = 1; col < this.m_Game.GameBoard.Size; col++)
            {
                resultedString.Append($"{col}");
                resultedString.Append(space, 3);
            }

            resultedString.AppendLine();
            for(int row = 1; row < this.m_Game.GameBoard.Size; row++)
            {
                resultedString.Append($"{row}|");
                for(int col = 1; col < this.m_Game.GameBoard.Size; col++)
                {
                    eSignsOfPlayers currentSign = this.m_Game.GameBoard.GameBoard[row, col];
                    char currentChar = k_Empty;
                    if(currentSign == Cross)
                    {
                        currentChar = k_Cross;
                    }
                    else if(currentSign == Circle)
                    {
                        currentChar = k_Circle;
                    }

                    resultedString.Append($" {currentChar} |");
                }

                resultedString.AppendLine();
                resultedString.Append(space);
                for(int col = 1; col < this.m_Game.GameBoard.Size; col++)
                {
                    // 1-5 2-9 3-13 4-17 5-21 --> 1=5 2=5+4 3=5+4+3 4=5+4+3+2
                    resultedString.Append('=', 4);
                }

                resultedString.Append('=');
                resultedString.AppendLine();
            }

            return resultedString.ToString();
        }

        private static int getValidBoardSize()
        {
            Console.WriteLine("Please enter the board size - an integer between 3-9");

            return getValidSizeBoardFromUser();
        }

        private static bool isPlayerIsComputer()
        {
            bool isComputer = false;
            Console.WriteLine(
                @"Please choose who is you rival:
1) Human player
2) Computer");
            bool isValid = false;
            while(!isValid)
            {
                string userInput = Console.ReadLine();
                if(userInput == "1")
                {
                    // ReSharper disable once RedundantAssignment
                    isComputer = false;
                    isValid = true;
                }
                else if(userInput == "2")
                {
                    isComputer = true;
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Please enter 1 or 2 !");
                }
            }

            return isComputer;
        }

        /// <summary>
        /// TODO: remove the duplication to code $OMER$
        /// </summary>
        /// <returns></returns>
        private int getValidNumberInBoardRangeFromUser()
        {
            bool isValidNumberFormat = true;
            bool isValidRange = true;

            // ReSharper disable once RedundantAssignment
            int number = 0;
            string userInput = Console.ReadLine();
            if(userInput == "q" || userInput == "Q")
            {
                number = -1;
            }
            else
            {
                isValidNumberFormat = int.TryParse(userInput, out number);
                isValidRange = this.m_Game.IsInBoardRangeSize(number);
            }

            while(!isValidNumberFormat || !isValidRange)
            {
                if(!isValidNumberFormat)
                {
                    Console.WriteLine("Your input is not a one digit number, try again");
                }
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                else if(!isValidRange)
                {
                    Console.WriteLine($"Your input is not in range 1 - {this.m_Game.GameBoard.Size - 1}, try again");
                }

                userInput = Console.ReadLine();
                isValidNumberFormat = int.TryParse(userInput, out number);
                isValidRange = this.m_Game.IsInBoardRangeSize(number);
            }

            return number;
        }

        private static int getValidSizeBoardFromUser()
        {
            bool isValidNumberFormat = false;
            bool isValidRange = false;
            // ReSharper disable once InlineOutVariableDeclaration
            int number = 0;
            string userInput = Console.ReadLine();
            isValidNumberFormat = int.TryParse(userInput, out number);
            isValidRange = GameLogic.IsValidSizeBoard(number);
            while(!isValidNumberFormat || !isValidRange)
            {
                if(!isValidNumberFormat)
                {
                    Console.WriteLine("Your input is not a one digit number, try again");
                }
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                else if(!isValidRange)
                {
                    Console.WriteLine($"Your input is not in range 3-9, try again");
                }

                userInput = Console.ReadLine();
                isValidNumberFormat = int.TryParse(userInput, out number);
                isValidRange = GameLogic.IsValidSizeBoard(number);
            }

            return number;
        }

        internal void CleanAndShowBeforeNewTurn()
        {
            UserInterface.clearBoardBeforeNewMove();
            this.printBoard();
        }
    }
}