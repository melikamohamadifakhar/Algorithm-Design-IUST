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
        public int CCnum = 0;
        public bool IsInStack = false;
    }
    
    class Program
    {
        public static bool cycle(Node node)
        {
            node.visited = true;
            node.IsInStack = true;
            foreach (var adj in node.adjacents)
            {
                if (adj.IsInStack)
                    return true;
                else if (! adj.visited)
                {
                    if(cycle(adj))
                        return true;
                }
            }
            node.IsInStack = false;
            return false;
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
            }
            for(int i = 0; i < Nodes.Count; i++){
                if(!Nodes[i].visited)
                    if(cycle(Nodes[i]))
                    {
                        System.Console.WriteLine(1);
                        return;
                    }
            }
            System.Console.WriteLine(0);
        }
    }
}
