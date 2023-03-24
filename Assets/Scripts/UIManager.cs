using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverMenu;
    public CastleHealth castle;
    public TextMeshProUGUI gameOverText;
    public AudioSource winFX;

    private void OnEnable()
    {
        CastleHealth.OnCastleDestroyed += EnableGameOverMenu;
    }

    private void OnDisable()
    {
        CastleHealth.OnCastleDestroyed -= EnableGameOverMenu;
    }


    public void EnableGameOverMenu()
    {
        if (castle.CastleSurvived())
        {
            gameOverText.text = "Congratulations, you won!";
            winFX.Play();
        }
        gameOverMenu.SetActive(true);
    }
}

