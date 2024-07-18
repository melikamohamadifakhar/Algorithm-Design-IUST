using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q2AddExitToMaze : Processor
    {
        public Q2AddExitToMaze(string testDataName) : base(testDataName) {}

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public void Explore(Node v, int cc)
        {
            v.visited = true;
            v.CCnum = cc;
            foreach(var adj in v.adjacents)
                if(!adj.visited)
                    Explore(adj, cc);
        }
        public void DFS(List<Node> Nodes) // it doesn't pass 5 last tests bcuz of overflow exception
                                        // so i used BFS instead
        {
            int cc = 1;
            foreach(var node in Nodes)
                if(!node.visited)
                {
                    Explore(node, cc);
                    cc++;
                }
        }
        public void BFS(List<Node> Nodes)
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

        public long Solve(long nodeCount, long[][] edges)
        {
            List<Node> Nodes = new List<Node>();
            for(int i = 1; i <= nodeCount; i++){
                Nodes.Add(new Node(i));
            }
            for(int i = 0; i < edges.Length; i++){
                Nodes[(int)edges[i][0]-1].adjacents.Add(Nodes[(int)edges[i][1]-1]);
                Nodes[(int)edges[i][1]-1].adjacents.Add(Nodes[(int)edges[i][0]-1]);
            }
            BFS(Nodes);
            long con_comp = 0;
            for(int i = 0; i < nodeCount; i++){
                if(Nodes[i].CCnum > con_comp)
                    con_comp = Nodes[i].CCnum;
            }
            return con_comp;
        }
    }
}
