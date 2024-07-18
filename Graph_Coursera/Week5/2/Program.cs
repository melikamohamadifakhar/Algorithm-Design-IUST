using System;
using System.Collections.Generic;
using System.Linq;

namespace _2
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
        public static double Solve(long pointCount, long[][] points, long clusterCount)
        {
            List<Node> Nodes = new List<Node>();
            List<Edge> Edges = new List<Edge>();
            for( int i = 0; i < pointCount; i++){
                Node node = new Node(i, points[i][0], points[i][1]);
                Nodes.Add(node);
            }
            for( int i = 0; i < pointCount; i++){
                for ( int j = i+1; j < points.Length; j++){
                    Edges.Add(new Edge(Nodes[i], Nodes[j]));
                }
            }
            List<Edge> SortedEdges = Edges.OrderBy(x => x.Cost).ToList();
            DisjointUnionSets DisjointSet = new DisjointUnionSets(pointCount, Nodes);
            int cnt = 0;
            for(int i = 0; i < SortedEdges.Count; i++)
            {
                if(DisjointSet.find(SortedEdges[i].Src.value) != DisjointSet.find(SortedEdges[i].Dst.value))
                { 
                    cnt++;
                    DisjointSet.union(SortedEdges[i].Src.value, SortedEdges[i].Dst.value);
                }
                if(pointCount - cnt < clusterCount){
                    return Math.Round(SortedEdges[i].Cost, 6);
                }
            }
            return 0;
        }
        static void Main(string[] args)
        {
            long pointcount = Int64.Parse(Console.ReadLine());
            long[][] array = new long[pointcount][];
            for ( int i = 0; i < pointcount; i++)
            {
                array[i] = new long[2];
                var x = Console.ReadLine().Split();
                for( int j = 0; j < 2; j++){
                    array[i][j] = Int64.Parse(x[j]);
                }
            }
            long clusterCount = Int64.Parse(Console.ReadLine());
            System.Console.WriteLine(Solve(pointcount, array, clusterCount)); 
        }
    }
}
