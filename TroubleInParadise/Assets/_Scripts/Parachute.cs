using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour
{
    [Header("Box collider of the the object")]
    [SerializeField] BoxCollider2D collider;
    [Header("The total time that the parachute is not allowed to be collected after drop")]
    [SerializeField] float timeOfNoCollection;

    private bool dropped = false;
    private float timer = 0.0f;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(dropped)
        {
            timer += Time.deltaTime;
            if(timer >= timeOfNoCollection)
            {
                timer = 0.0f;
                dropped = false;
                collider.enabled = true;
            }
        }
	}

    public void Dropped()
    {
        dropped = true;
        collider.enabled = false;
    }
}
