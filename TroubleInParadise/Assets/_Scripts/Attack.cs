﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float TimeBetweenAttacks = 0.5f;
    float timer = 0.0f;

    public string playerTag = "Player";
    bool playerInRange = false; 

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= TimeBetweenAttacks)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                attack();
            }
        }
    }

    void attack()
    {
        if (playerInRange)
        {
            if (this.GetComponent<Grab>().IsHolding)
            {
                //knock parachute out of other player, nudge them back
                //Hit();

                this.GetComponent<Grab>().releaseOBJ();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == playerTag && collision.gameObject != this)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }
}
