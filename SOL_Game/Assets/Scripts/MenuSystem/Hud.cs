﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
	#region Enums

	#endregion

	#region Public Variables
	public Sprite[] HeartSprites; // The different heart sprites for players health
	public Image HeartUI;         // The heart containers
	#endregion

	#region Private Variables
	private Player player;
	#endregion

	// Unity Named Methods
	#region Main Methods
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	void Update()
	{
		HeartUI.sprite = HeartSprites[player.health];
	}
	#endregion

	#region Utility Methods

	#endregion

	#region Coroutines

	#endregion
}