using System;
using System.Collections.Generic;
namespace _2
{
    public class Edge
    {
        public Edge(Node src, Node dst, double cost){
            Src = src; Dst = dst; Cost = cost;
        }
        public Node Src, Dst;
        public double Cost;
    }
    public class Node
    {
        public Node(long v, double init_dist)
            { value = v; distance = init_dist; }
        public long value;
        public List<Node> adjacents = new List<Node>();
        public Dictionary<long, long> Costs = new Dictionary<long, long>();
        public bool visited = false;
        public double distance;
        public Node Previous = null;
    }

    class Program
    {
        public static bool BellmanFord(List<Edge> Edges, long Nodecount)
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
        static void Main(string[] args)
        {
            string[] strs = Console.ReadLine().Split();
            long nodeCount = Int64.Parse(strs[0]);
            long edgeCount = Int64.Parse(strs[1]);
            List<Node> Nodes = new List<Node>();
            List<Edge> Edges = new List<Edge>();
            for(int i = 1; i <= nodeCount; i++){
                Nodes.Add(new Node(i, double.PositiveInfinity));
            }
            for(int i = 0; i < edgeCount; i++)
            {
                string[] node = Console.ReadLine().Split();
                long n = Int64.Parse(node[0]);
                long adj = Int64.Parse(node[1]);
                long cost = Int64.Parse(node[2]);
                Edges.Add(new Edge(Nodes[(int)n - 1], Nodes[(int)adj - 1], cost));
            }
            for(int i = 0; i < nodeCount; i++){
                if(!Nodes[i].visited){
                    Nodes[i].distance = 0;
                if(BellmanFord(Edges, nodeCount))
                    { System.Console.WriteLine(1);
                        return; }
                }
            }
            System.Console.WriteLine(0);
        }
    }
}
