
// using TestCommon;

// namespace E2
// {
//     class partition
//     {
//         List<int> possibles = new List<int>();
//         public partition(int? v, int dim, int start)
//         {
//             if (v==null)
//             {
//                 for(int i = 0; i < dim; i++)
//                     possibles.Add(i);
//             }
//             else 
//             {
//                 possibles.Add((int)v);
//             }
//         }
//     }
//     public class Q1LatinSquareSAT : Processor
//     {
//         public Q1LatinSquareSAT(string testDataName) : base(testDataName)
//         {
//             // this.ExcludeTestCases(1, 2, 3);
//             // this.ExcludeTestCaseRangeInclusive(1, 3);
//         }

//         public override string Process(string inStr) =>
//             TestTools.Process(inStr, (Func<int,int?[,],string>)Solve);

//         public override Action<string, string> Verifier =>
//             TestTools.SatVerifier;
//         public int clause;
//         public string row (int num, int dim, int?[,] square)
//         {
//             Dictionary<int, bool> dict = new Dictionary<int, bool>();
//             for (int i = 0; i < dim; i++)
//             {
//                 if (!square[num, i].HasValue)
//                 {
//                     dict.Add((int)square[num, i], false);
//                 }
//                 else if (! dict.ContainsKey((int) square[num, i]))
//                 {
//                     dict.Add((int)square[num, i], true);
//                 }
//             }
//             List<int> exist = new List<int>();
//             List<int> not_exist = new List<int>();
//             foreach (var key in dict.Keys)
//             {
//                 if(dict[key]) {exist.Add(key);}
//                 else {not_exist.Add(key);}
//             }
//             string res = "";
//             string s = "";
//             clause ++;
//             for (int i = 0; i < not_exist.Count(); i++)
//             {
//                 s += $"{not_exist[i]} ";
//             }
//             if (s.Length >= 0)
//             {
//             res += s;
//             res += System.Environment.NewLine;
//             }
            
//             for (int i = 0; i < not_exist.Count(); i++)
//                 for (int j = i+1; j < not_exist.Count(); j++)
//                 {
//                     res+=$"-{not_exist[i]} -{not_exist[j]}";
//                     res += System.Environment.NewLine;
//                     clause ++;
//                 }
//             return res;
//         }
//         public string column (int num, int dim, int?[,] square)
//         {
//             Dictionary<int, bool> dict = new Dictionary<int, bool>();
//             for (int i = 0; i < dim; i++)
//             {
//                 if (!square[i, num].HasValue)
//                 {
//                     dict.Add((int)square[i, num], false);
//                 }
//                 else if (! dict.ContainsKey((int) square[i, num]))
//                 {
//                     dict.Add((int)square[i, num], true);
//                 }
//             }
//             List<int> exist = new List<int>();
//             List<int> not_exist = new List<int>();
//             foreach (var key in dict.Keys)
//             {
//                 if(dict[key]) {exist.Add(key);}
//                 else {not_exist.Add(key);}
//             }
//             string res = "";
//             string s = "";
//             for (int i = 0; i < not_exist.Count(); i++)
//             {
//                 s += $"{not_exist[i]} ";
//             }
//             if (s.Length >= 0)
//             {
//                 res += s;
//                 res += System.Environment.NewLine;
//             }
//             clause++;
//             for (int i = 0; i < not_exist.Count(); i++)
//                 for (int j = i+1; j < not_exist.Count(); j++)
//                 {
//                     res+=$"-{not_exist[i]} -{not_exist[j]}";
//                     res += System.Environment.NewLine;
//                     clause++;
//                 }
//             return res;
//         }
//         // public void idk(int?[,] square, int i, int j)
//         // {
//         //     if(!square[i,j].HasValue)
//         // }
//         public virtual string Solve(int dim, int?[,] square)
//         {
//             clause = 0;
//             string final = "";
//             List<partition> p = new List<partition>();
//             for ( int i = 0; i < dim; i++ ){
//                 for ( int j = i+1; j < square.Length; j++ )
//                 {
//                     // var new_partition = new partition(square[i,j], dim);
//                     // p.Add(new_partition);
//                 }
//             }
//             for ( int i = 0; i < dim; i++ ){
//                 for ( int j = i+1; j < square.Length; j++ )
//                 {
//                     if (!square[i,j].HasValue)
//                     {
//                         // var p = new partition(square[i,j])
//                     }
//                 }
//             }
//             int globe = 0;
//             for ( int i = 0; i<dim; i++)
//             {
//                 final += row(i, dim, square);
//                 final += column(i, dim, square);
//             }
//             final.Insert(0, $"{clause} {dim}" + System.Environment.NewLine);
//             return final;
//         }
//     }
// }
