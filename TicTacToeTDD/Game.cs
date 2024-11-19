﻿namespace TicTacToeTDD
{
    public class Game
    {
        public string[,] Board = new string[3, 3];
        public string CreateMap()
        {
            string result = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Board[i, j] = "■";
                    result += "■";
                }
                result += "\n";
            }
            return result;
        }
        public string GetValuesToPosition(int x, int y) => Board[x, y];
    }
}
