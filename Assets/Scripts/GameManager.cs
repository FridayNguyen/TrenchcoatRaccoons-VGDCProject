﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void LoadGameWon()
    {
        SceneManager.LoadScene("GameWon");
    }

    public void Restart()
    {
        SceneManager.LoadScene("SceneWithBullet");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
