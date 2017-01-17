using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public float startSpeed = 10f;
	[HideInInspector] public float speed;
	
	public float startHealth = 100f;
	[HideInInspector] public float health = 0f;
	
	public int worth = 50;

	[SerializeField] private GameObject deathEffect;

	[Header("Unity Stuff")]
	public Image healthBar;

	void Start() {
		health = startHealth;
		speed = startSpeed;
	}

	public void TakeDamage(float amount) {
		health -= amount;

		healthBar.fillAmount = health/startHealth;

		if (health <= 0) {
			Die();
		}
	}

	private void Die() {
		PlayerStats.money += worth;
		
		GameObject _effect = (GameObject) Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(_effect, 4f);

		WaveSpawner.enemiesAlive--;

		Destroy(gameObject);
	}

	public void Slow(float slow) {
		speed = startSpeed * slow;
	}
}
