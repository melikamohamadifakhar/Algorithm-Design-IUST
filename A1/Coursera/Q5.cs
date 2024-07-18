using System;
using System.Collections.Generic;

namespace _3
{
    public class Node
    {
        public Node(long v) { value = v; }
        public long value;
        public List<Node> adjacents = new List<Node>();
        public bool visited = false;
        public int CCnum = 0;
        public bool IsInStack = false;
        public long preVisited =0;
        public long postVisited =0;
        public long IncomingEdge =0;

    }
    class Program
    {
        public static Stack<Node> stack = new Stack<Node>();
        public static bool push = true; 
        public static long SccNum = 0;
        public static void Explore(Node v)
        {
            v.visited = true;
            foreach(var adj in v.adjacents)
            {
                if(!adj.visited){
                    Explore(adj);
                }
            }
            if(push)
                stack.Push(v);
        }
        static void Main(string[] args)
        {
            string[] strs = Console.ReadLine().Split();
            long nodeCount = Int64.Parse(strs[0]);
            long edgeCount = Int64.Parse(strs[1]);
            SccNum = 0; push = true;
            List<Node> Nodes = new List<Node>();
            List<Node> ReversedG = new List<Node>();
            for(int i = 1; i <= nodeCount; i++){
                Nodes.Add(new Node(i));
                ReversedG.Add(new Node(i));
            }
            for(int i = 0; i < edgeCount; i++)
            {
                string[] node = Console.ReadLine().Split();
                long n = Int64.Parse(node[0]);
                long adj = Int64.Parse(node[1]);
                Nodes[(int)n-1].adjacents.Add(Nodes[(int)adj-1]);
                ReversedG[(int)adj-1].adjacents.Add(ReversedG[(int)n-1]);
            }
            for(int i = 0; i < nodeCount; i++){
                if(!Nodes[i].visited)
                    Explore(Nodes[i]);
            }
            push = false;
            while(stack.Count > 0){
                var poped = stack.Pop().value;
                if(!ReversedG[(int)poped-1].visited){
                    Explore(ReversedG[(int)poped-1]);
                    SccNum ++;
                }
            }
            System.Console.WriteLine(SccNum);
        }
    }
}
