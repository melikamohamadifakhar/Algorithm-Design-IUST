using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;
using TestCommon;

namespace A3
{
    public class Q4FriendSuggestion : Processor
    {
        public Q4FriendSuggestion(string testDataName) : base(testDataName) { 
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], 
            long, long[][], long[]>)Solve);

        public long BidirectionalDijkstra(List<Node> Nodes, List<Node> ReversedG
                                        ,long startNode, long endNode, long nodeCount){
            List<Node> Processed = new List<Node>();
            List<Node> Processed_R = new List<Node>();

            List<Node> visited = new List<Node>();
            List<Node> visited_r = new List<Node>();

            Nodes[(int) startNode - 1].distance = 0;
            ReversedG[(int) endNode - 1].distance = 0;

            SimplePriorityQueue<Node, double> p_queue = new SimplePriorityQueue<Node, double>();
            SimplePriorityQueue<Node, double> p_queue_R = new SimplePriorityQueue<Node, double>();

            for (int i = 0; i < nodeCount; i++)
            {
                p_queue.Enqueue(Nodes[i], Nodes[i].distance);
                p_queue_R.Enqueue(ReversedG[i], ReversedG[i].distance);
            }

            int j = 0;
            while (j<(nodeCount/2)+1)
            {
                Node dequeued = p_queue.Dequeue();
                Process(dequeued, Processed, p_queue, visited, false);
                Node dequeued_r = p_queue_R.Dequeue();
                Process(dequeued_r, Processed_R, p_queue_R, visited_r, true);
                var commons = Processed.Select(s1 => s1.value).ToList()
                .Intersect(Processed_R.Select(s2 => s2.value).ToList()).ToList();
                if(commons.Count > 0) {
                    return (long) ShortestPath(Nodes, ReversedG, Processed, Processed_R,
                    visited);
                }
                j++;
            }
            return -1;
        }
        public double ShortestPath(List<Node> G, List<Node> G_R, List<Node> Processed,
                                List<Node> Processed_R, List<Node> visited)
                                // , List<Node> visited_r)
        {
            double distance = double.PositiveInfinity;
            for(int i = 0; i < visited.Count; i++)
            {
                if(G_R[(int)visited[i].value - 1].visited_r)
                {
                    double newDist = G[(int) visited[i].value - 1].distance +
                        G_R[(int) visited[i].value - 1].distance;
                    if(newDist < distance)
                    {
                        distance = newDist;
                    }
                }
            }
            return distance;
        }
        public void Process(Node u, List<Node> processed,
                            SimplePriorityQueue<Node, double> p_queue, List<Node> visited,
                            bool reverse)
        {
            if(!reverse){
                if(!u.visited){
                    u.visited = true;
                    visited.Add(u);
                }
            }
            else
            {
                if(!u.visited_r){
                    u.visited_r = true;
                    visited.Add(u);
                }
            }

            foreach (var adj in u.adjacents){
                double newDist = u.distance + u.Costs[adj.value];
                if(newDist < adj.distance){
                    adj.distance = newDist;
                    p_queue.UpdatePriority(adj, adj.distance);
                }
                if(!reverse){
                    if(!adj.visited){
                        adj.visited = true;
                        visited.Add(adj);
                    }
                }
                else
                {
                    if(!adj.visited_r){
                        adj.visited_r = true;
                        visited.Add(adj);
                    }
                }
            }
            processed.Add(u);
        }

        public long[] Solve(long nodeCount, long edgeCount,
                            long[][] edges, long queriesCount,
                            long[][] queries)
        {
            long[] result = new long[queriesCount];
            List<Node> Nodes = new List<Node>();
            List<Node> ReversedG = new List<Node>();
            for(int i = 1; i <= nodeCount; i++){
                Nodes.Add(new Node(i, double.PositiveInfinity));
                ReversedG.Add(new Node(i, double.PositiveInfinity));
            }
            for(int i = 0; i < edges.Length; i++){
                Nodes[(int)edges[i][0]-1].adjacents.Add(Nodes[(int)edges[i][1]-1]);
                Nodes[(int)edges[i][0]-1].Costs.Add(Nodes[(int)edges[i][1]-1].value, edges[i][2]);

                ReversedG[(int)edges[i][1]-1].adjacents.Add(ReversedG[(int)edges[i][0]-1]);
                ReversedG[(int)edges[i][1]-1].Costs.Add(Nodes[(int)edges[i][0]-1].value, edges[i][2]);
            }
            for(int j = 0; j < queriesCount; j++)
            {
                for(int i = 0; i < nodeCount; i++){
                    Nodes[i].distance = double.PositiveInfinity;
                    ReversedG[i].distance = double.PositiveInfinity;
                    Nodes[i].visited = false;
                    ReversedG[i].visited_r = false;
                }
                long ans = BidirectionalDijkstra(Nodes, ReversedG,
                queries[j][0], queries[j][1], nodeCount);
                if(ans < 0) { result[j] = -1; }
                else { result[j] = ans; }
            }
            return result;
        }
    }
}
