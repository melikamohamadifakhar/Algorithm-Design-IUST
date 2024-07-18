using System;
using System.Collections.Generic;
using TestCommon;

namespace A1
{
    public class Graph
    {
        List<Node> Nodes = new List<Node>();
    }
    public class Q3Acyclic : Processor
    {
        public Q3Acyclic(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);
        
        public static bool cycle(Node node)
        {
            node.visited = true; node.IsInStack = true;
            foreach (var adj in node.adjacents)
            {
                if (adj.IsInStack)
                    return true;
                else if (! adj.visited)
                {
                    if(cycle(adj))
                        return true;
                }
            }
            node.IsInStack = false;
            return false;
        }
        public long Solve(long nodeCount, long[][] edges)
        {
            List<Node> Nodes = new List<Node>();
            for(int i = 1; i <= nodeCount; i++){
                Nodes.Add(new Node(i));
            }
            for(int i = 0; i < edges.Length; i++){
                Nodes[(int)edges[i][0]-1].adjacents.Add(Nodes[(int)edges[i][1]-1]);
            }
            for(int i = 0; i < Nodes.Count; i++){
                if(!Nodes[i].visited)
                    if(cycle(Nodes[i]))
                        return 1;
            }
            return 0;
        }
    }
}