using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Item
{
    [System.Serializable]
    public class Gun : Item
    {
        [ReadOnly, SerializeField] private int currentAmmoCount;
        [SerializeField] private List<Ammo> usableAmmos;

        public int CurrentAmmoCount => currentAmmoCount;
        public List<Ammo> UsableAmmos => usableAmmos;
        
        public Gun(ItemData data) : base(data)
        {
            currentAmmoCount = 0;
            usableAmmos = new List<Ammo>();
            
            foreach (var usableAmmo in ((GunData) data).UsableAmmos)
            {
                usableAmmos.Add(new Ammo(usableAmmo));
            }
        }

        public void FireAmmo(int amount)
        {
            currentAmmoCount = currentAmmoCount - amount > 0 ? currentAmmoCount - amount : 0;
        }

        public void Reload()
        {
            currentAmmoCount = ((GunData) ItemData).MaxBulletCount;
        }
    }
}