using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Number")]
    [SerializeField] int playerNum;
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
    [Header("The amount of force applied on contact")]
    [SerializeField] float contactForceAmount;

    //0: left
    //1: right
    private int side;
    private int hasParachute;
    private int lockControls;

    private Vector2 leftForce;
    private Vector2 rightForce;
    private Vector2 upForce;
    private Vector2 downForce;

    private KeyCode left;
    private KeyCode right;
    private KeyCode up;
    private KeyCode down;

    // Use this for initialization
    void Start ()
    {
        leftForce = new Vector2(-xMovementForce, 0.0f);
        rightForce = new Vector2(xMovementForce, 0.0f);
        upForce = new Vector2(0.0f, maxYVelocity);
        downForce = new Vector2(0.0f, -maxYVelocity);

        lockControls = 0;

        if(playerNum == 1)
        {
            left = KeyCode.A;
            right = KeyCode.D;
            up = KeyCode.W;
            down = KeyCode.S;
        }
        else if(playerNum == 2)
        {
            left = KeyCode.LeftArrow;
            right = KeyCode.RightArrow;
            up = KeyCode.UpArrow;
            down = KeyCode.DownArrow;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (lockControls == 0)
        {
            //Sets the side and direction of collider and player
            if (Input.GetKeyDown(left))
            {
                //sets the side to left
                side = -1;
                ChangeSides();
            }
            else if (Input.GetKeyDown(right))
            {
                //sets the side to right
                side = 1;
                ChangeSides();
            }

            //For movement
            if (Input.GetKey(left))
            {
                if (rigid.velocity.x >= -maxXVelocity)
                {
                    rigid.AddForce(leftForce);
                }
            }
            else if (Input.GetKey(right))
            {
                if (rigid.velocity.x <= maxXVelocity)
                {
                    rigid.AddForce(rightForce);
                }
            }
            if (Input.GetKey(up))
            {
                if (rigid.velocity.y <= maxYVelocity)
                {
                    rigid.AddForce(upForce);
                }
            }
            else if (Input.GetKey(down))
            {
                if (rigid.velocity.y >= -maxYVelocity)
                {
                    rigid.AddForce(downForce);
                }
            }
        }

	}

    void ChangeSides()
    {
        float yOffset = attackSideCollider.offset.y;
        //Left side
        if(side == -1)
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
    
    public int HasParachute()
    {
        return hasParachute;
    }
    public int GetDirection()
    {
        return side;
    }
    public void LockControls(float fallvalue)
    {
        lockControls = 1;
        if (hasParachute == 1)
        {
            rigid.velocity = new Vector2(0.0f, -fallvalue);
        }
        else if(hasParachute == 0)
        {
            rigid.velocity = new Vector2(0.0f, -fallvalue);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Vector2 collisionDirection = collision.contacts[0].point - (Vector2)this.transform.position;
            collisionDirection = -collisionDirection.normalized;

            rigid.AddForce(collisionDirection * contactForceAmount, ForceMode2D.Impulse);
        }
    }
 
}
