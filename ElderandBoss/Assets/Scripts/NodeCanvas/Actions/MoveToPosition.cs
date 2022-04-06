using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Elderand.Nodecanvas{

	[Category("Custom")]
	[Description("Move the Rigidbody2D to the correspondent position")]
	public class MoveToPosition : ActionTask<Rigidbody2D>
    {
        public BBParameter<Vector2> position;
        public BBParameter<float> speed;
        public BBParameter<bool> localPosition;

        private Vector2 direction;
        private Vector2 finalPosition;

        protected override void OnExecute()
        {
			if(localPosition.value)
                SetupLocalPosition();
            else
                SetupGlobalPosition();
        }

		protected override void OnUpdate()
        {
            agent.position = Vector3.MoveTowards(agent.position, finalPosition, speed.value * Time.deltaTime);
            if(Vector3.Distance(agent.position, finalPosition) < 0.02f)
                EndAction(true);
        }

        private void SetupGlobalPosition()
        {
            finalPosition = position.value;
        }

        private void SetupLocalPosition()
        {
            finalPosition = (Vector2)agent.transform.position + position.value;
        }

	}
}