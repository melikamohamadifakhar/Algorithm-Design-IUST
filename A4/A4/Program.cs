using System;
using System.Collections.Generic;

namespace A4
{
    public class Edge
    {
        public Node Src, Dst;
        public double Cost;
        public Edge(Node src, Node dst)
        {
            Src = src; Dst = dst;
            Cost = ComputeDistance(src, dst);
        }
        public double ComputeDistance(Node a, Node b){
            return Math.Sqrt(Math.Pow((a.x - b.x), 2) + Math.Pow((a.y - b.y), 2));
        }
    }
    public class Node
    {
        public long value;
        public long x, y;
        public Node(long value, long x, long y)
        {
            this.value = value;
            this.x = x; 
            this.y = y;
        }
    }
    class DisjointUnionSets
    {
        int[] rank;
        List<Node> parent = new List<Node>();
        long n;
        public DisjointUnionSets(long n, List<Node> parents)
        {
            rank = new int[n];
            parent = parents;
            this.n = n;
        }
        public long find(long x)
        {
            if (parent[(int)x].value != x)
            {
                parent[(int)x].value = find(parent[(int)x].value);
            }
            return parent[(int)x].value;
        }
        public void union(long x, long y)
        {
            long xRoot = find(x), yRoot = find(y);
            if (xRoot == yRoot)
                return;
            if (rank[xRoot] < rank[yRoot])
                parent[(int)xRoot].value = yRoot;
            else if (rank[yRoot] < rank[xRoot])
                parent[(int)yRoot].value = xRoot;

            else
            {
                parent[(int)yRoot].value = xRoot;
                rank[xRoot] = rank[xRoot] + 1;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
