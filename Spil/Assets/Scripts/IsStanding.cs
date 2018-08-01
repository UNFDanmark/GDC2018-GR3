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

    public void Ded()
    {
        playermovement.playerAudioSource.PlayOneShot(playermovement.deathSound);
        print("Player " + playermovement.PlayerID + " died");
        GetComponentInParent<PlayerMovement>().lives--;
        if (GetComponentInParent<PlayerMovement>().lives <= 0)
        {
            isded = true;
            UIHandler.GetComponent<UIController>().livingPlayers.Remove(GetComponentInParent<PlayerMovement>().PlayerID);
        }
        else Respawn();
    }

    void Respawn()
    {
        print("Respawning player " + playermovement.PlayerID);
        int platform = Random.Range(0, 4);
        if (platform == 0)
        {
            playerTransform.position = new Vector3(6, 3.5f, 0);
        }
        else if (platform == 1)
        {
            playerTransform.position = new Vector3(-6, 3.5f, 0);
        }
        else if (platform == 2)
        {
            playerTransform.position = new Vector3(0, 8, 0);
        }
        else if (platform == 3)
        {
            playerTransform.position = new Vector3(0, 0, 0);
        }
        gameObject.GetComponentInParent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
}
