using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlaceTower : MonoBehaviour
{
    public List<GameObject> towerPrefab = new List<GameObject>();
    private GameObject tower;
    private GameManagerBehaviour gameManager;
    [SerializeField] GameObject selectionMenu;
    private int towerSelectionIndex = -1;
    public Button balistaTower;
    public Button freezeTower;
    public AudioSource soundFX;


    private bool CanPlaceTower()
    {
        int cost = towerPrefab[0].GetComponent<TowerData>().levels[0].cost;
        soundFX.Play();
        return tower == null && gameManager.Gold >= cost;
        

    }

    void OnMouseUp()
    {
        if (CanPlaceTower())
        {
            if(towerSelectionIndex == -1)
            {
                TowerSelection(-1);
            }
            else if (towerSelectionIndex != -1)
            {
                tower = (GameObject)Instantiate(towerPrefab[towerSelectionIndex], transform.position, Quaternion.identity);
                gameManager.Gold -= tower.GetComponent<TowerData>().CurrentLevel.cost;
            }
            
        }
        else if (CanUpgradeTower())
        {
            tower.GetComponent<TowerData>().IncreaseLevel();
          
            gameManager.Gold -= tower.GetComponent<TowerData>().CurrentLevel.cost;
        }

    }

    private bool CanUpgradeTower()
    {
          
        if (tower != null)
        {
            
            TowerData towerData = tower.GetComponent<TowerData>();
            TowerLevel nextLevel = towerData.GetNextLevel();
            if (nextLevel != null)
            {
                return gameManager.Gold >= nextLevel.cost;
            }
        }
        return false;
    }

    void ButtonSelection()
    {
        balistaTower.onClick.AddListener(delegate { TowerSelection(0); });
        freezeTower.onClick.AddListener(delegate { TowerSelection(1); });
    }

    public void TowerSelection(int selectedTower)
    {
        selectionMenu.SetActive(true);
        if(selectedTower == -1)
        {
            ButtonSelection();
        }
            
        if (selectedTower != -1)
        {
            towerSelectionIndex = selectedTower;
            selectionMenu.SetActive(false);
        }
       
    }

 
    


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("UIManager").GetComponent<GameManagerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}