using Elderand.ExtensionMethods;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Elderand.Nodecanvas{

	[Category("Custom")]
	[Description("Change Direction, according to transform's X position")]
	public class LookAtTransform : ActionTask<Transform>
    {

        public BBParameter<Transform> target;
        public BBParameter<bool> reverse;

        private Vector3 normalDirection;
        private Vector3 reverseDirection;

        protected override void OnExecute()
        {
            if (reverse.value)
            {
                normalDirection = new Vector3(-1f, 1f, 1f);
                reverseDirection = Vector3.one;
            }
            else
            {
                normalDirection = Vector3.one;
                reverseDirection = new Vector3(-1f, 1f, 1f);
            }

            if (target.value.position.x > agent.position.x)
                agent.localScale = normalDirection;
            else
                agent.localScale = reverseDirection;

            EndAction(true);
        }
    }
}