using System;
using System.Collections.Generic;
using TestCommon;

namespace A1
{
    public class Node
    {
        public Node(long v) { value = v; }
        public long value;
        public List<Node> adjacents = new List<Node>();
        public bool visited = false;
        public int CCnum = 0;
        public bool IsInStack = false;
        public long preVisited =0;
        public long postVisited =0;
        public long IncomingEdge =0;

    }
    public class Q1MazeExit : Processor
    {
        public Q1MazeExit(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

        public void Explore(Node v)
        {
            v.visited = true;
            foreach(var adj in v.adjacents)
                if(!adj.visited)
                    Explore(adj);
        }
        public static void DFS(Node startNode){
            Stack<Node> stack = new Stack<Node>();
            startNode.visited = true;
            stack.Push(startNode);
            while (stack.Count != 0)
            {
                Node poped = stack.Pop();
                for(int i = 0; i < poped.adjacents.Count; i++)
                {
                    if(!poped.adjacents[i].visited)
                    {
                        poped.adjacents[i].visited = true;
                        stack.Push(poped.adjacents[i]);
                    }
                }
            }
        }
        public long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
            List<Node> Nodes = new List<Node>();
            for(int i = 1; i <= nodeCount; i++){
                Nodes.Add(new Node(i));
            }
            for(int i = 0; i < edges.Length; i++){
                Nodes[(int)edges[i][0]-1].adjacents.Add(Nodes[(int)edges[i][1]-1]);
                Nodes[(int)edges[i][1]-1].adjacents.Add(Nodes[(int)edges[i][0]-1]);
            }
            // Explore(Nodes[(int)StartNode-1]); it works correct too
            DFS(Nodes[(int)StartNode-1]);
            if(Nodes[(int)EndNode - 1].visited)
                return 1;
            return 0;
        }
    }
}
