using UnityEngine.InputSystem;
using UnityEngine;

namespace Player
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float shootingSpeed;

        private void Update()
        {
            if (!Keyboard.current.spaceKey.wasPressedThisFrame) return;
            
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * shootingSpeed;
        }
    }
}