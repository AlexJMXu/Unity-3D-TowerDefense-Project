using UnityEngine;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;

	public GameObject buildEffect;

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
	public bool HasMoney { get { return PlayerStats.money >= turretToBuild.cost; } }

	public void BuildTurretOn (Node node) {
		// if (!HasMoney) {
		// 	turretToBuild = null;
		// 	Debug.Log("Not enough money to build that!");
		// 	return;
		// }

		PlayerStats.money -= turretToBuild.cost;

		GameObject turret = (GameObject) Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
		node.turret = turret;

		GameObject effect = (GameObject) Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 2f);

		Debug.Log("Turret built! Money left: " + PlayerStats.money);
	}

	public void SelectTurretToBuild(TurretBlueprint turret) {
		turretToBuild = turret;
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

}
