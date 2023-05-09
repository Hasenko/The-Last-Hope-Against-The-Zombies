using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winmenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Respawn()
    {
        SceneManager.LoadScene("main scene", LoadSceneMode.Single);
    }

    // Update is called once per frame
    public void MainMenu()
    {
        SceneManager.LoadScene("main menu", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
