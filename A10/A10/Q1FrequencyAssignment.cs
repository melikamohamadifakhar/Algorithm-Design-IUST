using System;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class Node 
    {
        public int color_1;
        public int color_2;
        public int color_3;
        public int value;
        public Node (int v)
        {
            color_1 = 3*v - 2;
            color_2 = 3*v - 1;
            color_3 = 3*v;
            value = v;
        }
    }
    public class Q1FrequencyAssignment : Processor
    {
        public Q1FrequencyAssignment(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);


        public String[] Solve(int V, int E, long[,] matrix)
        {
            List<string> ans = new List<string>();
            List<Node> nodes = new List<Node>();
            ans.Add($"{V*4 + E*3} {3*V}");
            for (int i = 1; i <= V; i++)
            {
                var node = new Node(i);
                nodes.Add(node);
                ans.Add($"{node.color_1} {node.color_2} {node.color_3} 0");
                ans.Add($"-{node.color_1} -{node.color_2} 0");
                ans.Add($"-{node.color_1} -{node.color_3} 0");
                ans.Add($"-{node.color_2} -{node.color_3} 0");
            }
            for (int i = 0; i < E; i++)
            {
                var src = nodes[(int)matrix[i, 0] - 1];
                var dst =  nodes[(int)matrix[i, 1] - 1];
                ans.Add($"-{src.color_1} -{dst.color_1} 0");
                ans.Add($"-{src.color_2} -{dst.color_2} 0");
                ans.Add($"-{src.color_3} -{dst.color_3} 0");
            }
            return ans.ToArray();
        }

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

    }
}
