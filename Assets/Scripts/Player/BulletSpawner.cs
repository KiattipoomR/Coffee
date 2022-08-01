using System;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

namespace Player
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float shootingSpeed;
        [SerializeField] private float reloadingTime;
        [SerializeField] private int magazineSize;
        
        private int _bulletRemaining;
        private bool _isReloading;

        private void Start()
        {
            if (magazineSize < 1) throw new Exception("Max bullet count must be at least 1 !");

            _bulletRemaining = magazineSize;
        }

        private void Update()
        {
            if(_isReloading) return;
            
            if (Keyboard.current.spaceKey.wasPressedThisFrame &&  _bulletRemaining > 0 && !_isReloading)
            {

                GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * shootingSpeed;
                _bulletRemaining--;
                Debug.Log($"Bullet remaining: {_bulletRemaining}/{magazineSize}");
                return;
            }

            if (Keyboard.current.rKey.wasPressedThisFrame && _bulletRemaining < magazineSize)
            {
                StartCoroutine(Reload());
            }
        }

        private IEnumerator Reload()
        {
            _isReloading = true;
            Debug.Log("Reloading !!");
            
            yield return new WaitForSeconds(reloadingTime);
            
            _bulletRemaining = magazineSize;
            _isReloading = false;
            Debug.Log($"Reloading Done. Bullet remaining: {_bulletRemaining}/{magazineSize}");
        }
    }
}