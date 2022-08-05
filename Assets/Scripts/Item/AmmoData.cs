using UnityEngine;

namespace Item
{
    
    [CreateAssetMenu(fileName = "AmmoData", menuName = "Items/Ammo", order = 2)]
    public class AmmoData : ItemData
    {
        [SerializeField] private int baseDamage;

        public int BaseDamage => baseDamage;
    }
}