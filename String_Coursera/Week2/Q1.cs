// using System;
// using System.Collections.Generic;

// namespace Week2
// {
//     class Program
//     {
//         public static void Solve(string text)
//         {
//             List<string> Strings = new List<string>();
//             for(int i = 0; i < text.Length; i++)
//             {
//                 Strings.Add(text);
//                 text = text.Insert(0, text[text.Length - 1].ToString());
//                 text = text.Remove(text.Length - 1);
//             }
//             Strings.Sort();
//             string BWT = "";
//             foreach (var s in Strings)
//             {
//                 BWT += s[text.Length - 1];
//             }
//             System.Console.WriteLine(BWT);
//         }
//         // static void Main(string[] args)
//         // {
//         //     var s = Console.ReadLine();
//         //     Solve(s);
//         // }
//     }
// }
