// using System;
// using System.Collections.Generic;
// using System.Linq;

// namespace Week2_1
// {
//     class Program
//     {
//         static void Solve(string text)
//         {

//             SortedDictionary<string, long> dictionary = new SortedDictionary<string, long>();
//             int len = text.Length;
//             for(int i = 0; i < len; i++)
//             {
//                 dictionary.Add(text.Substring(i), i);
//             }
//             foreach (var key in dictionary.Keys)
//             {
//                 System.Console.Write(dictionary[key] + " "); 
//             }
//         }
//         static void Main(string[] args)
//         {
//             var s = Console.ReadLine();
//             Solve(s);
//         }
//     }
// }
