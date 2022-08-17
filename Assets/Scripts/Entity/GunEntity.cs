using System.Collections;
using Item;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Entity
{
    public class GunEntity : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Gun gun;

        [Header("Gun's Realtime Data")]
        [ReadOnly, SerializeField] private string currentAmmo;
        [ReadOnly, SerializeField] private bool isReloading;

        private Gun Gun => gun;
        private GunData GunData => (GunData) gun.ItemData;
        
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
            
            gun.Reload();
            SetCurrentAmmo();
        }

        private void Update()
        {
            if(isReloading) return;
            
            if (Keyboard.current.spaceKey.wasPressedThisFrame &&  Gun.CurrentAmmoCount > 0 && !isReloading)
            {
                FireAmmo();
                return;
            }

            if (Keyboard.current.rKey.wasPressedThisFrame && Gun.CurrentAmmoCount < GunData.MaxBulletCount)
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
            bullet.GetComponent<AmmoEntity>().Init(Gun.UsableAmmos[_currentBulletTypeIndex]);
            bullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * GunData.ShootingSpeed;
                
            Gun.FireAmmo(1);
            Debug.Log($"Bullet remaining: {Gun.CurrentAmmoCount}/{GunData.MaxBulletCount}");
        }

        private IEnumerator Reload()
        {
            isReloading = true;
            Debug.Log("Reloading !!");
            
            yield return new WaitForSeconds(GunData.ReloadingTime);
            
            Gun.Reload();
            isReloading = false;
            Debug.Log($"Reloading Done. Bullet remaining: {Gun.CurrentAmmoCount}/{GunData.MaxBulletCount}");
        }

        private void SetCurrentAmmo()
        {
            currentAmmo = GunData.UsableAmmos[_currentBulletTypeIndex].name;
            Debug.Log($"Using bullet: {currentAmmo}");
        }
    }
}