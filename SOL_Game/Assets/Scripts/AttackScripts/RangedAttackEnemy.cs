﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackEnemy : RangedAttackBase
{
	#region Enums
	#endregion

	#region Public Variables
	public int rangeAttackDamage;

	public float maxTimeBetweenAttacks = 2f;
	public float minTimeBetweenAttacks = 1f;
	#endregion

	#region Private Variables
	private float countDownTimer;
    #endregion


    #region Before Start
    private void Awake()
    {
        rangeAttackDamage = (int)damageToGive.initialValue;
    }
    #endregion

    // Unity Named Methods
    #region Main Methods
    public void Start()
	{
        enemy          = GetComponent<Enemy>();
        countDownTimer = Random.Range(minTimeBetweenAttacks, maxTimeBetweenAttacks);
	}

    public void FixedUpdate()
    {
        if (enemy.aggro)
        {
            if (countDownTimer <= 0)
            {
                Shoot();
                countDownTimer = Random.Range(minTimeBetweenAttacks, maxTimeBetweenAttacks);
            }
            else
            {
                countDownTimer -= Time.deltaTime;
            }
        }
    }
	#endregion

	#region Utility Methods
	public override void Shoot()
	{
		base.Shoot();

		// there's a delay here so that there is a delay before the enemy shoots
		Invoke("InstantiateBullet", .2f);
	}

	public void InstantiateBullet()
	{
		GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

		BulletLogic bulletLogic = bulletInstance.GetComponent<BulletLogic>();
		bulletLogic.bulletDamage = rangeAttackDamage;
	}
	#endregion

	#region Coroutines
	#endregion
}
