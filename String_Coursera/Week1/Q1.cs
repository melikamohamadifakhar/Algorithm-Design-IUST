using System;
using System.Collections.Generic;

namespace Week1
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
    class Program
    {
        static void Main(string[] args)
        {
            long n = Int64.Parse(Console.ReadLine());
            string[] patterns = new string[n];
            for (int i = 0; i < n; i++)
            {
                patterns[i] = Console.ReadLine();
            }
            var result = Solve(n, patterns);
            foreach (var r in result)
            {
                Console.WriteLine(r);
            }
        }
        public static List<string> Solve(long n, string[] patterns)
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
                        string s = current_node.tag.ToString() + "->" + child_node.tag.ToString()
                        + ":" + label.ToString();
                        result.Add(s);
                        current_node = child_node;
                    }
                }
            }
            return result;
        }
    }
}
