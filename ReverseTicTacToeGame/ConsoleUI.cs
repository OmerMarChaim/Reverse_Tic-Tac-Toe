using System;


namespace ReverseTicTacToeGame
{
    internal class ConsoleUI
    {
       public void InitGame()
       {
        ///ToDo
      int boardSize = ValiditionUI.getValidBoardSize();
      bool player1IsComputer = ValiditionUI.isPlayerIsComputer();
      bool player2IsComputer = ValiditionUI.isPlayerIsComputer();
      GameLogic.InitSettings(boardSize, player1IsComputer, player2IsComputer);
       }
    }
}
