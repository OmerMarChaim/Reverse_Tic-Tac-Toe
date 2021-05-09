# Reverse_Tic-Tac-Toe
The program will allow two human players to play against eachother in turns, or for a human player to play against the computer. The program will display an empty board of of cells:
1 2 3 4 5
1| | | | | |
=====================
2| | | | | |
=====================
3| | | | | |
=====================
4| | | | | |
=====================
5| | | | | |
=====================
Each turn, the user will be asked to choose in which cell he wants to put his ‘coin’ (‘X’ or ‘O’). The goal is to avoid having a sequence of N coins (N is the width of the board).
Object Oriented Programing in .NET and C# Exercise 2 Guy Ronen ©
2
The flow:
1. The user will be asked to choose board size (minimum 3x3, maximum 9x9). The board must be a square (rows == cols).
2. The user will be asked to choose weather to play against a computer upponent or another human upponent.
3. The game starts with an initialize board (5x5 example):
1 2 3 4 5
1| | | | | |
=====================
2| | | | | |
=====================
3| | | | | |
=====================
4| | | | | |
=====================
5| | | | | |
=====================
Dani's turn:
4. Each turn, the user will be asked to choose in which cell he wants to put his ‘coin’. If the cell is 'illegal' (occupied or out of bounds), the user will be prompted to enter a valid choice, and so on until he chooses a legal choice (a legal choice is an empty cell in the board. Any other input is invalid and a proper message should be displayed to the user).
5. After a valid input was entered by the user, the screen will be cleared and the board will be re-drawn with the selected cell 'exposed':
1 2 3 4 5
1| | | | | |
=====================
2| X | | | | |
=====================
3| | | O | O | |
=====================
4| | X | | X | |
=====================
5| | | X | | O |
=====================
6. If the new move did not complete a sequence of N coins (5 coins in this case), the turn will move to the opponent (back to phase 5).
7. If the new move DID complete a sequence of N coins, this is the losing player. The winner (the one without a sequence of N coins) will get a point and the current updated score will be displayed.
8. If the new mode did not complete a sequence and the board is full, a TIE message will be displayed.
9. After a tie or a win, The user will be ask to decide if he wants to play another round, or to quit.
10. Starting stage 5, the user can quit the app at any point by typing 'Q' as input.
