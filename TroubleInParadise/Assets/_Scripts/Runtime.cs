﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Runtime : MonoBehaviour
{
    [Header("The total time the game needs to run")]
    public float totalFallTime = 120.0f;

    //current percentage of slider
    //private float currentpercentage = 0.0f;

    public Slider fallingslider;
    
    //changes at which points the sprite changes
    public float firstPointPercent = 0.0f;
    public float secondPointPercent = 0.5f;
    public float thirdPointPercent = 1.0f;

    //3 different objects that slider head becomes
    public GameObject firstSprite = null;
    public GameObject secondSprite = null;
    public GameObject thirdSprite = null;
    //the knob
    public GameObject sliderHeadSprite = null;

    [Header("The 2 players")]
    [SerializeField] Player player1;
    [SerializeField] Player player2;
    [Header("The fall speed difference between having parachute and without")]
    [SerializeField] float fallSpeedWithoutParachute;
    [SerializeField] float fallSpeedWithParachute;
    [Header("The bottom collision box which should disappear")]
    [SerializeField] BoxCollider2D bottomBoxCollider;

    private float gameTime = 0.0f;

    private Player playerThatWon;

    private int gameOver;
    // Use this for initialization
    void Start()
    {
        fallingslider.value = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //updates the current percentage
        slidervalue();

        gameTime += Time.deltaTime;
        //Trigger end scene
        if (gameTime >= totalFallTime)
        {
            //Player 1 has the parachute
            if (player1.HasParachute() == 1)
            {
                playerThatWon = player1;
            }
            //Player 2 has the parachute
            else if (player2.HasParachute() == 1)
            {
                playerThatWon = player2;
            }
            //No player has parachute
            else
            {
                playerThatWon = null;
            }

            //locks player controls and sets velocity of the x to 0
            if (player1 != playerThatWon)
            {
                player1.LockControls(fallSpeedWithoutParachute);
            }
            else
            {
                player1.LockControls(fallSpeedWithParachute);
            }
            if (player2 != playerThatWon)
            {
                player2.LockControls(fallSpeedWithoutParachute);
            }
            else
            {
                player2.LockControls(fallSpeedWithParachute);
            }

            bottomBoxCollider.enabled = false;
            gameOver = 1;
        }
        if (gameOver == 1)
        {
            float cameraZPos = Camera.main.transform.position.z;
            if (player1 == playerThatWon)
            {
                if (Camera.main.transform.position.y > player2.transform.position.y)
                {
                    Camera.main.transform.position = new Vector3(0.0f, player2.transform.position.y, cameraZPos);
                }
            }
            else if (player2 == playerThatWon)
            {
                if (Camera.main.transform.position.y > player1.transform.position.y)
                {
                    Camera.main.transform.position = new Vector3(0.0f, player1.transform.position.y, cameraZPos);
                }
            }
            else if (player1 != playerThatWon && player2 != playerThatWon)
            {
                float averageYPos = (player2.transform.position.y + player1.transform.position.y) / 2;
                //
                if (Camera.main.transform.position.y > averageYPos)
                {
                    Camera.main.transform.position = new Vector3(0.0f, averageYPos, cameraZPos);
                }
            }
        }
    }

    //Updates the current percentage
    public void slidervalue()
    {
        float singlePercentage = 100.0f/totalFallTime;
        fallingslider.value = gameTime * singlePercentage * Time.deltaTime;
        firstVal();
        secondVal();
        thirdVal();
        if (fallingslider.value > 1.0f)
            fallingslider.value = 1.0f;

    }
    //first value effect
    public void firstVal()
    {
        if (fallingslider.value >= firstPointPercent && fallingslider.value < secondPointPercent)
        {
            sliderHeadSprite = firstSprite;
        }
    }
    //second value effect
    public void secondVal()
    {
        if (fallingslider.value >= secondPointPercent && fallingslider.value < thirdPointPercent)
        {
            sliderHeadSprite = secondSprite;
        }
    }
    //third value effect
    public void thirdVal()
    {
        if (fallingslider.value >= thirdPointPercent)
        {
            sliderHeadSprite = thirdSprite;
        }
    }
}
