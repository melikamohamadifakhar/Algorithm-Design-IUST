// using System;
// using System.Collections.Generic;
// using System.Linq;

// namespace Week2_
// {
//     class Program
//     {
//         static string bwtInverseTransform(Dictionary<string, string> LastToFirst)
//         {
//             string k = "$0";
//             string result = "";
//             for(int i = 0; i < LastToFirst.Count; i++)
//             {
//                 result = result.Insert(0, k.ElementAt(0).ToString());
//                 k = LastToFirst[k];
//             }
//             return result;
//         }
//         static string Solve(string bwt)
//         {
//             Dictionary<char, int> dict_l = new Dictionary<char, int>();
//             Dictionary<char, int> dict_f = new Dictionary<char, int>();
//             Dictionary<string, string> LastToFirst = new Dictionary<string, string>();
//             var chars = new List<char>();
//             chars.Add('A'); chars.Add('T'); chars.Add('C'); chars.Add('G'); chars.Add('$');
//             foreach (char c in chars)
//             {
//                 dict_l.Add(c, 0);
//                 dict_f.Add(c, 0);
//             }
//             var firstColoumn = String.Concat(bwt.OrderBy(c => c));
//             int l = bwt.Length;
//             for(int i = 0; i < l; i++)
//             {
//                 bwt = bwt.Insert(i+1, dict_l[bwt[i]].ToString());
//                 var len = dict_l[bwt[i]].ToString().Length;
//                 LastToFirst.Add(bwt.Substring(i, len+1), "");
//                 dict_l[bwt[i]]++;
//                 l+=len;
//                 i+=len;
//             }
//             l = firstColoumn.Length;
//             for(int i = 0; i < l; i++)
//             {
//                 firstColoumn = firstColoumn.Insert(i+1,dict_f[firstColoumn[i]].ToString());
//                 var len = dict_f[firstColoumn[i]].ToString().Length;
//                 LastToFirst[firstColoumn.Substring(i, len+1)] = bwt.Substring(i, 2);
//                 dict_f[firstColoumn[i]]++;
//                 l+=len;
//                 i+=len;
//             }
//             return bwtInverseTransform(LastToFirst);
//         }
//         static void Main(string[] args)
//         {
//             var s = Console.ReadLine();
//             System.Console.WriteLine(Solve(s));
//         }
//     }
// }
