﻿using UnityEngine;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;

	public GameObject standardTurretPrefab;
	public GameObject missileTurretPrefab;

	private TurretBlueprint turretToBuild;

	void Awake() {
		if (instance != null) {
			Debug.LogError("Already another instance of BuildManager in scene.");
			return;
		} else {
			instance = this;
		}
	}

	public bool CanBuild { get { return turretToBuild != null; } }

	public void BuildTurretOn (Node node) {
		if (PlayerStats.money < turretToBuild.cost) {
			turretToBuild = null;
			Debug.Log("Not enough money to build that!");
			return;
		}

		PlayerStats.money -= turretToBuild.cost;

		GameObject turret = (GameObject) Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
		node.turret = turret;

		Debug.Log("Turret built! Money left: " + PlayerStats.money);

		turretToBuild = null;
	}

	public void SelectTurretToBuild(TurretBlueprint turret) {
		turretToBuild = turret;
	}

	public GameObject GetTurretToBuildPreview() {
		return turretToBuild.preview;
	}

	public int GetTurretCost() {
		return turretToBuild.cost;
	}

}
