using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float movspd = 1337;
    public float jumpForce = 10;
    public float secondJumpMultiplier = 0.9f;
    public int PlayerID;
    public string lastDirection = "none";

    public bool grounded = true;
    public bool canAirjump = true;

    // Use this for initialization
    void Start () {
        if (Input.GetAxis("Horizontal" + PlayerID) > 0) lastDirection = "right";
        else lastDirection = "left";
	}


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump" + PlayerID))
        {
            //First jump
            if (grounded) Jump();
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
        Move(movspd * Input.GetAxis("Horizontal"+PlayerID));
    }

    
    void Move(float speed)
    {
        rigidBody.velocity = (Vector3.up * rigidBody.velocity.y) + (transform.right * speed);
    }

    void Jump()
    {
        grounded = false;
        rigidBody.velocity = new Vector3 (rigidBody.velocity.x, 0 , rigidBody.velocity.z);
        rigidBody.AddForce(jumpForce * transform.up, ForceMode.Impulse);
    }
}
