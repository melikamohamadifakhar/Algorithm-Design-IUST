using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q2CunstructSuffixArray : Processor
    {
        public Q2CunstructSuffixArray(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, long[]>)Solve);
        public long[] order(string Str)
        {
            var order = new long[Str.Length];
            var count = countDifferChar(Str);
            for (int i = 1; i < count.Count; i++)
            {
                count[count.ElementAt(i).Key] += count[count.ElementAt(i-1).Key];
            }
            for (int i = Str.Length - 1; i >= 0; i--)
            {
                char c = Str[i];
                count[c] -= 1;
                order[count[c]] = i;
            }
            return order;
        }

        public long[] ComputeCharClasses(string S, long[] order)
        {
            var _class = new long[S.Length];
            _class[order[0]] = 0;
            for (int i = 1; i < S.Length; i++)
            {
                if(S[(int)order[i]] != S[(int)order[i - 1]])
                    _class[order[i]] = _class[order[i-1]] + 1;
                else
                    _class[order[i]] = _class[order[i-1]];
            }
            return _class;
        }
        public long[] SortDoubled(string S, long L, long[] order, long[] classes)
        {
            var count = new long[S.Length];
            var newOrder = new long[S.Length];
            for (int i = 0; i < S.Length; i++)
                count[classes[i]] = count[classes[i]] + 1;
            for (int i = 1; i < S.Length; i++)
                count[i] = count[i] + count[i-1];
            for (int i = S.Length - 1; i >= 0; i--)
            {
                var start = (order[i] - L + S.Length) % S.Length;
                var cl = classes[start];
                count[cl] -= 1;
                newOrder[count[cl]] = start;
            }
            return newOrder;
        }
        public long[] UpdateClasses(long[] newOrder, long[] classes, long L)
        {
            var n = newOrder.Length;
            var newClass = new long[n];
            newClass[newOrder[0]] = 0;
            for (int i = 1; i < n; i++)
            {
                var cur = newOrder[i];
                var prev = newOrder[i - 1];
                var mid = (cur + L) % n;
                var midPrev = (prev + L) % n;
                if( (classes[cur] != classes[prev]) || (classes[mid] != classes[midPrev]) )
                    newClass[cur] = newClass[prev] + 1;
                else
                    newClass[cur] = newClass[prev];
            }
            return newClass;
        }

        public SortedDictionary<char, long> countDifferChar(string str)
        {
            SortedDictionary<char, long> d = new SortedDictionary<char, long>();
            foreach (var s in str)
            {
                if (!d.ContainsKey(s))
                {
                    d.Add(s, 1);
                }
                else
                {
                    d[s] += 1;
                }
            }
            return d;
        }

        protected virtual long[] Solve(string text)
        {
            var ord = order(text);
            var classes = ComputeCharClasses(text, ord);
            long L = 1;
            while (L < text.Length)
            {
                ord = SortDoubled(text, L, ord, classes);
                classes = UpdateClasses(ord, classes, L);
                L *= 2;
            }
            return ord;
        }
    }
}
