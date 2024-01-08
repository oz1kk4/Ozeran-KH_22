using System;
using System.Threading;

class GameOfLife
{
    static int width = 40;
    static int height = 20;
    static bool[,] board = new bool[width, height];
    static bool[,] nextGeneration = new bool[width, height];

    static void Main()
    {
        InitializeBoard();
        while (true)
        {
            DrawBoard();
            CalculateNextGeneration();
            UpdateBoard();
            Thread.Sleep(200);
        }
    }

    static void InitializeBoard()
    {
        Random rand = new Random();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                board[x, y] = rand.Next(0, 10) > 5;
            }
        }
    }

    static void DrawBoard()
    {
        Console.Clear();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Console.Write(board[x, y] ? "#" : " ");
            }
            Console.WriteLine();
        }
    }

    static void CalculateNextGeneration()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int neighbors = CountNeighbors(x, y);
                if (board[x, y])
                {
                    nextGeneration[x, y] = neighbors == 2 || neighbors == 3;
                }
                else
                {
                    nextGeneration[x, y] = neighbors == 3;
                }
            }
        }
    }

    static int CountNeighbors(int x, int y)
    {
        int count = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int neighborX = (x + i + width) % width;
                int neighborY = (y + j + height) % height;
                if (!(i == 0 && j == 0) && board[neighborX, neighborY])
                {
                    count++;
                }
            }
        }
        return count;
    }

    static void UpdateBoard()
    {
        Array.Copy(nextGeneration, board, width * height);
    }
}
