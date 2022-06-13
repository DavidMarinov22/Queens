using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueensGame
{
    public class Board
    {
        private string[,] board;
        private int sizeX = 0;
        private int sizeY = 0;

        public int SizeX 
        {
            get
            {
                return sizeX;
            }
            private set
            {
                if (value <= 3 || value > 30)
                {
                    throw new ArgumentException("!! - Your value for columns is invalid. - !!");
                }
                sizeX = value;
            }
        }
        public int SizeY 
        {
            get
            {
                return sizeY;
            }
            private set
            {
                if (value <= 3 || value > 30)
                {
                    throw new ArgumentException("!! - Your value for rows is invalid. - !!");
                }
                sizeY = value;
            }
        }
        
        public Board(int sizeX, int sizeY)
        {
            this.SizeX = sizeX; 
            this.SizeY = sizeY;

            this.board = new string[SizeY, SizeX];
            FillBoard();
        }

        public void DrawField(int top, int left)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                Console.SetCursorPosition(left, top + row);
                for (int coll = 0; coll < board.GetLength(1); coll++)
                {
                    if (board[row, coll] == "{ * }")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(board[row, coll] + " ");
                        Console.ResetColor();
                    }
                    else if (board[row, coll] == "{ 1 }" || board[row, coll] == "{ 2 }")
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(board[row, coll] + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(board[row, coll] + " ");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
        }
        public void InsertPlayer(string player, int playerRow, int playerCol)
        {
            string playerSymbol = "";
            if (player == "Player1")
                playerSymbol = "{ 1 }";
            else
                playerSymbol = "{ 2 }";

            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int coll = 0; coll < board.GetLength(1); coll++)
                {
                    if (board[playerRow, playerCol] == "{ 1 }")
                    {
                        throw new ArgumentException("!! - You cannot place queen on this position. - !!"); 
                    }
                    else if (board[playerRow, playerCol] == "{ 2 }")
                    {
                        throw new ArgumentException("!! - You cannot place queen on this position. - !!");
                    }
                    else if(board[playerRow, playerCol] == "{ * }")
                    {
                        throw new ArgumentException("!! - You cannot place queen on this position. - !!");
                    }
                    else if (row == playerRow && coll == playerCol)
                    {
                        int[] startColAndRow = GetStartColAndRow(playerRow, playerCol);
                        board[row, coll] = playerSymbol;
                        FillMoves(startColAndRow, playerRow, playerCol);
                        return;
                    }
                }
            }
            
        }
        public bool CheckBordIsFull()
        {
            foreach (var item in board)
            {
                if (item == "{ 0 }")
                {
                    return false;
                }
            }
            return true;
        }

        private void FillMoves(int[] startColAndRow, int playerRow, int playerCol)
        {
            int row = startColAndRow[1];
            int col = startColAndRow[0];
            //DiagonalLeftToRight
            for (int r = row; r < board.GetLength(0); r++)
            {
                for (int coll = col; coll < board.GetLength(1); coll++)
                {
                    if (board[r, coll] == "{ 1 }" || board[r, coll] == "{ 2 }")
                    {
                        break;
                    }
                    board[r, coll] = "{ * }";
                    break;
                }
                col++;
            }
            //LeftAndRight
            for (int c = 0; c < board.GetLength(1); c++)
            {
                if (board[playerRow, c] == "{ 1 }")
                {
                    continue;
                }
                else if (board[playerRow, c] == "{ 2 }")
                {
                    continue;
                }
                else
                {
                    board[playerRow, c] = "{ * }";
                }
            }
            //TopAndBottom
            for (int r = 0; r < board.GetLength(0); r++)
            {
                if (board[r, playerCol] == "{ 1 }")
                {
                    continue;
                }
                else if (board[r, playerCol] == "{ 2 }")
                {
                    continue;
                }
                else
                {
                    board[r, playerCol] = "{ * }";
                }
            }
            //DiagonalRightToLeft
            int[] rights = GetStartColAndRowRight(playerRow, playerCol);
            row = rights[1];
            col = rights[0];
            for (int r = row; r < board.GetLength(0); r++)
            {
                for (int coll = col; coll >= 0; coll++)
                {
                    if (board[r, coll] == "{ 1 }" || board[r, coll] == "{ 2 }")
                    {
                        break;
                    }
                    board[r, coll] = "{ * }";
                    break;
                }
                col--;
            }
        }
        private int[] GetStartColAndRow(int playerRow, int playerCol)
        {
            int[] startColAndRow = new int[2];
            startColAndRow[0] = playerCol;
            startColAndRow[1] = playerRow;
            if (playerCol == 0)
            {
                return startColAndRow;
            }
            if (startColAndRow[1] == 0)
            {
                return startColAndRow;
            }
            if (startColAndRow[0] >= startColAndRow[1])
            {
                for (int row = startColAndRow[1] - 1; row >= 0; row--)
                {
                    startColAndRow[0]--;
                    startColAndRow[1]--;
                }
            }
            else
            {
                for (int col = startColAndRow[0] - 1; col >= 0; col--)
                {
                    startColAndRow[0]--;
                    startColAndRow[1]--;
                }
            }

            return startColAndRow;
        }
        private int[] GetStartColAndRowRight(int playerRow, int playerCol)
        {
            int[] startColAndRow = new int[2];
            startColAndRow[0] = playerCol;
            startColAndRow[1] = playerRow;
            if (playerCol == board.GetLength(1) - 1)
            {
                return startColAndRow;
            }
            else if (playerRow == board.GetLength(0) - 1)
            {
                for (int col = playerCol; col < board.GetLength(1) - 1; col++)
                {
                    startColAndRow[1]--;
                    startColAndRow[0]++;

                }
                return startColAndRow;
            }
            else if (playerCol == 0)
            {
                for (int col = playerCol; col < board.GetLength(1) - 1; col++)
                {
                    if (startColAndRow[1] == 0)
                    {
                        break;
                    }
                    else
                    {
                        startColAndRow[1]--;
                        startColAndRow[0]++;
                    }

                }
            }
            if (startColAndRow[0] >= startColAndRow[1])
            {
                for (int col = startColAndRow[0] + 1; col < board.GetLength(1); col++)
                {
                    if (startColAndRow[1] == 0)
                    {
                        break;
                    }
                    else
                    {
                        startColAndRow[0]++;
                        startColAndRow[1]--;
                    }
                }
            }
            else
            {
                for (int col = startColAndRow[0] + 1; col < board.GetLength(1); col++)
                {
                    if (startColAndRow[1] == 0)
                    {
                        break;
                    }
                    else
                    {
                        startColAndRow[0]++;
                        startColAndRow[1]--;
                    }
                }
            }

            return startColAndRow;
        }
        private void FillBoard()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int coll = 0; coll < board.GetLength(1); coll++)
                {
                    board[row, coll] = "{ 0 }";
                }
            }
        }
    }
}
