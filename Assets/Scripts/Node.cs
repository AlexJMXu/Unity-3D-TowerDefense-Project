using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	public Color hoverColor;
	public Color notEnoughMoneyColor;
	public Vector3 positionOffset;

	[HideInInspector] public GameObject turret;
	[HideInInspector] public TurretBlueprint turretBlueprint;
	[HideInInspector] public bool isUpgraded = false;

	private Renderer rend;
	private Color startColor;

	BuildManager buildManager;
	GameManager gameManager;

	private GameObject turretPreview;

	void Start() {
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;

		buildManager = BuildManager.instance;
		gameManager = GameManager.instance;
	}

	public Vector3 GetBuildPosition() {
		return transform.position + positionOffset;
	}

	void OnMouseDown() {
		if (EventSystem.current.IsPointerOverGameObject()) return;

		if (turret != null) {
			buildManager.SelectNode(this);
			return;
		}

		if (!buildManager.CanBuild) return;

		BuildTurret(buildManager.GetTurretBlueprint());

		//buildManager.BuildTurretOn(this);
		rend.material.color = startColor;

		//rend.material.color = startColor;
		if (turretPreview != null) Destroy(turretPreview);
	}

	void BuildTurret(TurretBlueprint bp) {
		if (PlayerStats.money < bp.cost) {
			Debug.Log("Not enough money to build that!");
			return;
		}

		PlayerStats.money -= bp.cost;

		GameObject turret = (GameObject) Instantiate(bp.prefab, GetBuildPosition(), Quaternion.identity);
		this.turret = turret;

		turretBlueprint = bp;

		GameObject effect = (GameObject) Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 2f);
	}

	public void UpgradeTurret() {
		if (PlayerStats.money < turretBlueprint.upgradeCost) {
			Debug.Log("Not enough money to upgrade that!");
			return;
		}

		PlayerStats.money -= turretBlueprint.upgradeCost;

		Destroy(this.turret);

		GameObject turret = (GameObject) Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
		this.turret = turret;

		GameObject effect = (GameObject) Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 3f);

		isUpgraded = true;
	}

	public void SellTurret() {
		PlayerStats.money += turretBlueprint.GetSellAmount();

		GameObject effect = (GameObject) Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 3f);

		Destroy(this.turret);
		this.turret = null;
		this.turretBlueprint = null;
		this.isUpgraded = false;
	}

	void OnMouseEnter() {
		if (EventSystem.current.IsPointerOverGameObject()) return;

		if (!buildManager.CanBuild || turret != null) return;

		if (PlayerStats.money >= buildManager.GetTurretBlueprint().cost) {
			rend.material.color = hoverColor;
		} else {
			rend.material.color = notEnoughMoneyColor;
		}

		gameManager.SetSelectedNode(this);
		
		turretPreview = (GameObject) Instantiate(buildManager.GetTurretToBuildPreview(), GetBuildPosition(), Quaternion.identity);
	}

	void OnMouseExit() {
		ResetToDefault();
		gameManager.SetSelectedNode(null);
	}

	public void ResetToDefault() {
		rend.material.color = startColor;

		if (turretPreview != null) Destroy(turretPreview);
	}
}
