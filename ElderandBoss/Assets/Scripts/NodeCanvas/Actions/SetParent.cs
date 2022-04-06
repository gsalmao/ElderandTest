using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Elderand.Nodecanvas{

	[Category("Custom")]
	[Description("Set the transform's parent")]
	public class SetParent : ActionTask<Transform>
    {

        public BBParameter<Transform> parent;
		protected override void OnExecute()
        {
			agent.transform.SetParent(parent.value);
			EndAction(true);
		}
    }
}