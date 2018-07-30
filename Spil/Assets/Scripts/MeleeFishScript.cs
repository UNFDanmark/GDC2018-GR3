using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFishScript : MonoBehaviour {
    public bool hitting = false;
    public float fishKnockback = 12.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider otherPlayer)
    {
        //If touching a player that is not you and hitting with fish
        if (((otherPlayer.name == "Player 1" || otherPlayer.name == "Player 2") && otherPlayer.name != transform.parent.name) && hitting == true)
        {
            print("hitting");
            string playerDirection = GetComponentInParent<PlayerMovement>().lastDirection;
            otherPlayer.GetComponent<PlayerMovement>().Knockback(fishKnockback, playerDirection);
            hitting = false;
        }
    }
}
