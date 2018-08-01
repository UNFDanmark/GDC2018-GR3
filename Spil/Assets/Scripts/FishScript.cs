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
    public GameObject fishOnGround;
    public GameObject fishSpawner;

    void Awake()
    {
        fishSpawner = GameObject.FindGameObjectWithTag("Spawner");
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
        /*if (flying) transform.LookAt(transform.position + fishRigidbody.velocity);*/
        if (fishRigidbody.velocity.x >= 0) direction = "right";
        else direction = "left";
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            Destroy(gameObject);
            fishSpawner.GetComponent<FishSpawnerScript>().fishOnScreen--;
        }
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Fish")
        {
            flying = false;
            Destroy(gameObject);
            GameObject newFishOnGround = Instantiate(fishOnGround, transform.position, transform.rotation);
            newFishOnGround.GetComponent<Rigidbody>().velocity = fishRigidbody.velocity;    
            pickUpAble = true;
        }
        if (collision.gameObject.tag == "Player")
        {if (flying)
            {
                flying = false;
                collision.gameObject.GetComponent<PlayerMovement>().Knockback(5, direction);
                collision.gameObject.GetComponent<PlayerMovement>().damageTaken += 0.1f;
            }
        }
    }

}
