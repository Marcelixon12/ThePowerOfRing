using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public static int enemiesCount = 10;
    public static int enemiesDead = 0;
    public GameObject enemy;    
    public int waves = 1;
    public CharacterMovement cm;
    

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("waves"))
        {
            
            waves = PlayerPrefs.GetInt("waves");
            
        }
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerPrefs.SetInt("waves", waves);
        if (enemiesDead >= 10 && waves < 4)
        {
            Spawn();
            waves += 1;
            if (waves != 4)
                enemiesDead = 0;
            enemiesCount = 10;
            
        }
        else if (waves == 4 && enemiesDead >= 10)
        {
            cm.isCanForest = true;
        }

        

    }
    public void Spawn()
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            int x = Random.Range(0, spawnPoints.Length);
            Instantiate(enemy, new Vector3(spawnPoints[x].transform.position.x + Random.Range(10,30), spawnPoints[x].transform.position.y, spawnPoints[x].transform.position.z + Random.Range(10, 30)), Quaternion.identity);
            

        }



    }
}
