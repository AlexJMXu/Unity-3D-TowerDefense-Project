using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	
	BuildManager buildManager;

	private Node currentSelectedNode;

	private bool gameEnded = false;

	void Start() {
		instance = this;

		buildManager = BuildManager.instance;
	}

	// Update is called once per frame
	void Update () {
		if (gameEnded) return;

		if (Input.GetMouseButtonDown(1)) {
			buildManager.SelectTurretToBuild(null);
			if (currentSelectedNode != null)
				currentSelectedNode.ResetToDefault();
		}

		if (PlayerStats.lives <= 0) {
			EndGame();
		}
	}

	public void SetSelectedNode(Node node) {
		currentSelectedNode = node;
	}

	private void EndGame() {
		gameEnded = true;
		Debug.Log("Game Over!");
	}
}
