using Interface;
using UnityEngine;

namespace Entity
{
    public class Dummy : MonoBehaviour, IDamageable
    {
        [SerializeField] private int health;
        
        public void OnDamage()
        {
            health--;
            
            if(health < 1) Destroy(gameObject);
        }
    }
}