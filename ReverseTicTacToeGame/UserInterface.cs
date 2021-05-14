using System;

namespace ReverseTicTacToeGame
{
    internal static class UserInterface
    {
        public enum eTurnOf
        {
            Player1,
            Player2
        }

        public static void InitGame()
        {
            //ToDo
            int boardSize = ValiditionUi.GetValidBoardSize() + 1 ; /// we want board size +1 then what we got 
            bool player1IsComputer = ValiditionUi.IsPlayerIsComputer();
            bool player2IsComputer = ValiditionUi.IsPlayerIsComputer();
            GameLogic game = new GameLogic( boardSize  , player1IsComputer, player2IsComputer);
            startGame();
        }

        private static void startGame()
        {
            bool wantAnotherGameFlag = true;

            //eTurnOf currentPlaying = eTurnOf.Player1;
            (int, int) point;
            while(wantAnotherGameFlag == true)
            {
                while(GameLogic.CurrentGameState == GameLogic.eGameState.Playing)
                {
                    GameLogic.OneMoveInGame();

                }

                updateTheUserInterfaceAccordingTheState(); // win/tie? "msg" ->> score 
                wantAnotherGameFlag = isUserWantAnotherGame(); // ->> Do you want Another game? please press y/n
            }
        }

        // TODO: IsUserWantAnotherGame() ask the user if want another game  
        private static bool isUserWantAnotherGame()
        {
            Console.WriteLine($"Do you want another game? please enter y/n");

            return getValidYesOrNo();
        }

        private static bool getValidYesOrNo()
        {
            bool yesOrNoFlag = false;
            bool isValid = false;
            while(!isValid)
            {
                string userInput = Console.ReadLine();
                if(userInput == "y" || userInput == "n")
                {
                    isValid = true;
                    if(userInput == "y")
                    {
                        yesOrNoFlag = true;
                    }
                }
            }

            return yesOrNoFlag;
        }

        private static void updateTheUserInterfaceAccordingTheState()
        {
            GameLogic.eGameState currentState = GameLogic.CurrentGameState;
            //switch(currentState)
            char signOfTheWinner = GameLogic.WinnerPlayer.Sign;

            if (currentState == GameLogic.eGameState.Win)
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
                $"The current score is:{GameLogic.Player1.Sign}:{GameLogic.Player1.NumberOfWins}, {GameLogic.Player2.Sign}:{GameLogic.Player2.NumberOfWins}");
        }

 

        public static void PrintTieMessage()
        {
            Console.WriteLine($"No one is going to win this game, there's a tie! This game is over without winner.");
        }

        internal static void PrintWinMessage(char i_SignOfTheWinner)
        {
            Console.WriteLine($"Well done! The winner is {i_SignOfTheWinner}");
        }

        internal static void PrintQuitMessage(char i_SignOfTheWinner)
        {
            ///need to add
            Console.WriteLine($"The winner is {i_SignOfTheWinner} after the other player quit");
        }

        internal static (int, int) GetValidPointFromUser()
        {
            (int row, int column) spotInValidFormatUI = ValiditionUi.getValidFormatOfSpot();
            bool isValidSpotOnBoard = false;

            Console.WriteLine($"Please enter one digit number as row spot for your next move:");
            int row = getValidNumberInBoardRangeFromUser();
            Console.WriteLine($"Please enter one digit number as column spot for your next move:");
            int column = getValidNumberInBoardRangeFromUser();
            return (row, column);
        }

       

        private static int getValidNumberInBoardRangeFromUser()
        {
            bool isValidNumberFormat = false;
            bool isValidRange = false;
            isValidNumberFormat = false;
            int number = 0;
            string userInput = Console.ReadLine();
            isValidNumberFormat = int.TryParse(userInput, out number);
            isValidRange = GameLogic.isInRangeOfBoard(number);
            while(!isValidNumberFormat || !isValidRange)
            {
                if(!isValidNumberFormat)
                {
                    Console.WriteLine("Your input is not a one digit number, try again");
                    userInput = Console.ReadLine();
                    isValidNumberFormat = int.TryParse(userInput, out number);
                    isValidRange = GameLogic.isInRangeOfBoard(number);
                }
                else if(!isValidRange)
                {
                    Console.WriteLine($"Your input is not in range 1 - {GameLogic.GameBoard.Size - 1}, try again");
                    Console.Read();
                    isValidNumberFormat = int.TryParse(userInput, out number);
                    isValidRange = GameLogic.isInRangeOfBoard(number);
                }
            }

            return number;
        }

        public static void UpdateTheUserInterfaceTheNewState()
        {
            throw new NotImplementedException();
        }

        public static void clearBoardBeforeNewMove()
        {
            throw new NotImplementedException();
        }

        public static void printBoard()
        {
            throw new NotImplementedException();
        }
    }
}

