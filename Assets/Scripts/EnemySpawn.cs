using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public static int enemiesCount = 10;
    public static int enemiesDead = 0;
    public GameObject enemy;    
    public int waves = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesDead >= 10 && waves < 4)
        {
            Spawn();
            waves += 1;
            enemiesDead = 0;
        }
        else if (waves == 4)
        {

        }

        

    }
    public void Spawn()
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            int x = Random.Range(0, spawnPoints.Length);
            Instantiate(enemy, spawnPoints[x].transform.position, Quaternion.identity);
            
        }
    }
}
