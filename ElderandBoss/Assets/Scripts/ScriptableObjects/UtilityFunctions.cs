using UnityEngine;

namespace Elderand.ScriptableObjects
{
    /// <summary>
    /// This class makes all the math related to movement.
    /// </summary>
    public static class UtilityFunctions
    {
        public static Vector3 GetNextPosition(BezierCurve currentCurve, float transition)
        {
            Vector3 nextPosition;
            nextPosition = CalculateCubicBezierPoint(transition,
                currentCurve.startPosition,
                currentCurve.startTangent,
                currentCurve.endTangent,
                currentCurve.endPosition);
            return nextPosition;
        }

        public static Vector2 CalculateCubicBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            float u = 1 - t;

            float tt = t * t;
            float uu = u * u;
            float ttt = tt * t;
            float uuu = uu * u;
            //Calculo = (1 - 3)³ P0 + 3(1 - t)² tP1    + 3(1 - t) t² P2    + t³ P3
            Vector2 p = (uuu * p0) + (3 * uu * t * p1) + (3 * u * tt * p2) + (ttt * p3);
            return p;
        }
    }


}