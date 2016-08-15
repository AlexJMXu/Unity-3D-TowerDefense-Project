using UnityEngine;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;

	public GameObject standardTurretPrefab;

	private GameObject turretToBuild;

	void Awake() {
		if (instance != null) {
			Debug.LogError("Already another instance of BuildManager in scene.");
			return;
		} else {
			instance = this;
		}
	}

	void Start() {
		turretToBuild = standardTurretPrefab;
	}

	public GameObject GetTurretToBuild() {
		return turretToBuild;
	}

}
