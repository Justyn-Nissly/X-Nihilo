﻿using UnityEngine;

public class TeleporterStation : MonoBehaviour
{
	#region Enums (Empty)
	#endregion

	#region Public Variables
	public SpriteRenderer
		portal;
	public ConveyorBelt
		tractorField;
	public CircleCollider2D
		teleportPoint;
	public int
		teleporterOrder; // Order in which the teleporters are unlocked
	public bool
		spawnHere;
	#endregion

	#region Private Variables
	private float
		returnTimer;
	private bool
		canReturn;
	#endregion

	// Unity Named Methods
	#region Main Methods
	/// <summary> Turn on the conveyor belt </summary>
	void Awake()
	{
		portal.enabled = (teleporterOrder <= Globals.bossesDefeated || spawnHere);
		tractorField.GetComponent<BoxCollider2D>().isTrigger = teleporterOrder <= Globals.bossesDefeated || spawnHere;
		teleportPoint.enabled = false;
		canReturn = false;
		tractorField.direction = ConveyorBelt.Direction.Up;
		returnTimer = 7.0f;
		
	}

	/// <summary> Turn on the conveyor belt </summary>
	void FixedUpdate()
	{
		if (canReturn == false)
		{
			returnTimer -= Time.deltaTime;
			if (returnTimer <= 3.0f)
			{
				tractorField.direction = ConveyorBelt.Direction.Down;
			}
			if (returnTimer <= 1.0f)
			{
				teleportPoint.enabled = true;
			}
			if (returnTimer <= 0.0f && teleporterOrder <= Globals.bossesDefeated)
			{
				canReturn = true;
				tractorField.direction = ConveyorBelt.Direction.Up;
			}
		}
	}
	#endregion

	#region Utility Methods (Empty)
	#endregion

	#region Coroutines (Empty)
	#endregion
}