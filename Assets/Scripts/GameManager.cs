using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	
	BuildManager buildManager;
	private Node currentSelectedNode;

	public static bool gameIsOver;

	public GameObject gameOverUI;
	public GameObject completeLevelUI;

	void Awake() {
		if (instance != null) {
			Debug.LogError("Already another instance of GameManager in scene.");
			return;
		} else {
			instance = this;
		}
	}

	void Start() {
		buildManager = BuildManager.instance;
		gameIsOver = false;
	}

	// Update is called once per frame
	void Update () {
		if (gameIsOver) return;

		if (Input.GetMouseButtonDown(1)) {
			buildManager.ResetSelections();
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
		gameIsOver = true;
		gameOverUI.SetActive(true);
	}

	public void WinLevel() {
		gameIsOver = true;
		completeLevelUI.SetActive(true);
	}
}
