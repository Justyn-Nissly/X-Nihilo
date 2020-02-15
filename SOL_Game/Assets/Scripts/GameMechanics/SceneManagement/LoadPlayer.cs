﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayer : MonoBehaviour
{
	#region Enums (Empty)
	#endregion

	#region Public Variables
	public Transform 
		defaultPlayerStartingPosition,
		altStartingPosition;

	public GameObject
		playerPrefab; // the player prefab, it will be instantiated if there is no player in the scene already
	
	#endregion

	#region Private/Protected Variables
	protected Transform
		startingPosition;
	protected GameObject
		playerInScene;
	private _2dxFX_NewTeleportation2
		teleportScript;
	#endregion

	// Unity Named Methods
	#region Main Methods
	///<summary> Ensure the player is at the starting position </summary>
	private void Awake()
	{
		SetStartingPostion();

		PlacePlayer();

		teleportScript = playerInScene.GetComponent<_2dxFX_NewTeleportation2>();
		StartCoroutine(TeleportInPlayer());
	}

	#endregion

	#region Utility Methods
	///<summary> Set the starting position </summary>
	public virtual void SetStartingPostion()
	{
		playerInScene = GameObject.FindGameObjectWithTag("Player");

		// Assign the starting position
		if (GlobalVarablesAndMethods.startInBeginingPosition == false && altStartingPosition != null)
		{
			startingPosition = altStartingPosition;
		}
		else
		{
			startingPosition = defaultPlayerStartingPosition;
		}
	}

	/// <summary> If not already present instantiate the player at the starting position </summary>
	public virtual void PlacePlayer()
	{
		// Place the player in the starting position
		if (playerInScene != null)
		{
			playerInScene.transform.position = startingPosition.position;
		}
		// Instantiate the player in the starting position
		else
		{
			playerInScene = Instantiate(playerPrefab, startingPosition.position, playerPrefab.transform.rotation);
		}
	}
	#endregion

	#region Coroutines
	/// <summary> plays the teleport in shader effect </summary>
	private IEnumerator TeleportInPlayer()
	{
		float percentageComplete = 0;

		// make the player invisible, this is not set by default in the prefab because
		// then the player would be invisible in Dev rooms because they don't have this script running in them
		teleportScript._Fade = 1;

		// disable player movement
		playerInScene.GetComponent<Player>().FreezePlayer();

		// Pause before playing teleport effect
		yield return new WaitForSeconds(1f);

		// teleport the player in, it does this by "sliding" a float from 0 to 1 over time
		while (percentageComplete < 1)
		{
			teleportScript._Fade = Mathf.Lerp(1f, 0f, percentageComplete);
			percentageComplete += Time.deltaTime;
			yield return null;
		}

		teleportScript._Fade = 0;

		// enable player movement
		playerInScene.GetComponent<Player>().UnFreezePlayer();
	}
	#endregion
}
