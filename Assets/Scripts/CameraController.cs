using UnityEngine;

public class CameraController : MonoBehaviour {

	private bool doMovement = true;

	public float panSpeed = 30f;
	public float panBorderThickness = 10f;

	public float scrollSpeed = 5f;
	public float minY = 10f;
	public float maxY = 90f;

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			doMovement = !doMovement;
		}

		if (!doMovement) return;

		if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) {
			transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
			Vector3 clampedPos = transform.position;
			clampedPos.z = Mathf.Clamp(clampedPos.z, -15f, 5f);
			transform.position = clampedPos;
		}
		if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness) { 
			transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
			Vector3 clampedPos = transform.position;
			clampedPos.x = Mathf.Clamp(clampedPos.x, 25f, 45f);
			transform.position = clampedPos;
		}
		if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness) {
			transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
			Vector3 clampedPos = transform.position;
			clampedPos.z = Mathf.Clamp(clampedPos.z, -15f, 5f);
			transform.position = clampedPos;
		}
		if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness) {
			transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
			Vector3 clampedPos = transform.position;
			clampedPos.x = Mathf.Clamp(clampedPos.x, 25f, 45f);
			transform.position = clampedPos;
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel");

		Vector3 pos = transform.position;

		pos.y -= scroll * 100 * scrollSpeed * Time.deltaTime;
		pos.y = Mathf.Clamp(pos.y, minY, maxY); 

		transform.position = pos;
	
	}
}
