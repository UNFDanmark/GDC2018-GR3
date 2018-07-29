using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsStanding : MonoBehaviour {
    public bool debugging = true;

    public bool onGround = false;
    public PlayerMovement playermovement;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (playermovement.grounded > 0) onGround = true;
	}

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            playermovement.grounded++;
            playermovement.canAirjump = true;
        }
        else if (collision.gameObject.tag.ToLower() == "water" && debugging)
        {
            playermovement.transform.Translate(new Vector3(0, 30, 0));
        }
    }

    private void OnTriggerExit(Collider bye)
    {
        if(bye.gameObject.tag == "Platform") playermovement.grounded--;
    }
}
