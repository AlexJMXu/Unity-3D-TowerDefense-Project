﻿using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	private Transform target;
	private Enemy targetEnemy;

	[Header("General")]
	public float range = 15f;

	[Header("Use Bullets (default)")]
	public GameObject bulletPrefab;
	public float fireRate = 1f;
	private float fireCountdown = 0f;

	[Header("User Laser")]
	public bool useLaser = false;
	public int damageOverTime = 30;
	public float slowFactor = 0.5f;
	public LineRenderer lineRenderer;
	public ParticleSystem impactEffect;
	public Light impactLight;

	[Header("Unity Setup Fields")]
	public string enemyTag = "Enemy";
	public Transform partToRotate;
	public float rotationSpeed = 10f;

	public Transform firePoint;

	// Use this for initialization
	void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			if (useLaser) {
				if (lineRenderer.enabled) {
					lineRenderer.enabled = false;
					impactEffect.Stop();
					impactLight.enabled = false;
				}
			}

			return;
		}

		LockOnTarget();

		if (useLaser) {
			Laser();
		} else {
			if (fireCountdown <= 0) {
				Shoot();
				fireCountdown = 1f / fireRate;
			}

			fireCountdown -= Time.deltaTime;
		}
	}

	private void LockOnTarget() {
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	private void Shoot() {
		GameObject bulletGO = (GameObject) Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if (bullet != null) {
			bullet.Seek(target);
		}
	}

	private void Laser() {
		if (target != null) {
			targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
			targetEnemy.Slow(slowFactor);
		}

		if (!lineRenderer.enabled) {
			lineRenderer.enabled = true;
			impactEffect.Play();
			impactLight.enabled = true;
		}

		lineRenderer.SetPosition(0, firePoint.position);
		lineRenderer.SetPosition(1, target.position);

		Vector3 dir = firePoint.position - target.position;

		impactEffect.transform.position = target.position + dir.normalized;
		impactEffect.transform.rotation = Quaternion.LookRotation(dir);
	}

	private void UpdateTarget() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach (GameObject enemy in enemies) {
			float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance) {
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range) {
			target = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<Enemy>();
		} else {
			target = null;
		}
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
