using System;
namespace HOO.ComLib
{
    public class MrMath
    {
        public static Int64 HighPow(Int64 a, Int64 b)
        {
            Int64 res = 1;
            Int64 pow = a;
            if ((b & 1) == 1)
            {
                res = res*pow;
                b = b >> 1;
            }

            while (b>0)
            {
                pow = pow*pow;
                if ((b & 1) == 1)
                {
                    res = res*pow;
                }
                b = b >> 1;
            }
            return res;
        }
    }
}
