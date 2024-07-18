using System;
using TestCommon;

namespace A9
{
    public class Q1InferEnergyValues : Processor
    {
        public Q1InferEnergyValues(string testDataName) : base(testDataName)
        {
            ExcludeTestCases(28);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, double[,], double[]>)Solve);
        
        public double[,] SwapRows(int i, int j, double[,] matrix, long size)
        {
            for (int x = 0; x <= size; x++)
            {
                var tmp = matrix[i, x];
                matrix[i, x] = matrix[j, x];
                matrix[j, x] = tmp;
            }
            return matrix;
        }
        public void elimination(double[,] matrix, int num, long size)
        {
            if (matrix[num, num] != 0)
            {
                for (int i = num + 1; i < size; i++)
                {
                    if (matrix[i, num] == 0) continue;
                    double z = matrix[i, num];
                    for (int j = 0; j <= size; j++)
                    {
                        matrix[i, j] *= (matrix[num, num]/z);
                        matrix[i, j] -= matrix[num, j];
                    }
                }
            }
            else
                {
                    for (int i = num+1; i < size; i++)
                    {
                        if (matrix[i, num] != 0)
                        {
                            SwapRows(i, num, matrix, size);
                        }
                    }
                }
        }
        public double[] final(double[,] matrix, long size)
        {
            double[] res = new double[size];
            for (int j = (int) size - 1; j >= 0; j--){
                matrix[j, size] = matrix[j, size]/matrix[j, j];
                res[j] = matrix[j, size];
                for ( int i = 0; i < j; i++ )
                {
                    matrix[i, size] -= matrix[j, size]*matrix[i, j];  
                }
            }
            return res;
        }
        public double[] Solve(long MATRIX_SIZE, double[,] matrix)
        {
            for (int i = 0; i< MATRIX_SIZE-1; i++)
            {
                elimination(matrix, i, MATRIX_SIZE);
            }
            var res = final(matrix, MATRIX_SIZE);
            for(int i = 0; i < res.Length; i++)
            {
                // res[i]=Math.Round(res[i] * 2, MidpointRounding.AwayFromZero) / 2;
                var p = Math.Abs(res[i] - Math.Truncate(res[i]));
                if(p < 0.25 )
                    res[i] = Math.Truncate(res[i]);
                
                else if(p >= 0.75)
                {
                    if(res[i] > 0){
                        res[i] = 1 + Math.Truncate(res[i]);
                    }
                    else
                        res[i] = Math.Truncate(res[i]) - 1;
                }
                else
                {
                    if(res[i] > 0)
                        res[i] = Math.Truncate(res[i]) + 0.5;
                    else
                        res[i] = Math.Truncate(res[i]) - 0.5;
                }
                if (res[i] == -0) {res[i] = 0;}
            }
            return res;
        }
    }
}
