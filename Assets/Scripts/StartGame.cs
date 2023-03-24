using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public AudioSource clickFX;
    public void LoadNewGame()
    {
        clickFX.Play();
        SceneManager.LoadScene(1);
    }
}
