using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public BoxCollider2D attackBox;

    public float modificationFactor = 1;

    public string pickupObjectTag = "Pickup";
    public GameObject heldOBJ = null;

    public bool IsHolding = false;

    void Awake()
    {
    }
    void Update()
    {
        if (this.GetComponentInChildren<SpawnableObject>() != null)
        {
            if (this.GetComponentInChildren<SpawnableObject>().getIsHeld())
            {
                int dir = this.GetComponent<Player>().GetDirection();
                heldOBJ.transform.position = this.transform.position + new Vector3(dir * modificationFactor, 0, 0);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == pickupObjectTag && IsHolding == false)
        {
            if (collision.GetComponent<SpawnableObject>().getIsHeld() == true && collision.gameObject.transform.parent == this.gameObject.transform)
            {
               heldOBJ = collision.gameObject;
               IsHolding = true;
            }
        }
    }
}
