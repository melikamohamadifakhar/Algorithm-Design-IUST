using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A4
{
    public class Q1BuildingRoads : Processor
    {
        public Q1BuildingRoads(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], double>)Solve);

        public double Solve(long pointCount, long[][] points)
        {
            double min_dist = 0;
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
            for(int i = 0; i < SortedEdges.Count; i++){
                if(DisjointSet.find(SortedEdges[i].Src.value) == DisjointSet.find(SortedEdges[i].Dst.value)){ 
                    continue;
                }
                DisjointSet.union(SortedEdges[i].Src.value, SortedEdges[i].Dst.value);
                min_dist += SortedEdges[i].Cost;
            }
            return double.Parse(min_dist.ToString("#.000000"));
        }
    }
}