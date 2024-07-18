using System;
using System.Collections.Generic;
using System.Linq;

namespace _2
{
    public class Node
    {
        public Node(long v) { value = v; }
        public long value;
        public List<Node> adjacents = new List<Node>();
        public bool visited = false;
        public int CCnum = 0;
        public bool IsInStack = false;
        public long preVisited = 0;
        public long postVisited = 0;
        public long IncomingEdge = 0;
    }
    class Program
    {
        public static List<Node> DFS(List<Node> nodes){
            long visit = 0;
            List<Node> result = new List<Node>();
            Stack<Node> stack = new Stack<Node>();
            for (int j = 0; j < nodes.Count; j++)
            {
                if(!nodes[j].visited){
                    nodes[j].visited = true;
                    nodes[j].preVisited = visit;
                    visit += 1;
                    stack.Push(nodes[j]);
                    while (stack.Count != 0)
                    {
                        Node poped = stack.Pop();
                        result.Add(poped);
                        for(int i = 0; i < poped.adjacents.Count; i++)
                        {
                            if(!poped.adjacents[i].visited)
                            {
                                poped.adjacents[i].visited = true;
                                poped.adjacents[i].preVisited = visit;
                                visit ++;
                                stack.Push(poped.adjacents[i]);
                            }
                        }
                    }
                        nodes[j].postVisited = visit;
                        visit ++;
                }
            }
            result.Reverse();
            return result;
        }
        public static long pre_post = 0;
        public static void Explore(Node v)
        {
            v.visited = true;
            v.preVisited = pre_post;
            pre_post ++;
            foreach(var adj in v.adjacents)
            {
                if(!adj.visited){
                    Explore(adj);
                }
            }
            {v.postVisited = pre_post; pre_post++;}
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
                Nodes[(int)adj-1].IncomingEdge ++;
            }
            for(int i = 0; i < nodeCount; i++){
                if(Nodes[i].IncomingEdge == 0)
                    Explore(Nodes[i]);
            }
            var ans = Nodes.OrderByDescending(x => x.postVisited);
            foreach (Node n in ans){
                Console.Write(n.value + " ");
            }
        }
    }
}
