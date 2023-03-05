using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;

    private KeyCode reloadKey = KeyCode.R;
    private void Update()
    {
        if (Input.GetMouseButton(0))
            shootInput?.Invoke();

        if (Input.GetKeyDown(reloadKey))
            reloadInput?.Invoke();
    }
}