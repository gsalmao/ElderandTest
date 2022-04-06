using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Elderand.Nodecanvas
{

    [Category("Custom")]
    [Description("Move the Rigidbody2D to the transform's location")]
    public class MoveToTransform : ActionTask<Rigidbody2D>
    {
        public BBParameter<Transform> position;
        public BBParameter<float> speed;

        private Vector2 finalPosition;

        protected override void OnExecute()
        {
            finalPosition = position.value.position;
        }

        protected override void OnUpdate()
        {
            agent.position = Vector3.MoveTowards(agent.position, finalPosition, speed.value * Time.deltaTime);
            if (Vector3.Distance(agent.position, finalPosition) < 0.02f)
                EndAction(true);
        }
    }
}