using UnityEngine;

namespace Player
{
    public class PlayerEquippedItem : MonoBehaviour
    {
        private void OnEnable()
        {
            PlayerController.OnPlayerAimed += PerformAim;
        }

        private void OnDisable()
        {
            PlayerController.OnPlayerAimed -= PerformAim;
        }
        
        private void PerformAim(Vector2 mousePositionInWorld)
        {
            var currentPosition = transform.position;
            var direction = new Vector2(mousePositionInWorld.x - currentPosition.x, mousePositionInWorld.y - currentPosition.z);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = angle < 0 ? angle + 360 : angle;
            transform.rotation = Quaternion.Euler(0f, 90 - angle, 0f);
        }
    }
}