using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    //private rigibody variable
    Rigidbody2D rb;
    //private bool for checking if the player is holding something
    bool isHeld = false;


    public BoxCollider2D externalCollider = null;

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
        if (isHeld)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        //resets dropped object
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

    }
    //trigger event to check if the player is close enough to the gameobject
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == playersTag && !this.isHeld)
        {
            if (!other.gameObject.GetComponent<Grab>().IsHolding)
            {
                this.isHeld = true;
                this.transform.SetParent(other.transform);
                externalCollider.enabled = false;
            }
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
