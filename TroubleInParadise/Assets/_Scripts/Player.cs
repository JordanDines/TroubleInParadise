﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Player : MonoBehaviour
{
    [Header("Player Number")]
    [SerializeField] int playerNum;
    [Header("Players 2D Rigidbody")]
    [SerializeField] Rigidbody2D rigid;
    [Header("Players character object( the objects child)")]
    [SerializeField] GameObject withoutParachute;
    [SerializeField] GameObject withParachute;
    [Header("Box Collider 2D for the attacking side")]
    [SerializeField] BoxCollider2D attackSideCollider;
    [Header("The amount of force to be applied to the player")]
    [SerializeField] float xMovementForce;
    [SerializeField] float yMovementForce;
    [Header("The maximum x and y velocity in both direction")]
    [SerializeField] float maxXVelocity;
    [SerializeField] float maxYVelocity;
    [Header("The amount of force applied on contact")]
    [SerializeField] float contactForceAmount;
    [Header("Make this the same as the object tag in grab class")]
    [SerializeField] string weaponTag;
    [Header("How long between pickups are allowed")]
    [SerializeField] float cooldownPickup = 5.0f;

    //0: left
    //1: right
    private int side;
    private int hasParachute;
    private int lockControls;

    private Vector2 leftForce;
    private Vector2 rightForce;
    private Vector2 upForce;
    private Vector2 downForce;
    private Vector2 weaponCollisionDirection;

    private float timer = 0.0f;

    private KeyCode left;
    private KeyCode right;
    private KeyCode up;
    private KeyCode down;

    private XboxCtrlrInput.XboxController controller;

    private bool hasController = false;
    private bool leftRotated = false;
    private bool rightRotated = false;
    private bool droppedParachute = false;

    private GameObject parachuteRefrence;
    // Use this for initialization
    void Start ()
    {
        withParachute.SetActive(false);

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

            if (XboxCtrlrInput.XCI.IsPluggedIn(1))
            {
                controller = XboxCtrlrInput.XboxController.First;
                hasController = true;
            }
        }
        else if(playerNum == 2)
        {
            left = KeyCode.LeftArrow;
            right = KeyCode.RightArrow;
            up = KeyCode.UpArrow;
            down = KeyCode.DownArrow;

            if (XboxCtrlrInput.XCI.IsPluggedIn(2))
            {
                controller = XboxCtrlrInput.XboxController.Second;
                hasController = true;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(droppedParachute)
        {
            timer += Time.deltaTime;
            if(timer >= cooldownPickup)
            {
                droppedParachute = false;
                timer = 0.0f;
            }
        }
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
            if (hasController)
            {
                if (XboxCtrlrInput.XCI.GetAxisRaw(XboxCtrlrInput.XboxAxis.LeftStickX, controller) < 0.0f)
                {
                    if (!leftRotated)
                    {
                        side = -1;
                        ChangeSides();
                        leftRotated = true;
                        rightRotated = false;
                    }
                    if (rigid.velocity.x >= -maxXVelocity)
                    {
                        rigid.AddForce(leftForce);
                    }
                }
                else if (XboxCtrlrInput.XCI.GetAxisRaw(XboxCtrlrInput.XboxAxis.LeftStickX, controller) > 0.0f)
                {
                    if (!rightRotated)
                    {
                        side = 1;
                        ChangeSides();
                        leftRotated = false;
                        rightRotated = true;
                    }
                    if (rigid.velocity.x <= maxXVelocity)
                    {
                        rigid.AddForce(rightForce);
                    }
                }
                if(XboxCtrlrInput.XCI.GetAxisRaw(XboxCtrlrInput.XboxAxis.LeftStickY, controller) < 0.0f)
                {
                    if (rigid.velocity.y >= -maxYVelocity)
                    {
                        rigid.AddForce(downForce);
                    }
                }
                else if(XboxCtrlrInput.XCI.GetAxisRaw(XboxCtrlrInput.XboxAxis.LeftStickY, controller) > 0.0f)
                {
                    if (rigid.velocity.y <= maxYVelocity)
                    {
                        rigid.AddForce(upForce);
                    }
                }
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
            //Change the direction of the player assets here
            this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
        }
        //Right side
        else if(side == 1)
        {
            //Change the direction of the player assets here
            this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 1.0f, 0.0f));
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
        if(collision.gameObject.tag == weaponTag)
        {
            weaponCollisionDirection = collision.contacts[0].point - (Vector2)this.transform.position;
            weaponCollisionDirection = -weaponCollisionDirection.normalized;
        }
    }
    public void Hit()
    {
        withParachute.SetActive(false);
        withoutParachute.SetActive(true);

        parachuteRefrence.SetActive(true);
        parachuteRefrence.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.9f);

        parachuteRefrence.GetComponent<Rigidbody2D>().AddForce(weaponCollisionDirection * 200, ForceMode2D.Force);

        hasParachute = 0;
        droppedParachute = true;
        parachuteRefrence = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Parachute" && droppedParachute == false)
        {
            parachuteRefrence = collision.gameObject;
            parachuteRefrence.SetActive(false);
            hasParachute = 1;
            withParachute.SetActive(true);
            withoutParachute.SetActive(false);
        }
    }

}
