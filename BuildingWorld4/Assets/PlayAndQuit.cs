using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAndQuit : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene(1);
        //loadscene
    }

    public void endGame()
    {
        Application.Quit();
    }
}
