using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elderand.ScriptableObjects
{
    /// <summary>
    /// This scriptable object stores the enemy's path.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "BezierPath", menuName = "Elderand/Bezier Path", order = 0)]
    public class BezierPath : ScriptableObject
    {
        public BezierPath()
        {
            curves = new List<BezierCurve>();
            curves.Add(new BezierCurve(Vector3.zero));
        }

        public List<BezierCurve> curves;
        public Vector3 GetInitialPosition() {
            return curves[0].startPosition;
        }

        public Vector3 GetLastPosition()
        {
            return curves.Last().endPosition;
        }
    }

    public class PathFraction
    {
        public List<BezierCurve> curves;
    }
}
