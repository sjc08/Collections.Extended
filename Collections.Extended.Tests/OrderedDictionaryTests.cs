using Asjc.Utils.Extensions;

namespace Asjc.Collections.Extended.Tests
{
    [TestClass]
    public class OrderedDictionaryTests
    {
        private static void CheckConsistency<TKey, TValue>(OrderedDictionary<TKey, TValue> od) where TKey : notnull
        {
            Assert.IsTrue(od.Keys.ContentEqual(od.OrderedKeys));
            Assert.IsTrue(od.Values.ContentEqual(od.OrderedValues));
            Assert.IsTrue(od.Count == od.Keys.Count && od.Count == od.Values.Count);
            Assert.IsFalse(od.Keys.HasDuplicates());
        }

        [TestMethod]
        public void Indexer1_ReturnsCorrectValue()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            Assert.AreEqual("A", od["a"]);
            CheckConsistency(od);
        }

        [TestMethod]
        public void Indexer1_KeyNotContained_ThrowsKeyNotFoundException()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            Assert.ThrowsExactly<KeyNotFoundException>(() => _ = od["c"]);
            CheckConsistency(od);
        }

        [TestMethod]
        public void Indexer1_KeyNotContained_AddsNewElement()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            od["c"] = "C";
            Assert.AreEqual(new("c", "C"), od[2]);
            CheckConsistency(od);
        }

        [TestMethod]
        public void Indexer2_ReturnsCorrectPair()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            Assert.AreEqual(od[0], new("a", "A"));
            CheckConsistency(od);
        }

        [TestMethod]
        public void Indexer2_KeyNotMatched_ReplacesOldElement()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            od[0] = new("c", "C");
            Assert.AreEqual(new("c", "C"), od[0]);
            Assert.AreEqual(2, od.Count);
            Assert.IsFalse(od.ContainsKey("a"));
            CheckConsistency(od);
        }

        [TestMethod]
        public void Indexer2_DuplicateKey_ThrowsArgumentException()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            Assert.ThrowsExactly<ArgumentException>(() => od[0] = new("b", "B"));
            CheckConsistency(od);
        }

        [TestMethod]
        public void Add1_DuplicateKey_ThrowsArgumentException()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            Assert.ThrowsExactly<ArgumentException>(() => od.Add(new("b", "C")));
            Assert.AreEqual(2, od.Count);
            CheckConsistency(od);
        }

        [TestMethod]
        public void Add2_DuplicateKey_ThrowsArgumentException()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            Assert.ThrowsExactly<ArgumentException>(() => od.Add("b", "B"));
            CheckConsistency(od);
        }

        [TestMethod]
        public void Clear_Emptiesictionary()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            od.Clear();
            Assert.AreEqual(0, od.Count);
            CheckConsistency(od);
        }

        [TestMethod]
        public void Insert_IndexLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => od.Insert(-1, new("c", "C")));
            CheckConsistency(od);
        }

        [TestMethod]
        public void Insert_IndexGreaterThanCount_ThrowsArgumentOutOfRangeException()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => od.Insert(od.Count + 1, new("c", "C")));
            CheckConsistency(od);
        }

        [TestMethod]
        public void Remove1_RemovesSuccessfully()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            od.Remove("b");
            Assert.IsFalse(od.ContainsKey("b"));
            Assert.IsFalse(od.Values.Contains("B"));
            CheckConsistency(od);
        }

        [TestMethod]
        public void Remove2_RemovesSuccessfully()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            od.Remove(new KeyValuePair<string, string>("b", "B"));
            Assert.IsFalse(od.ContainsKey("b"));
            Assert.IsFalse(od.Values.Contains("B"));
            CheckConsistency(od);
        }

        [TestMethod]
        public void RemoveAt_RemovesSuccessfully()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            od.RemoveAt(1);
            Assert.IsFalse(od.ContainsKey("b"));
            Assert.IsFalse(od.Values.Contains("B"));
            CheckConsistency(od);
        }

        [TestMethod]
        public void RemoveAt_IndexLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => od.RemoveAt(-1));
            CheckConsistency(od);
        }

        [TestMethod]
        public void RemoveAt_IndexGreaterThanCount_ThrowsArgumentOutOfRangeException()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => od.RemoveAt(od.Count + 1));
            CheckConsistency(od);
        }

        [TestMethod]
        public void ConversionOperator1()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            var dictionary = (Dictionary<string, string>)od;
            dictionary.Clear();
            CheckConsistency(od);
        }

        [TestMethod]
        public void ConversionOperator2()
        {
            OrderedDictionary<string, string> od = new() { { "a", "A" }, { "b", "B" } };
            var list = (List<KeyValuePair<string, string>>)od;
            list.Clear();
            CheckConsistency(od);
        }
    }
}