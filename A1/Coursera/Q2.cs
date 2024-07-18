using System;
using System.Collections.Generic;
namespace _2
{
    public class Node
    {
        public Node(long v) { value = v; }
        public long value;
        public List<Node> adjacents = new List<Node>();
        public bool visited = false;
        public int CCnum = 0;
    }
    class Program
    {
        public static void BFS(List<Node> Nodes)
        {
            int cc = 1;
            Queue<Node> Q = new Queue<Node>();
            for(int i = 0; i < Nodes.Count; i++)
            {
                if(!Nodes[i].visited)
                {
                    Nodes[i].visited = true;
                    Nodes[i].CCnum = cc;
                    Q.Enqueue(Nodes[i]);
                    while(Q.Count != 0)
                    {
                        for(int j = 0; j < Q.Count; j++)
                        {
                                Node Dequeued = Q.Dequeue();
                                long adj_cnt = Dequeued.adjacents.Count;
                                for(int z = 0; z < adj_cnt; z++){
                                    Node node = Dequeued.adjacents[z];
                                    if(!node.visited){
                                        Q.Enqueue(node);
                                        node.visited = true;
                                        node.CCnum = cc;
                                    }
                                }
                        }
                    }
                    cc++;}
            }
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
            BFS(Nodes);
            long con_comp = 0;
            for(int i = 0; i < nodeCount; i++){
                if(Nodes[i].CCnum > con_comp)
                    con_comp = Nodes[i].CCnum;
            }
            System.Console.WriteLine(con_comp);

        }
    }
}
