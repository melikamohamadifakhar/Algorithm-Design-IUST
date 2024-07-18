using System;
using System.Collections.Generic;
using System.Linq;

namespace Week1_2
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
    class Program1
    {
        public static Node trie(long n, string[] patterns)
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
        public static long[] Solve(string text, long n, string[] patterns)
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
            
            return found.Distinct().ToArray();
        }
        static void Main(string[] args)
        {
            string text = Console.ReadLine();
            long n = Int64.Parse(Console.ReadLine());
            string[] patterns = new string[n];
            for (int i = 0; i < n; i++)
            {
                patterns[i] = Console.ReadLine();
            }
            var result = Solve(text, n, patterns);
            string s = "";
            foreach (var r in result)
            {
                s += r.ToString();
                s += " ";
            }
            System.Console.WriteLine(s.TrimEnd());
        }
    }
}
