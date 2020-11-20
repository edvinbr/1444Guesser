using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	private float camMoveSpeed = 12.0f;
	private float camZoomSpeed = 10.0f;
	private float camSizeDefault;
	private Sprite mapOutlined;

	void Start()
	{
		camSizeDefault = Camera.main.GetComponent<Camera>().orthographicSize;
	}

	void Update() {

		int pixelWidth = 5632;
		int pixelHeight = 2048;
		float unitsToPixels = 100.0f;

		float mapWidth = pixelWidth / unitsToPixels;
		float mapHeight = pixelHeight / unitsToPixels;

		// Camera Movement
		float camSize = Camera.main.GetComponent<Camera>().orthographicSize;
		float aspect = Camera.main.GetComponent<Camera>().aspect;
		
		if (Input.GetKey(KeyCode.RightArrow)) {
			print(camSize);
			print(transform.position.x);
			print(mapWidth);
			transform.Translate(new Vector3(camMoveSpeed * Time.deltaTime * camSize / camSizeDefault, 0, 0));
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			print(camSize);
			print(transform.position.x);
			print(mapWidth);
			transform.Translate(new Vector3(-camMoveSpeed * Time.deltaTime * camSize / camSizeDefault, 0, 0));
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			print(camSize);
			print(transform.position.y);
			print(mapHeight);
			transform.Translate(new Vector3(0, -camMoveSpeed * Time.deltaTime * camSize / camSizeDefault, 0));
		}
		if (Input.GetKey(KeyCode.UpArrow)) {
			print(camSize);
			print(transform.position.y);
			print(mapHeight);
			transform.Translate(new Vector3(0, camMoveSpeed * Time.deltaTime * camSize / camSizeDefault, 0));
		}
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -mapWidth/2 + camSize * aspect, mapWidth/2 - camSize * aspect),
										Mathf.Clamp(transform.position.y, -mapHeight/2 + camSize, mapHeight/2 - camSize),
										transform.position.z);

		// Camera zoom
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		if (scroll != 0.0f) {
			camSize = Mathf.Clamp(camSize + scroll * camZoomSpeed, 1.5f, 8.0f);
			Camera.main.GetComponent<Camera>().orthographicSize = camSize;
		}
	}
}
