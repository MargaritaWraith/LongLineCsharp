using System;
using System.Numerics;
using static System.Math;

namespace LongLine
{
    /// <summary>
    /// Основной вычислитель
    /// </summary>
    public class LLine
    {        
        // public double f0;
        public double lmd;
        public double W; //волновое сопротивление
        public double Rn;
        public double Xn;
        public Complex Zn;
                
        public LLine()
        {
            Complex Zn = new Complex(Rn, Xn);
        }

        public (double[] z, Complex[] U, Complex[] I) Solve(double Un, double l)
        {
            const int N = 1000; //размер массива
            double dz = l / (N-1);

            var z = new double[N];
            var Uz = new Complex[N];
            var Iz = new Complex[N];

            for (var i=0; i<N; i++)
            {
                z[i] = i * dz;
                Uz[i] = Un * Cos(2 * PI * z[i] / lmd) + W * (Un/Zn) * Sin(2 * PI * z[i] / lmd) * Complex.ImaginaryOne;
                Iz[i] = (Un/Zn) * Cos(2 * PI * z[i] / lmd) + (Un/W) * Sin(2 * PI * z[i] / lmd) * Complex.ImaginaryOne;
            }

            return (z, Uz, Iz);
        }



    }
}