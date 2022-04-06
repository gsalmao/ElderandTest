using System.Collections;
using System.Collections.Generic;
using Elderand.ScriptableObjects;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace Elderand.Nodecanvas
{
    [Category("Custom")]
    [Description("Change Direction, according to transform's X position")]
    public class MoveThroughBezier : ActionTask<Transform>
    {
        public BBParameter<BezierPath> bezierPath;
        public BBParameter<float> speed;
        public BBParameter<bool> updateDirection;
        private int curCurve;
        private float transition;

        private const int Resolution = 20;
        private Vector3 inverse = new Vector3(-1f, 1f, 1f);
        protected override void OnExecute()
        {
            transition = 0f;
            curCurve = 0;
        }

        protected override void OnUpdate()
        {
            Vector3 newPosition = GetNewPosition(bezierPath.value.curves);
            if (updateDirection.value)
            {
                if (newPosition.x > agent.position.x) agent.localScale = Vector3.one;
                else agent.localScale = inverse;
            }
            agent.position = Vector3.MoveTowards(agent.position, newPosition, Time.deltaTime * speed.value);
        }

        private Vector3 GetNewPosition(List<BezierCurve> curves)
        {
            float curveLength = curves[curCurve].CalculateCurveLength(Resolution);
            transition += Time.deltaTime * speed.value / curveLength;

            if (transition > 1)
            {
                transition--;
                UpdateCurveIndex(curves);
            }

            return curves[curCurve].GetTransitionPoint(transition);
        }

        private void UpdateCurveIndex(List<BezierCurve> curves)
        {
            if (curCurve < curves.Count - 1)
                curCurve++;
            else
                EndAction(true);
        }
    }
}

