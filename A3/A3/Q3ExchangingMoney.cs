using System;
using System.Collections.Generic;
using TestCommon;
namespace A3
{
    public class Q3ExchangingMoney : Processor
    {
        public Q3ExchangingMoney(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, string[]>)Solve);

        public List<Node> BellmanFord(List<Edge> Edges, long Nodecount)
        {
            List<Node> Nodes = new List<Node>();
            for (int i = 0; i < Nodecount; i++)
            {
                for (int j = 0; j < Edges.Count; j++)
                {
                    if (Edges[j].Dst.distance > Edges[j].Src.distance + Edges[j].Cost)
                    {
                        if (i == Nodecount - 1)
                        {
                            Nodes.Add(Edges[j].Dst);
                            Nodes.Add(Edges[j].Src);
                        }
                        Edges[j].Dst.distance = Edges[j].Src.distance + Edges[j].Cost;
                    }
                }
            }
            return Nodes;
        }
        public void BFS(List<Node> Nodes)
        {
            Queue<Node> Q = new Queue<Node>();
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (!Nodes[i].visited)
                {
                    Nodes[i].visited = true;
                    Q.Enqueue(Nodes[i]);
                    while (Q.Count != 0)
                    {
                        for (int j = 0; j < Q.Count; j++)
                        {
                            Node Dequeued = Q.Dequeue();
                            long adj_cnt = Dequeued.adjacents.Count;
                            for (int z = 0; z < adj_cnt; z++)
                            {
                                Node node = Dequeued.adjacents[z];
                                if (!node.visited)
                                {
                                    Q.Enqueue(node);
                                    node.visited = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        public string[] Solve(long nodeCount, long[][] edges, long startNode)
        {
            string[] result = new string[nodeCount];
            List<Node> Nodes = new List<Node>();
            List<Edge> Edges = new List<Edge>();
            for (int i = 1; i <= nodeCount; i++)
            {
                Nodes.Add(new Node(i, double.PositiveInfinity));
            }

            Nodes[(int)startNode - 1].distance = 0;

            for (int j = 0; j < edges.Length; j++)
            {
                Nodes[(int)edges[j][0] - 1].adjacents.Add(Nodes[(int)edges[j][1] - 1]);
            }
            for (int j = 0; j < edges.Length; j++)
            {
                Edges.Add(new Edge(Nodes[(int)edges[j][0] - 1], Nodes[(int)edges[j][1] - 1], edges[j][2]));
            }

            List<Node> InCycle = BellmanFord(Edges, nodeCount);
            BFS(InCycle);
            for (int i = 0; i < nodeCount; i++)
            {
                if (Nodes[i].visited)
                {
                    result[i] = "-";
                    continue;
                }
                if (Nodes[i].distance == double.PositiveInfinity)
                { result[i] = "*"; }
                else { result[i] = Nodes[i].distance.ToString(); }
            }

            return result;
        }
    }
}
