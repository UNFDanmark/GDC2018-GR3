﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rigidBody;
    public GameObject fishPrefab;
    public GameObject meleeFish;
    public Renderer seeFish;
    public CapsuleCollider touchFish;

    public float acceleration = 0.01f;
    public float MaxSpeed = 0.02f;
    public float jumpHeight = 10;
    public float secondJumpMultiplier = 0.9f;
    public float fishOffset = 0.2f;
    public float fishMoveSpeed = 5;
    public float fishSpeedUp = 1.5f;
    public float damageMultiplier = 1.01f;
    public int grounded = 0;
    public int damageTaken = 0;
    public int PlayerID;
    public string lastDirection = "none";
    public KeyCode throwButton = KeyCode.Comma;
    public KeyCode hitButton = KeyCode.Period;
    public bool hasFish = true;
    public bool canAirjump = true;

    // Use this for initialization
    void Start () {
        if (rigidBody.transform.position.x > 0) lastDirection = "right";
        else lastDirection = "left";
	}


    // Update is called once per frame
    void Update()
    {
        lastDirection = GetDirection(Input.GetAxis("Horizontal" + PlayerID));
        if (lastDirection == "right") meleeFish.transform.localPosition = new Vector3(0.65f, 0, 0);
        else meleeFish.transform.localPosition = new Vector3(-0.65f, 0, 0);
        if (hasFish) seeFish.enabled = true;
        else seeFish.enabled = false;
        if (Input.GetKeyDown(throwButton))
        {
            Throw();
        }


        if (Input.GetButtonDown("Jump" + PlayerID))
        {
            //First jump
            if (grounded > 0) Jump();
            //Double jump
            else if (canAirjump)
            {
                Jump();
                canAirjump = false;
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y * secondJumpMultiplier, rigidBody.velocity.z);
            }
        }
    }

    private void FixedUpdate()
    {
        Move(Input.GetAxis("Horizontal" + PlayerID), acceleration);
    }
    

    string GetDirection(float number)
    {
        if (number > 0) return "right";
        else if (number < 0) return "left";
        else return lastDirection;
    }

    void Move(float axis, float accelSpeed)
    {
        /*{
            axis *= (1 + (MaxSpeed - rigidBody.velocity.x) * 0.1f);
        }*/
        if (axis == 0) axis = -(rigidBody.velocity.x * 1);
        rigidBody.velocity = new Vector3(rigidBody.velocity.x + (axis * accelSpeed), rigidBody.velocity.y, rigidBody.velocity.z);

        if (rigidBody.velocity.x > MaxSpeed)
        {
            rigidBody.velocity = new Vector3(MaxSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
        }
        if (rigidBody.velocity.x < -MaxSpeed)
        {
            rigidBody.velocity = new Vector3(-MaxSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
        }
    }

    void Jump()
    {
        rigidBody.velocity = new Vector3 (rigidBody.velocity.x, jumpHeight , rigidBody.velocity.z);
    }

    void Throw()
    {
        if (hasFish)
        {
            hasFish = false;
            GameObject thrownFish = Instantiate(fishPrefab);
            float x;
            if (lastDirection == "right") x = 1;
            else x = -1;
            thrownFish.transform.position = transform.position + new Vector3(x * 0.65f, 0.1f, 0);
            thrownFish.GetComponent<Rigidbody>().velocity = rigidBody.velocity + new Vector3(fishMoveSpeed * x, fishSpeedUp, 0);
            thrownFish.GetComponent<FishScript>().pickUpAble = false;
        }
    }

    void Hit()   //REMEMBER DIS!
    {

    }

    public void Knockback(float strength, string direction)
    {
        int xModifyer = 1;
        if (direction == "left") xModifyer = -1;
        float knockbackMultiplier = (damageTaken + 1) * damageMultiplier;
        Vector3 knockbackVector = new Vector3(10 * xModifyer, 1, 0) * knockbackMultiplier * strength;
        
        rigidBody.velocity = knockbackVector;

    }
}
