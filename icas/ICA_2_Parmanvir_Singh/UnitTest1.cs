namespace ICA_2
{

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            KNIGHTS_CLASS kNIGHTS_ = new KNIGHTS_CLASS(4, 4);
            foreach ((int, int) k in kNIGHTS_)
            {
                Console.WriteLine($"{k.Item1},{k.Item2}");
            }

        }

        [TestClass]
        public class IteratorUtilsTests
        {
            // ---------- PEEL ----------
            [TestMethod]
            public void Peel_EvenCount_ReturnsPairs()
            {
                var input = new[] { 1, 2, 3, 4 };
                var result = input.Peel().ToList();

                Assert.AreEqual(2, result.Count);
                CollectionAssert.AreEqual(new List<int> { 1, 4 }, result[0]);
                CollectionAssert.AreEqual(new List<int> { 2, 3 }, result[1]);
            }

            [TestMethod]
            public void Peel_OddCount_ReturnsPairsAndSingle()
            {
                var input = new[] { 1, 2, 3, 4, 5 };
                var result = input.Peel().ToList();

                Assert.AreEqual(3, result.Count);
                CollectionAssert.AreEqual(new List<int> { 1, 5 }, result[0]);
                CollectionAssert.AreEqual(new List<int> { 2, 4 }, result[1]);
                CollectionAssert.AreEqual(new List<int> { 3 }, result[2]);
            }

            [TestMethod]
            public void Peel_Empty_ReturnsNothing()
            {
                var input = Array.Empty<int>();
                var result = input.Peel().ToList();

                Assert.AreEqual(0, result.Count);
            }

            // ---------- SHUFFLE ----------
            [TestMethod]
            public void Shuffle_PermutationProperty()
            {
                var input = Enumerable.Range(1, 10).ToArray();
                var shuffled = input.Shuffle().ToArray();

                Assert.AreEqual(input.Length, shuffled.Length);
                foreach (var val in input)
                    Assert.IsTrue(shuffled.Contains(val));
            }

            [TestMethod]
            public void Shuffle_Distribution_Fairness()
            {
                var input = Enumerable.Range(1, 5).ToArray();
                int trials = 10000;
                var counts = new Dictionary<int, int>();
                foreach (var i in input) counts[i] = 0;

                for (int t = 0; t < trials; t++)
                {
                    var first = input.Shuffle().First();
                    counts[first]++;
                }

                double expected = trials / (double)input.Length;
                foreach (var kv in counts)
                {
                    Assert.IsTrue(kv.Value >= expected * 0.8 && kv.Value <= expected * 1.2,
                        $"Element {kv.Key} count {kv.Value} is outside tolerance.");
                }
            }

            // ---------- SAMPLE ----------
            [TestMethod]
            public void Sample_ProducesOnlyValidElements()
            {
                var input = new[] { 10, 20, 30 };
                var results = input.Sample().Take(100).ToList();

                foreach (var r in results)
                    CollectionAssert.Contains(input.ToList(), r);
            }

            [TestMethod]
            public void Sample_UniformDistribution()
            {
                var input = new[] { 1, 2, 3, 4 };
                int trials = 20000;
                var counts = input.ToDictionary(x => x, x => 0);

                foreach (var val in input.Sample().Take(trials))
                    counts[val]++;

                double expected = trials / (double)input.Length;
                foreach (var kv in counts)
                {
                    Assert.IsTrue(kv.Value >= expected * 0.8 && kv.Value <= expected * 1.2,
                        $"Element {kv.Key} count {kv.Value} is outside tolerance.");
                }
            }

            [TestMethod]
            public void Sample_SingleElement_Repeats()
            {
                var input = new[] { 42 };
                var results = input.Sample().Take(10).ToList();

                foreach (var r in results)
                    Assert.AreEqual(42, r);
            }

            [TestMethod]
            public void Sample_Empty_NoResults()
            {
                var input = Array.Empty<int>();
                var results = input.Sample().Take(10).ToList();

                Assert.AreEqual(0, results.Count);
            }




        }
    }
}