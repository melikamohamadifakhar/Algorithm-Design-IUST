using System;
using System.Collections.Generic;

namespace _1
{
    public class Node
    {
        public Node(long v) { value = v; }
        public long value;
        public List<Node> adjacents = new List<Node>();
        public bool visited = false;
    }
    class Program
    {
        public static void Explore(Node v)
        {
            v.visited = true;
            foreach(var adj in v.adjacents)
                if(!adj.visited)
                    Explore(adj);
        }
        static void Main(string[] args)
        {
            string[] strs = Console.ReadLine().Split();
            long nodeCount = Int64.Parse(strs[0]);
            long edgeCount = Int64.Parse(strs[1]);
            List<Node> Nodes = new List<Node>();
            for(int i = 1; i <= nodeCount; i++){
                Nodes.Add(new Node(i));
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
            Explore(Nodes[(int)Int64.Parse(start_end[0])-1]);
            if(Nodes[(int)Int64.Parse(start_end[1])-1].visited)
                System.Console.WriteLine(1);
            else
                System.Console.WriteLine(0);
        }
    }
}
