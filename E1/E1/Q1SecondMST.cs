using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;


namespace E1
{
    public class Edge
    {
        public long Src, Dst;
        public long Cost;
        public Edge(long src, long dst, long cst)
        {
            Src = src; Dst = dst; Cost = cst; 
        }
    }
    // public class Node
    // {
    //     public long value;
    //     // public Dictionary<Node, long> adj = new Dictionary<Node, long>();
    //     public Node(long value)
    //     {
    //         this.value = value;
    //     }
    // }
    public class DisjointUnionSets
    {
        int[] rank;
        List<long> parent = new List<long>();
        long n;
        public DisjointUnionSets(long n, List<long> parents)
        {
            rank = new int[n];
            parent = parents;
            this.n = n;
        }
        public long find(long x)
        {
            if (parent[(int)x] != x)
            {
                parent[(int)x] = find(parent[(int)x]);
            }
            return parent[(int)x];
        }
        public void union(long x, long y)
        {
            long xRoot = find(x), yRoot = find(y);
            if (xRoot == yRoot)
                return;
            if (rank[xRoot] < rank[yRoot])
                parent[(int)xRoot] = yRoot;
            else if (rank[yRoot] < rank[xRoot])
                parent[(int)yRoot] = xRoot;

            else
            {
                parent[(int)yRoot] = xRoot;
                rank[xRoot] = rank[xRoot] + 1;
            }
        }
    }
    public class Q1SecondMST : Processor
    {
        public Q1SecondMST(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public (List<Edge>, long) MST(DisjointUnionSets DisjointSet, List<long> Nodes, List<Edge> SortedEdges, long nodeCount ){
            long min_dist = 0;
            bool[] visited = new bool[nodeCount];
            for(int i = 0; i < nodeCount; i++) visited[i] = false;
            List<Edge> used_edge = new List<Edge>();
            for(int i = 0; i < SortedEdges.Count; i++){
                if(DisjointSet.find(SortedEdges[i].Src) == DisjointSet.find(SortedEdges[i].Dst)){ 
                    continue;
                }
                visited[SortedEdges[i].Src] = true;
                visited[SortedEdges[i].Dst] = true;
                DisjointSet.union(SortedEdges[i].Src, SortedEdges[i].Dst);
                min_dist += SortedEdges[i].Cost;
                used_edge.Add(SortedEdges[i]);
            }
            if(visited.Contains(false)) min_dist = -1;
            return (used_edge, min_dist);
        }
        public long Solve(long nodeCount, long[][] edges)
        {
            long sec_min_dist = long.MaxValue;
            List<long> Nodes = new List<long>();
            List<Edge> Edges = new List<Edge>();
            for( int i = 0; i < nodeCount; i++){
                Nodes.Add(i);
            }
            long[] Nodes_p = Nodes.ToArray();
            for (int i = 0; i < edges.Length; i++)
            {
                Edges.Add(new Edge(edges[i][0] - 1, edges[i][1] - 1, edges[i][2]));
            }
            List<Edge> SortedEdges = Edges.OrderBy(x => x.Cost).ToList();
            DisjointUnionSets DisjointSet = new DisjointUnionSets(nodeCount, Nodes);
            var TheMST = MST(DisjointSet, Nodes, SortedEdges, nodeCount);
            
            for (int i = 0; i<SortedEdges.Count; i++)
            {
                var DisjointSet_ = new DisjointUnionSets(nodeCount, Nodes_p.ToList());
                Edge temp = SortedEdges[i];
                SortedEdges.RemoveAt(i);
                var newmst = MST(DisjointSet_, Nodes_p.ToList(), SortedEdges, nodeCount);
                if (newmst.Item2 != -1){
                    if (newmst.Item2 < sec_min_dist)
                    {
                        if (newmst.Item2 == TheMST.Item2)
                        {
                            if(!newmst.Item1.All(newmst.Item1.Contains))
                            {
                                sec_min_dist = newmst.Item2;
                            }
                        }
                        else
                        {
                            sec_min_dist = newmst.Item2;
                        }
                    }
                }
                Edges.Insert(i, temp);
            }
            if (sec_min_dist == long.MaxValue) {return -1;}
            return sec_min_dist;
        }

    }
}