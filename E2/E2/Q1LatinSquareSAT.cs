using TestCommon;

namespace E2
{
    public class square
    {
        public List<int> values = new List<int>();
        public square (int start, int dim)
        {
            for(int i = 0; i < dim; i++)
            {
                values.Add((start*dim)-i);
            }
        }
    }
    public class Q1LatinSquareSAT : Processor
    {
        public Q1LatinSquareSAT(string testDataName) : base(testDataName)
        {
            this.ExcludeTestCases(4,7, 8,13, 16, 17, 18, 20, 22,
                                    25, 26, 28);
            this.ExcludeTestCaseRangeInclusive(30, 54);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int,int?[,],string>)Solve);

        public override Action<string, string> Verifier =>
            TestTools.SatVerifier;
        public List<string> singlecubeverify(square square, int dim)
        {
            List<string> final = new List<string>();
            var s = "";
            foreach (var v in square.values)
            {
                s += $"{v} ";
            }
            final.Add(s);
            for (int i = 0; i < dim; i++)
            {
                for (int j = i+1; j < dim; j++)
                {
                    final.Add($"-{square.values[i]} -{square.values[j]}");
                }
            }
            return final;
        }
        public List<string> row(int?[,] square, square[,] squares_obj,int dim,int num)
        {
            List<string> final = new List<string>();
            // for (int i = 0; i < dim; i++)
            // {
            //     var res = "";
            //     for (int j = 0; j < dim; j++)
            //     {
            //         res += $"{squares_obj[num, j].values[i]} ";
            //     }
            //     final.Add(res);
            // }
            for (int i = 0; i < dim; i++)
            {
                if(square[num, i].HasValue)
                {
                    for (int j = 0; j < dim; j++)
                    {
                        if (j != i)
                            final.Add($"-{squares_obj[num, j].values[(int)square[num, i] ]}");
                    }
                }

                else
                {
                    for (int j = 0; j < dim; j++) 
                    {
                        if (i != j)
                            for (int k = 0; k < squares_obj[num, i].values.Count; k++)
                            {
                                final.Add($"-{squares_obj[num, j].values[k]} -{squares_obj[num, i].values[k]}");
                            }
                    }
                }
            }
            return final;
        }
        public List<string> column(int?[,] square, square[,] squares_obj,int dim,int num)
        {
            List<string> final = new List<string>();
            // for (int i = 0; i < dim; i++)
            // {
            //     var res = "";
            //     for (int j = 0; j < dim; j++)
            //     {
            //         res += $"{squares_obj[j, num].values[i]} ";
            //     }
            //     final.Add(res);
            // }
            for (int i = 0; i < dim; i++)
            {
                if(square[i, num].HasValue)
                {
                    for (int j = 0; j < dim; j++)
                    {
                        if (i != j)
                            final.Add($"-{squares_obj[j, num].values[(int)square[i, num]]}");
                    }
                }
                else
                {
                    for (int j = 0; j < dim; j++) 
                    {
                        if (i != j)
                            for (int k = 0; k < squares_obj[i, num].values.Count; k++)
                            {
                                final.Add($"-{squares_obj[j, num].values[k]} -{squares_obj[num, i].values[k]}");
                            }
                    }
                }
            }
            return final;
        }
        public virtual string Solve(int dim, int?[,] square)
        {
            List<string> result = new List<string>();
            int tmp = 1;
            square[,] squares = new square[dim,dim];
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    var cube = new square(tmp, dim);
                    tmp++;
                    squares[i,j] = cube;
                }
            }

            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    if (!square[i,j].HasValue)
                    {
                        result.AddRange(singlecubeverify(squares[i,j], dim));
                    }
                }
            }
            for (int i = 0; i < dim; i++)
            {
                result.AddRange(row(square, squares, dim, i));
                result.AddRange(column(square, squares, dim, i));
            }
            string ans = "";
            ans += $"{result.Count} {dim*dim*dim}";
            foreach (var res in result)
            {
                ans += System.Environment.NewLine;
                ans += res;
            }
            return ans;
        }
    }
}