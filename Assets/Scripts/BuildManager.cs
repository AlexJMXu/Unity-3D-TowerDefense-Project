using UnityEngine;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;

	public GameObject standardTurretPrefab;
	public GameObject anotherTurretPrefab;

	private GameObject turretToBuild;

	void Awake() {
		if (instance != null) {
			Debug.LogError("Already another instance of BuildManager in scene.");
			return;
		} else {
			instance = this;
		}
	}

	public GameObject GetTurretToBuild() {
		return turretToBuild;
	}

	public void SetTurretToBuild(GameObject turret) {
		turretToBuild = turret;

	}

}
