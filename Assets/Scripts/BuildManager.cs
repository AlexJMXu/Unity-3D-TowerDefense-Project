using UnityEngine;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;

	public GameObject buildEffect;

	private TurretBlueprint turretToBuild;
	private Node selectedNode;

	public NodeUI nodeUI;

	void Awake() {
		if (instance != null) {
			Debug.LogError("Already another instance of BuildManager in scene.");
			return;
		} else {
			instance = this;
		}
	}

	public bool CanBuild { get { return turretToBuild != null; } }

	public void SelectNode(Node node) {
		selectedNode = node;
		turretToBuild = null;

		nodeUI.SetTarget(node);
	}

	public void SelectTurretToBuild(TurretBlueprint turret) {
		turretToBuild = turret;
		selectedNode = null;

		nodeUI.Hide();
	}

	public GameObject GetTurretToBuild() {
		return turretToBuild.prefab;
	}

	public GameObject GetTurretToBuildPreview() {
		return turretToBuild.preview;
	}

	public int GetTurretCost() {
		return turretToBuild.cost;
	}

	public void ResetSelections() {
		SelectTurretToBuild(null);
		selectedNode = null;
		nodeUI.Hide();
	}

	public TurretBlueprint GetTurretBlueprint() {
		return turretToBuild;
	}
}
