using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;

    public float walkSpeed = 6f;
    public float jumpPower = 4f;
    public float mouseSensitivity = 2f;
    public float mouseVerticalLimit = 80f;
    public float SprintSpeed = 7f;

    public bool Sprinting = false;

    private float gravity = 0f;
    private float verticalAngle = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; //enlever la souris
    }

    void Update()
    {
        
      
        // Input de déplacement du joueur
        Vector3 motion = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // rotation gauche/droite
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);

        //rotation haut/bas
        verticalAngle -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalAngle = Mathf.Clamp(verticalAngle, -mouseVerticalLimit, mouseVerticalLimit); //limiter l'angle vertical
        Camera.main.transform.localRotation = Quaternion.Euler(verticalAngle, 0, 0);


        gravity = gravity - (9.81f * Time.deltaTime);
       
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Sprinting = true;
        }
        else
        {
            Sprinting = false;
        }

        if (Sprinting == false)
        {
            if (motion.magnitude > 1f)
            {
                motion = motion.normalized;
            }

            motion = motion * walkSpeed;

        }
        else
        {
            if (motion.magnitude > 1f)
            {
                motion = motion.normalized;
            }

            motion = motion * SprintSpeed;
        }

        if(characterController.isGrounded)
        {
            gravity= -0.5f;
            if (Input.GetButton("Jump"))
            {
                gravity = jumpPower;
            }
        }

        motion.y = gravity;
        motion = transform.rotation * motion;

        characterController.Move(motion * Time.deltaTime);
    }
}
