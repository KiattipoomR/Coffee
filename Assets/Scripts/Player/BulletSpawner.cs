using System;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

namespace Player
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float shootingSpeed;
        [SerializeField] private float reloadingTime;
        [SerializeField] private int magazineSize;
        [SerializeField] private GameObject[] bulletTypes;

        private int _bulletRemaining;
        private bool _isReloading;
        private int _currentBulletTypeIndex;

        #region Object Behaviors
        private void Start()
        {
            if (magazineSize < 1) throw new Exception("Max bullet count must be at least 1 !");
            if (bulletTypes.Length < 1) throw new Exception("There must be at least 1 bullet type !");
            
            _bulletRemaining = magazineSize;
            _currentBulletTypeIndex = 0;
        }

        private void Update()
        {
            if(_isReloading) return;
            
            if (Keyboard.current.spaceKey.wasPressedThisFrame &&  _bulletRemaining > 0 && !_isReloading)
            {

                var bullet = Instantiate(bulletTypes[_currentBulletTypeIndex], spawnPoint.position, spawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * shootingSpeed;
                _bulletRemaining--;
                Debug.Log($"Bullet remaining: {_bulletRemaining}/{magazineSize}");
                return;
            }

            if (Keyboard.current.rKey.wasPressedThisFrame && _bulletRemaining < magazineSize)
            {
                StartCoroutine(Reload());
            }
            
            if (Keyboard.current.fKey.wasPressedThisFrame && bulletTypes.Length > 1)
            {
                _currentBulletTypeIndex++;
                if (_currentBulletTypeIndex >= bulletTypes.Length) _currentBulletTypeIndex -= bulletTypes.Length;
                Debug.Log($"Using bullet: {bulletTypes[_currentBulletTypeIndex].name}");
            }
        }
        #endregion

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