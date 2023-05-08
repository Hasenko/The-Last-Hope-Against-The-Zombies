using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light Flashlight;

    void Start()
    {
        // D�sactiver la lumi�re de la lampe torche au d�marrage
        Flashlight.enabled = false;
    }

    void Update()
    {
        // V�rifier si la touche T est appuy�e
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Activer ou d�sactiver la lumi�re de la lampe torche selon son �tat actuel
            Flashlight.enabled = !Flashlight.enabled;
        }
    }
}
