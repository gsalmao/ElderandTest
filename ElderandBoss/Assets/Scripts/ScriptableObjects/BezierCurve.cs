using UnityEngine;
using System;

namespace Elderand.ScriptableObjects
{
    [Serializable]
    public class BezierCurve
    {
        public Vector3 startPosition;
        public Vector3 startTangent;

        public Vector3 endTangent;
        public Vector3 endPosition;

        public BezierCurve(Vector2 firstPoint)
        {
            startPosition = firstPoint;

            startTangent = startPosition;
            startTangent.x += .5f;

            endPosition = firstPoint;
            endPosition.x += 3;

            endTangent = endPosition;
            endTangent.x -= .5f;
        }

        public Vector3 GetTransitionPoint(float transition)
        {
            return UtilityFunctions.CalculateCubicBezierPoint(transition, startPosition, startTangent, endTangent, endPosition);
        }

        /// <summary>
        /// Return the total size of the bezier curve.
        /// </summary>
        /// <remarks>The resolution defines how many points there will be. The bigger the resolution,
        /// the closer it gets from the real length.</remarks>
        /// <param name="resolution">The amount of points in the length calculation.</param>
        public float CalculateCurveLength(int resolution)
        {
            float transitionSize = 1f / resolution;
            float length = 0;
            Vector3 previousPosition = startPosition;

            for (int i = 1; i <= resolution; i++)
            {
                float transition = i * transitionSize;
                Vector3 newPosition = UtilityFunctions.CalculateCubicBezierPoint(transition, startPosition, startTangent, endTangent, endPosition);
                length += Vector3.Distance(previousPosition, newPosition);
                previousPosition = newPosition;
            }

            return length;
        }

    }

}