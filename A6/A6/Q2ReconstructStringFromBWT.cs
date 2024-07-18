using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A6
{
    public class Q2ReconstructStringFromBWT : Processor
    {
        public Q2ReconstructStringFromBWT(string testDataName) 
        : base(testDataName) {
            this.ExcludeTestCaseRangeInclusive(13, 40);
         }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String>)Solve);

        /// <summary>
        /// Reconstruct a string from its Burrows–Wheeler transform
        /// </summary>
        /// <param name="bwt"> A string Transform with a single “$” sign </param>
        /// <returns> The string Text such that BWT(Text) = Transform.
        /// (There exists a unique such string.) </returns>
        public string bwtInverseTransform(Dictionary<string, string> LastToFirst)
        {
            string k = "$0";
            string result = "";
            for(int i = 0; i < LastToFirst.Count; i++)
            {
                result = result.Insert(0, k.ElementAt(0).ToString());
                k = LastToFirst[k];
            }
            return result;
        }
        public string Solve(string bwt)
        {
            Dictionary<char, int> dict_l = new Dictionary<char, int>();
            Dictionary<char, int> dict_f = new Dictionary<char, int>();
            Dictionary<string, string> LastToFirst = new Dictionary<string, string>();
            var chars = new List<char>();
            chars.Add('A'); chars.Add('T'); chars.Add('C'); chars.Add('G'); chars.Add('$');
            foreach (char c in chars)
            {
                // if(!dict_l.ContainsKey(c))
                    dict_l.Add(c, 0);
                // if(!dict_f.ContainsKey(c))
                    dict_f.Add(c, 0);
            }
            var firstColoumn = String.Concat(bwt.OrderBy(c => c));
            int l = bwt.Length;
            for(int i = 0; i < l; i++)
            {
                bwt = bwt.Insert(i+1, dict_l[bwt[i]].ToString());
                var len = dict_l[bwt[i]].ToString().Length;
                LastToFirst.Add(bwt.Substring(i, len+1), "");
                dict_l[bwt[i]]++;
                l+=len;
                i+=len;
            }
            l = firstColoumn.Length;
            for(int i = 0; i < l; i++)
            {
                firstColoumn = firstColoumn.Insert(i+1,dict_f[firstColoumn[i]].ToString());
                var len = dict_f[firstColoumn[i]].ToString().Length;
                LastToFirst[firstColoumn.Substring(i, len+1)] = bwt.Substring(i, 2);
                dict_f[firstColoumn[i]]++;
                l+=len;
                i+=len;
            }
            return bwtInverseTransform(LastToFirst);
        }
    }
}
