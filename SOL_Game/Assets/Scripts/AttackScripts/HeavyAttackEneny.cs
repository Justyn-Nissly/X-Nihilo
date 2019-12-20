﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackEneny : MeleeAttackBase
{
	#region Enums
	#endregion

	#region Public Variables
	public float maxTimeBetweenAttacks = 2f;
	public float minTimeBetweenAttacks = 1f;
	public BaseCharacter characterBeingAtacked;
	#endregion

	#region Private Variables
	private float countDownTimer;
	#endregion

	// Unity Named Methods
	#region Main Methods
	private void FixedUpdate()
	{
		if (countDownTimer <= 0)
		{
			countDownTimer = Random.Range(minTimeBetweenAttacks, maxTimeBetweenAttacks); // reset the time between attacks

			Attack();
		}
		else
		{
			countDownTimer -= Time.deltaTime;
		}
	}
	#endregion

	

}