using System;
using System.Collections.Generic;
namespace Week4
{
    class Program
    {
        static int[] ComputePrefixFunction(string P)
        {
            int[] s = new int[P.Length];
            int border = 0; s[0] = 0;
            for (int i = 1; i < P.Length; i++)
            {
                while ((border > 0) && (P[i] != P[border]))
                {
                    border = s[border - 1];
                }
                if (P[i] == P[border])
                {
                    border += 1;
                }
                else
                    border = 0;
                s[i] = border;
            }
            return s;
        }
        static void Solve(string text, string pattern)
        {
            var pLen = pattern.Length;
            List<long> ans = new List<long>();
            string S = pattern + "$" + text;
            var pFunc = ComputePrefixFunction(S);
            for (int i = pLen+1; i < S.Length; i++)
            {
                if (pFunc[i] == pLen)
                {
                    ans.Add(i - 2*pLen);
                }
            }
            foreach (var pos in ans)
                Console.Write(pos + " ");
        }
        // static void Main(string[] args)
        // {
        //     string ptrn = Console.ReadLine();
        //     string txt = Console.ReadLine();
        //     Solve(txt, ptrn);
        // }
    }
}
