using System.Collections.Generic;
using UnityEngine;

namespace Cashing
{
    internal static class YieldInstruction
    {
        class FloatCompare : IEqualityComparer<float>
        {
            bool IEqualityComparer<float>.Equals(float x, float y)
            {
                return x == y;
            }
            int IEqualityComparer<float>.GetHashCode(float obj)
            {
                return obj.GetHashCode();
            }
        }

        private static readonly Dictionary<float, WaitForSeconds> waitForSec = new Dictionary<float, WaitForSeconds>(new FloatCompare());
        public static WaitForSeconds WaitForSeconds(float seconds)
        {
            WaitForSeconds w;

            if (!waitForSec.TryGetValue(seconds, out w))
            {
                waitForSec.Add(seconds, w = new WaitForSeconds(seconds));
            }

            return w;
        }
    }
}
