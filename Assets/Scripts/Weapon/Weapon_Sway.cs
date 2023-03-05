using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Sway : MonoBehaviour
{
    [Header("Sway Settings")]

    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplier;

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.right);
        Quaternion targetRotarion = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotarion, smooth * Time.deltaTime);
    }
}
