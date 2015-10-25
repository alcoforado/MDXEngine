using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.MMath
{
    public class Interval
    {
        double _a;
        double _b;
        public double a { get { return _a; } }
        public double b { get{ return _b; } }
        private static Random rand1 = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);


        public Interval(double a, double b)
        {
            this._a = Math.Min(a, b);
            this._b = Math.Max(a, b);

        }

        public int RandomInt()
        {

            var f = rand1.NextDouble();
            return (int) Math.Round((f * (b - a) + a));
        }




        internal double RandomDouble()
        {
            var f = rand1.NextDouble();
            return ((f * (b - a) + a));
        }
    }
}
