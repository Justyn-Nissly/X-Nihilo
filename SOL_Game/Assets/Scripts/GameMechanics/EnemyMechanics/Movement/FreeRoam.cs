﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeRoam : MonoBehaviour
{
    #region Enums and Defined Constants
    const int
        UP    = 1,
        LEFT  = 2,
        DOWN  = 3,
        RIGHT = 4;
    #endregion

    #region Public Variables
    public float
        maxTime,  // Maximum time the enemy moves before choosing new direction
        minTime,  // Minimum time the enemy moves before choosing new direction
        waitTime; // Waiting time before moving in new direction
    #endregion

    #region Private Variables
    protected float
        moveTime, // How long the enemy will move before choosing a new direction
        waiting,  // The amount of time left for enemy to wait
        x = 0.0f, // Horizontal movement
        y = 0.0f; // Vertical movement
    private int
        direction,     // The enemy's current direction (for choosing where to go)
        lastDirection; // The enemy's last direction
    private Enemy
        enemy; // Reference the enemy
    private Vector2
        path,    // The enemy's path: up, down, left, or right (for actually moving)
        lastPos; // The enemy's previous position

	private bool
	stopped; // An obstacle is hindering the enemy
	#endregion

	// Unity Named Methods
	#region Unity Main Methods
	/// <summary> Set up the enemy's movement </summary>
	void Start()
    {
        new Random();
        stopped  = false;
        enemy    = GetComponent<Enemy>();
        path     = new Vector2(0.0f, enemy.moveSpeed);
        moveTime = Random.Range(minTime, maxTime);
    }

	/// <summary> Roam randomly while the player has not been detected </summary>
	void FixedUpdate()
	{
		if (enemy.aggro == false &&
		    GameObject.FindObjectOfType<DialogueManager>().GetComponentInChildren<Animator>().GetBool("IsOpen") == false)
		{
			Roam();
		}
		else
		{
			waiting = waitTime;
		}
	}
	#endregion

	#region Utility Functions
	/// <summary> Move in a new direction </summary>
	public void ChooseNewPath()
    {
        lastDirection = direction;

        // If the enemy hit an obstacle it will not try to move in the same direction
        direction = Random.Range(UP, RIGHT + 1);
        if (stopped)
        {
            if (direction == lastDirection)
            {
                direction = RIGHT - direction / 2;
            }
        }

        // Set the new movement direction
        x = 0.0f;
        y = 0.0f;
        switch (direction)
        {
            case UP:
                y = 1.0f;
                break;
            case DOWN:
                y = -1.0f;
                break;
            case LEFT:
                x = -1.0f;
                break;
            case RIGHT:
                x = 1.0f;
                break;
            default: // This should never occur
                Debug.Log("ERROR: random number outside accepted bounds.");
                break;
        }

        // Set the new path and movement time
        path     = new Vector2(x * enemy.moveSpeed, y * enemy.moveSpeed);
        moveTime = Random.Range(minTime, maxTime);
    }

	/// <summary> Roam around randomly </summary>
	public void Roam()
    {
        // If the enemy stops moving it chooses a new direction
        if (moveTime <= 0.0f)
        {
            waiting = waitTime;
            ChooseNewPath();
            stopped = false;
        }

        // The enemy delays before moving in a new direction
        else if (waiting > 0.0f)
        {
            waiting -= Time.deltaTime;
        }

        // The enemy is moving
        else
        {
            enemy.rb2d.MovePosition((Vector2)transform.position + path * Time.deltaTime);
            moveTime -= Time.deltaTime;
            // If the enemy hits an obstacle it stops and chooses a different direction
            if (Vector2.Distance(transform.position, lastPos) < 0.01f * enemy.moveSpeed * Time.deltaTime)
            {
                moveTime      = -0.1f;
                waiting       = waitTime * 0.4f;
                stopped       = true;
                lastDirection = direction;
            }

            // The enemy keeps moving
            else
            {
                lastPos       = transform.position;
                lastDirection = -1;
            }
        }
    }
	#endregion

	#region Coroutines (Empty)
	#endregion
}