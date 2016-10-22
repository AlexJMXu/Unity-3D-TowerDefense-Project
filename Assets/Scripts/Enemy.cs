using UnityEngine;

public class Enemy : MonoBehaviour {

	public float startSpeed = 10f;
	[HideInInspector] public float speed;
	
	public float health = 100f;
	public int worth = 50;

	[SerializeField] private GameObject deathEffect;

	void Start() {
		speed = startSpeed;
	}

	public void TakeDamage(float amount) {
		health -= amount;

		if (health <= 0) {
			Die();
		}
	}

	private void Die() {
		PlayerStats.money += worth;
		
		GameObject _effect = (GameObject) Instantiate(deathEffect, transform.position, Quaternion.identity);
		
		Destroy(_effect, 4f);
		Destroy(gameObject);
	}

	public void Slow(float slow) {
		speed = startSpeed * slow;
	}
}
