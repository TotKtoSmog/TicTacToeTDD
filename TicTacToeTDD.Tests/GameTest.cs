namespace TicTacToeTDD.Tests
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

        [DataTestMethod]
        [DynamicData(nameof(GetValuesToPosition))]
        public void GetValuesToPosition_Correctly(int x, int y, string p)
        {
            Game game = new();

            game.CreateMap();

            string actual = game.GetValuesToPosition(x, y);

            Assert.AreEqual(p, actual);
        }


    }
}