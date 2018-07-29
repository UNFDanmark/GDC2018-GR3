using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour {

    public Rigidbody fishRigidbody;

    public float moveSpeed = 5;
    public float moveUpSpeed = 3;
    public bool pickUpAble = true;
    public bool flying = true;
    public string direction = "";

    void Awake()
    {
        fishRigidbody = GetComponent<Rigidbody>();
        if (fishRigidbody.velocity.x >= 0) direction = "right";
        else direction = "left";
    }

    // Use this for initialization
    void Start ()
    {
		
    }

    // Update is called once per frame
    void Update () {
        if (flying) transform.LookAt(transform.position + fishRigidbody.velocity);
        if (fishRigidbody.velocity.x >= 0) direction = "right";
        else direction = "left";
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Fish")
        {
            flying = false;
            pickUpAble = true;
        }
        if (collision.gameObject.tag == "Player")
        {
            if (pickUpAble == true && collision.gameObject.GetComponent<PlayerMovement>().hasFish == false)
            {
                Destroy(gameObject);
                collision.gameObject.GetComponent<PlayerMovement>().hasFish = true;
            }
            else if (flying)
            {
                collision.gameObject.GetComponent<PlayerMovement>().Knockback(5, direction);
            }
        }
    }

}
