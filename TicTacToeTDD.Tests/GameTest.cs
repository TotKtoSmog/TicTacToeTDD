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

    }
}