using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utils;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, PlayerInputActions.IPlayerActions
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private Transform playerModel;
        [SerializeField] private Transform mousePointer;

        public static UnityAction<Vector2> OnPlayerAimed;

        private CharacterController _controller;
        private Camera _camera;
        private PlayerInputActions _playerInput;
        private Vector2 _playerMovement;
        
        #region Object Behaviors
        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _camera = Camera.main;
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
        
        public void OnAim(InputAction.CallbackContext context)
        {
            var rawMovement = context.ReadValue<Vector2>();

            var worldMovement = _camera.ScreenPointToRay(rawMovement);
            if (!Physics.Raycast(worldMovement, out var raycastHit)) return;
            
            mousePointer.position = new Vector3(raycastHit.point.x, transform.localPosition.y - 0.875f, raycastHit.point.z);

            var currentPosition = transform.position;
            var mousePosition = mousePointer.position;

            var playerDirection = new Vector2(mousePosition.x - currentPosition.x, mousePosition.z - currentPosition.z);
            var angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
            angle = angle < 0 ? angle + 360 : angle;
            playerModel.rotation = Quaternion.Euler(0f, 90 - angle, 0f);
                
            OnPlayerAimed?.Invoke(new Vector2(mousePosition.x, mousePosition.z));
        }
        
        #endregion
    }
}
