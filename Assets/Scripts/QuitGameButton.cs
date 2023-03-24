using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameButton : MonoBehaviour
{
    public AudioSource quitFX;
    public void QuitGame()
    {
        quitFX.Play();
        Application.Quit();
    }

}
