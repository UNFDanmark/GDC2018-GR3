﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {
    public PlayerMovement player1;
    public PlayerMovement player2;
    public TextMeshProUGUI damage1;
    public TextMeshProUGUI damage2;
    public Text winScreen;
    public List<int> livingPlayers = new List<int>();
    public int winner = 9001;
    public GameObject[] p1HP = new GameObject[3];
    public GameObject[] p2HP = new GameObject[3];
    public GameObject[] p1ded = new GameObject[3];
    public GameObject[] p2ded = new GameObject[3];
    public GameObject soundPlayer;

    void Awake()
    {

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        PrintDamageText();
        ShowLives();
        if (livingPlayers.Count == 1)
        {
            winner = livingPlayers[0];
            winScreen.text = "Player " + winner + " wins";
            soundPlayer.GetComponent<SoundPlayerScript>().WanSound();
        }
        else if (livingPlayers.Count == 0)
        {
            if (winner != 9001) winScreen.text = "Player " + winner + " died last.\nThat's still a win.";
            else winScreen.text = "Both players died at the same time.";
        }
        else winScreen.text = "";
    }

    void PrintDamageText()
    {
        damage1.text = (player1.damageTaken * 100).ToString();
        damage2.text = (player2.damageTaken * 100).ToString();
    }

    void ShowLives()
    {
        for (int life = 0; life < 3; life++)
        {
            if (player1.lives > life)
            {
                p1HP[life].SetActive(true);
                p1ded[life].SetActive(false);
            }
            else
            {
                p1HP[life].SetActive(false);
                p1ded[life].SetActive(true);
            }
            if (player2.lives > life)
            {
                p2HP[life].SetActive(true);
                p2ded[life].SetActive(false);
            }
            else
            {
                p2HP[life].SetActive(false);
                p2ded[life].SetActive(true);
            }
        }
    }
}
