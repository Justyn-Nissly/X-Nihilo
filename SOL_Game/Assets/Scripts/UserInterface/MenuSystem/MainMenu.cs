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
	public Dropdown firstNameField,
					middleNameField,
					lastNameField;
	List<string> names = new List<string> { "option1", "option2", "option3" };
	#endregion

	#region Private Variables
	private GameObject pauseMenu;
	private PasswordGenerator password;
	private int firstNameIndex,
				middleNameIndex,
				lastNameIndex;
	public static string passwordString;
	public string m_Path;//////////////////
	#endregion

	// Unity Named Methods
	#region Main Methods
	void Start()
	{
		firstNameField.AddOptions(names);
	}
	/// Check every frame if the user has hit the "end" key to open the developer menu
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.End))
		{
			SceneManager.LoadScene("DevMenu");
		}
	}
	#endregion

	#region Utility Methods
	/// Create a new Game
	public void CreateNewGame()
	{
		int index = 2;
		//FindObjectOfType<AudioManager>().StartBackground();
		m_Path = Application.dataPath;

		//Output the Game data path to the console
		Debug.Log(Globals.firstName);
		/*Debug.Log("dataPath : " + m_Path);
		GameData.middleName = names[index];
		GameData.lastName   = names[index];
		GameData.password   = password.GetComponent<PasswordGenerator>().GetRandomPassword();
		SceneManager.LoadScene("Hub");*/
	}
	
	/// Create a random name for the user
	public void CreateRandomName()
	{
		// Get a random option from the list of names
		firstNameIndex  = Random.Range(0, 3);
		Debug.Log(firstNameIndex);
		middleNameIndex = Random.Range(0, 4);
		lastNameIndex   = Random.Range(0, 4);

		// Set the dropdown boxes to the new random index
		firstNameField.value  = firstNameIndex;
		Debug.Log(firstNameField.value);
		Debug.Log(names[firstNameIndex]);
		Globals.firstName = names[firstNameIndex];
		middleNameField.value = middleNameIndex;
		lastNameField.value   = lastNameIndex;
	}

	/// Load a game save
	public void LoadGame()
	{

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