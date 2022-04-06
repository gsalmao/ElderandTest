using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
using Elderand.ExtensionMethods;

namespace Elderand.Player
{
    public class PlayerController : MonoBehaviour
    {
        public bool Grounded { get; private set; } = false;


        [FoldoutGroup("References")] [SerializeField] private Rigidbody2D rb;

        [FoldoutGroup("Player Values")] [SerializeField] private LayerMask groundLayer;
        [FoldoutGroup("Player Values")] [SerializeField] private Vector3 groundOffset;
        [FoldoutGroup("Player Values")] [SerializeField] private Vector2 groundArea;
        [FoldoutGroup("Player Values")] [SerializeField] private Vector2 jumpForce;
        [FoldoutGroup("Player Values")] [SerializeField] private float moveSpeed;


        private PlayerControls controls;
        private Vector2 movement;

        private void Awake()
        {
            controls = new PlayerControls();
        }

        private void OnEnable()
        {
            controls.Enable();
            controls.Gameplay.Jump.performed += Jump;
            controls.Gameplay.Attack.performed += Attack;
        }

        private void OnDisable()
        {
            controls.Gameplay.Jump.performed -= Jump;
            controls.Gameplay.Attack.performed -= Attack;
            controls.Disable();
        }

        private void Update()
        {
            Grounded = Physics2D.OverlapBox(transform.position + groundOffset, groundArea, 0f, groundLayer);
            movement = controls.Gameplay.Movement.ReadValue<Vector2>();
            rb.SetVelocityX(movement.x * moveSpeed);
        }

        private void Jump(InputAction.CallbackContext callback)
        {
            if(Grounded)
                rb.AddForce(jumpForce, ForceMode2D.Impulse);
        }

        private void Attack(InputAction.CallbackContext callback)
        {

        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(transform.position + groundOffset, groundArea);
        }
    }
}
