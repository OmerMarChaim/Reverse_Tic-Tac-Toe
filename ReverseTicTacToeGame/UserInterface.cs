using System;
using System.Text;
using static ReverseTicTacToeGame.Enums;
using static ReverseTicTacToeGame.GameLogic;
using static ReverseTicTacToeGame.GameLogic.eGameState;
using static ReverseTicTacToeGame.Enums.eSignsOfPlayers;

namespace ReverseTicTacToeGame
{
    internal static class UserInterface
    {
        private const char k_Circle = 'O'; 
        private const char k_Cross = 'X';
        private const char k_Empty = ' ';
        private const char k_Player1Sign = k_Cross;
        private const char k_Player2Sign = k_Circle;
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

            int boardSize = ValiditionUi.GetValidBoardSize() + 1;
            bool player1IsComputer = false;
            bool player2IsComputer = ValiditionUi.IsPlayerIsComputer();
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            new GameLogic(boardSize, player1IsComputer, player2IsComputer);
            UserInterface.PrintBoard();
            startGame();
        } 

        private static void startGame() // Checked
        {
            bool wantAnotherGameFlag = true;

            while(wantAnotherGameFlag)
            {
                while(GameLogic.CurrentGameState == GameLogic.eGameState.Playing)
                {
                    GameLogic.OneRoundInGame();
                }

                updateTheUserInterfaceAccordingTheState();
                wantAnotherGameFlag = isUserWantAnotherGame();
            }
        }

        private static bool isUserWantAnotherGame() // Checked
        {
            Console.WriteLine($"Do you want another game? please enter 'Y' for yes ,'Q' for Quit");
            bool wantAnotherGameFlag = getValidYesOrNo();
            // In case which the user want another game - we ask the logic to make for us Preparing For Another Round
            if(wantAnotherGameFlag)
            {
                GameLogic.PreparingForAnotherRound();
            }
            else
            {
                Console.WriteLine("Thank you for playing with us! see you in Ex03");
            }

            return wantAnotherGameFlag;
        }

        private static bool getValidYesOrNo() // Checked
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
                    if(userInput == "Y")
                    {
                        yesOrNoFlag = true;
                    }
                }

                Console.WriteLine("Your input is invalid. if you want to play more enter 'Y', other 'Q'");
            }

            return yesOrNoFlag;
        }

        private static void updateTheUserInterfaceAccordingTheState() // Checked
        {
            eGameState currentState = CurrentGameState;
            eSignsOfPlayers signOfTheWinner = WinnerPlayer.Sign;

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

        private static void printScore() // Checked
        {
            Console.WriteLine(
                $@"The current score is:
{GameLogic.Player1.Sign}:{GameLogic.Player1.NumberOfWins}
{GameLogic.Player2.Sign}:{GameLogic.Player2.NumberOfWins}");
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

        internal static (int, int) GetValidPointFromUser(eSignsOfPlayers i_PlayerSign) // Checked
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
                row = ValiditionUi.GetValidNumberInBoardRangeFromUser();
                if(row == -1)
                {
                    column = -1;

                    break;
                }

                Console.WriteLine($"{i_PlayerSign} : Please enter column number:");
                column = ValiditionUi.GetValidNumberInBoardRangeFromUser();
                if(column == -1)
                {
                    row = -1;

                    break;
                }

                isValidPoint = GameLogic.IsEmptySpot(row, column);
                if(isValidPoint == false)
                {
                    Console.WriteLine(
                        $@"{i_PlayerSign} : Sorry, this spot is not empty. Please choose a new one! 
REMINDER: choose one digit number as ROW and then COLUMN in valid range size, for your next move");
                }
            }

            return (row, column);
        }

        public static void ClearBoardBeforeNewMove() // Checked
        {
            // Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("Clear!");
        }

        public static void PrintBoard() // Checked
        {
            Console.WriteLine(toStringBoard());
        }

        private static string toStringBoard()
        {
            char space = ' ';
            StringBuilder resultedString = new StringBuilder($"{space}{space}");
            
            for(int col = 1; col < GameLogic.GameBoard.Size; col++)
            {
                resultedString.Append($"{col}");
                resultedString.Append(space,3);
            }

            resultedString.AppendLine();
            for(int row = 1; row < GameLogic.GameBoard.Size; row++)
            {
                resultedString.Append($"{row}|");
                for(int col = 1; col < GameLogic.GameBoard.Size; col++)
                {
                    eSignsOfPlayers currentSign = GameLogic.GameBoard.GameBoard[row, col];
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
                for(int col = 1; col < GameLogic.GameBoard.Size; col++)
                { // 1-5 2-9 3-13 4-17 5-21 --> 1=5 2=5+4 3=5+4+3 4=5+4+3+2
                        resultedString.Append('=', 4);
                }
                resultedString.Append('=');
                resultedString.AppendLine();
            }
            

            return resultedString.ToString();
        }
    }
}