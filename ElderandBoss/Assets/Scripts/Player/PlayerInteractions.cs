using System.Collections;
using UnityEngine;
using Elderand.Managers;

namespace Elderand.Player
{
    public class PlayerInteractions : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BoxCollider2D trigger;

        public void OnTriggerEnter2D()
        {
            if (!trigger.enabled)
                return;
            StartCoroutine(Damage());
        }

        public void OnTriggerStay2D()
        {
            if (!trigger.enabled)
                return;
            StartCoroutine(Damage());
        }

        private IEnumerator Damage()
        {
            CameraManager.ShakeCamera(5f, .5f, 0);
            trigger.enabled = false;
            for (int i = 0; i < 10; i++)
            {
                spriteRenderer.color = Color.clear;
                yield return new WaitForSeconds(0.05f);
                spriteRenderer.color = Color.white;
                yield return new WaitForSeconds(0.05f);
            }
            trigger.enabled = true;
        }
    }
}
