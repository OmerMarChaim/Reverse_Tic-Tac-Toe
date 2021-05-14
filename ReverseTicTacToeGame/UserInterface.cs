using System;
using System.Text;

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
            bool player1IsComputer = false;
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
            Console.WriteLine($"Well done! The winner in this round is the player that play with the sign:{i_SignOfTheWinner}");
        }

        internal static void PrintQuitMessage(char i_SignOfTheWinner)
        {
            Console.WriteLine($"You Quit from the Game! The winner in this round is the player that play with the sign:{i_SignOfTheWinner}");
        }

        internal static (int, int) GetValidPointFromUser()
        {
            Console.WriteLine($"Please enter one digit number as row spot for your next move:");
            int row = ValiditionUi.getValidNumberInBoardRangeFromUser();
            Console.WriteLine($"Please enter one digit number as column spot for your next move:");
            int column = ValiditionUi.getValidNumberInBoardRangeFromUser();
            return (row, column);
        }

        public static void clearBoardBeforeNewMove()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }

        public static void printBoard()
        {
            
        }
    }
}

