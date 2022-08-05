using Interface;
using UnityEngine;

namespace Item
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ammo : Item
    {
        private const float Lifetime = 3;
        private AmmoData Data => (AmmoData) itemData;

        #region Object Behaviors
        private void Start()
        {
            if (itemData != null && itemData is not AmmoData)
            {
                Debug.LogError("The injected item data is not ammo !");
                Destroy(gameObject);
            }
            
            Destroy(gameObject, Lifetime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var damageableEntity = collision.gameObject.GetComponentInParent<IDamageable>();
            damageableEntity?.OnDamage(Data.BaseDamage);

            Destroy(gameObject);
        }
        #endregion
    }
}