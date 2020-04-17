﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	#region Enums

	#endregion

	#region Public Variables

	#endregion

	#region Private Variables
	private GameObject pauseMenu;
	#endregion

	// Unity Named Methods
	#region Main Methods
	private void Start()
	{
		GameObject.Find("NewGameButton").GetComponent<Button>().Select();
		GameObject.FindObjectOfType<SaveManager>().ShowFiles();
		Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		if(player != null)
		{
			player.FreezePlayer();
			player.canvasFadeImage.color = Color.clear; // make image transparent
		}
	}

	/// Check every frame if the user has hit the "end" key to open the developer menu
	void Update()
	{
		/*if (Input.GetKeyDown(KeyCode.End))
		{
			SceneManager.LoadScene("DevMenu");
		}*/
	}
	#endregion

	#region Utility Methods
	/// Loads a new game
	public void NewGame()
	{
		//FindObjectOfType<AudioManager>().StartBackground();
		SceneManager.LoadScene("Hub");
		Globals.StartNewGame();
	}
	
	/// Loads the game
	public void PlayGame()
	{
		SceneManager.LoadScene("Hub");
	}

	/// continues the game from the last scene the player was in before dying
	public void ContinueGame()
	{
		//FindObjectOfType<AudioManager>().StartBackground();
		SceneManager.LoadScene(Globals.sceneToLoad);
	}

	public void PlayCredits()
	{
		SceneManager.LoadScene("Credits");
	}

	/// Quits the game
	public void QuitGame()
	{
		Debug.Log("QUIT");
		Application.Quit();
	}
	#endregion
 
	#region Coroutines

	#endregion
}