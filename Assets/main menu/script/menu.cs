using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    //charger la scene
    public void Play()
    {
        SceneManager.LoadScene("main scene", LoadSceneMode.Single);
    }
    //quiter le jeu
    public void Quit()
    {
        Application.Quit();
    }
}
