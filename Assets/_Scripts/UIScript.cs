﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startGame()
	{
		SceneManager.LoadScene ("scene1");
	}

    public void QuitGame()
    {
        Application.Quit();
    }
}
