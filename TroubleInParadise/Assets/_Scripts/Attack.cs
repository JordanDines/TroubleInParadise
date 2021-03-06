using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] Controls controls;

    public float TimeBetweenAttacks = 0.5f;
    private GameObject player;
    float timer = 0.0f;

    public float slapForce = 10.0f;

    public string playerTag = "Player";
    bool playerInRange = false; 

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= TimeBetweenAttacks)
        {
            if (Input.GetKey(controls.attack))
            {
                attack();
            }
        }
    }

    void attack()
    {
        if (!playerInRange)
        {
            if (this.GetComponent<Grab>().IsHolding)
            {

            }
            else
            {

            }
        }

        if (playerInRange)
        {
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(GetComponent<Player>().GetDirection() * 1 * slapForce, 0));
            if (this.GetComponent<Grab>().IsHolding)
                {
                    //knock parachute out of other player, nudge them back
                    this.GetComponent<Grab>().releaseOBJ();

                    if (player.GetComponent<Player>().HasParachute() == 1)
                        player.GetComponent<Player>().Hit();
                }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == playerTag && collision.gameObject != this) {
			player = collision.gameObject;
			playerInRange = true;
		}
	}

	void OnTriggerExit2D(Collider2D collision) 
	{
			playerInRange = false;
	}
}

