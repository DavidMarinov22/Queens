using System;
using System.Text;

namespace QueensGame
{
    internal class Program
    {
        /*
              ____                                  __       ___                            
             / ___| __ _ _ __ ___   ___       ___  / _|     / _ \ _   _  ___  ___ _ __  ___ 
            | |  _ / _` | '_ ` _ \ / _ \     / _ \| |_     | | | | | | |/ _ \/ _ \ '_ \/ __|
            | |_| | (_| | | | | | |  __/    | (_) |  _|    | |_| | |_| |  __/  __/ | | \__ \
             \____|\__,_|_| |_| |_|\___|     \___/|_|       \__\_\\__,_|\___|\___|_| |_|___/
                                                                                            
        */
        static void Main()
        {
            //Pre declared variables
            Console.Title = "Queens";
            Board board = null;

            bool check = false;
            bool boardFull = false;
            bool sizeDone = false;

            int sizeOfX = 0, sizeOfY = 0;

            string player = "Player1";
            string continueGame = "";

            int playerRow = 0;
            int playerCol = 0;

            //Menu
            DrawTitle();
            Console.WriteLine("1.Play");
            Console.WriteLine("2.Help");
            Console.WriteLine("3.Exit");
            Console.WriteLine();
            bool isTrue = false;
            while (!isTrue)
            {
                Console.Write("Choose: ");
                string options = Console.ReadLine();
                switch (options)
                {
                    case "1":
                        Console.Clear();
                        DrawTitle();
                        isTrue = true;
                        break;
                    case "2":
                        Console.WriteLine();
                        DrawHelp();
                        break;
                    case "3":
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("!! - Please enter valid option. - !!");
                        Console.ResetColor();
                        break;
                }
            }
            
            //Size of the board
            while (!sizeDone)
            {
                try
                {
                    Console.WriteLine("Enter the board sizes:");
                    Console.Write(" The amount of columns(X) - ");
                    check = int.TryParse(Console.ReadLine(), out sizeOfX);
                    if(!check)
                    {
                        throw new ArgumentException("!! - Your value for columns is invalid. - !!");
                    }
                    Console.Write(" The amount of rows(Y) - ");
                    check = int.TryParse(Console.ReadLine(), out sizeOfY);
                    if (!check)
                    {
                        throw new ArgumentException("!! - Your value for rows is invalid. - !!");
                    }
                    board = new Board(sizeOfX, sizeOfY);
                    sizeDone = true;
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message.ToString());
                    Console.ResetColor();
                    sizeDone = false;
                }
            }

            //Draw board 
            Console.Clear();
            DrawTitle();
            // --DrawBorder
            string[][] border = new string[sizeOfY + 1][];
            DrawBorder(border, sizeOfY, sizeOfX);


            // --Positioning field next to border
            int row = Console.GetCursorPosition().Top - sizeOfY;
            int col = Console.GetCursorPosition().Left + 2;
            board.DrawField(row, col);

           

            //Controller
            while (!boardFull)
            {
                try
                {
                    if (player == "Player1")
                    {
                        Console.WriteLine("Player 1 enter the coordinates you want: ");
                    }
                    else
                    {
                        Console.WriteLine("Player 2 enter the coordinates you want: ");
                    }

                    //Value x
                    Console.Write("The value for (x) - ");
                    playerCol = int.Parse(Console.ReadLine()) - 1;
                    if (playerCol < 0 || playerCol >= board.SizeX)
                    {
                        throw new ArgumentException("!! - The value for (x) is invalid. - !!");
                    }

                    //Value Y
                    Console.Write("The value for (y) - ");
                    playerRow = int.Parse(Console.ReadLine()) - 1;
                    if (playerRow < 0 || playerRow >= board.SizeX)
                    {
                        throw new ArgumentException("!! - The value for (y) is invalid. - !!");
                    }
                    board.InsertPlayer(player, playerRow, playerCol);
                    
                    //Redraw field
                    Console.Clear();
                    DrawTitle();
                    DrawBorder(border, sizeOfY, sizeOfX);
                    row = Console.GetCursorPosition().Top - sizeOfY;
                    col = Console.GetCursorPosition().Left + 2;
                    board.DrawField(row, col);
                    if (player == "Player1")
                        player = "Player2";
                    else
                        player = "Player1";
                    boardFull = board.CheckBordIsFull();
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message.ToString());
                    Console.ResetColor();
                }
            }
            //End and Winner
            Console.Clear();
            DrawWinner(player);
            Console.Write("Do you want to play more ? \n(Y / N): ");
            continueGame = Console.ReadLine();
            continueGame = continueGame.ToLower();
            if (continueGame == "y")
            {
                Console.Clear();
                Main();
            }
        }

        private static void DrawHelp()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("ABOUT THE GAME");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(" -- Queens game is a board game played between two players.\n    The game is the work of the great minds of the I Can Here and Now initiative, in 2022 in June.\n    The game includes strategic thinking and good concentration into the game. ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("GAMEPLAY");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(" -- At the beginning you need to set the size of the board, which should be in the range of 4 - 30.\n    Then the first player must set the coordinates for the queen they want to place in the range of 1 - n size.\n    The '*' are possible moves for queen and player cannot place another queen there.\n    The winner is the one who last puts a queen and fills the whole board.");
            Console.ResetColor();
        }

        private static void DrawWinner(string player)
        {
            StringBuilder sb = new StringBuilder();
            if (player == "Player1")
            {
                sb.AppendLine(" ____  _                           ____               _              _ ");
                sb.AppendLine("|  _ \\| | __ _ _   _  ___ _ __    |___ \\    __      _(_)_ __  ___   | |");
                sb.AppendLine("| |_) | |/ _` | | | |/ _ \\ '__|     __) |   \\ \\ /\\ / / | '_ \\/ __|  | |");
                sb.AppendLine("|  __/| | (_| | |_| |  __/ |       / __/     \\ V  V /| | | | \\__ \\  |_|");
                sb.AppendLine("|_|   |_|\\__,_|\\__, |\\___|_|      |_____|     \\_/\\_/ |_|_| |_|___/  (_)");
                sb.AppendLine("               |___/                                                   ");

            }
            else
            {
                sb.AppendLine(" ____  _                           _              _              _ ");
                sb.AppendLine("|  _ \\| | __ _ _   _  ___ _ __    / |   __      _(_)_ __  ___   | |");
                sb.AppendLine("| |_) | |/ _` | | | |/ _ \\ '__|   | |   \\ \\ /\\ / / | '_ \\/ __|  | |");
                sb.AppendLine("|  __/| | (_| | |_| |  __/ |      | |    \\ V  V /| | | | \\__ \\  |_|");
                sb.AppendLine("|_|   |_|\\__,_|\\__, |\\___|_|      |_|     \\_/\\_/ |_|_| |_|___/  (_)");
                sb.AppendLine("               |___/                                               ");
            }
            sb.AppendLine("══════════════════════════════════════════════════════════════════════════");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(sb.ToString());
            Console.ResetColor();
        }
        private static void DrawTitle()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("════════════════════════════════════════════════════════════════════════════════════");
            sb.AppendLine("  ____                                  __       ___                            ");
            sb.AppendLine(" / ___| __ _ _ __ ___   ___       ___  / _|     / _ \\ _   _  ___  ___ _ __  ___ ");
            sb.AppendLine("| |  _ / _` | '_ ` _ \\ / _ \\     / _ \\| |_     | | | | | | |/ _ \\/ _ \\ '_ \\/ __|");
            sb.AppendLine("| |_| | (_| | | | | | |  __/    | (_) |  _|    | |_| | |_| |  __/  __/ | | \\__ \\");
            sb.AppendLine(" \\____|\\__,_|_| |_| |_|\\___|     \\___/|_|       \\__\\_\\__,_|\\___|\\___|_| |_|___/");
            sb.AppendLine("════════════════════════════════════════════════════════════════════════════════════");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(sb.ToString());
            Console.ResetColor();
        }
        private static void DrawBorder(string[][] border, int sizeOfY, int sizeOfX)
        {
            for (int y = 0; y < sizeOfY + 1; y++)
            {
                if (y == 0)
                {
                    border[y] = new string[sizeOfX + 1];
                    for (int x = 0; x < sizeOfX + 1; x++)
                    {
                        if (x == 1)
                        {
                            border[y][x] = "   " + (x);
                        }
                        else if (x != 1 && x > 0)
                        {
                            border[y][x] = "    " + (x);
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Join(" ", border[y]));
                }
                else
                {
                    border[y] = new string[1];
                    border[y][0] = (y).ToString();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Join(" ", border[y]));
                }
                Console.ResetColor();
            }
        }
    }
}
