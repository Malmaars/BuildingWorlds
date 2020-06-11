using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("End the game");
        SceneManager.LoadScene(0);
    }
}
