﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Node
    {
        public Node(int t) { tag = t; }
        public int tag;
        public Dictionary<char, Node> children = new Dictionary<char, Node>();
        public bool has_child(char c){
            if(children.ContainsKey(c))
                return true;
            return false;
        }
        public int pat_num = -1;
        public bool is_end = false;

    }
    public class Q1ConstructTrie : Processor
    {
        public Q1ConstructTrie(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, String[], String[]>) Solve);

        public string[] Solve(long n, string[] patterns)
        {
            List<string> result = new List<string>();
            int tag = 0;
            Node root = new Node(tag);
            tag ++;
            for (int i = 0; i < n; i++)
            {
                Node current_node = root;
                foreach (var label in patterns[i])
                {
                    if(current_node.has_child(label))
                    {
                        current_node = current_node.children[label];
                    }
                    else
                    {
                        Node child_node = new Node(tag);
                        tag++;
                        current_node.children.Add(label, child_node);
                        result.Add($"{current_node.tag}->{child_node.tag}:{label}");
                        current_node = child_node;
                    }
                }
            }
            return result.ToArray();
        }
    }
}
