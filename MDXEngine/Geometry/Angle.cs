using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.Geometry
{
    //An angle between Vectors.
    //The angle is stored not as  a number but as its sin and cos.
    //
    public struct Angle
    {

        public readonly static double PI = System.Math.PI;
        public readonly static double PI_2 = System.Math.PI / 2.0;
        public readonly static double PI_4 = System.Math.PI / 4.0;
        public readonly static double PI_3 = System.Math.PI / 3.0;
        public readonly static double PI_6 = System.Math.PI / 6.0;

        double _sin;
        double _cos;


        public double Sin { get { return _sin; } }
        public double Cos { get { return _cos; } }

        /// <summary>
        /// Construct an angle from rad
        /// </summary>
        public Angle(double rad)
        {
            _sin = System.Math.Sin(rad);
            _cos = System.Math.Cos(rad);
        }


        /// <summary>
        /// Be carefull, this method uses acos and asin.
        /// It is expensive
        /// </summary>
        public double GetRad()
        {
            var rad = System.Math.Asin(_sin);
            if (_cos < 0)
            {
                rad=System.Math.PI - rad;
            }
            if (rad < 0)
            {
                rad = 2 * System.Math.PI + rad;
            }
            return rad;
        }
        
        //Set an angle given a 2D coordinate
        public Angle(double x1,double x2)
        {
            var hyp = System.Math.Sqrt(x1 * x1 + x2 * x2);
            _cos = x1 / hyp;
            _sin = x2 /hyp; 
        }

        public Angle  fAdd90()
        {
            var aux = _sin;
            _sin = _cos;
            _cos = -aux;
            return this;
        }

        public Angle Add90()
        {
            return new Angle(-_sin, _cos);
        }

        public Angle Add(Angle angle)
        {
            return new Angle(
                _cos * angle._cos - angle._sin * _sin,
                _sin * angle._cos + angle._sin * _cos
                );
        }

    }
}
