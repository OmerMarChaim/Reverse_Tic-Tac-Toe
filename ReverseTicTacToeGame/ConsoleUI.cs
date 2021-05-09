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
           bool WantAnotherGame = ValiditionUI.WantAnotherGame();
       }

       public static (int, int) GetValidSpotInBoard()
       {
           throw new NotImplementedException();
       }

       public static (int, int) GetNewValidSpotInBoard((int, int) spot)
       {
           Console.WriteLine("the spot"+spot.ToString()+"is not empty, pleasr enter a new one :)");
           return GetValidSpotInBoard();
       }
    }
}
