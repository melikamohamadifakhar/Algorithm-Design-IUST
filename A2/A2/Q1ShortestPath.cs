using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A2
{
    public class Node
    {
        public Node(long v, long init_dist = 0, char c = 'w')
            { value = v; distance = init_dist; color = c; }
        public long value;
        public List<Node> adjacents = new List<Node>();
        public bool visited = false;
        public long distance;
        public char color;

    }
    public class Q1ShortestPath : Processor
    {
        public Q1ShortestPath(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long,long[][], long, long, long>)Solve);
        
        public void ShortestPath(List<Node> nodes, Node startNode){
            Queue<Node> Q = new Queue<Node>();
            startNode.distance = 0; startNode.visited = true;
            Q.Enqueue(startNode);
            while(Q.Count != 0)
            {
                Node Dequeued = Q.Dequeue();
                foreach(Node adj in Dequeued.adjacents)
                {
                    if (!adj.visited)
                    {
                        adj.visited = true;
                        Q.Enqueue(adj);
                        adj.distance = Dequeued.distance + 1;
                    }
                }
            }
        }
        public long Solve(long NodeCount, long[][] edges, 
                          long StartNode,  long EndNode)
        {
            List<Node> Nodes = new List<Node>();
            for(int i = 1; i <= NodeCount; i++){
                Nodes.Add(new Node(i, NodeCount));
            }
            for(int i = 0; i < edges.Length; i++){
                Nodes[(int)edges[i][0]-1].adjacents.Add(Nodes[(int)edges[i][1]-1]);
                Nodes[(int)edges[i][1]-1].adjacents.Add(Nodes[(int)edges[i][0]-1]);
            }
            ShortestPath(Nodes, Nodes[(int) StartNode - 1]);
            if(!Nodes[(int) EndNode - 1].visited)
                return -1;
            return Nodes[(int) EndNode - 1].distance;
        }
    }
}
