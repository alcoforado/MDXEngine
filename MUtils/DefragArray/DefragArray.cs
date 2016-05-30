using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUtils.DefragArray
{
    public static class DefragArray
    {
        private static List<CopyPlan> OptimizePlan(List<CopyPlan> plan)
        {
            plan.Sort((a, b) => a.Orig.offI - b.Orig.offI);
            var optimumPlan = new List<CopyPlan>();
            if (plan.Count > 2)
            {
                var mergedSegment = plan[0];
                for (int i = 1; i < plan.Count; i++)
                {
                    if (!mergedSegment.TryRightMerge(plan[i]))
                    {
                        optimumPlan.Add(mergedSegment);
                        mergedSegment = plan[i];
                    }
                }
            }
            else
            {
                optimumPlan.Add(plan.First());
            }
            return optimumPlan;
        }

        public static void CopyArraySegments<T>(T[] src, T[] dst, List<CopyPlan> plan)
        {
            var optPlan = OptimizePlan(plan);
            foreach (var cp in optPlan)
            {
                System.Diagnostics.Debug.Assert(cp.Dst.size >= cp.Orig.size);
                System.Diagnostics.Debug.Assert(cp.Orig.offI + cp.Orig.size <= src.Length);
                System.Diagnostics.Debug.Assert(cp.Dst.offI + cp.Dst.size <= dst.Length);

                Array.Copy(src,cp.Orig.offI,dst,cp.Dst.offI,cp.Orig.size);
            }
        }



    }
}
