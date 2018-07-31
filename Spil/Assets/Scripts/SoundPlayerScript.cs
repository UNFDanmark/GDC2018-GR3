using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayerScript : MonoBehaviour {
    public AudioSource soundPlayer;
    public AudioClip sangLoop;
    public AudioClip win;
    private bool playerWan = false;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

		if (!soundPlayer.isPlaying && !playerWan)
        {
            soundPlayer.clip = sangLoop;
            soundPlayer.Play();
            
        }
	}

    public void WanSound()
    {
        if (!playerWan)
        {
            playerWan = true;
            soundPlayer.Stop();
            soundPlayer.clip = win;
            soundPlayer.Play();
        }
    }
}
