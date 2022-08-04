using Interface;
using UnityEngine;

namespace Entity
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int bulletDamage;

        private const float Lifetime = 3;

        #region Object Behaviors
        private void Awake()
        {
            Destroy(gameObject, Lifetime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var damageableEntity = collision.gameObject.GetComponentInParent<IDamageable>();
            damageableEntity?.OnDamage(bulletDamage);

            Destroy(gameObject);
        }
        #endregion
    }
}