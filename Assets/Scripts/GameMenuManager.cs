using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    public void BotonStart()
    {
        SceneManager.LoadScene(1);
    }

    public void BotonQuit()
    {
        Debug.Log("Salimos de la aplicacion");
        Application.Quit();
    }
}
