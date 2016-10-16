using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	public Color hoverColor;
	public Color notEnoughMoneyColor;
	public Vector3 positionOffset;

	[Header("Optional")]
	public GameObject turret;

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

		if (!buildManager.CanBuild) return;

		if (!buildManager.HasMoney) {
			Debug.Log("Not enough money to build that!");
			return;
		}

		if (turret != null) {
			Debug.Log("Can't build there! - TODO: Display on screen.");
			return;
		}

		buildManager.BuildTurretOn(this);
		rend.material.color = startColor;

		//rend.material.color = startColor;
		if (turretPreview != null) Destroy(turretPreview);
	}

	void OnMouseEnter() {
		if (EventSystem.current.IsPointerOverGameObject()) return;

		if (!buildManager.CanBuild || turret != null) return;

		if (buildManager.HasMoney) {
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
