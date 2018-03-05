using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject MaxPoint;
    public GameObject MinPoint;

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
            int rand = Random.Range((int)MinPoint.transform.position.x, (int)MaxPoint.transform.position.x);
            Vector3 spawnLocation = new Vector3(rand, this.transform.position.y, this.transform.position.z);
            int randDrop = Random.Range(0, pickupArray.Length);
            //creates the spawnable object in the scene
            Instantiate(pickupArray[randDrop], spawnLocation, Quaternion.identity);
            //resets counetr
            m_counter = 0.0f;
        }

    }
}
