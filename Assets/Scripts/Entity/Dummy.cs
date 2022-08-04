using System;
using Interface;
using UnityEngine;

namespace Entity
{
    public class Dummy : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth;

        private int _health;
        
        #region Object Behaviors
        private void Start()
        {
            if (maxHealth < 1) throw new Exception("Max health must be at least 1 !");

            _health = maxHealth;
        }
        #endregion
        
        #region Inherited Methods
        public void OnDamage(int damage)
        {
            _health = _health - damage > 0 ? _health - damage : 0;
            Debug.Log($"{gameObject.name} received {damage} damage. Current health: {_health}/{maxHealth}");

            if (_health > 0) return;
            Destroy(gameObject);
            Debug.Log($"{gameObject.name} died.");
        }
        #endregion
    }
}