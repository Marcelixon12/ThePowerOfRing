using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartManager : MonoBehaviour
{
    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public int enemyDead;
    public static bool isDestroyed = false;
    public GameObject[] goblins;
    
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("Deads"))
        {
            enemyDead = 9;
            
        }
        if (enemyDead >= 9)
        {
            for (int i = 0; i < goblins.Length; i++)
            {
                goblins[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyDead >= 9 && !isDestroyed)
        {
            part1.SetActive(true);
            PlayerPrefs.SetInt("Deads", enemyDead);
        }
        
    }
}
