using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    //charger la scene
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //quiter le jeu
    public void Quit()
    {
        Application.Quit();
        Debug.Log("player has quit the game");
    }
}
