using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerBehaviour : MonoBehaviour
{
    public TextMeshProUGUI goldLabel;
    private int gold  = 0;
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            goldLabel.text = gold.ToString();
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        Gold = 350;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
