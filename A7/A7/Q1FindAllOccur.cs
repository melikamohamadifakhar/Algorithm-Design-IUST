using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q1FindAllOccur : Processor
    {
        public Q1FindAllOccur(string testDataName) : base(testDataName)
        {
			this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String, long[]>)Solve, "\n");

        public int[] ComputePrefixFunction(string P)
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
        protected virtual long[] Solve(string text, string pattern)
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
            if (ans.Count == 0) { ans.Add(-1); }
            return ans.ToArray();
        }
    }
}
