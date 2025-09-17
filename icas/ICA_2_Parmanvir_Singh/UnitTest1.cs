namespace ICA_2
{    ////////////////////////////////////////////////////////////
    // ICA #02 - IENURABLE
    // Parmanvir Singh - Jan 25 2024
    // Submission Code : CMPE2800_1232_ICA01
    // Mostly test code stuff 
    ////////////////////////////////////////////////////////////
    [TestClass]
    public class UnitTest1
    {
        // -------------------------------
        // Test for the KNIGHTS_CLASS
        // -------------------------------
        [TestMethod]
        
        public void TestMethod1()
        {
            // Create a knight positioned at (4,4) on an 8x8 board
            KNIGHTS_CLASS kNIGHTS_ = new KNIGHTS_CLASS(4, 4);
            KNIGHTS_CLASS kNIGHTS_1 = new KNIGHTS_CLASS(0, 0);


            // Iterate through all legal knight moves (tuples of (x,y))
            foreach ((int, int) k in kNIGHTS_)
            {
                Console.WriteLine($"{k.Item1},{k.Item2}");// prints all valid moves for the knight from (4,4)
            }
            Console.WriteLine();
            foreach ((int, int) item in kNIGHTS_1)
            {
                Console.WriteLine($"{item.Item1},{item.Item2}");

            }


        }

        [TestClass]
        public class IteratorUtilsTests
        {
            // ---------- PEEL ----------

            // Case 1: Even number of elements
            // Should return pairs from front+back moving inward
            [TestMethod]
            public void Peel_EvenCount_ReturnsPairs()
            {                         //1 first item   4 last item
                int[] input = new[] { 1, 2, 3, 4 };
                List<List<int>> result = input.Peel().ToList();
                //
                Assert.AreEqual(2, result.Count);
                CollectionAssert.AreEqual(new List<int> { 1, 4 }, result[0]);
                CollectionAssert.AreEqual(new List<int> { 2, 3 }, result[1]);
            }

            // Case 2: Odd number of elements
            // Should return pairs plus a single element in the middle
            [TestMethod]
            public void Peel_OddCount_ReturnsPairsAndSingle()
            {                               //middle left gonna return as it is 
                int[] input = new[] { 1, 2, 3, 4, 5 };
                List<List<int>> result = input.Peel().ToList();

                Assert.AreEqual(3, result.Count);
                CollectionAssert.AreEqual(new List<int> { 1, 5 }, result[0]);
                CollectionAssert.AreEqual(new List<int> { 2, 4 }, result[1]);
                CollectionAssert.AreEqual(new List<int> { 3 }, result[2]);
            }

            // Case 3: Empty collection
            // Should return nothing at all
            [TestMethod]
            public void Peel_Empty_ReturnsNothing()
            {
                int[] input = Array.Empty<int>();
                List<List<int>> result = input.Peel().ToList();
                //jsut gives you nothing 
                Assert.AreEqual(0, result.Count);
            }
            // Case 4: Null collection
            // Should return nothing at all
            [TestMethod]
            public void Peel_Null()
            {
                //int[] input =null;
                //IEnumerable<List<int>> result =
                //jsut gives you nothing 
            
               Assert.ThrowsException<ArgumentNullException> (() => UTILITY.Peel<int>(null).ToList());

            }

            // ---------- SHUFFLE ----------

            // Test that Shuffle produces a permutation:
            // - Same number of elements
            // - All original values still present
            [TestMethod]
            public void Shuffle_PermutationProperty()
            {
                int[] input = Enumerable.Range(1, 10).ToArray();
                int[] shuffled = input.Shuffle().ToArray();

                Assert.AreEqual(input.Length, shuffled.Length);//just for checking same amount of elements 
                foreach (int val in input)
                    Assert.IsTrue(shuffled.Contains(val));//just checking not value is missed
            }

            // Distribution/Fairness test for Shuffle
            // Run Shuffle many times, check that elements are evenly distributed
            [TestMethod]
            public void Shuffle_Distribution_Fairness()
            {
                int[] input = Enumerable.Range(1, 10).ToArray();
                int trials = 10000000;
                Dictionary<int, int> counts = new Dictionary<int, int>();
                foreach (int i in input) counts[i] = 0;

                // Run Shuffle repeatedly and record the first element
                for (int t = 0; t < trials; t++)
                {
                    int first = input.Shuffle().First();
                    counts[first]++;//just adding to dictoanaty for lTER CHECKING 
                }


                double expected = trials / (double)input.Length;

                Console.WriteLine("=== Shuffle Fairness Check ===");
                Console.WriteLine($"Total Trials: {trials}, Expected per element: ~{expected:N0}");


                foreach (KeyValuePair<int, int> kv in counts)
                {
                    Console.WriteLine($"Element {kv.Key} → {kv.Value:N0} times");

                    Assert.IsTrue(kv.Value >= expected * 0.95 && kv.Value <= expected * 1.05, // tighter tolerance with more trials
               $"Element {kv.Key} count {kv.Value} is outside tolerance. Expected ~{expected}.");
                }
            }

            // ---------- SAMPLE ----------

            // Ensure Sample always picks values that exist in the collection
            [TestMethod]
            public void Sample_ProducesOnlyValidElements()
            {
                int[] input = new[] { 10, 20, 30 };
                List<int> results = input.Sample().Take(100).ToList();
                //taking the 100samples as test and check that produced elemets are
                foreach (int r in results)//form the 10,20,30
                    CollectionAssert.Contains(input.ToList(), r);
            }

            // Check that Sample returns elements uniformly (rough distribution check)
            [TestMethod]
            //GPT
            public void Sample_UniformDistribution()
            {
                int[] input = new[] { 1, 2, 3, 4 };
                int trials = 10000000;
                Dictionary<int, int> counts = input.ToDictionary(x => x, x => 0);

                foreach (int val in input.Sample().Take(trials))
                    counts[val]++;                
               
                double expected = trials / (double)input.Length;

                Console.WriteLine($"Sample_UniformDistribution\nTotal Trials: {trials}, Expected per element: ~{expected:N0}");

                // Each element should appear within 2-3% of expected frequency
                //Citation : Write a unit test for my Sample<T> extension method
                //to check that it produces a uniform random distribution. Use an
                //input array [1,2,3,4]. Run the sampler for 10000000 trials, count h
                //ow many times each element appears, and store the counts in a dicti
                //onary. Then verify that each element appears within ±2-4% of the expected frequen
                //cy. If any element is outside the tolerance, the test should fail with a clear message.
                foreach (KeyValuePair<int, int> kv in counts)
                {
                    Console.WriteLine($"Element {kv.Key} → {kv.Value:N0} times");

                    Assert.IsTrue(kv.Value >= expected * 0.95 && kv.Value <= expected * 1.05, // tighter tolerance with more trials
               $"Element {kv.Key} count {kv.Value} is outside tolerance. Expected ~{expected}.");
                }
            }
            //GPT  
            // Edge case: Single element collection
            // Sample should always return the same element
            [TestMethod]
            public void Sample_SingleElement_Repeats()
            {
                int[] input = new[] { 42 };
                List<int> results = input.Sample().Take(10).ToList();
                //take 10 times snice collection is infinite and just 1 item
                foreach (int r in results)
                    Assert.AreEqual(42, r);
            }

            // Edge case: Empty collection
            // Sample should produce nothing
            [TestMethod]
            public void Sample_Empty_NoResults()
            {
                int[] input = Array.Empty<int>();
                List<int> results = input.Sample().Take(10).ToList();
                //IF ITS EMPTY SO 0=0
                Assert.AreEqual(0, results.Count);
            }
        }
    }
}
