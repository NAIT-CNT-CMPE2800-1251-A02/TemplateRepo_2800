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
            Dictionary<int, int> categorized = iNums.Categorize();//getting back in the dic<int,int>

            Assert.AreEqual(2, categorized[4]);// we have two 4's
            Assert.AreEqual(2, categorized[12]);
            Assert.AreEqual(1, categorized[3]);//just one
            Assert.AreEqual(6, categorized.Count); //giving us the counnt
        }

        [TestMethod]
        public void TestCategorize_Strings()
        {
            List<string> names = new List<string> { "Rick", "Glenn", "Rick", "Carl", "Michonne", "Rick", "Glenn" };
            Dictionary<string,int> categorized = names.Categorize();

            Assert.AreEqual(3, categorized["Rick"]);//rick just 3times
            Assert.AreEqual(2, categorized["Glenn"]);
            Assert.AreEqual(1, categorized["Carl"]);
            Assert.AreEqual(4, categorized.Count); // Carl, Glenn, Michonne, Rick
        }

        [TestMethod]
        public void TestCategorize_EmptyAndOtherEnumerables()
        {
            List<int> emptyList = new List<int>();
            Dictionary<int,int> emptyDict = emptyList.Categorize();

            Assert.IsNotNull(emptyDict);//checking its not null
            Assert.AreEqual(0, emptyDict.Count);

            // LinkedList<char> test (small deterministic sample) liks singles 'a''b'
            LinkedList<char> ll = new LinkedList<char>(new[] { 'A', 'B', 'A', 'C', 'B' });
            Dictionary<char,int> charCat = ll.Categorize();
            Assert.AreEqual(2, charCat['A']);//checking same as the upper
            Assert.AreEqual(2, charCat['B']);
            Assert.AreEqual(1, charCat['C']);

            // string (IEnumerable<char>) same checking the catorize
            string s = "AAB";
            Dictionary<char,int> sCat = s.Categorize(); // so it  would be like A A B
            Assert.AreEqual(2, sCat['A']);
            Assert.AreEqual(1, sCat['B']);
        }

        // ---------- Penultimate Tests ----------
        [TestMethod]
        public void TestPenultimate_Normal()
        {                                               //3 secoond last element
            IEnumerable<int> items = new List<int> { 1, 2, 3, 4 };
            // As assignment requires: return second-last or default on error.
            Assert.AreEqual(3, items.Penultimate());
        }

        [TestMethod]
        public void TestPenultimate_EmptyOrSingle_ReturnsDefault()
        {
            IEnumerable<int> empty = null;//chekng for null and also just for one element what it would return
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
            Assert.AreEqual(dict[pick.Key], pick.Value);//checcking the like  4 has ony one  val same as 1
            Assert.IsTrue(pick.Value > 0);//hmm this gonna be true
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRando_EmptyDictionary_Throws()
        {
            Dictionary<int,int> empty = new Dictionary<int, int>();
            var i = empty.Rando();
        }

        // ---------- AdjacentDuplicate Tests ----------
        [TestMethod]
        public void TestAdjacentDuplicate_Found()
        {
            List<int> list = new List<int> { 1, 2, 2, 3, 4 };
            int dup = list.AdjacentDuplicate();//checking for dub like 
            Assert.AreEqual(2, dup);//2 2 in the list
        }

        [TestMethod]
        public void TestAdjacentDuplicate_NotFound_ReturnsDefault()
        {
            List<int> list = new List<int> { 1, 2, 3, 4 };
            int dup = list.AdjacentDuplicate();//if not found then return defult type
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
            Assert.AreEqual(3, display.count);//like he function we make if nothing it have to give
            Assert.IsTrue(display.displaystring.Contains("Glenn"));
            // Ensure no trailing comma (simple check: not ending with comma)
            Assert.IsFalse(display.displaystring.EndsWith(","));//just for something like if it end with ',' not with the given
        }

        [TestMethod]
        public void TestStringDisplay_Empty()
        {
            List<string> empty = new List<string>();
            var display = empty.StringDisplay();//gonna give us {[EMPTY]} if nothing there
            Assert.AreEqual("[EMPTY]", display.displaystring);
            Assert.AreEqual(0, display.count);//also same for the count
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
            //checking if its catogrize or not
            // Key indexer: returns count for that key
            Assert.AreEqual(3, stack["A"]);//now checking the vlue by  the  key
            Assert.AreEqual(2, stack["B"]);//same for this
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
            //was giving the error and which we suppose it would but cant find the was to handle that exception
            //From GPT
            string result = stack[3];// This should throw IndexOutOfRangeException
            IndexOutOfRangeException exception = Assert.ThrowsException<IndexOutOfRangeException>(() => stack[3]);
            //FROM _GPT   
            //PROMOT
            /**"I have a C# class STACK<T> that defines two indexers: one by position (this[int index]) which throws 
             * IndexOutOfRangeException if the index is invalid, and one by key (this[T key]) which throws ArgumentException 
             * if the key does not exist. I need you to generate MSTest unit tests that:Verify the position indexer throws
             * IndexOutOfRangeException when accessed out of range.Verify the key indexer throws ArgumentException when 
             * querying a missing key.Use Assert.ThrowsException<>() for both tests. Show both examples clearly with comments
             * explaining what’s happening."**/
        }



    }
}




   