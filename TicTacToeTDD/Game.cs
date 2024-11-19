namespace TicTacToeTDD
{
    public class Game
    {
        private const string Filler = "■";
        private const string _player1 = "X";
        private const string _player2 = "0";
        private const int x = 3, y = 3;
        private readonly Dictionary<int, int[]> _map = new Dictionary<int, int[]>
        {
            { 1, new int[] { 0, 0 } },
            { 2, new int[] { 0, 1 } },
            { 3, new int[] { 0, 2 } },
            { 4, new int[] { 1, 0 } },
            { 5, new int[] { 1, 1 } },
            { 6, new int[] { 1, 2 } },
            { 7, new int[] { 2, 0 } },
            { 8, new int[] { 2, 1 } },
            { 9, new int[] { 2, 2 } },
        };

        private string statusGame = "";
        private int _numberStep = 1;

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
        public void SetValuesToPosition(int x, int y, string p) => Board[x, y] = p;
        public bool IsCorrectlyStep(int x, int y) => GetValuesToPosition(x, y) == Filler;

        public void MakeMove(int x, int y, string p)
        {
            if (IsCorrectlyStep(x, y))
            {
                SetValuesToPosition(x, y, p);
                if (!IsEndGame()) _numberStep++;

            }
        }
        public string GetCurrentPlayer() => _numberStep % 2 == 1 ? _player1 : _player2;
        public bool IsEndGame()
        {
            if (_numberStep > 9)
            {
                statusGame = "Ничья!";
                return true;
            }
            else
            {
                for (int i = 0; i < 3; i++)
                    if (Board[i, 0] == Board[i, 1] && Board[i, 0] == Board[i, 2] && Board[i, 0] != Filler ||
                        Board[0, i] == Board[1, i] && Board[0, i] == Board[2, i] && Board[0, i] != Filler
                        )
                    {
                        statusGame = $"Победил {GetCurrentPlayer()}!";
                        return true;
                    }

                if (Board[0, 0] == Board[1, 1] && Board[0, 0] == Board[2, 2] && Board[0, 0] != Filler ||
                    Board[0, 2] == Board[1, 1] && Board[0, 2] == Board[2, 0] && Board[0, 2] != Filler)
                {
                    statusGame = $"Победил {GetCurrentPlayer()}!";
                    return true;
                }
            }
            statusGame = $"Идёт игра!";
            return false;
        }
        


        public string GetBoardToString()
        {
            string result = "";

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                    result += Board[i, j];
                result += "\n";
            }
            return result;
        }
        public void StartGame()
        {
            statusGame = "Игра началась !";
            CreateMap();
            _numberStep = 1;
        }
        public void PlayerStep(int n)
        {
            if (_map.ContainsKey(n))
                MakeMove(_map[n][0], _map[n][1], GetCurrentPlayer());
            else
                MakeMove(-1, -1, GetCurrentPlayer());
        }
        public string GetStatusGame() => statusGame;

    }
}
