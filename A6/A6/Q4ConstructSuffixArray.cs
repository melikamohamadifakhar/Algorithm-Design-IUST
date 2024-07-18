using System;
using System.Collections.Generic;
using TestCommon;

namespace A6
{
    public class Q4ConstructSuffixArray : Processor
    {
        public Q4ConstructSuffixArray(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        /// <summary>
        /// Construct the suffix array of a string
        /// </summary>
        /// <param name="text"> A string Text ending with a “$” symbol </param>
        /// <returns> SuffixArray(Text), that is, the list of starting positions
        /// (0-based) of sorted suffixes separated by spaces </returns>
        public long[] Solve(string text)
        {
            long[] result = new long[text.Length];
            SortedDictionary<string, long> dictionary = new SortedDictionary<string, long>();
            int len = text.Length;
            for(int i = 0; i < len; i++)
            {
                dictionary.Add(text.Substring(i, len-i), i);
            }
            int j = 0;
            foreach (var key in dictionary.Keys)
            {
                result[j] = dictionary[key];
                j++;
            }
            return result;
        }
    }
}
