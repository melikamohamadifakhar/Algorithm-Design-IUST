using System;
using System.Collections.Generic;

namespace _2
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
        public static bool BipartiteGraphOrNot(List<Node> nodes){
            Queue<Node> queue = new Queue<Node>();
            for(int i = 0; i < nodes.Count; i++)
            {
                if(!nodes[i].visited)
                {
                    queue.Enqueue(nodes[i]);
                    while(queue.Count != 0)
                    {
                        Node Dequeued = queue.Dequeue();
                        foreach(Node adj in Dequeued.adjacents)
                        {
                            if(adj.visited){
                                if(adj.color == Dequeued.color)
                                    return false;
                            }
                            else{
                                adj.visited = true;
                                if(Dequeued.color == 'r') {adj.color = 'b';}
                                else {adj.color = 'r';}
                                queue.Enqueue(adj);
                            }
                        }
                    }
                }
            }
            return true;
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
            if(BipartiteGraphOrNot(Nodes)) System.Console.WriteLine(1); 
            else System.Console.WriteLine(0);
        }
    }
}
