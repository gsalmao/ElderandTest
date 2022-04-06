using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Elderand.Nodecanvas{

	[Category("Custom")]
	[Description("Set the agent's rigidbody2D configurations")]
	public class SetVelocity : ActionTask<Rigidbody2D>
    {
        public BBParameter<Vector2> velocity;

		protected override void OnExecute()
        {
            agent.velocity = velocity.value;
			EndAction(true);
		}

	}
}