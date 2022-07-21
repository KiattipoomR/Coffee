using System;
using UnityEngine;

namespace Entity
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        private const float Lifetime = 3;

        private void Awake()
        {
            Destroy(gameObject, Lifetime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}