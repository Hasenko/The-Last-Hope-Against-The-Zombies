using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light Flashlight;

    void Start()
    {
        // Désactiver la lumière de la lampe torche au démarrage
        Flashlight.enabled = false;
    }

    void Update()
    {
        // Vérifier si la touche T est appuyée
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Activer ou désactiver la lumière de la lampe torche selon son état actuel
            Flashlight.enabled = !Flashlight.enabled;
        }
    }
}
