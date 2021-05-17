using System;

namespace ReverseTicTacToeGame
{
    static class ValiditionUi
    {
        public static int GetValidBoardSize()
        {
            Console.WriteLine("Please enter the board size - an integer between 3-9");

            return GetValidSizeBoardFromUser();
        }

        //
        public static bool IsPlayerIsComputer()
        {
            bool isComputer = false;
            Console.WriteLine(
                @"Please choose who is you rival:
1) Human player
2) Computer");
            bool isValid = false;
            while(!isValid)
            {
                string userInput = Console.ReadLine();
                if(userInput == "1")
                {
                    isComputer = false;
                    isValid = true;
                }
                else if(userInput == "2")
                {
                    isComputer = true;
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Please enter 1 or 2 !");
                }
            }

            return isComputer;
        }

        internal static int GetValidNumberInBoardRangeFromUser()
        {
            bool isValidNumberFormat = true;
            bool isValidRange = true;

            int number = 0;
            string userInput = Console.ReadLine();
            if(userInput == "q" || userInput == "Q")
            {
                number = -1;
            }
            else
            {
                isValidNumberFormat = int.TryParse(userInput, out number);
                isValidRange = GameLogic.IsInBoardRangeSize(number);
            }

            while(!isValidNumberFormat || !isValidRange)
            {
                if(!isValidNumberFormat)
                {
                    Console.WriteLine("Your input is not a one digit number, try again");
                }
                else if(!isValidRange)
                {
                    Console.WriteLine($"Your input is not in range 1 - {GameLogic.GameBoard.Size - 1}, try again");
                }

                userInput = Console.ReadLine();
                isValidNumberFormat = int.TryParse(userInput, out number);
                isValidRange = GameLogic.IsInBoardRangeSize(number);
            }

            return number;
        }

        internal static int GetValidSizeBoardFromUser()
        {
            bool isValidNumberFormat = false;
            bool isValidRange = false;
            isValidNumberFormat = false;
            int number = 0;
            string userInput = Console.ReadLine();
            isValidNumberFormat = int.TryParse(userInput, out number);
            isValidRange = GameLogic.IsValidSizeBoard(number);
            while(!isValidNumberFormat || !isValidRange)
            {
                if(!isValidNumberFormat)
                {
                    Console.WriteLine("Your input is not a one digit number, try again");
                }
                else if(!isValidRange)
                {
                    Console.WriteLine($"Your input is not in range 3-9, try again");
                }

                userInput = Console.ReadLine();
                isValidNumberFormat = int.TryParse(userInput, out number);
                isValidRange = GameLogic.IsValidSizeBoard(number);
            }

            return number;
        }
    }
}