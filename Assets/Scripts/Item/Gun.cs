using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using Utils;

namespace Item
{
    public class Gun : Item
    {
        [SerializeField] private Transform spawnPoint;

        [Header("Gun's Realtime Data")]
        [ReadOnly, SerializeField] private string currentAmmo;
        [ReadOnly, SerializeField] private int bulletRemaining;
        [ReadOnly, SerializeField] private bool isReloading;
        
        private GunData GunData => (GunData) itemData;
        
        private int _currentBulletTypeIndex;

        #region Object Behaviors
        private void Start()
        {
            if (GunData.MaxBulletCount < 1)
            {
                Debug.LogError("Max bullet count must be at least 1 !");
                return;
            }
            
            if (GunData.UsableAmmos.Length < 1)
            {
                Debug.LogError("There must be at least 1 bullet type !");
                return;
            }
            
            bulletRemaining = GunData.MaxBulletCount;
            SetCurrentAmmo();
        }

        private void Update()
        {
            if(isReloading) return;
            
            if (Keyboard.current.spaceKey.wasPressedThisFrame &&  bulletRemaining > 0 && !isReloading)
            {
                FireAmmo();
                return;
            }

            if (Keyboard.current.rKey.wasPressedThisFrame && bulletRemaining < GunData.MaxBulletCount)
            {
                StartCoroutine(Reload());
                return;
            }
            
            if (Keyboard.current.fKey.wasPressedThisFrame && GunData.UsableAmmos.Length > 1)
            {
                _currentBulletTypeIndex++;
                if (_currentBulletTypeIndex >= GunData.UsableAmmos.Length) _currentBulletTypeIndex -= GunData.UsableAmmos.Length;
                SetCurrentAmmo();
            }
        }
        #endregion

        private void FireAmmo()
        {
            var bullet = Instantiate(GunData.UsableAmmos[_currentBulletTypeIndex].Model, spawnPoint.position, spawnPoint.rotation);
            bullet.GetComponent<Ammo>().Init(GunData.UsableAmmos[_currentBulletTypeIndex]);
            bullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * GunData.ShootingSpeed;
                
            bulletRemaining--;
            Debug.Log($"Bullet remaining: {bulletRemaining}/{GunData.MaxBulletCount}");
        }

        private IEnumerator Reload()
        {
            isReloading = true;
            Debug.Log("Reloading !!");
            
            yield return new WaitForSeconds(GunData.ReloadingTime);
            
            bulletRemaining = GunData.MaxBulletCount;
            isReloading = false;
            Debug.Log($"Reloading Done. Bullet remaining: {bulletRemaining}/{GunData.MaxBulletCount}");
        }

        private void SetCurrentAmmo()
        {
            currentAmmo = GunData.UsableAmmos[_currentBulletTypeIndex].name;
            Debug.Log($"Using bullet: {currentAmmo}");
        }
    }
}