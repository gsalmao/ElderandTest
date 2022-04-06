using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Elderand.Nodecanvas{

	[Category("Custom")]
	[Description("Set the agent's rigidbody2D configurations")]
	public class SetGravity : ActionTask<Rigidbody2D>
    {
        public BBParameter<float> gravity;

		protected override void OnExecute()
        {
            agent.gravityScale = gravity.value;
			EndAction(true);
		}

	}
}