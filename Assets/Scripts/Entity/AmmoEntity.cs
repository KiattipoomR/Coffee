using Interface;
using Item;
using UnityEngine;
namespace Entity
{
    [RequireComponent(typeof(Rigidbody))]
    public class AmmoEntity : MonoBehaviour
    {
        [SerializeField] private Ammo ammo;
        
        private const float Lifetime = 3;
        private AmmoData Data => (AmmoData) ammo.ItemData;

        #region Object Behaviors
        private void Start()
        {
            Destroy(gameObject, Lifetime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var damageableEntity = collision.gameObject.GetComponentInParent<IDamageable>();
            damageableEntity?.OnDamage(Data.BaseDamage);

            Destroy(gameObject);
        }
        #endregion

        public void Init(Ammo data)
        {
            ammo = data;
        }
    }
}