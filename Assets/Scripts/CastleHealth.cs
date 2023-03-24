using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour
{
    [SerializeField] float castleHP = 5f;
    [SerializeField] Slider healthBarSlider;
    AudioSource enemyHit;


    public static event Action OnCastleDestroyed;
    private bool castleSurvived = true;
    EnemySpawner enemySpawner;


    void Start()
    {
        healthBarSlider.maxValue = castleHP;
        healthBarSlider.value = castleHP;
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Update()
    {
        if (enemySpawner.WavesComplete())
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && castleSurvived)
            {
              
                OnCastleDestroyed?.Invoke();
            }
        }
    }

    public bool CastleSurvived()
    {
        return castleSurvived;
    }

    void Awake(){
        enemyHit = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy") && castleHP > 0)
        {
            castleHP--;
            healthBarSlider.value = castleHP;
            enemyHit.Play();
        }
        else
        {
            castleSurvived = false;
            OnCastleDestroyed?.Invoke();
        }
    }
}
