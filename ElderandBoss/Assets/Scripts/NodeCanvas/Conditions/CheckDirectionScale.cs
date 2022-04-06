using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Elderand.Nodecanvas
{
    [Description("Check the Transform's scale and return true if scale is not inverted in X axis.")]
    public class CheckDirectionScale : ConditionTask<Transform>
    {
        protected override bool OnCheck()
        {
            return agent.localScale.x > 0f;
		}
	}
}