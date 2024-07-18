using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;
using TestCommon;

namespace A3
{
    public class Node
    {
        public Node(long v, double init_dist)
            { value = v; distance = init_dist; }
        public long value;
        public List<Node> adjacents = new List<Node>();
        public Dictionary<long, double> Costs = new Dictionary<long, double>();
        public bool visited = false;
        public bool visited_r = false;
        public double distance;
        public Node Previous = null;
        public bool IsInNegativeCycle = false;
    }

    public class Q1MinCost : Processor
    {
        public Q1MinCost(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

        static double Dijkstra(List<Node> Nodes, Node startNode, Node endNode)
        {
            SimplePriorityQueue<Node, double> p_queue = new SimplePriorityQueue<Node, double>();
            startNode.distance = 0;
            foreach (var node in Nodes){
                p_queue.Enqueue(node, node.distance);
            }
            
            while(p_queue.Count != 0)
            {
                Node dequeuedNode = p_queue.Dequeue();
                foreach (var node in dequeuedNode.adjacents){
                    double newcost = dequeuedNode.distance + dequeuedNode.Costs[node.value];
                    if(node.distance > newcost)
                    {
                        node.distance = newcost;
                        node.Previous = dequeuedNode;
                        p_queue.UpdatePriority(node, newcost);
                    }
                }
            }
            return endNode.distance;
        }
        public long Solve(long nodeCount, long[][] edges, long startNode, long endNode)
        {
            List<Node> Nodes = new List<Node>();
            for(int i = 1; i <= nodeCount; i++){
                Nodes.Add(new Node(i, double.PositiveInfinity));
            }
            for(int j = 0; j < edges.Length; j++){
                Nodes[(int)edges[j][0]-1].adjacents.Add(Nodes[(int)edges[j][1]-1]);
                Nodes[(int)edges[j][0]-1].Costs.Add(Nodes[(int)edges[j][1]-1].value, edges[j][2]);
            }
            double min_dist = Dijkstra(Nodes, Nodes[(int)startNode-1], Nodes[(int)endNode-1]);
            if(Double.IsInfinity(min_dist)) { min_dist = -1; }
            return (long) min_dist;
        }
    }
}



