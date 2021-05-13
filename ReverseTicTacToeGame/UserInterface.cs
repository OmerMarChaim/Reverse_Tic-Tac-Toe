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
            Byte boardSize = ValiditionUi.GetValidBoardSize();
            bool player1IsComputer = ValiditionUi.IsPlayerIsComputer();
            bool player2IsComputer = ValiditionUi.IsPlayerIsComputer();
            GameLogic game = new GameLogic(boardSize, player1IsComputer, player2IsComputer);
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
            switch(GameLogic.CurrentGameState)
            
            {
                case GameLogic.eGameState.Win:
                    {
                        char signOfTheWinner = GameLogic.Winner.Sign;
                        PrintWinMessege(signOfTheWinner);
                        break;
                    }
                case GameLogic.eGameState.Tie:
                    PrintTieMessege();
                    break;
                case GameLogic.eGameState.Quit:
                    PrintQuitMessege();
                    break;
            }
            printScore();
        }

        private static void printScore()
        {
            Console.WriteLine(
                $"The current score is:{GameLogic.Player1.Sign}:{GameLogic.Player1.NumberOfWins}, {GameLogic.Player2.Sign}:{GameLogic.Player2.NumberOfWins}");
        }

        public static (int, int) GetValidSpotInBoard()
        {
            throw new NotImplementedException();
        }

        public static (int, int) GetNewValidSpotInBoard((int, int) i_Spot)
        {
            Console.WriteLine($"the spot {i_Spot.ToString()} is not empty, please enter a new one :)");

            return GetValidSpotInBoard();
        }

        public static void PrintTieMessege()
        { 
            Console.WriteLine($"No one is going to win this game, there's a tie! This game is over without winner.");
        }

        internal static void PrintWinMessege(char i_SignOfTheWinner)
        {
            Console.WriteLine($"Well done! The winner is {i_SignOfTheWinner}");
        }
        
        internal static void PrintQuitMessege()
        {
            Console.WriteLine($"No one is going to win this game, there's a tie! This game is over without winner.");
        }

        internal static (int, int) GetValidPointFromUser()
        {
            bool isEmptySpot = true;
            
            (int, int) validSpotInBoard = UserInterface.GetValidSpotInBoard();
            isEmptySpot = GameLogic.isThisEmptySpot(validSpotInBoard);
            // should be in function 
            while(!isEmptySpot)
            {
                validSpotInBoard = UserInterface.GetNewValidSpotInBoard(validSpotInBoard);
                isEmptySpot = GameLogic.isThisEmptySpot(validSpotInBoard);
            }
            
            return validSpotInBoard;
        }

        public static void UpdateTheUserInterfaceTheNewState()
        {
            throw new NotImplementedException();
        }
    }
}

/*
big update of logic and the following class:
1. Change name of ConsoleUI.cs to UserInterface.cs - when we will implement the same game with other UI, we dont want to change the word "CONSOLE" in the logic part.
2. GameLogic.cs :
 - edit names var
 - add member s_Winner that store the winner (player type) of the last game
 - modifiers
 - OneMoveInGame - try a new flow 
 - 
 
  */