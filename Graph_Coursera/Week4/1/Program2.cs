using System;
using System.Collections.Generic;
namespace _12
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
    class Program
    {
     static double Dijkstra(List<Node> Nodes, Node startNode, Node endNode)
        {
            startNode.distance = 0;
            Nodes.Sort(delegate(Node n1, Node n2) 
                { return n1.distance.CompareTo(n2.distance); });
            while(Nodes.Count != 0)
            {
                Nodes.Sort(delegate(Node n1, Node n2) 
                    { return n1.distance.CompareTo(n2.distance); });
                Node dequeuedNode = Nodes[0];
                Nodes.RemoveAt(0);
                foreach (var node in dequeuedNode.adjacents){
                    double newcost = dequeuedNode.distance + dequeuedNode.Costs[node.value];
                    if(node.distance > newcost)
                    {
                        node.distance = newcost;
                        node.Previous = dequeuedNode;
                    }
                }
            }
            return endNode.distance;
        }
        static void Main(string[] args)
        {
            string[] strs = Console.ReadLine().Split();
            long nodeCount = Int64.Parse(strs[0]);
            long edgeCount = Int64.Parse(strs[1]);
            List<Node> Nodes = new List<Node>();
            for(int i = 1; i <= nodeCount; i++){
                Nodes.Add(new Node(i, double.PositiveInfinity));
            }
            for(int i = 0; i < edgeCount; i++)
            {
                string[] node = Console.ReadLine().Split();
                long n = Int64.Parse(node[0]);
                long adj = Int64.Parse(node[1]);
                long cost = Int64.Parse(node[2]);
                Nodes[(int)n-1].adjacents.Add(Nodes[(int)adj-1]);
                Nodes[(int)n-1].Costs.Add(adj, cost);
            }
            string[] start_end = Console.ReadLine().Split();
            long StartNode = Int64.Parse(start_end [0]);
            long EndNode = Int64.Parse(start_end [1]);
            double min_dist = Dijkstra(Nodes, Nodes[(int)StartNode-1], Nodes[(int)EndNode-1]);
            if(Double.IsInfinity(min_dist)) { min_dist = -1; }
            System.Console.WriteLine((long)min_dist);
        }
    }
}
