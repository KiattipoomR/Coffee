using UnityEngine.InputSystem;
using UnityEngine;

namespace Player
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float shootingSpeed;

        private void Update()
        {
            if (!Keyboard.current.spaceKey.wasPressedThisFrame) return;
            
            Transform thisTransform = transform;
            GameObject bullet = Instantiate(bulletPrefab, thisTransform.position, thisTransform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = thisTransform.forward * shootingSpeed;
        }
    }
}