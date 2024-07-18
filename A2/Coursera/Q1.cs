using System;
using System.Collections.Generic;

namespace _1
{
    public class Node
    {
        public Node(long v, long init_dist = 0, char c = 'w')
            { value = v; distance = init_dist; color = c; }
        public long value;
        public List<Node> adjacents = new List<Node>();
        public bool visited = false;
        public long distance;
        public char color;
    }
    class Program
    {
        public static void ShortestPath(List<Node> nodes, Node startNode){
            Queue<Node> Q = new Queue<Node>();
            startNode.distance = 0; startNode.visited = true;
            Q.Enqueue(startNode);
            while(Q.Count != 0)
            {
                Node Dequeued = Q.Dequeue();
                foreach(Node adj in Dequeued.adjacents)
                {
                    if (!adj.visited)
                    {
                        adj.visited = true;
                        Q.Enqueue(adj);
                        adj.distance = Dequeued.distance + 1;
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            string[] strs = Console.ReadLine().Split();
            long nodeCount = Int64.Parse(strs[0]);
            long edgeCount = Int64.Parse(strs[1]);
            List<Node> Nodes = new List<Node>();
            for(int i = 1; i <= nodeCount; i++){
                Nodes.Add(new Node(i, nodeCount));
            }
            for(int i = 0; i < edgeCount; i++)
            {
                string[] node = Console.ReadLine().Split();
                long n = Int64.Parse(node[0]);
                long adj = Int64.Parse(node[1]);
                Nodes[(int)n-1].adjacents.Add(Nodes[(int)adj-1]);
                Nodes[(int)adj-1].adjacents.Add(Nodes[(int)n-1]);
            }
            string[] start_end = Console.ReadLine().Split();
            long StartNode = Int64.Parse(start_end [0]);
            long EndNode = Int64.Parse(start_end [1]);
            ShortestPath(Nodes, Nodes[(int) StartNode - 1]);
            if(!Nodes[(int) EndNode - 1].visited)
                System.Console.WriteLine(-1); 
            else
            System.Console.WriteLine(Nodes[(int) EndNode - 1].distance); 
        }
    }
}
