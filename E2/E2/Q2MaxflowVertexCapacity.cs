using TestCommon;

namespace E2;

public class Q2MaxflowVertexCapacity : Processor
{
    public Q2MaxflowVertexCapacity(string testDataName) : base(testDataName)
    {
        // this.ExcludeTestCases(1, 2, 3);
        // this.ExcludeTestCaseRangeInclusive(1, 3);
    }
    public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long[], long, long, long>)Solve);

    public virtual long Solve(long nodeCount, long edgeCount, long[][] edges, long[] nodeWeight
        , long startNode, long endNode)
    {
        throw new NotImplementedException();
    }
}
