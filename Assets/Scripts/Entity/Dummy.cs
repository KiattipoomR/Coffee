using Interface;
using UnityEngine;

namespace Entity
{
    public class Dummy : MonoBehaviour, IDamageable
    {
        [SerializeField] private int health;
        
        #region Inherited Methods
        public void OnDamage()
        {
            health--;
            
            if(health < 1) Destroy(gameObject);
        }
        #endregion
    }
}