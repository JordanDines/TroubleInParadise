using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Players 2D Rigidbody")]
    [SerializeField] Rigidbody2D rigid;
    [Header("Players character object( the objects child)")]
    [SerializeField] GameObject visualAssest;
    [Header("Box Collider 2D for the attacking side")]
    [SerializeField] BoxCollider2D attackSideCollider;
    [Header("The positive and negative offset for the colliders")]
    [SerializeField] float xOffset1;
    [SerializeField] float xOffset2;
    [Header("The amount of force to be applied to the player")]
    [SerializeField] float xMovementForce;
    [SerializeField] float yMovementForce;
    [Header("The maximum x and y velocity in both direction")]
    [SerializeField] float maxXVelocity;
    [SerializeField] float maxYVelocity;

    //0: left
    //1: right
    private int side;
    private Vector2 leftForce;
    private Vector2 rightForce;
    private Vector2 upForce;
    private Vector2 downForce;
    
	// Use this for initialization
	void Start ()
    {
        leftForce = new Vector2(-xMovementForce, 0.0f);
        rightForce = new Vector2(xMovementForce, 0.0f);
        upForce = new Vector2(0.0f, maxYVelocity);
        downForce = new Vector2(0.0f, -maxYVelocity);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Sets the side and direction of collider and player
		if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //sets the side to left
            side = 0;
            ChangeSides();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            //sets the side to right
            side = 1;
            ChangeSides();
        }

        //For movement
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            if (rigid.velocity.x >= -maxXVelocity)
            {
                rigid.AddForce(leftForce);
            }
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            if (rigid.velocity.x <= maxXVelocity)
            {
                rigid.AddForce(rightForce);
            }
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            if(rigid.velocity.y <= maxYVelocity)
            {
                rigid.AddForce(upForce);
            }
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            if(rigid.velocity.y >= -maxYVelocity)
            {
                rigid.AddForce(downForce);
            }
        }

	}

    void ChangeSides()
    {
        float yOffset = attackSideCollider.offset.y;
        //Left side
        if(side == 0)
        {
            //sets the collider x offset to the corrisponding value
            attackSideCollider.offset = new Vector2(xOffset2, yOffset);
            //Change the direction of the player assets here
        }
        //Right side
        else if(side == 1)
        {
            //sets the collider x offset to the corrisponding value
            attackSideCollider.offset = new Vector2(xOffset1, yOffset);
            //Change the direction of the player assets here
        }
    }
}
