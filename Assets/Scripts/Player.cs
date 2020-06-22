using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        private const float rotationLimit = 60f;
      
        [SerializeField] private float      crouchSpeed = 5f;
        [SerializeField] private float      runSpeed = 10f;
        [SerializeField] private float      sprintSpeed = 15f;
        [SerializeField] private float      mouseSpeed = 100f;
        [SerializeField] private Transform  cameraTrans = null;

        private CharacterController characterController = null;
        private Vector3 move = new Vector3();

        private float   mouseX = 0f;
        private float   mouseY = 0f;
        private float   vertical = 0f;
        private float   horizontal = 0f;
        private bool    crouch = false;
        private bool    sprint = false;
        private int   height = 2;

        private float rotationY = 0f;
        private float rotationX = 0f;


        

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
                       

        }
        private void Update()
        {
            GetInput();
            move.x = horizontal;
            move.z = vertical;

            Vector3 tempMove = move;
            tempMove.y = 0;
            tempMove = Vector3.ClampMagnitude(tempMove, 1f);
            tempMove = transform.TransformDirection(tempMove);
            move.x = tempMove.x;
            move.z = tempMove.z;

            float speed = Calspeed();
            CalcHeight();
            if (!characterController.isGrounded)
            {
                move.y += Physics.gravity.y * Time.deltaTime;
            }
            else if(Input.GetButtonDown("Jump"))
            {
                move.y = 2f;
            }
            
            characterController.Move(move *(speed * Time.deltaTime));

            Vector3 forward = cameraTrans.forward;
            forward.y = 0f;
            transform.forward = forward;

        }
        private void LateUpdate()
        {
            float mouseDeltaSpeed = Time.deltaTime * mouseSpeed;
            rotationY += mouseX * mouseDeltaSpeed;
            rotationX -= mouseY * mouseDeltaSpeed;

            if (rotationX > rotationLimit)
            {
                rotationX = rotationLimit;
            }
            else if (rotationX < -rotationLimit)
            {
                rotationX = -rotationLimit;
            }

            cameraTrans.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
            
        }
        private void GetInput()
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            crouch = Input.GetButton("Crouch");
            sprint = Input.GetButton("Sprint");
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
        }

        private float Calspeed()
        {
            float speed = runSpeed;

            if(sprint)
            {
                speed = sprintSpeed;
            }
            

            else if(crouch)
            {
                speed = crouchSpeed;
            }
            return speed;
        }

        private void CalcHeight()
        {
            if (crouch && height != 1)
            {
                characterController.height = 1f;
                height = 1;
            }
            else if(!crouch && height != 2)
            {
                characterController.height = 2f;
                height = 2;
            }
        }

        
    }
        
    
}
