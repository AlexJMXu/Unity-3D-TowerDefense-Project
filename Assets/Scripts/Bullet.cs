using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;
	private Vector3 lastTargetPosition;

	public float speed = 70f;
	public float explosionRadius = 0f;
	public int damage = 50;

	public GameObject impactEffect;

	public void Seek(Transform _target) {
		target = _target;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir;

		if (target == null) {
			dir = lastTargetPosition - transform.position;
		} else {
			dir = target.position - transform.position;
			lastTargetPosition = target.position;
		}

		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame) {
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);
	}

	void HitTarget() {
		GameObject effectIns = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 5f);

		if (explosionRadius > 0f) {
			Explode();
		} else {
			if (target != null) Damage(target);
		}

		Destroy(gameObject);
	}

	void Explode() {
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].tag == "Enemy") {
				Damage(colliders[i].transform);
			}
		}
	}

	void Damage(Transform enemy) {
		Enemy e = enemy.GetComponent<Enemy>();

		if (e != null) {
			e.TakeDamage(damage);
		}
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}
