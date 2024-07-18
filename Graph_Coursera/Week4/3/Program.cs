using System;
using System.Collections.Generic;

namespace _3
{
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
    public class Edge
    {
        public Edge(Node src, Node dst, double cost){
            Src = src; Dst = dst; Cost = cost;
        }
        public Node Src, Dst;
        public double Cost;
    }
    class Program
    {
        public static List<Node> BellmanFord(List<Edge> Edges, long Nodecount)
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
        public static void BFS(List<Node> Nodes)
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
        static void Main(string[] args)
        {
            string[] strs = Console.ReadLine().Split();
            long nodeCount = Int64.Parse(strs[0]);
            long edgeCount = Int64.Parse(strs[1]);
            string[] result = new string[nodeCount];
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
            string start = Console.ReadLine();
            long startNode = Int64.Parse(start);
            Nodes[(int)startNode - 1].distance = 0;
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

            foreach(var ans in result){
                System.Console.WriteLine(ans);
            }
        }
    }
}
