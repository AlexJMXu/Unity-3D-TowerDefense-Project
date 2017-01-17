 using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public static int enemiesAlive = 0;

	public Wave[] waves;

	public Transform spawnPoint;

	public float timeBetweenWaves = 5.5f;
	private float countdown = 2f;

	private int waveIndex = 0;

	public Text waveCountdownText;

	void Update() {
		if (enemiesAlive > 0) return;

		if (countdown <= 0f) {
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
			return;
		}

		countdown -= Time.deltaTime;

		countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

		waveCountdownText.text = string.Format("{0:00.00}", countdown) + "s";
	}

	private IEnumerator SpawnWave() {
		PlayerStats.rounds++;

		Wave wave = waves[waveIndex];

		for (int i = 0; i < wave.count; i++) {
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(1f/wave.rate);
		}

		waveIndex++;

		if (waveIndex == waves.Length) {
			Debug.Log("LEVEL COMPLETE!");
			this.enabled = false;
		}
	}

	private void SpawnEnemy(GameObject enemy) {
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
		enemiesAlive++;
	}

}
