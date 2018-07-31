using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsStanding : MonoBehaviour {
    public bool isded = false;
    public bool onGround = false;
    public PlayerMovement playermovement;
    public GameObject UIHandler;
    public Transform playerTransform;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (playermovement.grounded > 0) onGround = true;
        if ((playerTransform.position.x < -15 || playerTransform.position.x > 15) && isded == false)
        {
            Ded();
        }
        if (isded) playermovement.stunned = true; 
	}

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            playermovement.grounded++;
            playermovement.canAirjump = true;
        }
        else if (collision.gameObject.tag == "Water" && isded == false)
        {
            Ded();
        }
    }

    private void OnTriggerExit(Collider bye)
    {
        if(bye.gameObject.tag == "Platform") playermovement.grounded--;
    }

    void Ded()
    {
        isded = true;
        UIHandler.GetComponent<UIController>().livingPlayers.Remove(GetComponentInParent<PlayerMovement>().PlayerID);
    }
}
