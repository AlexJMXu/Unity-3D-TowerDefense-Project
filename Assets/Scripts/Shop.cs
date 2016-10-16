using UnityEngine;

public class Shop : MonoBehaviour {

	public TurretBlueprint standardTurret;
	public TurretBlueprint missileTurret;
	public TurretBlueprint laserTurret;

	BuildManager buildManager;

	void Start() {
		buildManager = BuildManager.instance;
	}

	public void SelectStandardTurret() {
		Debug.Log("Standard Turret Selected");
		buildManager.SelectTurretToBuild(standardTurret);
	}

	public void SelectMissileTurret() {
		Debug.Log("Missile Turret Selected");
		buildManager.SelectTurretToBuild(missileTurret);		
	}

	public void SelectLaserTurret() {
		Debug.Log("Laser Turret Selected");
		buildManager.SelectTurretToBuild(laserTurret);		
	}

}
