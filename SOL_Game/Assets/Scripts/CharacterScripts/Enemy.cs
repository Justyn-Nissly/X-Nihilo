﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Enemy : BaseCharacter
{
	#region Enums (Empty)
	#endregion

	#region Public Variables
	public Image
		healthBar;
    public string
        enemyName; // The enemy's name
	public float
		aggroRange,  // The max range where the enemy can detect the player
		//duration,
		followRange, // How far away the player must get for the enemy to deaggro
		attackDmg,   // Base damage from an intentional attack
        contactDmg,  // Base damage from making contact with the player
		healPerLoop,
		MAXPOSSIBLEHEALTH,
        maxHealOverTime,
        moveSpeed;   // Base movement speed
    public bool
        aggro; // The enemy has detected the player
    public Vector2[]
        patrol; // Enemy patrol points
	public Vector2
		playerPos,   // Track the player's position
		enemyVector; // Track the enemy's vector
    public Rigidbody2D
		rb2d; // The enemy's rigidBody
	public AudioManager
		enemyAudioManager;
	public GameObject
		powerUp; // Reference PowerUp prefab.
	#endregion

	#region Private Variables
	private float
    amountHealed = 0,
    countDownTimer;
	#endregion

	// Unity Named Methods
	#region Main Methods
	/// <summary> Initialize the enemy </summary>
	public virtual void Start()
	{
		player        = GameObject.FindObjectOfType<Player>();
		rb2d          = GetComponent<Rigidbody2D>();
		currentHealth = maxHealth.initialValue;

		enemyAudioManager = GameObject.FindObjectOfType<AudioManager>();
    countDownTimer = maxHealOverTime;
	}
	/// <summary> Enemy activity depends on whether or not it has detected the player </summary>
	public virtual void FixedUpdate()
	{
		// Check if the player is close enough to aggro
		playerPos = GameObject.FindWithTag("Player").transform.position;
		if (aggro == false && Vector2.Distance(transform.position, playerPos) <= aggroRange)
		{
			aggro = true;
		}
		// Enemies that are not aggro heal over time
		else if (Vector2.Distance(transform.position, playerPos) >= followRange)
		{
			aggro = false;
            if (countDownTimer <= 0)
            {
                countDownTimer = maxHealOverTime; // reset the time after going to 0

                if (currentHealth < maxHealth.initialValue) // only heal if health less than full
                {
                    currentHealth += healPerLoop;
                    SetHealth(currentHealth / maxHealth.initialValue);
                    Debug.Log("enemy CurrentHealth = " + currentHealth);
                }
            }
            else
            {
                countDownTimer -= Time.deltaTime;
            }
        }

		//// Enemies attack the player only if aggroed
		//if (aggro)
		//{
		//	canAttack = true;
		//}
	}
	#endregion

	#region Utility Methods
	///<summary> Deal damage to the enemy </summary>
	public override void TakeDamage(int damage, bool playSwordImpactSound)
	{
		base.TakeDamage(damage + player.extraDamage, playSwordImpactSound);
		SetHealth(currentHealth / maxHealth.initialValue);

		Debug.Log("enemy CurrentHealth = " + currentHealth);

		// The enemy gets destroyed if it runs out of health
		if (currentHealth <= 0)
		{
			enemyAudioManager.PlaySound();
			// The enemy might drop a power up
			if (Random.Range(0.0f, 5.0f) > 4.0f)
			{
				Instantiate(powerUp, transform.position, Quaternion.identity);
			}
			Destroy(gameObject);
		}
	}

	///<summary> Make the health bar show the current health </summary>
	void SetHealth(float percentHelth)
	{
		healthBar.fillAmount = percentHelth;
	}
    #endregion


    #region Coroutines

    #endregion
}