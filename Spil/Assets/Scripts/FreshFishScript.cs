using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreshFishScript : MonoBehaviour {

    public bool flying = true;
    public Rigidbody thisFish;
    public GameObject fishSpawner;

    void Awake()
    {
        fishSpawner = GameObject.FindGameObjectWithTag("Spawner");
        thisFish = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (flying) transform.LookAt(transform.position + thisFish.velocity);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && ((collider.GetComponentInParent<PlayerMovement>().hasFish == false) || GetComponent<PlayerMovement>().hasFish == false))
        {
            collider.GetComponentInParent<PlayerMovement>().hasFish = true;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        flying = false;
        if (collision.gameObject.tag == "Water")
        {
            fishSpawner.GetComponent<FishSpawnerScript>().fishOnScreen--;
            Destroy(gameObject);
        }
    }
}
