using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "GunData", menuName = "Items/Gun", order = 1)]
    public class GunData : ItemData
    {
        [SerializeField] private GameObject[] usableAmmos;
        [SerializeField] private int maxBulletCount;
        [SerializeField] private float shootingSpeed;
        [SerializeField] private float reloadingTime;
        
        public GameObject[] UsableAmmos => usableAmmos;
        public int MaxBulletCount => maxBulletCount;
        public float ShootingSpeed => shootingSpeed;
        public float ReloadingTime => reloadingTime;
    }
}