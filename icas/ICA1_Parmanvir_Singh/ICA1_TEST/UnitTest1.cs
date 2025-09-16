using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICA1_TEST;
using Microsoft.VisualStudio.TestTools.UnitTesting;

////////////////////////////////////////////////////////////
// ICA #01 - Generics, Extension Methods, Indexers
// Parmanvir Singh - Jan 25 2024
// Submission Code : CMPE2800_1232_ICA01
// Mostly test code stuff 
////////////////////////////////////////////////////////////

namespace ICA1_TEST
{
    [TestClass]
    public class UnitTest1
    {
        // ---------- Categorize Tests ----------
        [TestMethod]
        public void TestCategorize_Ints()
        {
            List<int> iNums = new List<int> { 4, 12, 4, 3, 5, 6, 7, 6, 12 };
            Dictionary<int, int> categorized = iNums.Categorize();

            Assert.AreEqual(2, categorized[4]);
            Assert.AreEqual(2, categorized[12]);
            Assert.AreEqual(1, categorized[3]);
            Assert.AreEqual(6, categorized.Count); 
        }

        [TestMethod]
        public void TestCategorize_Strings()
        {
            List<string> names = new List<string> { "Rick", "Glenn", "Rick", "Carl", "Michonne", "Rick", "Glenn" };
            var categorized = names.Categorize();

            Assert.AreEqual(3, categorized["Rick"]);
            Assert.AreEqual(2, categorized["Glenn"]);
            Assert.AreEqual(1, categorized["Carl"]);
            Assert.AreEqual(4, categorized.Count); // Carl, Glenn, Michonne, Rick
        }

        [TestMethod]
        public void TestCategorize_EmptyAndOtherEnumerables()
        {
            List<int> emptyList = new List<int>();
            Dictionary<int,int> emptyDict = emptyList.Categorize();
            Assert.IsNotNull(emptyDict);
            Assert.AreEqual(0, emptyDict.Count);

            // LinkedList<char> test (small deterministic sample)
            LinkedList<char> ll = new LinkedList<char>(new[] { 'A', 'B', 'A', 'C', 'B' });
            Dictionary<char,int> charCat = ll.Categorize();
            Assert.AreEqual(2, charCat['A']);
            Assert.AreEqual(2, charCat['B']);
            Assert.AreEqual(1, charCat['C']);

            // string (IEnumerable<char>)
            string s = "AAB";
            Dictionary<char,int> sCat = s.Categorize();
            Assert.AreEqual(2, sCat['A']);
            Assert.AreEqual(1, sCat['B']);
        }

        // ---------- Penultimate Tests ----------
        [TestMethod]
        public void TestPenultimate_Normal()
        {
            IEnumerable<int> items = new List<int> { 1, 2, 3, 4 };
            // As assignment requires: return second-last or default on error.
            Assert.AreEqual(3, items.Penultimate());
        }

        [TestMethod]
        public void TestPenultimate_EmptyOrSingle_ReturnsDefault()
        {
            IEnumerable<int> empty = null;
            IEnumerable<int> single = new List<int> { 42 };

            Assert.AreEqual(default(int), empty.Penultimate());
            Assert.AreEqual(default(int), single.Penultimate());
        }

        // ---------- Rando Tests ----------
        [TestMethod]
        public void TestRando_NonEmptyDictionary()
        {
            List<int> list = new List<int> { 1, 2, 3, 4 };
            Dictionary<int,int> dict = list.Categorize(); // keys 1,4 with count 1
            var pick = dict.Rando();

            Assert.IsTrue(dict.ContainsKey(pick.Key));
            Assert.AreEqual(dict[pick.Key], pick.Value);
            Assert.IsTrue(pick.Value > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRando_EmptyDictionary_Throws()
        {
            Dictionary<int,int> empty = new Dictionary<int, int>();
            var i = empty.Rando(); // should throw ArgumentException
        }

        // ---------- AdjacentDuplicate Tests ----------
        [TestMethod]
        public void TestAdjacentDuplicate_Found()
        {
            List<int> list = new List<int> { 1, 2, 2, 3, 4 };
            int dup = list.AdjacentDuplicate();
            Assert.AreEqual(2, dup);
        }

        [TestMethod]
        public void TestAdjacentDuplicate_NotFound_ReturnsDefault()
        {
            List<int> list = new List<int> { 1, 2, 3, 4 };
            int dup = list.AdjacentDuplicate();
            Assert.AreEqual(default(int), dup);
        }

        // ---------- StringDisplay Tests ----------
        [TestMethod]
        public void TestStringDisplay_NonEmpty()
        {
            List<string> names = new List<string> { "Rick", "Rick", "Glenn" };
            var display = names.StringDisplay();

            // The assignment/test expects named tuple members displaystring and count
            Assert.IsTrue(display.displaystring.Contains("Rick"));
            Assert.AreEqual(3, display.count);
            Assert.IsTrue(display.displaystring.Contains("Glenn"));
            // Ensure no trailing comma (simple check: not ending with comma)
            Assert.IsFalse(display.displaystring.EndsWith(","));
        }

        [TestMethod]
        public void TestStringDisplay_Empty()
        {
            List<string> empty = new List<string>();
            var display = empty.StringDisplay();
            Assert.AreEqual("[EMPTY]", display.displaystring);
            Assert.AreEqual(0, display.count);
        }

        // ---------- STACK<T> Indexer Tests ----------
        [TestMethod]
        public void TestCategorizedStack_Indexers_ByPositionAndKey_String()
        {
            STACK<string> stack = new STACK<string>();
            stack.Push("A");
            stack.Push("B");
            stack.Push("A");
            stack.Push("C");
            stack.Push("B");
            stack.Push("A");

            Assert.AreEqual("A", stack[0]); // first ordered categorized key
            Assert.AreEqual("B", stack[1]); // second
            Assert.AreEqual("C", stack[2]); // third

            // Key indexer: returns count for that key
            Assert.AreEqual(3, stack["A"]);
            Assert.AreEqual(2, stack["B"]);
            Assert.AreEqual(1, stack["C"]);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestCategorizedStack_Indexer_KeyNotFound_Throws()
        {
            STACK<string> stack = new STACK<string>();
            stack.Push("1");
            stack.Push("2");
            stack.Push("2");
            stack.Push("3");
            string result = stack[3];// This should throw IndexOutOfRangeException
            IndexOutOfRangeException exception = Assert.ThrowsException<IndexOutOfRangeException>(() => stack[3]);

        }



    }
}




   