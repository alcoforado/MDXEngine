using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDXEngine.MMath
{
    /// <summary>
    /// Implements a hiearchal score. This is a list of integers where you can say if a list is 
    /// less or greater than other. The list represents a kink of quantity where the first element has one
    /// order more than the next element (integer). In this way the element (1,9) > (0,9) > 0,8 > 0,7 and so on.
    /// </summary>
    public class HScore
    {
        private List<long> _lst;

        public HScore()
        {
            _lst = new List<long>();
        }

        public long this[int i] { get { return _lst[i]; } set { _lst[i] = value; } }

        public HScore AppendScore(long score)
        {
            this._lst.Add(score);
            return this;
        }

        public HScore(long[] numbers)
        {
            _lst = numbers.ToList();
        }

        public static bool operator <(HScore a,HScore b)
        {
            return !(a >= b);
        }

        public static bool operator <=(HScore a, HScore b)
        {
            if (a._lst.Count != b._lst.Count)
                throw new Exception("Can't compare scores, they have different length");
            else
            {
                for (int i = 0; i < a._lst.Count; i++)
                {
                    if (a[i] < b[i])
                        return true;
                    else if (a[i] > b[i])
                        return false;
                }
                return true;
            }
        }

        public static bool operator >=(HScore a, HScore b)
        {
            if (a._lst.Count != b._lst.Count)
                throw new Exception("Can't compare scores, they have different length");
            else
            {
                for (int i = 0; i < a._lst.Count; i++)
                {
                    if (a[i] > b[i])
                        return true;
                    else if (a[i] < b[i])
                        return false;
                }
                return true;
            }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return (int)this.Sum();
        }

        public long Sum()
        {
            long acum = 0;
            for (int i=0;i<_lst.Count;i++)
            {
                acum+=_lst[i];
            }
            return acum;
        }

        public static bool operator ==(HScore a,HScore b)
        {
            if (a._lst.Count != b._lst.Count)
                throw new Exception("Can't compare scores, they have different length");
            else
            {
                for (int i = 0; i < a._lst.Count; i++)
                {
                    if (a[i] !=  b[i])
                        return false;
                }
                return true;
            }
        }

        public static bool operator !=(HScore a, HScore b)
        {
            return !(a == b);
        }


        public static bool operator > (HScore a, HScore b)
        {
            return !(a <= b);
        }


    }
}
