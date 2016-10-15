using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	
	BuildManager buildManager;

	private Node currentSelectedNode;

	void Start() {
		instance = this;

		buildManager = BuildManager.instance;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1)) {
			buildManager.SelectTurretToBuild(null);
			currentSelectedNode.ResetToDefault();
		}
	}

	public void SetSelectedNode(Node node) {
		currentSelectedNode = node;
	}
}
