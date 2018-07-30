using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishOnGroundScript : MonoBehaviour {

    public Rigidbody fishRigidbody;

    public float moveSpeed = 5;
    public float moveUpSpeed = 3;
    public bool pickUpAble = true;
    public bool flying = true;
    public string direction = "";
    public GameObject fishOnGround;

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

       private void OnTriggerEnter(Collider collision)
    {
        if (pickUpAble && collision.gameObject.tag == "Player" && collision.GetComponentInParent<PlayerMovement>().hasFish == false)
        {
            collision.GetComponentInParent<PlayerMovement>().hasFish = true;
            Destroy(gameObject);
        }
    }

}
