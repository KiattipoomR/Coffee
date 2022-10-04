using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerEquippedItem : MonoBehaviour
    {
        private int _currentHotBarIndex = 1;
            
        private void OnEnable()
        {
            PlayerController.OnPlayerAimed += PerformAim;
        }

        private void OnDisable()
        {
            PlayerController.OnPlayerAimed -= PerformAim;
        }

        private void Update()
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame) _currentHotBarIndex = 1;
            if (Keyboard.current.digit2Key.wasPressedThisFrame) _currentHotBarIndex = 2;
            if (Keyboard.current.digit3Key.wasPressedThisFrame) _currentHotBarIndex = 3;
            if (Keyboard.current.digit4Key.wasPressedThisFrame) _currentHotBarIndex = 4;

            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                _currentHotBarIndex -= 1;
                if (_currentHotBarIndex < 1) _currentHotBarIndex = transform.childCount;
            }
            
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                _currentHotBarIndex += 1;
                if (_currentHotBarIndex > transform.childCount) _currentHotBarIndex = 1;
            }
            
            SelectEquippedItem();
        }

        private void PerformAim(Vector2 mousePositionInWorld)
        {
            var currentPosition = transform.position;
            var direction = new Vector2(mousePositionInWorld.x - currentPosition.x, mousePositionInWorld.y - currentPosition.z);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = angle < 0 ? angle + 360 : angle;
            transform.rotation = Quaternion.Euler(0f, 90 - angle, 0f);
        }

        private void SelectEquippedItem()
        {
            var hotBarIndex = _currentHotBarIndex - 1;

            for (var i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(i == hotBarIndex);
            }
        }
    }
}