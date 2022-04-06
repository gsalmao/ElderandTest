using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Elderand.Projectiles;
using UnityEngine;

namespace Elderand.Nodecanvas{

	[Category("Custom")]
	[Description("Shoots a fireball")]
	public class ShootFireball : ActionTask<Transform>
    {
        public BBParameter<Vector3> offset;
        public BBParameter<Vector3> eulerRotation;
        public BBParameter<float> speed;
        public BBParameter<float> damage;
        public bool getAgentDirection;

        private Vector3 position;
        private Vector3 rotation;
        protected override void OnExecute()
        {
            position = agent.position + offset.value;
            rotation = eulerRotation.value;

            if (getAgentDirection && agent.localScale.x < 0)
            {
                rotation.y += 180;
                position.x -= offset.value.x * 2;
            }

            FireballPool.InstantiateFireball(position, rotation, speed.value, damage.value);
			EndAction(true);
		}
    }
}