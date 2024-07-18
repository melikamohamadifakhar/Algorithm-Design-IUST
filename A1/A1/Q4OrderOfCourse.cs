using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q4OrderOfCourse: Processor
    {
        public Q4OrderOfCourse(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long[]>)Solve);
        public static long pre_post = 0;
        public static void Explore(Node v)
        {
            v.visited = true;
            v.preVisited = pre_post;
            pre_post ++;
            foreach(var adj in v.adjacents)
            {
                if(!adj.visited){
                    Explore(adj);
                }
            }
            {v.postVisited = pre_post; pre_post++;}
        }
        public long[] Solve(long nodeCount, long[][] edges)
        {
            long[] result = new long[nodeCount];
            List<Node> Nodes = new List<Node>();
            for(int i = 1; i <= nodeCount; i++){
                Nodes.Add(new Node(i));
            }
            for(int i = 0; i < edges.Length; i++){
                Nodes[(int)edges[i][0]-1].adjacents.Add(Nodes[(int)edges[i][1]-1]);
                Nodes[(int)edges[i][1]-1].IncomingEdge ++;
            }
            for(int i = 0; i < nodeCount; i++){
                if(Nodes[i].IncomingEdge == 0)
                    Explore(Nodes[i]);
            }
            var ans = Nodes.OrderByDescending(x => x.postVisited);
            pre_post = 0;
            return ans.Select(x => x.value).ToArray();
        }

        public override Action<string, string> Verifier { get; set; } = TopSortVerifier;

        public static void TopSortVerifier(string inFileName, string strResult)
        {
            long[] topOrder = strResult.Split(TestTools.IgnoreChars)
                .Select(x => long.Parse(x)).ToArray();

            long count;
            long[][] edges;
            TestTools.ParseGraph(File.ReadAllText(inFileName), out count, out edges);

            // Build an array for looking up the position of each node in topological order
            // for example if topological order is 2 3 4 1, topOrderPositions[2] = 0, 
            // because 2 is first in topological order.
            long[] topOrderPositions = new long[count];
            for (int i = 0; i < topOrder.Length; i++)
                topOrderPositions[topOrder[i] - 1] = i;
            // Top Order nodes is 1 based (not zero based).

            // Make sure all direct depedencies (edges) of the graph are met:
            //   For all directed edges u -> v, u appears before v in the list
            foreach (var edge in edges)
                if (topOrderPositions[edge[0] - 1] >= topOrderPositions[edge[1] - 1])
                    throw new InvalidDataException(
                        $"{Path.GetFileName(inFileName)}: " +
                        $"Edge dependency violoation: {edge[0]}->{edge[1]}");

        }
    }
}
