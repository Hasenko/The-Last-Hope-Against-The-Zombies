using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    private Rigidbody Rigid_Body;

    #region Camera

    public Camera Player_Camera;
    public float Sensibility = 3f;
    public float Max_Angle = 90f;
    private float X = 0.0f;
    private float Y = 0.0f;

    #endregion

    #region Movement

    public float Move_Speed = 5f;
    public float Move_Speed_Max = 7f;

    #endregion

    #region Sprint

    public bool Sprinting = false;
    public float Sprint_Multiplier;

    #endregion

    #region Jump

    public float Jump_Force = 17f;
    private bool On_The_Ground;

    #endregion


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Rigid_Body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        #region Camera

        Y = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * Sensibility;
        X = X - Input.GetAxis ("Mouse Y") * Sensibility;

        X = Mathf.Clamp(X, -Max_Angle, Max_Angle);

        transform.localEulerAngles = new Vector3(0, Y, 0);
        Player_Camera.transform.localEulerAngles = new Vector3(X, 0, 0);

        #endregion

        #region Jump

        if (Input.GetKeyDown(KeyCode.Space) && On_The_Ground)
        {
            Jump();
        }

        #endregion

        Check_Ground();
    }

    void Check_Ground()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float distance = .75f;

        if (Physics.Raycast(origin, direction, distance))
        {
            On_The_Ground = true;
        } else
        {
            On_The_Ground = false;
        }
    }

    private void FixedUpdate()
    {
        #region Movement and Sprint

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
            Vector3 Move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Move = transform.TransformDirection(Move) * Move_Speed;

            Vector3 Move_change = Move - Rigid_Body.velocity;
            Move_change.x = Mathf.Clamp(Move_change.x, -Move_Speed_Max, Move_Speed_Max);
            Move_change.z = Mathf.Clamp(Move_change.z, -Move_Speed_Max, Move_Speed_Max);
            Move_change.y = 0;
            Rigid_Body.AddForce(Move_change, ForceMode.VelocityChange);
        }
        else
        {
            Vector3 Move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Move = transform.TransformDirection(Move) * Move_Speed * Sprint_Multiplier;

            Vector3 Move_change = Move - Rigid_Body.velocity;
            Move_change.x = Mathf.Clamp(Move_change.x, -Move_Speed_Max, Move_Speed_Max);
            Move_change.z = Mathf.Clamp(Move_change.z, -Move_Speed_Max, Move_Speed_Max);
            Move_change.y = 0;
            Rigid_Body.AddForce(Move_change, ForceMode.VelocityChange);
        }

        #endregion

        #region Gravity

        Vector3 gravity = new Vector3(0, -9.81f, 0);
        float gravity_Multiplier = 5f;
        gravity *= gravity_Multiplier;

        Rigid_Body.AddForce(gravity, ForceMode.Force);
        if (Rigid_Body.velocity.y < 0)
        {
            Rigid_Body.AddForce(gravity/10f, ForceMode.Force);
        }
        
        #endregion
    }

    void Jump()
    {
        Rigid_Body.AddForce(new Vector3(0, Jump_Force, 0), ForceMode.Impulse);
    }

}