using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    private int mobchooser;
    private float timermob;
    private int rapidityspawn = 10;
    private float timertospawn = 0;
    private int spawnerchose;
    private float changerapidity;

    private GameObject[] spawners; // List of all the spawner in the scene
    private bool[] spawnerUsed; // List of all the spawner in the scene
    private GameObject[] mobs; // List of all the mob in the scene
    private int[] mobToSpawner; // List of all the mob linked to a spawner
    void Start()
    {
        timermob = Random.Range(5, rapidityspawn); // Spawn a mob in an seconds choose randomly bewteen 0 and rapidityspawn
        spawners = GameObject.FindGameObjectsWithTag("spawner"); // Find all the spawner in the scene
        mobs = GameObject.FindGameObjectsWithTag("Mob"); // Find all the mob in the scene
        
        spawnerUsed = new bool[spawners.Length]; // Create a list of boolean to know if the spawner is used or not
        for (int i = 0; i < spawners.Length; i++)
        {
            spawnerUsed[i] = false; // Set all the spawner to not used
        }
        
        mobToSpawner = new int[mobs.Length]; // Create a list of boolean to know if the mob is linked to a spawner or not
        for (int i = 0; i < mobs.Length; i++)
        {
            mobToSpawner[i] = -1; // Set all the mob to not linked to a spawner
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timertospawn <= timermob)
        {
            timertospawn += Time.deltaTime; // Let the mob spawn when timer is passed
        }
        else
        {
            mobchooser = Random.Range(0, mobs.Length);
            timermob = Random.Range(0, rapidityspawn);
            spawnerchose = Random.Range(0, spawners.Length);
            timertospawn = 0;
            
            // Reset the spawner if the chosen mob was previously assigned to a spawner
            if (mobToSpawner[mobchooser] != -1)
            {
                spawnerUsed[mobToSpawner[mobchooser]] = false; // Mark the previous spawner as unused
            }
            
            if (!spawnerUsed[spawnerchose])
            {
                    
                mobs[mobchooser].transform.position =
                    spawners[spawnerchose].transform.position; // Spawn the mob at the position of the spawner
                spawnerUsed[spawnerchose] = true; // Set the spawner to used
                mobToSpawner[mobchooser] = spawnerchose; // Link the mob to the spawner
            }
        }
        
        /* Debug.Log("["+spawnerUsed[0]+","+spawnerUsed[1]+","+spawnerUsed[2]+","+spawnerUsed[3]+","+spawnerUsed[4]+"," +
                  spawnerUsed[5]+","+spawnerUsed[6]+","+spawnerUsed[7]+","+spawnerUsed[8]+"]"); */
        
        changerapidity += Time.deltaTime;
        if (changerapidity >= 60)
        {
            changerapidity = 0;
            rapidityspawn--; //Each minutes the rapidityspawn is decrement of 1
        }
    }
}
