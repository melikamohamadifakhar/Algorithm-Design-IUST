using System;
using System.Collections.Generic;
using Priority_Queue;
using TestCommon;
namespace A3
{
    public class Edge
    {
        public Edge(Node src, Node dst, double cost){
            Src = src; Dst = dst; Cost = cost;
        }
        public Node Src, Dst;
        public double Cost;
    }
    public class Q2DetectingAnomalies : Processor
    {
        public Q2DetectingAnomalies(string testDataName) : base(testDataName) {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public bool BellmanFord(List<Edge> Edges, long Nodecount)
        {
            for(int i = 0; i < Nodecount; i++)
            {
                for(int j = 0; j < Edges.Count; j++)
                {
                    if(Edges[j].Dst.distance > Edges[j].Src.distance + Edges[j].Cost)
                    {
                        Edges[j].Dst.visited = true;
                        Edges[j].Src.visited = true;
                        if(i == Nodecount - 1) { return true; }
                        Edges[j].Dst.distance = Edges[j].Src.distance + Edges[j].Cost;
                    }
                }
            }
            return false;
        }

        public long Solve(long nodeCount, long[][] edges)
        {
            List<Node> Nodes = new List<Node>();
            List<Edge> Edges = new List<Edge>();
            for(int i = 1; i <= nodeCount; i++){
                Nodes.Add(new Node(i, double.PositiveInfinity));
            }
            Nodes[0].distance = 0;
            for(int j = 0; j < edges.Length; j++){
                Edges.Add(new Edge(Nodes[(int)edges[j][0]-1], Nodes[(int)edges[j][1]-1], edges[j][2]));
            }
            for(int i = 0; i < nodeCount; i++){
                if(!Nodes[i].visited){
                    Nodes[i].distance = 0;
                if(BellmanFord(Edges, nodeCount))
                    { return 1; }
                }
            }
            return 0;
        }
    }
}
