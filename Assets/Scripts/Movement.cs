using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PlayerMovement : MonoBehaviour
    {

        public CharacterController controller;

        [Header("Keybinds")]
        public KeyCode jumpKey = KeyCode.Space;
        public KeyCode sprintKey = KeyCode.LeftShift;
        public KeyCode crouchKey = KeyCode.LeftControl;

        [Header("Speed")]
        public float speed = 3f;
        public float crouchSpeed = 1.5f;
        public float sprintSpeed = 5f;

        [Header("Gravity")]
        Vector3 velocity;
        public float gravity = -19.62f;

        [Header("Jump Height")]
        public float jumpHeight = 1f;

        [Header("Ground Settings")]
        public float groundDistance = 0.4f;
        public Transform groundCheck;
        public LayerMask groundMask;
        bool isGrounded;

        [Header("Crouch Settings")]
        public float crouchHeight = 1f;
        private float originalHeight;
        bool isCrouching = false;

        void Start() {
            //originalHeight = transform.localScale.y;
            originalHeight = controller.height;
        }

        void Update()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            ApplyGravity();
            PlayerIsGrounded();
            //HandleCrouch();
            MovePlayer();
            //Jump();
        }

        private void ApplyGravity()
        {
            // Aplica la gravedad
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        private void PlayerIsGrounded()
        {
            // Comprueba si está en el suelo y no está saltando
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2.5f; // Aplicar una pequeña fuerza hacia abajo para mantener al jugador en el suelo
            }
        }

        private void MovePlayer()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            if (Input.GetKey(sprintKey) && isGrounded && !isCrouching)
            {
                controller.Move(move * sprintSpeed * Time.deltaTime);
            }     
            else if (isCrouching)
            {
                controller.Move(move * crouchSpeed * Time.deltaTime);
            }
            else
            {
                controller.Move(move * speed * Time.deltaTime);
            }
        }

        private void Jump()
        {
            // Salta si está en el suelo y no está agachado
            if (Input.GetKeyDown(jumpKey) && isGrounded && !isCrouching)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale = new Vector3(1, 1f, 1);

    private void HandleCrouch()
        {
            // Si presiona Ctrl, se agacha
            if (Input.GetKeyDown(crouchKey))
            {
                isCrouching = true;
                //transform.localScale = crouchScale;
                //transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                controller.height = crouchHeight;
                controller.center = new Vector3(controller.center.x, crouchHeight / 2, controller.center.z);

        }

            if (Input.GetKeyUp(crouchKey))
            {
                isCrouching = false;
                //transform.localScale = playerScale;
                //transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                controller.height = originalHeight;
                controller.center = new Vector3(controller.center.x, originalHeight / 2, controller.center.z);
            }
        }

}



