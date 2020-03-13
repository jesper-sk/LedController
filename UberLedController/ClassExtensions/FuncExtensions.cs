using System;


namespace UberLedController.ClassExtensions
{
    public static class FuncExtensions
    {
        public static T2[] ZipOver<T1, T2>(
            this Func<T1[], T2> func, 
            params T1[][] input)
        {
            T2[] res = new T2[input[0].Length];
            for(int i = 0; i < input[0].Length; i++)
            {
                T1[] curr = new T1[input.Length];
                for(int j = 0; j < input.Length; j++)
                {
                    curr[j] = input[j][i];
                }
                res[i] = func(curr);
            }
            return res;
        }
    }
}
