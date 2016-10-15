using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed = 10f;
	private Transform target;
	private int waypointIndex = 0;

	public int health = 100;
	public int value = 50;

	[SerializeField] private GameObject deathEffect;

	void Start() {
		target = Waypoints.points[0];
	}

	public void TakeDamage(int amount) {
		health -= amount;

		if (health <= 0) {
			Die();
		}
	}

	private void Die() {
		PlayerStats.money += value;
		
		GameObject _effect = (GameObject) Instantiate(deathEffect, transform.position, Quaternion.identity);
		
		Destroy(_effect, 4f);
		Destroy(gameObject);
	}

	private void Update() {
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
	
		if (Vector3.Distance(transform.position, target.position) <= 0.2f) {
			GetNextWaypoint();
		}
	}

	private void GetNextWaypoint() {
		if (waypointIndex >= Waypoints.points.Length - 1) {
			EndPath();
			return;
		}
		waypointIndex++;
		target = Waypoints.points[waypointIndex];
	}

	private void EndPath() {
		Destroy(this.gameObject);
		PlayerStats.lives--;
	}
}
