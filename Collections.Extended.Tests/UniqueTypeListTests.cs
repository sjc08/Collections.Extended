using Asjc.Utils.Extensions;

namespace Asjc.Collections.Extended.Tests
{
    [TestClass]
    public class UniqueTypeListTests
    {
        [TestMethod]
        public void Get()
        {
            UniqueTypeList utl = [0, "A", 1.0, 2.0f];
            Assert.AreEqual(0, utl.Get<int>());
            Assert.AreEqual("A", utl.Get<string>());
            Assert.AreEqual(1.0, utl.Get<double>());
            Assert.AreEqual(2.0f, utl.Get<float>());
        }

        [TestMethod]
        public void Indexer_Null()
        {
            UniqueTypeList utl = [0, "A", 1.0, 2.0f];
            utl[typeof(string)] = null;
            Assert.IsNull(utl.Get<string>());
        }

        [TestMethod]
        public void Set()
        {
            UniqueTypeList utl = [0, "A", 1.0, 2.0f];
            utl.Set(0.0);
            utl.Set("");
            Assert.IsTrue(((List<object>)utl).SequenceEqual([0, "", 0.0, 2.0f]));
        }

        [TestMethod]
        public void Set_Null()
        {
            UniqueTypeList utl = [0, "A", 1.0, 2.0f];
            utl.Set<string>(null!);
            Assert.IsNull(utl.Get<string>());
        }
    }
}