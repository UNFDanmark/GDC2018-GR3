using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float movspd = 1337;
    public float jumpForce = 10;
    public int PlayerID;
    // Use this for initialization
    /*void Start () {
		
	}*/


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump" + PlayerID))
        {
            jump();
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

    void jump()
    {
        rigidBody.AddForce(jumpForce * transform.up, ForceMode.Impulse);
    }
}
