using UnityEngine;

namespace Elderand.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static void SetVelocityX(this Rigidbody2D rb, float velocityX)
        {
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
        }

        public static void SetVelocityY(this Rigidbody2D rb, float velocityY)
        {
            rb.velocity = new Vector2(rb.velocity.x, velocityY);
        }

        public static Vector3 ReverseX(this Vector3 vector3)
        {
            return new Vector3(-1f, 1f, 1f);
        }

    }
}
