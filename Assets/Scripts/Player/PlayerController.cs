using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, PlayerInputActions.IPlayerActions
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private Transform playerModel;

        private CharacterController _controller;
        private PlayerInputActions _playerInput;
        private Vector2 _playerMovement;
        
        #region Object Behaviors
        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _playerInput = new PlayerInputActions();
            _playerInput.Player.SetCallbacks(this);
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }
        
        private void Update()
        {
            _controller.Move(new Vector3(_playerMovement.x, 0f, _playerMovement.y) * (movementSpeed * Time.deltaTime));
        }
        #endregion

        #region Inherited Methods
        public void OnMove(InputAction.CallbackContext context)
        {
            var rawMovement = context.ReadValue<Vector2>();
            var isoViewportMovement = MathUtil.RotateVector2(rawMovement, Mathf.PI / 4);
            
            _playerMovement = isoViewportMovement;
        }
        
        public void OnLook(InputAction.CallbackContext context)
        {
            var rawMovement = context.ReadValue<Vector2>();
            rawMovement.x = Mathf.Lerp(-1, 1, rawMovement.x / Screen.width);
            rawMovement.y = Mathf.Lerp(-1, 1, rawMovement.y / Screen.height);
            
            var isoViewportMovement = MathUtil.RotateVector2(rawMovement, Mathf.PI / 4);
            
            var angle = Mathf.Atan2(isoViewportMovement.y, isoViewportMovement.x) * Mathf.Rad2Deg;
            angle = angle < 0 ? angle + 360 : angle;
            playerModel.rotation = Quaternion.Euler(0f, 90 - angle, 0f);
        }
        
        #endregion
    }
}
