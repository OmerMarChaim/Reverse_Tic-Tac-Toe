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
    }
}
