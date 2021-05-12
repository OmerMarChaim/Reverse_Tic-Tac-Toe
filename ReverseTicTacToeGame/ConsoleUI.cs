using System;


namespace ReverseTicTacToeGame
{
    internal class ConsoleUI
    {
        public enum eTurnOf
        {
            Player1, Player2
        }
        public static void InitGame()
       {
        ///ToDo
      Byte boardSize = ValiditionUI.getValidBoardSize();
      bool player1IsComputer = ValiditionUI.isPlayerIsComputer();
      bool player2IsComputer = ValiditionUI.isPlayerIsComputer();
      new GameLogic(boardSize, player1IsComputer, player2IsComputer);
      startGame(game);
       }

        private static void startGame(GameLogic i_Game)
        {
            bool wantToPlayFlag = true;

            eTurnOf currentPlaying = eTurnOf.Player1;
            (int, int) point;
            while(wantToPlayFlag == true)
            {
                while(i_Game.CuurentGameState == GameLogic.gameState.Playing)
                {
                    switch(currentPlaying)
                    {
                        case eTurnOf.Player1
                    }
                }
            }
        }














        public static bool isUserWantAnotherGame()
       {
           ///ask if the user want another game 
           bool WantAnotherGame = ValiditionUI.WantAnotherGame();
           return false;
       }

       public static (int, int) GetValidSpotInBoard()
       {
           throw new NotImplementedException();
       }

       public static (int, int) GetNewValidSpotInBoard((int, int) spot)
       {
           Console.WriteLine("the spot"+spot.ToString()+"is not empty, please enter a new one :)");
           return GetValidSpotInBoard();
       }

       public static void PrintTieMessege()
       {
           ///Todo
           /// print tie messege
           /// call the function to ask if want another game
       }

        internal static void PrintWinMessege(string v)
        {
            throw new NotImplementedException();
        }
    }
}
