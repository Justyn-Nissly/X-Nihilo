﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	#region Enums

	#endregion

	#region Public Variables
	public static bool       isPaused = false;  // The value for if the game is paused
	public GameObject pauseMenuUI;       // The UI object for the pause menu
	public string            sceneName;         // The name of the acctive scene
															  //public Canvas pauseMenuCanvas; // The canvas that houses the controls for the pause menu
															  //public Signal pauseSignal;
	#endregion

	#region Private Variables
	private PlayerControls
			inputActions;
	#endregion
	private void Start()
	{
		// this sets up the players input detection
		inputActions = new PlayerControls(); // this in the reference to the new unity input system
		inputActions.Gameplay.Enable();
	}

	// Unity Named Methods
	#region Main Methods
	void Update()
	{

		if (inputActions.Gameplay.PauseMenu.triggered)
		{
			PauseButtonPressed();
		}

		/// Load the development menu when 'End' is pressed
		if (Input.GetKeyDown(KeyCode.End))
		{
			SceneManager.LoadScene("DevMenu");
		}
	}
	#endregion

	#region Utility Methods
	/// <summary> called when the pause button has been pressed</summary>
	private void PauseButtonPressed()
	{
		InstantiatePauseMenu();
		if (isPaused)
		{
			Resume();
		}
		else
		{
			Pause();
		}
	}

	/// Instantiates the pause menu when the player presses escape
	public void InstantiatePauseMenu()
	{
		if (GameObject.Find("PauseMenuCanvas(Clone)") == null)
		{
			Instantiate(pauseMenuUI, new Vector3(0, 0, 0), Quaternion.identity);
		}
	}
	/// Resume the game
	public void Resume()
	{
		Destroy(GameObject.Find("PauseMenuCanvas(Clone)"));
		Time.timeScale = 1.0f;
		isPaused = false;	
	}

	/// Pause the game
	void Pause()
	{
		
		Time.timeScale = 0.0f;
		isPaused = true;
	}

	/// Quit to the main menu
	public void QuitGame()
	{
		SceneManager.LoadScene("Menu");
		//FindObjectOfType<AudioManager>().StopBackground();
		Time.timeScale = 1.0f;
		isPaused = false;
		Destroy(GameObject.Find("PauseMenuCanvas(Clone)"));
	}
	#endregion

	#region Coroutines

	#endregion
}