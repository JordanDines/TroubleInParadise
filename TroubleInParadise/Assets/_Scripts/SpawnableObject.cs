﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    //private rigibody variable
    Rigidbody2D rb;
    //private bool for checking if the player is holding something
    bool isHeld = false;

    bool canHold = true;
    //default value for parent
    GameObject storeParent = null;
    //the distance from the player the object will be.
    public Vector2 distanceFromPlayer = new Vector2(1, 0);
   
    public string playersTag = "Player";

    //Awake function
    void Awake()
    {
        //initilises the rigidbody
        rb = this.GetComponent<Rigidbody2D>();

    }

    //updates the spawnable objects
    void Update()
    {
        //checks if the object has been picked up
        if (isHeld && storeParent != null && !canHold)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            int dir = storeParent.GetComponent<Player>().GetDirection();
            this.transform.position = storeParent.transform.position + new Vector3((float)dir * distanceFromPlayer.x, distanceFromPlayer.y, 0);
        }
        //resets dropped object
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            this.transform.SetParent(null);
            canHold = true;
        }

    }
    //trigger event to check if the player is close enough to the gameobject
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameObject.FindGameObjectsWithTag(playersTag)[0] && other.gameObject != this.gameObject && canHold)
        {
            this.transform.SetParent(other.transform);
            storeParent = other.gameObject;
            canHold = false;
            isHeld = true;
        }

        if (other.gameObject == GameObject.FindGameObjectsWithTag(playersTag)[1] && other.gameObject != this.gameObject && canHold)
        {
            this.transform.SetParent(other.transform);
            storeParent = other.gameObject;
            canHold = false;
            isHeld = true;
        }
    }   
    
    //Getter
    public bool getIsHeld()
    {
        return isHeld;
    }
    //Setter
    public void setIsheld(bool holding)
    {
        isHeld = holding;
    }
}
