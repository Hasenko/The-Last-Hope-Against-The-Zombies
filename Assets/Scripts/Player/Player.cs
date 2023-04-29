using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(WeaponController))]

public class Player : MonoBehaviour
{
    #region variables
    public float walkSpeed = 6f;
    public float SprintSpeed = 7f;
    public float jumpPower = 4f;
    public float mouseSensitivity = 2f;
    public float mouseVerticalLimit = 80f;
    
    public bool Sprinting = false;

    private CharacterController characterController;
    private WeaponController weaponController;
    private float gravity = 0f;
    private float verticalAngle = 0f;
    private Vector3 motion = Vector3.zero;

    public PlayerHUD hud;

    public AudioSource footStepSound, sprintSound;
    #endregion


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        weaponController = GetComponent<WeaponController>();
        Cursor.lockState = CursorLockMode.Locked; //enlever la souris
        hud = GetComponent<PlayerHUD>();
    }

    void Update()
    {
        // Input de déplacement du joueur
        motion = GetInput();

        if (Input.GetButton("Fire1"))
        {
            weaponController.FireWeapon();
        }
        if (Input.GetKey(KeyCode.R))
        {
            weaponController.currentWeapon.Reload();
            hud.UpdateAmmo(weaponController.currentWeapon.currentAmmoInClip, weaponController.currentWeapon.maxAmmoInClip);
            Debug.Log("4");
        }

        GetAimInfo();

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                footStepSound.enabled = false;
                sprintSound.enabled = true;

            }
            else
            {
                footStepSound.enabled = true;
                sprintSound.enabled = false;
            }
            
        }
            
        else
        {
            footStepSound.enabled = false;
            sprintSound.enabled = false;
        }

    }

    void FixedUpdate()
    {
        characterController.Move(motion * Time.fixedDeltaTime);
    }

    private Vector3 GetInput()
    {
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        RotateView();

        gravity = gravity - (9.81f * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift) && characterController.isGrounded)
        {
            Sprinting = true;
        }

        else
        {
            Sprinting = false;
        }

        if (Sprinting == false)
        {
            if (moveVector.magnitude > 1f)
            {
                moveVector = moveVector.normalized;
            }

            moveVector = moveVector * walkSpeed;

        }

        else
        {
            if (moveVector.magnitude > 1f)
            {
                moveVector = moveVector.normalized;
            }

            moveVector = moveVector * SprintSpeed;
        }

        if (characterController.isGrounded)
        {
            gravity = -10f;
            if (Input.GetButton("Jump"))
            {
                gravity = jumpPower;

            }
        }

        moveVector.y = gravity;
        moveVector = transform.rotation * moveVector;
        return moveVector;
    }

    private void RotateView()
    {
        // rotation gauche/droite
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);

        //rotation haut/bas
        verticalAngle -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalAngle = Mathf.Clamp(verticalAngle, -mouseVerticalLimit, mouseVerticalLimit); //limiter l'angle vertical
        Camera.main.transform.localRotation = Quaternion.Euler(verticalAngle, 0, 0);
    }
    

    private void GetAimInfo()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //Vector3 (X = 0, Y = 0, Z = 1)

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100f))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
        }
    }
}
