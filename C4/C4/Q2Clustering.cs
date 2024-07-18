using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
using static C4.Q1BuildingRoads;

namespace C4
{
    public class Q2Clustering : Processor
    {
        public Q2Clustering(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, double>)Solve);

        public double Solve(long pointCount, long[][] points, long clusterCount)
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
    }
}