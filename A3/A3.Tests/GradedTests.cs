using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCommon;

namespace A3.Tests
{

    [DeploymentItem("TestData", "A3_TestData")]
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(2000)]
        public void SolveTest_Q1MinCost()
        {
            RunTest(new Q1MinCost("TD1"));
        }

        [TestMethod(), Timeout(2000)]
        public void SolveTest_Q2DetectingAnomalies()
        {
            RunTest(new Q2DetectingAnomalies("TD2"));
        }

        [TestMethod(), Timeout(4000)]
        public void SolveTest_Q3ExchangingMoney()
        {
            RunTest(new Q3ExchangingMoney("TD3"));
        }
        //pass mishe. baraye e1 comment kardam.
         [TestMethod(), Timeout(39000)]
         public void SolveTest_Q4FriendSuggestion()
         {
             RunTest(new Q4FriendSuggestion("TD4"));
         }
        //
        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("A3", p.Process, p.TestDataName, p.Verifier,
                VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }

    }
}
// <14 
// 3
// 13
// 10
// 0
// 14
// 13
// -1
// 6
// 15
// 8
// -1
// 13
// 17
// -1
// 4
// 10
// 3
// 0
// 23
// 2
// 23
// 17
// 11
// -1
// 12
// 11
// 0
// 4
// 12
// -1
// 9
// 16///
// 12
// -1
// 14
// 4
// -1
// 2
// 8
// 0
// 18
// 5
// 0
// 12
// 16
// 19
// 15
// 13
// -1
// 12
// 8
// 9
// 6
// -1
// -1
// 23
// -1
// 19