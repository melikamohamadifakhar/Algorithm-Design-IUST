using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A2
{
    public class Q2BipartiteGraph : Processor
    {
        public Q2BipartiteGraph(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public bool BipartiteGraphOrNot(List<Node> nodes){
            Queue<Node> queue = new Queue<Node>();
            for(int i = 0; i < nodes.Count; i++)
            {
                if(!nodes[i].visited)
                {
                    queue.Enqueue(nodes[i]);
                    while(queue.Count != 0)
                    {
                        Node Dequeued = queue.Dequeue();
                        foreach(Node adj in Dequeued.adjacents)
                        {
                            if(adj.visited){
                                if(adj.color == Dequeued.color)
                                    return false;
                            }
                            else{
                                adj.visited = true;
                                if(Dequeued.color == 'r') {adj.color = 'b';}
                                else {adj.color = 'r';}
                                queue.Enqueue(adj);
                            }
                        }
                    }
                }
            }
            return true;
        }
        public long Solve(long NodeCount, long[][] edges)
        {
            List<Node> Nodes = new List<Node>();
            for(int i = 1; i <= NodeCount; i++){
                Nodes.Add(new Node(i));
            }
            for(int i = 0; i < edges.Length; i++){
                Nodes[(int)edges[i][0]-1].adjacents.Add(Nodes[(int)edges[i][1]-1]);
                Nodes[(int)edges[i][1]-1].adjacents.Add(Nodes[(int)edges[i][0]-1]);
            }
            if(BipartiteGraphOrNot(Nodes)) return 1;
            return 0;
        }
    }
}
