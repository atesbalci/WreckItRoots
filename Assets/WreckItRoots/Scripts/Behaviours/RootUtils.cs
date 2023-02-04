using UnityEngine;

namespace WreckItRoots.Behaviours
{
    public static class RootUtils
    {
        public static Vector3 ToRootAngularDirection(this float angle)
        {
            var angleRad = angle * Mathf.Deg2Rad;
            return new Vector3(Mathf.Sin(angleRad), -Mathf.Cos(angleRad), 0f);
        }
    }
}