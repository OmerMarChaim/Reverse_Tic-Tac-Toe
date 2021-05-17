using System;
using System.Text;
using static ReverseTicTacToeGame.Enums;

namespace ReverseTicTacToeGame
{
    internal static class UserInterface
    {
        public enum eTurnOf
        {
            Player1,
            Player2
        }
        private const char k_Circle = 'O';
        private const char k_Cross = 'X';
        private static char player1Sign = k_Cross;
      private static char player2Sign = k_Circle;
      public static char Player1Sign
        {
          get { return player1Sign; }
      }
      public static char Player2Sign
      {
          get { return player2Sign; }
      }


        public static void InitGame()
        {
            Console.WriteLine("Hello and Welcome to our game!");

            int boardSize = ValiditionUi.GetValidBoardSize() + 1; /// we want board size +1 then what we got 
            bool player1IsComputer = false;
          

            bool player2IsComputer = ValiditionUi.IsPlayerIsComputer();
            GameLogic game = new GameLogic(boardSize, player1IsComputer, player2IsComputer);
            startGame();
        }

        private static void startGame()
        {
            bool wantAnotherGameFlag = true;

            //eTurnOf currentPlaying = eTurnOf.Player1;
            // (int, int) point;
            while(wantAnotherGameFlag)
            {
                while(GameLogic.CurrentGameState == GameLogic.eGameState.Playing)
                {
                    GameLogic.OneMoveInGame();
                }

                updateTheUserInterfaceAccordingTheState(); // win/tie? "msg" ->> score 
                wantAnotherGameFlag = isUserWantAnotherGame(); // ->> Do you want Another game? please press y/n
                if(wantAnotherGameFlag)
                {
                    GameLogic.WantAnotherGame();
                }
            }
        }

        //  IsUserWantAnotherGame() ask the user if want another game  
        private static bool isUserWantAnotherGame()
        {
            Console.WriteLine($"Do you want another game? please enter 'Y' for yes ,'Q' for Quit");

            return getValidYesOrNo();
        }

        private static bool getValidYesOrNo()
        {
            string userInput;
            bool yesOrNoFlag = false;
            bool isValid = false;
            while(!isValid)
            {
                userInput = Console.ReadLine();
                if(userInput == "Y" || userInput == "Q")
                {
                    isValid = true;
                    if(userInput == "Y")
                    {
                        yesOrNoFlag = true;
                    }

                    if(yesOrNoFlag == false)
                    { 
                        Console.WriteLine("Thank you for playing with us! see you in Ex03");
                    }
                    break;
                }
                Console.WriteLine("Your input is invalid. if you want to play more enter 'Y', other 'Q'");
            }

            return yesOrNoFlag;
        }

        private static void updateTheUserInterfaceAccordingTheState()
        {
            GameLogic.eGameState currentState = GameLogic.CurrentGameState;
            eSignsOfPlayers signOfTheWinner = GameLogic.WinnerPlayer.Sign;

            if(currentState == GameLogic.eGameState.Win)
            {
                PrintWinMessage(signOfTheWinner);
            }
            else if(currentState == GameLogic.eGameState.Tie)
            {
                PrintTieMessage();
            }
            else if(currentState == GameLogic.eGameState.Quit)
            {
                PrintQuitMessage(signOfTheWinner);
            }

            printScore();
        }

        private static void printScore()
        {
            Console.WriteLine(
                $"The current score is: {GameLogic.Player1.Sign}:{GameLogic.Player1.NumberOfWins}, {GameLogic.Player2.Sign}:{GameLogic.Player2.NumberOfWins}");
        }

        public static void PrintTieMessage()
        {
            Console.WriteLine($"No one is going to win this game, there's a tie! This game is over without winner.");
        }

        internal static void PrintWinMessage(eSignsOfPlayers i_SignOfTheWinner)
        {
            Console.WriteLine($"Well done! The winner in this round is : {i_SignOfTheWinner}");
        }

        internal static void PrintQuitMessage(eSignsOfPlayers i_SignOfTheWinner)
        {
            Console.WriteLine(
                $"You Quit from the Game! The winner in this round is : {i_SignOfTheWinner}");
        }

        internal static (int, int) GetValidPointFromUser(eSignsOfPlayers i_PlayerSign)
        {
            bool isValidPoint = false;
            int row = 0;
            int column = 0;
            Console.WriteLine($@"{i_PlayerSign} :this is your turn ! 
Please enter one digit number as row and then column for your next move :");

            while (!isValidPoint)
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
                if (column == -1)
                {
                    row = -1;
                    break;
                }
                isValidPoint = GameLogic.IsEmptySpot(row, column);
                if(isValidPoint == false)
                {
                    Console.WriteLine($"{i_PlayerSign} : Sorry , this spot is not empty, please choose a new one! "
                                      + $"we remind you to choose one digit number as row and then column for your next move ");

                }
            }
            return (row, column);
        }

        public static void clearBoardBeforeNewMove()
        {
            Ex02.ConsoleUtils.Screen.Clear();
          //  Console.WriteLine("Clear!");
        }

        public static void printBoard()
        {
            Console.WriteLine(ToStringBoard()); 
           
        }
        internal static string ToStringBoard()
        {

            StringBuilder resultedString = new StringBuilder();
            for (int i = 1; i < GameLogic.GameBoard.Size; i++)
            {
                resultedString.Append($"   {i} ");
            }

            resultedString.AppendLine();
            for (int row = 1; row < GameLogic.GameBoard.Size; row++)
            {
                resultedString.Append($"{row}|");
                for (int col = 1; col < GameLogic.GameBoard.Size; col++)
                {
                    eSignsOfPlayers current = GameLogic.GameBoard.GameBoard[row, col];
                    if (current == eSignsOfPlayers.Empty)
                    {
                        resultedString.Append($"    |");
                    }

                    else if (current == eSignsOfPlayers.Player1)
                    {
                        resultedString.Append($" {Player1Sign} |");
                    }
                    else
                    {
                        resultedString.Append($" {Player2Sign} |");
                    }

                }

                resultedString.AppendLine("");
                resultedString.Append(" ");
                for (int col = 1; col < GameLogic.GameBoard.Size; col++)
                {
                    resultedString.Append($"=====");
                }

                resultedString.AppendLine();
            }

            return resultedString.ToString();
        }
    }
}