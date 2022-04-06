using System.Collections;
using UnityEngine;

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
            StartCoroutine(Blink());
        }

        public void OnTriggerStay2D()
        {
            if (!trigger.enabled)
                return;
            StartCoroutine(Blink());
        }

        private IEnumerator Blink()
        {
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
