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
            pathFractions = new List<PathFraction>();
            pathFractions.Add(new PathFraction());
            pathFractions[0].curves = new List<BezierCurve>();
            pathFractions[0].curves.Add(new BezierCurve(Vector3.zero));
        }

        public List<PathFraction> pathFractions;
        public Vector3 GetInitialPosition() {
            return pathFractions[0].curves[0].startPosition;
        }

        public Vector3 GetLastPosition()
        {
            return pathFractions.Last().curves.Last().endPosition;
        }
    }

    public class PathFraction
    {
        public List<BezierCurve> curves;
    }
}
