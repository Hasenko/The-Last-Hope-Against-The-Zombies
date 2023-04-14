using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public int Damage;
    public float Max_Distance;

    [Header("Reloading")]
    public int Current_Ammo;
    public int Magazine_Size;

    [Tooltip("In RPM")] public float Fire_Rate;
    public float Reload_Time;
    [HideInInspector] public bool Reloading;
}