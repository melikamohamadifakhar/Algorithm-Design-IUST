using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q5StronglyConnected: Processor
    {
        public Q5StronglyConnected(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public static Stack<Node> stack = new Stack<Node>();
        public static bool push = true; 
        public static long SccNum = 0;
        public static void Explore(Node v)
        {
            v.visited = true;
            foreach(var adj in v.adjacents)
            {
                if(!adj.visited){
                    Explore(adj);
                }
            }
            if(push)
                stack.Push(v);
        }

        public long Solve(long nodeCount, long[][] edges)
        {
            SccNum = 0; push = true;
            List<Node> Nodes = new List<Node>();
            List<Node> ReversedG = new List<Node>();
            for(int i = 1; i <= nodeCount; i++){
                Nodes.Add(new Node(i));
                ReversedG.Add(new Node(i));
            }
            for(int i = 0; i < edges.Length; i++){
                Nodes[(int)edges[i][0]-1].adjacents.Add(Nodes[(int)edges[i][1]-1]);
                ReversedG[(int)edges[i][1]-1].adjacents.Add(ReversedG[(int)edges[i][0]-1]);
            }
            for(int i = 0; i < nodeCount; i++){
                if(!Nodes[i].visited)
                    Explore(Nodes[i]);
            }
            push = false;
            while(stack.Count > 0){
                var poped = stack.Pop().value;
                if(!ReversedG[(int)poped-1].visited){
                    Explore(ReversedG[(int)poped-1]);
                    SccNum ++;
                }
            }
            return SccNum;
        }
    }
}
