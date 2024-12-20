﻿namespace TicTacToeTDD.Tests
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void CreateMapTest()
        {
            string expected = "■■■\n■■■\n■■■\n";
            Game game = new Game();
            string actual = game.CreateMap();
            Assert.AreEqual(expected, actual);
        }
        public static IEnumerable<object[]> GetValuesToPosition =>
        [
            [0, 0, "■"],
            [0, 1, "■"],
            [0, 2, "■"],
            [1, 0, "■"],
            [1, 1, "■"],
            [1, 2, "■"],
            [2, 0, "■"],
            [2, 1, "■"],
            [2, 2, "■"],
        ];
        public static IEnumerable<object[]> SetValuesToPosition =>
        [
            [0, 0, "X"],
            [0, 1, "■"],
            [0, 2, "0"],
            [1, 0, "■"],
            [1, 1, "X"],
            [1, 2, "■"],
            [2, 0, "0"],
            [2, 1, "■"],
            [2, 2, "X"],
        ];
        public static IEnumerable<object[]> SetPlayerStep =>
        [
            [1,"X■■\n■■■\n■■■\n"],
            [2,"■X■\n■■■\n■■■\n"],
            [3,"■■X\n■■■\n■■■\n"],
            [4,"■■■\nX■■\n■■■\n"],
            [5,"■■■\n■X■\n■■■\n"],
            [6,"■■■\n■■X\n■■■\n"],
            [7,"■■■\n■■■\nX■■\n"],
            [8,"■■■\n■■■\n■X■\n"],
            [9,"■■■\n■■■\n■■X\n"],
        ];

        [DataTestMethod]
        [DynamicData(nameof(GetValuesToPosition))]
        public void GetValuesToPosition_Correctly(int x, int y, string p)
        {
            Game game = new();

            game.CreateMap();

            string actual = game.GetValuesToPosition(x, y);

            Assert.AreEqual(p, actual);
        }

        
        [DataTestMethod]
        [DynamicData(nameof(SetValuesToPosition))]
        public void SetValuesToPosition_Correctly(int x, int y, string p)
        {
            Game game = new();
            game.CreateMap();
            game.SetValuesToPosition(x, y, p);

            string actual = game.GetValuesToPosition(x, y);

            Assert.AreEqual(p, actual);
        }

        [TestMethod]
        public void IsCorrectlyStep_Correctly()
        {
            Game game = new();
            game.CreateMap();

            bool actual = game.IsCorrectlyStep(0, 0);
            Assert.IsTrue(actual);

            game.SetValuesToPosition(0, 0, "X");
            actual = game.IsCorrectlyStep(0, 0);
            Assert.IsFalse(actual);

            actual = game.IsCorrectlyStep(1, 1);
            Assert.IsTrue(actual);

            game.SetValuesToPosition(1, 1, "0");
            actual = game.IsCorrectlyStep(0, 0);
            Assert.IsFalse(actual);
        }

        [DataTestMethod]
        [DataRow(0, 0, "X", "X")]
        [DataRow(1, 1, "X", "0")]
        [DataRow(2, 2, "0", "0")]
        public void MakeMove_UpdatesBoardCorrectly(int x, int y, string p, string expected)
        {
            Game game = new();
            game.CreateMap();
            game.MakeMove(1, 1, "0");
            game.MakeMove(x, y, p);

            string actual = game.GetValuesToPosition(x, y);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void GetCurrentPlayer_Update()
        {
            Game game = new();
            game.CreateMap();

            string actual = game.GetCurrentPlayer();

            string expected = "X";

            Assert.AreEqual(expected, actual);

            game.MakeMove(0, 0, actual);

            actual = game.GetCurrentPlayer();
            expected = "0";

            Assert.AreEqual(expected, actual);

            game.MakeMove(0, 0, actual);

            actual = game.GetCurrentPlayer();
            expected = "0";

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void IsEndGame_Draw()
        {
            Game game = new();
            game.CreateMap();

            game.MakeMove(0, 0, game.GetCurrentPlayer());
            game.MakeMove(0, 1, game.GetCurrentPlayer());
            game.MakeMove(0, 2, game.GetCurrentPlayer());
            game.MakeMove(1, 1, game.GetCurrentPlayer());
            game.MakeMove(1, 0, game.GetCurrentPlayer());
            game.MakeMove(1, 2, game.GetCurrentPlayer());
            game.MakeMove(2, 1, game.GetCurrentPlayer());
            game.MakeMove(2, 0, game.GetCurrentPlayer());
            game.MakeMove(2, 2, game.GetCurrentPlayer());
            Assert.IsTrue(game.IsEndGame());
        }

        [TestMethod]
        public void GetBoardToString_Update()
        {
            Game game = new();
            game.CreateMap();

            game.MakeMove(0, 0, game.GetCurrentPlayer());
            string expected = "X■■\n■■■\n■■■\n";
            Assert.AreEqual(expected, game.GetBoardToString());

            game.MakeMove(0, 1, game.GetCurrentPlayer());
            expected = "X0■\n■■■\n■■■\n";
            Assert.AreEqual(expected, game.GetBoardToString());

            game.MakeMove(0, 2, game.GetCurrentPlayer());
            expected = "X0X\n■■■\n■■■\n";
            Assert.AreEqual(expected, game.GetBoardToString());

            game.MakeMove(1, 1, game.GetCurrentPlayer());
            expected = "X0X\n■0■\n■■■\n";
            Assert.AreEqual(expected, game.GetBoardToString());

            game.MakeMove(1, 0, game.GetCurrentPlayer());
            expected = "X0X\nX0■\n■■■\n";
            Assert.AreEqual(expected, game.GetBoardToString());

            game.MakeMove(1, 2, game.GetCurrentPlayer());
            expected = "X0X\nX00\n■■■\n";
            Assert.AreEqual(expected, game.GetBoardToString());

            game.MakeMove(2, 1, game.GetCurrentPlayer());
            expected = "X0X\nX00\n■X■\n";
            Assert.AreEqual(expected, game.GetBoardToString());

            game.MakeMove(2, 0, game.GetCurrentPlayer());
            expected = "X0X\nX00\n0X■\n";
            Assert.AreEqual(expected, game.GetBoardToString());

            game.MakeMove(2, 2, game.GetCurrentPlayer());
            expected = "X0X\nX00\n0XX\n";
            Assert.AreEqual(expected, game.GetBoardToString());
        }

        [TestMethod]
        public void StartGame_Update()
        {
            Game game = new();
            game.StartGame();

            game.MakeMove(0, 0, game.GetCurrentPlayer());
            string expected = "X■■\n■■■\n■■■\n";
            Assert.AreEqual(expected, game.GetBoardToString());

            game.MakeMove(0, 1, game.GetCurrentPlayer());
            game.MakeMove(0, 2, game.GetCurrentPlayer());
            game.StartGame();

            expected = "■■■\n■■■\n■■■\n";
            Assert.AreEqual(expected, game.GetBoardToString());
        }

        [TestMethod]
        public void IsEndGame_Horizontal()
        {
            Game game = new();
            game.StartGame();

            game.MakeMove(0, 0, game.GetCurrentPlayer());
            game.MakeMove(2, 0, game.GetCurrentPlayer());
            game.MakeMove(0, 1, game.GetCurrentPlayer());
            game.MakeMove(2, 1, game.GetCurrentPlayer());
            game.MakeMove(0, 2, game.GetCurrentPlayer());

            Assert.IsTrue(game.IsEndGame());

            game.StartGame();
            game.MakeMove(2, 0, game.GetCurrentPlayer());
            game.MakeMove(0, 0, game.GetCurrentPlayer());
            game.MakeMove(2, 1, game.GetCurrentPlayer());
            game.MakeMove(0, 1, game.GetCurrentPlayer());
            game.MakeMove(1, 2, game.GetCurrentPlayer());
            game.MakeMove(0, 2, game.GetCurrentPlayer());

            Assert.IsTrue(game.IsEndGame());
        }

        [TestMethod]
        public void IsEndGame_Vertical()
        {
            Game game = new();
            game.StartGame();

            game.MakeMove(0, 0, game.GetCurrentPlayer());
            game.MakeMove(1, 1, game.GetCurrentPlayer());
            game.MakeMove(1, 0, game.GetCurrentPlayer());
            game.MakeMove(2, 1, game.GetCurrentPlayer());
            game.MakeMove(2, 0, game.GetCurrentPlayer());

            Assert.IsTrue(game.IsEndGame());

            game.StartGame();
            game.MakeMove(2, 0, game.GetCurrentPlayer());
            game.MakeMove(1, 1, game.GetCurrentPlayer());

            game.MakeMove(0, 2, game.GetCurrentPlayer());
            game.MakeMove(2, 1, game.GetCurrentPlayer());

            game.MakeMove(1, 2, game.GetCurrentPlayer());
            game.MakeMove(0, 1, game.GetCurrentPlayer());

            Assert.IsTrue(game.IsEndGame());
        }

        [TestMethod]
        public void IsEndGame_MainDiagonal()
        {
            Game game = new();
            game.StartGame();

            game.MakeMove(0, 0, game.GetCurrentPlayer());
            game.MakeMove(0, 1, game.GetCurrentPlayer());
            game.MakeMove(1, 1, game.GetCurrentPlayer());
            game.MakeMove(0, 2, game.GetCurrentPlayer());
            game.MakeMove(2, 2, game.GetCurrentPlayer());

            Assert.IsTrue(game.IsEndGame());

            game.StartGame();
            game.MakeMove(2, 0, game.GetCurrentPlayer());
            game.MakeMove(1, 1, game.GetCurrentPlayer());
            game.MakeMove(0, 1, game.GetCurrentPlayer());
            game.MakeMove(2, 2, game.GetCurrentPlayer());
            game.MakeMove(1, 2, game.GetCurrentPlayer());
            game.MakeMove(0, 0, game.GetCurrentPlayer());

            Assert.IsTrue(game.IsEndGame());
        }

        [TestMethod]
        public void IsEndGame_SideDiagonal()
        {
            Game game = new();
            game.StartGame();

            game.MakeMove(0, 2, game.GetCurrentPlayer());
            game.MakeMove(0, 1, game.GetCurrentPlayer());
            game.MakeMove(1, 1, game.GetCurrentPlayer());
            game.MakeMove(1, 2, game.GetCurrentPlayer());
            game.MakeMove(2, 0, game.GetCurrentPlayer());

            Assert.IsTrue(game.IsEndGame());

            game.StartGame();
            game.MakeMove(2, 1, game.GetCurrentPlayer());
            game.MakeMove(1, 1, game.GetCurrentPlayer());
            game.MakeMove(0, 1, game.GetCurrentPlayer());
            game.MakeMove(2, 0, game.GetCurrentPlayer());
            game.MakeMove(1, 2, game.GetCurrentPlayer());
            game.MakeMove(0, 2, game.GetCurrentPlayer());

            Assert.IsTrue(game.IsEndGame());
        }

        [DataTestMethod]
        [DynamicData(nameof(SetPlayerStep))]
        public void SetPlayerStep_Update(int n, string e)
        {
            Game game = new();
            game.StartGame();
            game.PlayerStep(n);

            string actual = game.GetBoardToString();

            Assert.AreEqual(e, actual);
        }

        [TestMethod]
        public void GetStatusGame_Update()
        {
            Game game = new();
            game.StartGame();

            string actual = game.GetStatusGame();
            string expected = "Игра началась !";

            Assert.AreEqual(expected, actual);

            game.PlayerStep(1);

            actual = game.GetStatusGame();
            expected = "Идёт игра!";

            Assert.AreEqual(expected, actual);
            game.PlayerStep(2);

            game.PlayerStep(4);
            game.PlayerStep(3);
            game.PlayerStep(7);

            actual = game.GetStatusGame();
            expected = "Победил X!";

            Assert.AreEqual(expected, actual);
        }


    }
}