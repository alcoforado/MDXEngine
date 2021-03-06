﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
namespace MDXEngine
{
    public class Normal3DArray<T> : IArray<Vector3> where T : INormal
    {
        SubArray<T> vSub;

        public Normal3DArray(T[] data, int start, int size)
        {
            vSub = new SubArray<T>(data,start,size);
        }




        public int Length{get {return vSub.size;}}


        public void CopyFrom(Vector3[] array)
        {
            Debug.Assert(array.Length == this.Length);
            var len = this.Length;
            for (int i = 0; i < len; i++)
            {
                this[i] = array[i];
            }
        }
        
        
        public Vector3 this[int index]
        {
            get
            {
                return vSub[index].Normal;
            }
            set
            {
                vSub[index].Normal = value;
            }
        }




    }
}
