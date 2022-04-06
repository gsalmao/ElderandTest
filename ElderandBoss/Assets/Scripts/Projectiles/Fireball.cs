using UnityEngine;

namespace Elderand.Projectiles
{
    public class Fireball : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particleSystem;
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        [SerializeField] private Animator fireballShootFX;

        public void SetFireball(float speed, float damage)
        {
            this.speed = speed;
        }

        private void Start()
        {
            particleSystem.gameObject.transform.SetParent(null);
            fireballShootFX.gameObject.transform.SetParent(null);
        }

        private void OnEnable()
        {
            fireballShootFX.transform.position = transform.position;
            fireballShootFX.transform.rotation = transform.rotation;
            fireballShootFX.Play("OnShootFX",0, 0f);
        }

        void Update() => transform.Translate(Vector3.right * speed * Time.deltaTime);

        public void OnCollisionEnter2D()
        {
            PlayParticles();
        }

        public void OnTriggerEnter2D()
        {
            PlayParticles();
        }

        private void PlayParticles()
        {
            particleSystem.gameObject.transform.position = transform.position;
            particleSystem.Play();
            transform.gameObject.SetActive(false);
        }
    }
}