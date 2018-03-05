using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    //This is the array of spawners:
    // - Should be empty gaemObjects or can be solid object depending on the scene.
    // - Holds the transforms of the locations the spawnable objects spawn in.
    [Header("Spawners")]
    [Tooltip("Spawner array")]
    public GameObject[] Spawners;

    //Array of Pickups:
    // - holds all pickups that can be spawned.
    [Header("Pickups")]
    [Tooltip("add all objects that can be spawned in this array")]
    public GameObject[] pickupArray;

    //Spawn rate of spawnable objects in seconds
    [Header("variables")]
    [Tooltip("how often items spawn in seconds")]
    public float spawnRate = 2.0f;

    //holds a count as a default value for the counter
    private float m_counter = 0.0f;

    //update function, updates the main controller for the spawner
    void Update()
    {
        //increments counter in seconds
        m_counter += Time.deltaTime;
        //checks if enough time has passed to spawn in an object
        if (m_counter >= spawnRate)
        {
            //random digit for the spawn location
            int selectedPoint = Random.Range(0, Spawners.Length);
            //random digit for drop picking
            int randDrop = Random.Range(0, pickupArray.Length);
            //creates the spawnable object in the scene
            Instantiate(pickupArray[randDrop], Spawners[selectedPoint].transform);
            //resets counetr
            m_counter = 0.0f;
        }

    }
}
