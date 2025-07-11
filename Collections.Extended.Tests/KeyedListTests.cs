namespace Asjc.Collections.Extended.Tests
{
    [TestClass]
    public class KeyedListTests
    {
        [TestMethod]
        public void Add1_DuplicateKey_ThrowsArgumentException()
        {
            KeyedList<string, string> kl = new(s => s.ToLower()) { { "a", "A" }, { "b", "B" } };
            Assert.ThrowsExactly<ArgumentException>(() => kl.Add("A"));
        }

        [TestMethod]
        public void Insert1()
        {
            KeyedList<string, string> kl = new(s => s.ToLower()) { { "a", "A" }, { "b", "B" } };
            kl.Insert(1, "C");
            Assert.AreEqual("C", kl[1]);
        }
    }
}