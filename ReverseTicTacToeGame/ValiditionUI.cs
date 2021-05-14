using System;

namespace ReverseTicTacToeGame
{
    class ValiditionUi
    {
        public static int GetValidBoardSize()
        {
            //get number between 3-9
            //return number between 3-9 
            Console.WriteLine("now enter the board size. please enter an integer between 3-9");
            return getValidNumberInBoardRangeFromUser() +1 ;

        }
        //
        public static bool IsPlayerIsComputer()
        {
            Console.WriteLine(@"Please choose how is you rival:
                                1) Human player
                                2) Computer");
            bool isValid = false;
            while(!isValid)
            {
                string userInput = Console.ReadLine();
                int oneOrTow; 
                if(userInput == "1")
                {
                    oneOrTow = 1;
                }else if(userInput == "2")
                {
                    oneOrTow = 2;
                }
                else
                {
                    Console.WriteLine("Please enter 1 or 2 !");
                }
            }
        }
        
        internal static int getValidNumberInBoardRangeFromUser()
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

    }
}
