using System.Collections;
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
    public float handFishDistance = 0.65f;
    public int grounded = 0;
    public int damageTaken = 0;
    public int PlayerID;
    public string lastDirection = "right";
    public KeyCode throwButton = KeyCode.Comma;
    public KeyCode hitButton = KeyCode.Period;
    public bool hasFish = true;
    public bool canAirjump = true;
    public bool stunned = false;
    public float stunHeight = 100;

    // Use this for initialization
    void Start () {
        //Players look towards the middle when spawning
        if (rigidBody.transform.position.x > 0) lastDirection = "right";
        else lastDirection = "left";
	}


    // Update is called once per frame
    void Update()
    {
        //Set lastDirection to "left" or "right" when the buttons are pressed
        lastDirection = GetDirection(Input.GetAxis("Horizontal" + PlayerID));
        //Make handFish point the right direction
        if (lastDirection == "right") meleeFish.transform.localPosition = new Vector3(0.65f, 0, 0);
        else meleeFish.transform.localPosition = new Vector3(-0.65f, 0, 0);
        //Show your fishes!
        if (hasFish) seeFish.enabled = true;
        else seeFish.enabled = false;
        //Throw fish if ya wanna
        if (Input.GetKeyDown(throwButton))
        {
            Throw();
        }

        //Jump if you can and want
        if (Input.GetButtonDown("Jump" + PlayerID) && stunned == false)
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
        if (stunned)
        {
            if (rigidBody.position.y < stunHeight || grounded > 0)
            {
                stunned = false;
            }
        }
        else
        {
            Move(Input.GetAxis("Horizontal" + PlayerID), acceleration);
        }
    }
    

    string GetDirection(float number)
    {
        if (number > 0) return "right";
        else if (number < 0) return "left";
        else return lastDirection;
    }

    void Move(float axis, float accelSpeed)
    {
        float xChange = axis * accelSpeed;
        if (rigidBody.velocity.x > MaxSpeed || rigidBody.velocity.x < -MaxSpeed)
        {
            xChange = 0;
        }
        if (axis == 0) xChange = -(rigidBody.velocity.x * 0.3f);

        rigidBody.velocity = new Vector3(rigidBody.velocity.x + xChange, rigidBody.velocity.y, rigidBody.velocity.z);
    }

    void Jump()
    {
        canAirjump = true;
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

    void Hit()
    {
        
    }

    public void Knockback(float strength, string direction)
    {
        int xModifyer = 1;
        if (direction == "left") xModifyer = -1;
        float knockbackMultiplier = (damageTaken + 1) * damageMultiplier;
        Vector3 knockbackVector = new Vector3(1 * xModifyer, 0.3f, 0) * knockbackMultiplier * strength;
        
        rigidBody.velocity = knockbackVector;
        stunned = true;
        stunHeight = rigidBody.position.y;
    }
}
