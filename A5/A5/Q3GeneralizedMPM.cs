using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q3GeneralizedMPM : Processor
    {
        public Q3GeneralizedMPM(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);
        public Node trie(long n, string[] patterns)
        {
            int tag = 0;
            Node root = new Node(tag);
            tag ++;
            for (int i = 0; i < n; i++)
            {
                Node current_node = root;
                for(int j = 0; j < patterns[i].Length; j++)
                {
                    if(current_node.has_child(patterns[i][j]))
                    {
                        current_node = current_node.children[patterns[i][j]];
                    }
                    else
                    {
                        Node child_node = new Node(tag);
                        tag++;
                        current_node.children.Add(patterns[i][j], child_node);
                        current_node = child_node;
                    }
                }
                current_node.is_end = true;
            }
            return root;
        }
        public long[] Solve(string text, long n, string[] patterns)
        {
            Node root = trie(n, patterns);
            List<long> found = new List<long>();
            for (int i = 0; i < text.Length; i++)
            {
                Node current_node = root;
                int j = i;
                while (j < text.Length)
                {
                    if(current_node.children.ContainsKey(text[j]))
                    {
                        current_node = current_node.children[text[j]];
                        j++;
                    }
                    else
                    {
                        break;
                    }
                    if(current_node.is_end) { found.Add(i); }
                }
            }
            
            if(found.Count == 0) {found.Add(-1);}
            return found.Distinct().ToArray();
        }
    }
}
