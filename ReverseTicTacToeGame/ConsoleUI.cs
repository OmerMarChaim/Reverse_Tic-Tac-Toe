using System;


namespace ReverseTicTacToeGame
{
    internal class ConsoleUI
    {
       public void InitGame()
       {
        ///ToDo
      Byte boardSize = ValiditionUI.getValidBoardSize();
      bool player1IsComputer = ValiditionUI.isPlayerIsComputer();
      bool player2IsComputer = ValiditionUI.isPlayerIsComputer();
     GameLogic game= new GameLogic(boardSize, player1IsComputer, player2IsComputer);
   
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
