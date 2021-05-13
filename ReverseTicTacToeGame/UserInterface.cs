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
            if(currentState == GameLogic.eGameState.Win)
            {
                char signOfTheWinner = GameLogic.Winner.Sign;
                PrintWinMessage(signOfTheWinner);
            }
            else if(currentState == GameLogic.eGameState.Tie)
            {
                PrintTieMessage();
            }
            else if(currentState == GameLogic.eGameState.Quit)
            {
                PrintQuitMessage();
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
            bool isEmptySpot = true;
            
            (int, int) validSpotInBoard = GetValidSpotInBoard();
            isEmptySpot = GameLogic.isThisEmptySpot(validSpotInBoard);
          
            while(!isEmptySpot)
            {
                validSpotInBoard = GetNewValidSpotInBoard(validSpotInBoard);
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

