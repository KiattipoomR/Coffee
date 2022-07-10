using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, PlayerInputActions.IPlayerActions
    {
        [SerializeField] private float movementSpeed = 5f;

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
            _controller.Move(new Vector3(_playerMovement.x, 0f, _playerMovement.y) * movementSpeed * Time.deltaTime);
        }
        #endregion
        
        public void OnMove(InputAction.CallbackContext context)
        {
            _playerMovement = context.ReadValue<Vector2>();
        }
    }
}
