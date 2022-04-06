using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elderand.Projectiles
{
    public class FireballPool : MonoBehaviour
    {
        private static event Action<Vector3, Vector3, float, float> OnInstantiate;

        [SerializeField] private List<Fireball> pool;
        [SerializeField] private GameObject prefab;

        private void OnEnable() => OnInstantiate += InstantiateObj;
        private void OnDisable() => OnInstantiate -= InstantiateObj;

        public static void InstantiateFireball(Vector3 position, Vector3 eulerRotation, float speed, float damage)
        {
            OnInstantiate(position, eulerRotation, speed, damage);
        }

        private void InstantiateObj(Vector3 position, Vector3 eulerRotation, float speed, float damage)
        {
            foreach (Fireball fireball in pool)
            {
                if (!fireball.gameObject.activeSelf)
                {
                    fireball.transform.position = position;
                    fireball.transform.rotation = Quaternion.Euler(eulerRotation);
                    fireball.SetFireball(speed, damage);
                    fireball.gameObject.SetActive(true);
                    return;
                }
            }

            CreateFireballInstance(position, eulerRotation, speed, damage);
        }

        private void CreateFireballInstance(Vector3 position, Vector3 eulerRotation, float speed, float damage)
        {
            GameObject newInstance = Instantiate(prefab, position, Quaternion.Euler(eulerRotation), transform);
            Fireball newFireball = newInstance.GetComponent<Fireball>();
            pool.Add(newFireball);
            newFireball.SetFireball(speed, damage);
        }
    }
}

